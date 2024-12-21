<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="VisaForm.aspx.cs" Inherits="VisaFacilitation.PreRegister1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="server">
    <title>Visa Facilitation Form</title>
    <style>
        .invisible{
            display:none;

        }
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f3f3f3;
        }

        .container {
            width: 100%;
            max-width: 1200px;
            margin: auto;
            background-color: white;
            padding: 20px;
        }

        h2 {
            background-color: #003366;
            color: white;
            padding: 10px;
        }

        h3 {
            margin-top: 20px;
            color: #003366;
        }

        .form-section {
            display: block; /* Ensures sections appear one below the other */
            margin-bottom: 20px;
        }

        .form-column {
            margin-bottom: 20px;
        }

        .form-group {
            margin-bottom: 15px;
        }

        label {
            display: block;
            margin-bottom: 5px;
        }

        input[type="text"],
        input[type="date"],
        select {
            width: 100%;
            padding: 10px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        button,
        .btn {
            background-color: #003366;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .hidden {
            display: none;
        }

    </style>

<script type="text/javascript">
    function toggleDependentSection() {
        var dependentSection = document.getElementById('dependentSection');
        var checkbox = document.getElementById('<%= chkAddDependents.ClientID %>');
        dependentSection.style.display = checkbox.checked ? 'block' : 'none';
    }
</script>

    <body>
        <div class="container">

            

            <!-- Personal Information Section -->
            <div class="form-section">
                <h3>Personal Information</h3>
                <div class="form-group">
                    <label for="txtName">Name:</label>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtPassportNumber">Passport Number:</label>
                    <asp:TextBox ID="txtPassportNumber" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtDOB">Date of Birth:</label>
                    <asp:TextBox ID="txtDOB" runat="server" TextMode="Date"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtPassportIssueDate">Passport Issuance Date:</label>
                    <asp:TextBox ID="txtPassportIssueDate" runat="server" TextMode="Date"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtPassportExpiryDate">Passport Expiry Date:</label>
                    <asp:TextBox ID="txtPassportExpiryDate" runat="server" TextMode="Date"></asp:TextBox>
                </div>
                <div class="form-group">
        <asp:Button ID="btnSubmitPersonalInfo" runat="server" CssClass="btn" Text="Submit Personal Information" OnClick="btnSubmitPersonalInfo_Click" />
    </div>
            </div>
                    <!-- Travel Information Section -->
        <div class="form-section">
            <h3>Travel Information</h3>
            <div class="form-group">
                
                <label for="ddlDestination">Destination:</label>
                <asp:DropDownList ID="ddlDestination" runat="server"></asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="txtDesignation">Designation:</label>
                <asp:TextBox ID="txtDesignation" runat="server"></asp:TextBox>
            </div>
          
            <div class="form-group">

                     <label for="txtPurpose"> Purpose:</label>
                    <asp:DropDownList ID="ddlPurpose" runat="server">
                             <asp:ListItem Value="Excursion">Excursion</asp:ListItem>
                            <asp:ListItem Value="Training">Training</asp:ListItem>
                            
                            </asp:DropDownList>

           
            <div class="form-group">
                <label for="txtCourseTitle">Course Title:</label>
                <asp:TextBox ID="txtCourseTitle" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtTrainingInstitution">Training Institution:</label>
                <asp:TextBox ID="txtTrainingInstitution" runat="server"></asp:TextBox>
            </div>
             <div class="form-group">
                  <label for="txttripnumber">Trip Number:</label>
                      <asp:TextBox ID="txttripnumber" runat="server"></asp:TextBox>
                         </div>
             <div class="form-group">
                     <label for="ddlLocation">Location</label>
                    <asp:DropDownList ID="ddlLocation" runat="server">
                             <asp:ListItem Value="A">A</asp:ListItem>
                            <asp:ListItem Value="B">B</asp:ListItem>
                             <asp:ListItem Value="C">C</asp:ListItem>
              
                            </asp:DropDownList>

                                        </div>
            <div class="form-group">
                <label for="txtVenue">Venue:</label>
                <asp:TextBox ID="txtVenue" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtDurationFrom">Duration (From):</label>
                <asp:TextBox ID="txtDurationFrom" runat="server" TextMode="Date"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtDurationTo">Duration (To):</label>
                <asp:TextBox ID="txtDurationTo" runat="server" TextMode="Date"></asp:TextBox>
            </div>
            <div class="form-group">
    <asp:Button ID="btnSubmitTravelInfo" runat="server" CssClass="btn" Text="Submit Travel Information" OnClick="btnSubmitTravelInfo_Click" />
</div>
        </div>
            
            <!-- Dependent Info -->

        <div class="row">
            <div class="col-md-12">
                <h3>Dependent Information</h3>
               <div class="form-group">
    <asp:CheckBox ID="chkAddDependents" runat="server" Text="Add Dependents" AutoPostBack="true" OnCheckedChanged="chkAddDependents_CheckedChanged" />
</div>
            </div>
        </div>

        <asp:Panel ID="dependentSection" runat="server" Visible="false">
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="txtDependentName">Dependent Name</label>
                        <asp:TextBox ID="txtDependentName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="ddlRelation">Relation</label>
                   <asp:DropDownList ID="ddlRelation" runat="server">
                        <asp:ListItem Value="Mother">Mother</asp:ListItem>
                        <asp:ListItem Value="Father">Father</asp:ListItem>
                        <asp:ListItem Value="Son">Son</asp:ListItem>
                        <asp:ListItem Value="Daughter">Daughter</asp:ListItem>
                        <asp:ListItem Value="Friend">Friend</asp:ListItem>
                        <asp:ListItem Value="Others">Others</asp:ListItem>
                    </asp:DropDownList>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtPassportNo">Passport Number</label>
                        <asp:TextBox ID="txtPassportNo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtDateOfBirth">Date of Birth</label>
                        <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtPassportExpiry">Passport Expiry Date</label>
                        <asp:TextBox ID="txtPassportExpiry" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtPassportIssue">Passport Issue Date</label>
                        <asp:TextBox ID="txtPassportIssue" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Button ID="btnsaveDependent" runat="server" Text="Save Dependent" OnClick="btnsaveDependent_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </asp:Panel>
             <asp:GridView ID="gvDependents" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Dependent Name" />
                    <asp:BoundField DataField="Relation" HeaderText="Relation" />
                    <asp:BoundField DataField="Passport_Number" HeaderText="Passport Number" />
                    <asp:BoundField DataField="DOB" HeaderText="Date of Birth" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="Passport_Expiry_DATE" HeaderText="Passport Expiry" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="Passport_Issue_DATE" HeaderText="Passport Issue" DataFormatString="{0:yyyy-MM-dd}" />
                </Columns>
            </asp:GridView>

<%--        <div class="row mt-4">
            <div class="col-md-12">
    <asp:GridView ID="gvDependents" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" 
    OnRowCommand="gvDependents_RowCommand" OnRowDataBound="gvDependents_RowDataBound">
    <Columns>
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Relation" HeaderText="Relation" />
        <asp:BoundField DataField="Passport_Number" HeaderText="Passport Number" />
        <asp:BoundField DataField="DOB" HeaderText="Date of Birth" DataFormatString="{0:d}" />
        <asp:BoundField DataField="Passport_Expiry_Date" HeaderText="Passport Expiry" DataFormatString="{0:d}" />
        
     
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteRow"
                    CommandArgument='<%# Eval("dependent_id") %>' CssClass="btn btn-danger btn-sm" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Dependent ID" Visible="False">
            <ItemTemplate>
                <asp:HiddenField ID="hfDependentId" runat="server" Value='<%# Eval("dependent_id") %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>--%>

            </div>
        </div>

        <!-- Other sections of your form -->
            <div class="col-md-12">
    <asp:Button ID="Button2" runat="server" Text="Save Form" OnClick="btnsaveform_Click" CssClass="btn btn-primary" />
              
                <asp:Button ID="btnVerify" runat="server" Text="Verify" OnClick="btnVerify_Click" CssClass="btn btn-primary" />

</div>

    </div>
</asp:Content>



       