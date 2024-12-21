<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC>

<html>
<head id="Head1" runat="server">
    <title>FFC VISA PORTAL : Login</title>

 	<meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Language" content="en" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no" />
    <meta name="description" content="For management of guest houses." />
    <meta name="msapplication-tap-highlight" content="no" />
	<link href="/css/main.css" rel="stylesheet" />
	<link rel="icon" href="Image/favicon.ico" type="image/x-icon" />
   <%--<script src="https://www.google.com/recaptcha/api.js?render=6Le3_cUeAAAAACYlCabSLta7lFn19nCDwNlp8Odp"></script>--%>

	

</head>
<body>
<noscript>
<meta http-equiv="refresh" content="0;URL=JSNotEnabled.htm" />
</noscript>





<div class="app-container app-theme-white body-tabs-shadow">
        <div class="app-container">
            <div class="h-100 bg-plum-plate bg-animation">
                <div class="d-flex h-100 justify-content-center align-items-center">
                    <div class="mx-auto app-login-box col-md-8">
                        <div class="app-logo mx-auto mb-3"></div>
                        <div class="modal-dialog w-100 mx-auto">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <div class="h5 modal-title text-center">
                                        <img src="Image/small-logo.png" />
                                      <h4>FFC VISA Portal</h4>
                                    </div><br />
                                 
                                    
                                 
                             
                                         <form id="form2" runat="server" autocomplete="off" requireSSL="true" class="user">
                                          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
                                                    <asp:HiddenField id="txtToken" runat="server" />
                                                  
                                              <div class="row">
                                                     <div class="col-sm-1">
                                                       
                                                        </div>
                                                        <div class="col-sm-3">
                                                        <asp:Label ID="lblUserId" text="User Id" runat="server" ></asp:Label>
                                                        </div>
                                                          <div class="col-sm-6">
                                                        <asp:TextBox ID="txtUserId" required="true" autocomplete="off" runat="server" class="form-control form-control-user" placeholder="User id" ClientIDmode="static" ToolTip="e.g. 45504-1808835-5"></asp:TextBox>
                                                        <asp:HiddenField ID="hfUserId" runat="server" />
                                                          </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-1">
                                                       
                                                        </div>
                                                    <div class="col-sm-3">
                                                          <asp:Label ID="lblPassword" text="Password" runat="server" ></asp:Label>
                                                      </div>  
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtPassword" required="true" autocomplete="off" runat="server" TextMode="Password" placeholder="Password" class="form-control form-control-user"></asp:TextBox>
                                                        <asp:HiddenField ID="hfPassword" runat="server" />
                                                    </div>
                                                 </div>

                                             <div class="row">
                                                     <div class="col-sm-1">
                                                       
                                                        </div>
                                                        <div class="col-sm-3">
                                                        <asp:Label ID="lblLoginCode" text="Login Code" runat="server" Visible="false" ></asp:Label>
                                                        </div>
                                                        <div class="col-sm-6">
                                                        <asp:TextBox ID="txtLoginCode" autocomplete="off" runat="server" Visible="false" class="form-control form-control-user" placeholder="" ClientIDmode="static" ToolTip="e.g. 45504-1808835-5"></asp:TextBox>
                                                         </div> 
                                                </div>

                                                 <div class="row">
                                                          <div class="col-sm-4">
                                                               <asp:Label ID="Label3" text="" runat="server" ></asp:Label>
                                                          </div> 
                                                     <div class="col-sm-4">
                                                        <asp:Button class="btn btn-primary btn-user btn-block" runat="server" Text="Login" ID="btnLogin" OnClick="Authenticate_login" />
                                                     </div></div>

                                             

                                                        <hr>
                                                        <asp:Label ID="lblStatus" runat="server" style="color: #FF0000" Text=""></asp:Label>
                                             
                                             
                                                     
                                                         
                                                        <div class="text-center">
                                                            <a class="small" href="forgot.aspx">Forgot Password?</a>
                                                        </div>
                                               
                                  </form>
                          
                                
				                       
                           
                                 
                            </div>
                        </div>
                        <div class="text-center text-white ">Fauji Fertilizer Company Limited</div>
                    </div>
                </div>
            </div>
        </div>
</div>
</body>    
    <script>
  grecaptcha.ready(function() {
      grecaptcha.execute('6Le3_cUeAAAAACYlCabSLta7lFn19nCDwNlp8Odp', {action: 'login'}).then(function(token) {
          document.getElementById("txtToken").value = token;
      });
  });
</script>

</html>