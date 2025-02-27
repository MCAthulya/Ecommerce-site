<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SendReply.aspx.cs" Inherits="prjct1.SendReply" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 17px;
        }
    </style>
    <style>
        .form-container {
            width: 400px;
            margin: 50px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
            font-family: Arial, sans-serif;
        }

        .form-container h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        .form-container label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }

        .form-container input,
        .form-container textarea,
        .form-container button {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .form-container button {
            background-color: #007BFF;
            color: white;
            border: none;
            cursor: pointer;
        }

        .form-container button:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <table class="nav-justified">
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <!-- Additional table rows as required -->
    </table>
    <div class="form-container">
        <h2>Send Gmail</h2>
        <asp:Label ID="lblSenderEmail" runat="server" Text="To:" />
        <asp:TextBox ID="txtSenderEmail" runat="server" CssClass="form-control" />

        <%-- <asp:Label ID="lblPassword" runat="server" Text="Your Password:" />
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" /> --%>

        <asp:Label ID="lblRecipientEmail" runat="server" Text="From:" />
        <asp:TextBox ID="txtRecipientEmail" runat="server" CssClass="form-control" />

        <asp:Label ID="lblSubject" runat="server" Text="Subject:" />
        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" />

        <asp:Label ID="lblMessageBody" runat="server" Text="Message Body:" />
        <asp:TextBox ID="txtMessageBody" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="5" />

        <asp:Button ID="btnSend" runat="server" Text="Send Email" CssClass="btn btn-primary" />
    </div>

    <table class="nav-justified">
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <!-- Additional table rows as required -->
    </table>
</asp:Content>
