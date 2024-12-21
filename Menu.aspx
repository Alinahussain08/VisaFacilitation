<%@ Page Language="C#" Debug="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="Menu.aspx.cs" Inherits="Menu" %>
<%@ Register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="Server">

    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@24,400,0,0" />
    <style>
        .btns{
            background-image: linear-gradient(to top, #a18cd1 0%, #fbc2eb 100%) !important;
            border-color: #fbc2eb;
        }
        .btns:hover{
            border-color: #fbc2eb;
        }


        .icons{
            width:30px;
            height:30px; 
        }
    </style>

    <%--Main Content:--%>
    <div class="container marketing">
        <div class="row">
            <div class="col-lg-6">
                <img class="icons" src="Image/edit2.png"/>
                <h2 class="fw-normal">Initiate</h2>
                <p>Initiate a new Asset Transfer Request</p>
                <%--<p>--%>
                    <asp:Button
                        class="px-3 pb-2 btn btns btn-sm btn-primary shadow-sm"
                        ID="btnInitiate"
                        runat="server"
                        OnClick="btnInitiate_Click"
                        Text="Initiate »"/>
            </div>
            <div class="col-lg-6">
                <img class="icons" src="Image/Inbox.png"/>
                <h2 class="fw-normal">Inbox (<span ID="numPendingRequests1" runat="server" />) </h2>
                <p>You have <span ID="numPendingRequests2" runat="server" /> pending requests waiting for approval
                </p>
                <%--<p>--%>
                    <asp:Button
                        class="px-3 pb-2 btn btns btn-sm btn-primary shadow-sm"
                        ID="btnApprove"
                        runat="server"
                        OnClick="btnApprove_Click"
                        Text="Approve »"
                        />
            </div>
        </div>
        <br />
        <hr class="featurette-divider">
    </div>
</asp:Content>

