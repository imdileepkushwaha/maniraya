<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminmaster.master" AutoEventWireup="true" CodeFile="UploadKYC.aspx.cs" Inherits="UploadKYC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">

        .Approved {
            background-color: #127113;
            color: #fff;
            padding: 3px;
            border-radius: 3px;
        }

        .Pending {
            background-color: #1596ab;
            color: #fff;
            padding: 3px;
            border-radius: 3px;
        }

        .Rejected {
            background-color: #c91d1d;
            color: #fff;
            padding: 3px;
            border-radius: 3px;
        }

    </style>

     <script>

         function validate() {
             
         }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
      <h1>
          KYC Upload
      </h1>
      <ol class="breadcrumb">
     <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">KYC</a></li>
        <li class="active"> Signup Form Upload   </li>
      
      </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal2">
                <div class="center2">
                    <img alt="" src="loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
          <div class="col-md-12">
              <div class="box box-primary">
                     <div class="row">
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User Id :</label>
                                  <asp:TextBox ID="txtuserid" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtchange" Enabled="false" />
                             </div>
                         </div>
                         <div class="col-md-6">
                             <div class="form-group">
                                 <label>User Name :</label>
                                  <asp:TextBox ID="txtusername" Enabled="false" runat="server" CssClass="form-control"  />
                             </div>
                         </div>
                        
                     </div>
                     <br />

           
                   <div class="box-body">
                  
                        <div class="box-header with-border">
              <h3 class="box-title">Signup form Upload </h3>
            </div>
                       <br />
                       <div class="row">
                           <div class="col-md-4">
                             <div class="form-group">
                                 <label>Signup from :</label>
                                 <asp:FileUpload ID="ImageUpload" runat="server" CssClass="ImageUpload" />    
                             </div>
                         </div>
                         <div class="col-md-4">
                             <div class="form-group">
                                 <asp:ImageButton ID="ImageShow" runat="server" Width="100px" Height="100px" CssClass="img IMageForm" />
                             </div>
                         </div>

                             <div class="col-md-4">
                             <div class="form-group">
                                   <div class="box-footer" id="div_update" runat="server">
                               <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                       &nbsp;
                                         <div id="divStatus" runat="server" visible="false">
                                  <label>Approval Status : </label>
                                <asp:Label ID="lblApprovalStatus" runat="server">Pending</asp:Label>
                            </div>
                                     
                                       </div>
                              </div>
                             </div>

                             </div>
                     <hr />
                       <div class="box-header with-border">
              <h3 class="box-title">Pan Card Upload </h3>
            </div>
                       <br />
                       <div class="row">
                           <div class="col-md-4">
                             <div class="form-group">
                                 <label>Pan Card No:</label>
                                 <asp:TextBox ID="txtpanno" runat="server" CssClass="form-control"></asp:TextBox>
                             </div>
                         </div>
                         <div class="col-md-4">
                             <div class="form-group">
                                 <asp:FileUpload ID="FileUpload1" runat="server"  CssClass="ImageUploadPan"/>  <br />  
                                 <asp:ImageButton ID="ImageButton1" runat="server" Width="100px" Height="100px"  CssClass="img IMageFormPan" />
                             </div>
                         </div>

                             <div class="col-md-4">
                             <div class="form-group">
                                   <div class="box-footer" id="div1" runat="server">
                               <asp:Button ID="Button1" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmitPan_Click" />
                                       &nbsp;
                                         <div id="divpanstatus" runat="server" visible="false">
                                  <label>Approval Status : </label>
                                <asp:Label ID="lblpanstatus" runat="server">Pending</asp:Label>
                            </div>
                                     
                                       </div>
                              </div>
                             </div>

                             </div>

                       <hr />
                       <div class="box-header with-border">
              <h3 class="box-title">Cancel Cheque/Passbook Upload </h3>
            </div>
                       <br />
                       <div class="row">
                           <div class="col-md-4">
                             <div class="form-group">
                                 <label>Cancel Cheque/Passbook :</label>
                                 <asp:FileUpload ID="FileUpload2" runat="server" CssClass="ImageUploadCheque" />    
                             </div>
                         </div>
                         <div class="col-md-4">
                             <div class="form-group">
                                 <asp:ImageButton ID="ImageButton2" runat="server" Width="100px" Height="100px"  CssClass="img IMageFormCheque"/>
                             </div>
                         </div>

                             <div class="col-md-4">
                             <div class="form-group">
                                   <div class="box-footer" id="div4" runat="server">
                               <asp:Button ID="Button2" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmitCheque_Click" />
                                       &nbsp;
                                         <div id="divchequestatus" runat="server" visible="false">
                                  <label>Approval Status : </label>
                                <asp:Label ID="lblchequestatus" runat="server">Pending</asp:Label>
                            </div>
                                     
                                       </div>
                              </div>
                             </div>

                             </div>
                        <hr />
                       <div class="box-header with-border">
              <h3 class="box-title">Address Proof/Aadhar Upload </h3>
            </div>
                       <br />
                        <div class="row">
                              <div class="col-md-2">
                                   <div class="form-group">
                                 <label>Address Prrof/Aadhar No:</label>
                             </div>
                              </div>
                            <div class="col-md-4">
                                   <div class="form-group">
                                 <asp:TextBox ID="txtaadharno" runat="server" CssClass="form-control"></asp:TextBox>
                             </div>
                              </div>
                        </div>
                       <div class="row">
                           <div class="col-md-4">
                             <div class="form-group">
                                 <label>Address Prrof/Aadhar Upload Front:</label>
                                 <asp:FileUpload ID="FileUpload3" runat="server" CssClass="ImageUploadAadhar" />    
                                 <br />
                                 <asp:ImageButton ID="ImageButton3" runat="server" Width="100px" Height="100px"  CssClass="img IMageFormAadhar"/>
                             </div>
                         </div>
                         <div class="col-md-4">
                             <div class="form-group">
                                  <label>Address Prrof/Aadhar Upload Back:</label>
                                 <asp:FileUpload ID="FileUpload4" runat="server" CssClass="ImageUploadAadharBack" />    
                                <br />
                                 <asp:ImageButton ID="ImageButton4" runat="server" Width="100px" Height="100px"  CssClass="img IMageFormAadharBack"/>
                             </div>
                         </div>

                             <div class="col-md-4">
                             <div class="form-group">
                                   <div class="box-footer" id="div3" runat="server">
                               <asp:Button ID="Button3" OnClientClick="return validate();" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmitAadhar_Click" />
                                       &nbsp;
                                         <div id="divaadharstatus" runat="server" visible="false">
                                  <label>Approval Status : </label>
                                <asp:Label ID="lblaadharstatus" runat="server">Pending</asp:Label>
                            </div>
                                     
                                       </div>
                              </div>
                             </div>

                             </div>

                     </div>
                        
                       </div>
                         <div class="box-footer">
                        
             

                               
                    
              </div>


                  
              </div>
              </div>
                  </div>


                <div id="DivPhotolarge" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                  
                    <div class="modal-body">
                       
                        <div class="form-group">
                          <img id="img1" runat="server" class="img1" src="" style="width:570px;height:400px;" />     
                        </div>
                        
                    </div>
                    <div class="modal-footer">
                       
                          <button type="button"  class="btn btn-danger"  data-dismiss="modal">Close</button>                  
                    </div>
                </div>
            </div>
        </div>

        
        </ContentTemplate>
        <Triggers>
      
        <asp:PostBackTrigger ControlID = "btnSubmit" />
            <asp:PostBackTrigger ControlID = "Button1" />
            <asp:PostBackTrigger ControlID = "Button2" />
            <asp:PostBackTrigger ControlID = "Button3" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {

            $(".ImageUpload").change(function () {

                var input = this;
                var img = $(".IMageForm");
                if (input.files && input.files[0]) {
                    var file = input.files[0];
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $('.IMageForm').attr("src",e.target.result);
                        fle = e.target.result;
                    };
                    reader.readAsDataURL(input.files[0]);
                }

            });

            $(".ImageUploadPan").change(function () {

                var input = this;
                var img = $(".IMageFormPan");
                if (input.files && input.files[0]) {
                    var file = input.files[0];
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $('.IMageFormPan').attr("src", e.target.result);
                        fle = e.target.result;
                    };
                    reader.readAsDataURL(input.files[0]);
                }

            });


            $(".ImageUploadCheque").change(function () {

                var input = this;
                var img = $(".IMageFormCheuqe");
                if (input.files && input.files[0]) {
                    var file = input.files[0];
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $('.IMageFormCheque').attr("src", e.target.result);
                        fle = e.target.result;
                    };
                    reader.readAsDataURL(input.files[0]);
                }

            });

            $(".ImageUploadAadhar").change(function () {

                var input = this;
                var img = $(".IMageFormAadhar");
                if (input.files && input.files[0]) {
                    var file = input.files[0];
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $('.IMageFormAadhar').attr("src", e.target.result);
                        fle = e.target.result;
                    };
                    reader.readAsDataURL(input.files[0]);
                }

            });
            $(".ImageUploadAadharBack").change(function () {

                var input = this;
                var img = $(".IMageFormAadharBack");
                if (input.files && input.files[0]) {
                    var file = input.files[0];
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $('.IMageFormAadharBack').attr("src", e.target.result);
                        fle = e.target.result;
                    };
                    reader.readAsDataURL(input.files[0]);
                }

            });

            $(".img").click(function () {

                $(".img1").attr("src", $(this).attr("src"));
                showModal1();
                return false;

            });

        });


        function showModal1() {
            $('#DivPhotolarge').modal({ backdrop: 'static', keyboard: false })
        }
        function Closepopup() {
            $('#DivPhotolarge').modal('hide');
            $('body').removeClass('modal-open');
            $('body').css('padding-right', '0');
            $('.modal-backdrop').remove();

        }
        </script>
</asp:Content>

