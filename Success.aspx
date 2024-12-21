<%@ Page Language="C#" Debug="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="Success.aspx.cs" Inherits="Success" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="Server">

    <style>
    #<%=btnBackToInbox.ClientID%>{    
        background-image: linear-gradient(to top, #a18cd1 0%, #fbc2eb 100%) !important;
        border-color: #fbc2eb;
    }
</style>

    <div class="container">
        <div class="row justify-content-center">
            <img src="./Image/success.gif" alt="Description of GIF" width="150" height="150" />
        </div>
        <div class="row justify-content-center">

            <p class="lead"><b>Success!</b></p>
        </div>
        <div class="row justify-content-center">
            <p id="pSuccessMessage" runat="server" class="lead"></p>
        </div>

        <hr />
        <div class="row justify-content-center">
            <asp:Button
                class="btn btn-primary px-4"
                ID="btnBackToInbox"
                runat="server"
                OnClick="btnBackToInbox_Click"
                Text="Go to inbox" />
        </div>
    </div>
</asp:Content>

