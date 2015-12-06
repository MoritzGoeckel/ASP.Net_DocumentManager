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
    for (i = 0; i < forms.length; i++) {
        var form = forms[i];
        output += "<a href='#' onclick='setNewCurrentForm(" + form.Id + ")'>" + form.Title + "</a><br />";
    }

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
    console.log("openForm");

    var output = "<table>";

    var fields = currentForm.Fields;
    for (f = 0; f < fields.length; f++) {
        var field = fields[f];
        output += "<tr><td>" + field.Title + ":</td> <td><input class='field_" + field.Id + "' onchange='redrawForm()' value='" + (field.Value != undefined ? field.Value:"" ) + "'></td></tr>";
    }

    output += "</table>";

    $("#data").html(output);

    $("#forms").hide();
    $("#export").hide();
    $("#data").show();

    $("#dataLink").html("Daten");
    $("#exportLink").html("<a href='#' onclick='renderExport();' style='color:black;'>Export</a>");
    redrawForm();
}

function redrawForm()
{  
    var output = currentForm.Template;

    for (i = 0; i < currentForm.Fields.length; i++) {
        var value = $(".field_" + currentForm.Fields[i].Id).val();

        output = output.replace("|" + currentForm.Fields[i].Id + "|", value);
        currentForm.Fields[i].Value = value;
    }

    $("#right").html(output);
}

function renderExport()
{
    $("#dataLink").html("<a href='#' onclick='openCurrentForm()'>Daten</a>");

    //Set PDF-Export-link
    var fieldsParam = "";
    for (i = 0; i < currentForm.Fields.length; i++) {
        fieldsParam += currentForm.Fields[i].Id + "=" + currentForm.Fields[i].Value + ";"
    }
    var params = currentForm.Id + "/" + fieldsParam + "/pdf";
    var url = "/api/export/" + params;

    $("#exportPdfLink").attr("href", url)

    //Set Mail-Link ???

    //Set Docx-Link ???

    //Set Mail-Link ???

    $("#forms").hide();
    $("#export").show();
    $("#data").hide();
}