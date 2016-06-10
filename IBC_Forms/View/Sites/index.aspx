<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="IBC_Forms.View.Sites.index" %>

<%
    if (Session["user"] == null)
    {
        Response.Redirect("login.aspx", false);
        Context.ApplicationInstance.CompleteRequest();
    }
%>

<!DOCTYPE html>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title></title>
<meta charset="utf-8" />
<script src="../Libraries/jquery-1.11.3.min.js"></script>
<script src="../Javascript/functions.js"></script>
<link rel="stylesheet" type="text/css" href="../style.css">
</head>
<body>

<div id="all">

    <table style="width: 100%;" cellspacing="0" cellpadding="0">
      <tr>
          <td class="blueBackground">
              <img src="../Images/logo_blue.jpg" style="width: 55%;"/>
          </td>
          <td class="blueBackground">
          </td>
      </tr>
      <tr>
        <td style="width: 380px; vertical-align: top;">
            <div id="left">
                <div id="leftHeader" style="text-align: center;">
                    <span id="formLink">Formulare</span>

                    <span id="dataLink">Daten</span>

                    <span id="exportLink">Export</span>

                    <%=
                        (Session["user"] != null && ((IBC_Forms.Model.User)Session["user"]).isAdmin() ? "<span id=\"adminLink\"><a href=\"#\" onclick=\"openAdminForm()\">Admin</a></span>" : "")
                    %>

                    <span><a href="login.aspx?logout=true">Logout</a></span>
                </div>

                <div id="forms" style="display:none;"></div>

                <div id="data" style="display:none;"></div>

                <div id="export" style="display:none;">
                    Wählen Sie ein Format in dem Sie das ausgefüllte Formular exportieren möchten<p />

                    <a href="#" id="exportPdfLink">Als .pdf</a><br />
                    <a href="#" id="exportDocxLink">Als .docx</a><br />
                    <a href="#" id="exportPrintLink">Ausdrucken</a><br />
                    <a href="#" id="exportMailLink">Sende E-Mail</a>
                </div>
            </div>
        </td> 
        <td>
            <div id="right"></div>
        </td>
      </tr>
    </table>
</div>

<iframe id="invisibleIframe" style="display: none"></iframe>

</body>
</html>
