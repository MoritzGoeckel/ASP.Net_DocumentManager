<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="IBC_Forms.View.Sites.login" %>

<%
    string outputRed = null;
    string outputGreen = null;

    if (Request.QueryString["logout"] != null)
    {
        Session["user"] = null;
        Session.Clear();
        outputGreen = "Sie sind nun abgemeldet";
    }
    else if (Session["user"] != null)
    {
        Response.Redirect("index.aspx", false);
        Context.ApplicationInstance.CompleteRequest();
    }

    NameValueCollection nvc = Request.Form;
    if (!string.IsNullOrEmpty(nvc["name"]) && !string.IsNullOrEmpty(nvc["pw"]))
    {
        string name = nvc["name"];
        string pw = nvc["pw"];

        if ((name == "m" || name == "a") && pw == "0") //Todo: Real auth
        {
            bool isAdmin = false;
            if (name == "a") //Todo: is admin real
                isAdmin = true;

            IBC_Forms.Model.User user = new IBC_Forms.Model.User(name, isAdmin);
            Session["user"] = user;

            Response.Redirect("index.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
        else
            outputRed = "Name oder Passwort inkorrekt";
    }
%>

<!DOCTYPE html>

<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta charset="utf-8" />
    <script src="../Libraries/jquery-1.11.3.min.js"></script>
    <script src="../Javascript/functions.js"></script>
    <link rel="stylesheet" type="text/css" href="../style.css">
</head>
<body>

    <div id="all">
        <div id="login_flags_wrapper">
            <a href="#"><img src="../Images/germany.png" class="flag_img"/></a>
            <a href="#"><img src="../Images/uk.png" class="flag_img"/></a>
            <a href="#"><img src="../Images/czech.png" class="flag_img"/></a>
            <a href="#"><img src="../Images/china.png" class="flag_img"/></a>
        </div>
        <div id="login_wrapper">
            <div class="login_third">
                <form method="post" action="login.aspx">
                    <h1>Login</h1>
                    <input style="width: 100%" name="name" placeholder="Name"><br />
                    <input style="width: 100%" name="pw" placeholder="Passwort" type="password"><p />
                    <input style="width: 40%" type="submit">
                </form>
                <%
                    if(outputRed != null)
                        Response.Write("<span class='errortext'>" + outputRed + "</span>");

                    if(outputGreen != null)
                        Response.Write("<span class='greentext'>" + outputGreen + "</span>");
                %>
            </div>
            <div class="login_third"><img src="../Images/getriebe.png" width="100%"/></div>
            <div class="login_third"><img src="../Images/logo_blue.jpg" width="100%"/></div>

        </div>
    </div>

    <iframe id="invisibleIframe" style="display: none"></iframe>

</body>
</html>

