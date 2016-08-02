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

        if (form.Active)
            output += "<a href='#' onclick='setNewCurrentForm(" + form.Id + ")'>" + form.Title + "</a><br />";
    }

    $("#formLink").html("Formulare");

    $('#formLink').addClass("activeMenuItem");
    $('#dataLink').removeClass("activeMenuItem");
    $('#exportLink').removeClass("activeMenuItem");
    $('#adminLink').removeClass("activeMenuItem");

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
    $('#formLink').removeClass("activeMenuItem");
    $('#dataLink').addClass("activeMenuItem");
    $('#exportLink').removeClass("activeMenuItem");

    console.log("openForm");

    $("#formLink").html('<a href="#" onclick="renderFormsList();">Formulare</a>');

    var output = "Bitte füllen Sie die Felder des Formulars aus<p />";

    output += "<table>";

    var fields = currentForm.Fields;
    for (f = 0; f < fields.length; f++) {
        var field = fields[f];

        if(field.Type == 0) //Text
            output += "<tr><td>" + field.Title + ":</td> <td><input class='field_" + field.Id + "' onchange='redrawForm()' value='" + (field.Value != undefined ? field.Value : "") + "'></td></tr>";

        if (field.Type == 1) //Checkbox
        {
            checked = false;
            if (field.Value == undefined && field.CheckBox_DefaultChecked)
                checked = true;

            if (field.Value != undefined && field.Value == "X")
                checked = true;

            output += "<tr><td>" + field.Title + ":</td> <td><input type='checkbox' class='field_" + field.Id + "' onchange='redrawForm()' " + (checked ? "checked" : "") + "></td></tr>";
        }

        if (field.Type == 2) //Dropdown
        {
            var options = "";

            for (o = 0; o < field.Dropdown_Options.length; o++) {
                var val = field.Dropdown_Options[o];
                var selected = "";
                if (val == field.Value)
                    selected = "selected";

                options += "<option " + selected + ">" + val + "</option>";
            }

            output += "<tr><td>" + field.Title + ":</td> <td><select style='width: 100%;' class='field_" + field.Id + "' size='" + field.Dropdown_Options.length + "' onchange='redrawForm()'>" + options + "</select></td></tr>";
        }
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
    var output = currentForm.Template_html;

    for (i = 0; i < currentForm.Fields.length; i++) {
        var element = $(".field_" + currentForm.Fields[i].Id);

        var value;

        if (currentForm.Fields[i].Type == 0 || currentForm.Fields[i].Type == 2) //Text || Dropdown
            value = element.val();

        if (currentForm.Fields[i].Type == 1) //Checkbox
            value = (element.is(":checked") ? "X" : "");
         
        if (value == null) 
            value = "";

        output = output.replace("|" + currentForm.Fields[i].Id + "|", value);
        currentForm.Fields[i].Value = value;
    }

    $("#right").html(output);
    console.log(output);
}

function renderExport()
{
    $('#formLink').removeClass("activeMenuItem");
    $('#dataLink').removeClass("activeMenuItem");
    $('#exportLink').addClass("activeMenuItem");

    $("#formLink").html('<a href="#" onclick="renderFormsList();">Formulare</a>');

    $("#exportLink").html("Export");

    $("#dataLink").html("<a href='#' onclick='openCurrentForm()'>Daten</a>");

    //Set PDF-Export-link
    var fieldsParam = "download=true;";
    for (i = 0; i < currentForm.Fields.length; i++) {
        if (currentForm.Fields[i].Value != "" && currentForm.Fields[i].Value != null && currentForm.Fields[i].Value != " ")
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

function openAdminForm()
{
    var output = "";

    output += "Bearbeiten Sie hier welche Formulare aktiv geschaltet sein sollen<p />";

    for (i = 0; i < forms.length; i++) {
        var form = forms[i];
        output += "<a href='#' style='color: " + (form.Active ? "green" : "red") + "' onclick='toggleFormActive(" + i + ")'>" + (form.Active ? "Aktiviert" : "Deaktiviert") + "</a> " + form.Title + "<br />";
    }

    $('#formLink').removeClass("activeMenuItem");
    $('#dataLink').removeClass("activeMenuItem");
    $('#exportLink').removeClass("activeMenuItem");

    $('#adminLink').addClass("activeMenuItem");

    $("#formLink").html('<a href="#" onclick="renderFormsList();">Formulare</a>');

    $("#forms").html(output);

    $("#forms").show();
    $("#export").hide();
    $("#data").hide();

    $("#exportLink").html("Export");
    $("#dataLink").html("Daten");

    $("#right").html("");
}

function toggleFormActive(index)
{
    var form = forms[index];
    console.log(form);

    $.get("/api/forms/setFormActive/" + form.Id + "/" + (form.Active ? "0" : "1"), function (data) {
        console.log(data);
        forms[index].Active = data.Active;
        openAdminForm();
    });
}