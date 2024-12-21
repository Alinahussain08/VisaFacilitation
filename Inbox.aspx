<%@ Page Language="C#" Debug="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="Inbox.aspx.cs" Inherits="Inbox" %>

<%@ Register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="Server">
    
    <script>
        function toggleRowBackground(chkBox) {
            var grid = document.getElementById('<%= InboxTransferRequests.ClientID %>');
            var checkboxes = grid.getElementsByTagName('input');
            var row = chkBox.parentNode.parentNode;

            if (chkBox.checked) {
                row.classList.add("selected-row");
                for (var i = 0; i < checkboxes.length; i++) {
                    if (checkboxes[i] !== chkBox && checkboxes[i].type === 'checkbox') {
                        checkboxes[i].disabled = true;
                    }
                }
            } else {
                row.classList.remove("selected-row");
                for (var i = 0; i < checkboxes.length; i++) {
                    if (checkboxes[i].type === 'checkbox') {
                        checkboxes[i].disabled = false;
                    }
                }
            }
        }

        window.onload = function () {
            // Get the grid element
            var grid = document.getElementById('<%= InboxTransferRequests.ClientID %>');

            // Get all the input elements within the grid (including checkboxes)
            var checkboxes = grid.getElementsByTagName('input');

            // Loop through the checkboxes
            for (var i = 0; i < checkboxes.length; i++) {
                // Check if the input element is a checkbox
                if (checkboxes[i].type === 'checkbox') {
                    // Call toggleRowBackground for each checkbox
                    toggleRowBackground(checkboxes[i]);
                }
            }
        };

    </script>
    <style>
        #<%=btnDetails.ClientID%> {
            background-image: linear-gradient(to top, #a18cd1 0%, #fbc2eb 100%) !important;
            border-color: #fbc2eb;
        }

        .selected-row {
            background-color: #CCCCCC !important;
        }
    </style>
    <!-- Begin Page Content -->
    <div class="container-fluid">
        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Visa Form</h1>
        </div>

            <div class="container mb-3">
                <div class="row" style="justify-content: center;">
                    <div class="card col-sm-12" style="padding: 20px;">
                        <h5 class="card-title">Pending Visa Requests</h5>
                        <h8> Select a request and click the button below to see its details and approve it</h8> 
                        
                        <div class="row">
                            <div class="col-md-12" style="overflow-y: auto; max-height: 300px;">
                                <div class="table-responsive">
                                    <asp:GridView
                                        ID="InboxTransferRequests"
                                        runat="server"
                                        
                                        class="mb-0 table table-striped"
                                        AutoGenerateColumns="false"
                                        BackColor="#003366"
                                        BorderStyle="Solid"
                                        BorderWidth="1px"
                                        EmptyDataText="There are no pending travel requests for you"
                                        EmptyDataRowStyle-ForeColor="WHITE"
                                        PageSize="5"
                                        OnRowDataBound="GridViewInboxTransferRequests_RowDataBound"
                                        AllowSorting="False"
                                        HeaderStyle-BackColor="#3F6AD8"
                                        HeaderStyle-ForeColor="White"
                                        ToolTip="Select a request to approve"
                                        EmptyDataRowStyle-Height="50px"
                                        EmptyDataRowStyle-Width="100px">
                                        <RowStyle BackColor="White" Width="100px" Wrap="False" />
                                        <EmptyDataRowStyle ForeColor="White"></EmptyDataRowStyle>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select Request">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBoxInbox" runat="server" onclick="toggleRowBackground(this)" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="TRAVEL_REQUEST_ID" HeaderText="Request ID" />
                                            <asp:BoundField DataField="INITIATOR_NAME" HeaderText="Request Initiator" />
                                            
                                            <asp:BoundField DataField="FROM_DATE" HeaderText="From Date" />
                                            <asp:BoundField DataField="TO_DATE" HeaderText="To Date" />
                                            <asp:BoundField DataField="DESTINATION" HeaderText="Destination" />




                                        </Columns>

                                    </asp:GridView>
                                </div>
                                <br />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <br />
            <hr />

        <div class="container mb-1">
            <div class="row justify-content-center">
                <asp:Label
                    class="ml-3"
                    ID="lblGreenBottom"
                    runat="server"
                    Text=""
                    ForeColor="#006600" />
                <asp:Label
                    ID="lblRedBottom"
                    runat="server"
                    Style="color: #ff0000"
                    Text=""
                    ForeColor="Red" />

            </div>
        </div>
        <div class="container mb-3">
            <div class="row justify-content-center">
                <asp:Button
                    class="btn btns btn-secondary mr-4 px-4"
                    ID="btnCancel"
                    OnClick="btnCancel_Click"
                    runat="server"
                    Text="Cancel" />
                <asp:Button
                    class="btn btns btn-primary px-4"
                    ID="btnDetails"
                    runat="server"
                    OnClick="btnDetails_Click"
                    Text="Details »" />

            </div>
        </div>

</asp:Content>



