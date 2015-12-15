var forms = [];

$().ready(loadFormList);

function loadFormList()
{
    console.log("loadFormList");

    $.get("/api/forms/", function (data) {
        forms = data;
        renderFormsList();
    });
}

function renderFormsList()
{
    console.log("renderFormsList");

    var output = "";

    output += "Bitte wählen Sie das Formular, welches Sie ausfüllen möchten<p />";

    for (i = 0; i < forms.length; i++) {
        var form = forms[i];
        output += "<a href='#' onclick='setNewCurrentForm(" + form.Id + ")'>" + form.Title + "</a><br />";
    }

    $("#formLink").html("Formulare");
    $('#formLink').css("text-decoration", "underline");

    $('#dataLink').css("text-decoration", "none");
    $('#exportLink').css("text-decoration", "none");

    $("#forms").html(output);

    $("#forms").show();
    $("#export").hide();
    $("#data").hide();

    $("#exportLink").html("Export");
    $("#dataLink").html("Daten");

    $("#right").html("");
}

var currentForm;
function setNewCurrentForm(id)
{
    for (i = 0; i < forms.length; i++) {
        var form = forms[i];
        if (form.Id == id) {
            currentForm = form;
            break;
        }
    }

    openCurrentForm();
}

function openCurrentForm()
{
    $('#formLink').css("text-decoration", "none");
    $('#dataLink').css("text-decoration", "underline");
    $('#exportLink').css("text-decoration", "none");

    console.log("openForm");

    $("#formLink").html('<a href="#" onclick="renderFormsList();">Formulare</a>');

    var output = "Bitte füllen Sie die Felder des Formulars aus<p />";

    output += "<table>";

    var fields = currentForm.Fields;
    for (f = 0; f < fields.length; f++) {
        var field = fields[f];
        output += "<tr><td>" + field.Title + ":</td> <td><input class='field_" + field.Id + "' onchange='redrawForm()' value='" + (field.Value != undefined ? field.Value:"" ) + "'></td></tr>";
    }

    output += "<tr><td></td><td align='right'><a href='#' onclick='renderExport();' style='text-decoration:none;'>Export</a></td></tr>"

    output += "</table>";

    $("#data").html(output);

    $("#forms").hide();
    $("#export").hide();
    $("#data").show();

    $("#dataLink").html("Daten");
    $("#exportLink").html("<a href='#' onclick='renderExport();'>Export</a>");
    redrawForm();
}

function redrawForm()
{  
    var output = currentForm.HTMLTemplate;

    for (i = 0; i < currentForm.Fields.length; i++) {
        var value = $(".field_" + currentForm.Fields[i].Id).val();

        output = output.replace("|" + currentForm.Fields[i].Id + "|", value);
        currentForm.Fields[i].Value = value;
    }

    $("#right").html(output);
}

function renderExport()
{
    $('#formLink').css("text-decoration", "none");
    $('#dataLink').css("text-decoration", "none");
    $('#exportLink').css("text-decoration", "underline");

    $("#formLink").html('<a href="#" onclick="renderFormsList();">Formulare</a>');

    $("#exportLink").html("Export");

    $("#dataLink").html("<a href='#' onclick='openCurrentForm()'>Daten</a>");

    //Set PDF-Export-link
    var fieldsParam = "";
    for (i = 0; i < currentForm.Fields.length; i++) {
        fieldsParam += currentForm.Fields[i].Id + "=" + currentForm.Fields[i].Value + ";"
    }
    var params = currentForm.Id + "/" + fieldsParam;
    var url = "/api/export/" + params;

    $("#exportPdfLink").attr("href", url + "/pdf")

    //Set Print-Link ???
    //$("#exportPrintLink").attr("href", url + "/html")

    //Set Docx-Link
    $("#exportDocxLink").attr("href", url + "/docx")

    //Set Mail-Link
    $("#exportMailLink").attr("href", url + "/mail")

    $("#forms").hide();
    $("#export").show();
    $("#data").hide();
}