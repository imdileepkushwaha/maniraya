<%@ page language="C#" autoeventwireup="true"  CodeFile="RegisterNew.aspx.cs" inherits="RegisterNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    
     <meta charset="UTF-8"/>
       <link href="../css/radio/style.css" rel="stylesheet" />
     <title> Maniraya  </title>
	
	
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'/>
   <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css"/> 
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css"/>  
    <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css"/>   
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css"/>  
    <link rel="stylesheet" href="dist/css/skins/_all-skins.min.css"/>
    <link href="sarah/assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/radio/style.css" rel="stylesheet" />
	
	

	
	

   <!-- Site Title -->
	
	<script type="text/javascript">
	    function dobcheck() {
	        var birth = document.getElementById("<%=txtdob.ClientID%>")
    if (birth != "") {



        var record = document.getElementById("<%=txtdob.ClientID%>").value.trim();
        var currentdate = new Date();
        var day1 = currentdate3.getDate();
        var month1 = currentdate3.getMonth();
        month1++;
        var year11 = currentdate3.getFullYear() - 17;
        var year2 = currentdate3.getFullYear() - 100;
        var record_day1 = record.split("/");
        var sum = record_day1[1] + '/' + record_day1[0] + '/' + record_day1[2];
        var current = month1 + '/' + day1 + '/' + year11;
        var current1 = month1 + '/' + day1 + '/' + year2;
        var d1 = new Date(current)
        var d2 = new Date(current1)
        var record1 = new Date(sum);
        if (record1 > d1) {

            alert("Sorry ! Minors need parential guidance to use this website");
            document.getElementById('txtdob').blur();
            document.getElementById('txtdob').value = "";
            document.getElementById('txtdob').focus();
            return false;
        }
    }
}
</script>


    <script type="text/javascript">
        function dobcheck() {
            var birth = document.getElementById("<%=txtdob2.ClientID%>")
	        if (birth != "") {



	            var record = document.getElementById("<%=txtdob2.ClientID%>").value.trim();
        var currentdate = new Date();
        var day1 = currentdate3.getDate();
        var month1 = currentdate3.getMonth();
        month1++;
        var year11 = currentdate3.getFullYear() - 17;
        var year2 = currentdate3.getFullYear() - 100;
        var record_day1 = record.split("/");
        var sum = record_day1[1] + '/' + record_day1[0] + '/' + record_day1[2];
        var current = month1 + '/' + day1 + '/' + year11;
        var current1 = month1 + '/' + day1 + '/' + year2;
        var d1 = new Date(current)
        var d2 = new Date(current1)
        var record1 = new Date(sum);
        if (record1 > d1) {

            alert("Sorry ! Minors need parential guidance to use this website");
            document.getElementById('txtdob2').blur();
            document.getElementById('txtdob2').value = "";
            document.getElementById('txtdob2').focus();
            return false;
        }
    }
}
</script>

<script>
        // Change the type of input to password or text
        function Toggle() {
            let temp = document.getElementById("<%=txtuserpassword.ClientID%>");
             
            if (temp.type === "password") {
                temp.type = "text";
            }
            else {
                temp.type = "password";
            }
        }
    </script>

      <script type="text/javascript">
          function validate() {
              if (document.getElementById("<%=TxtUsrid.ClientID%>").value == "") {

                  alert('Enter User Id');
                  document.getElementById("<%=TxtUsrid.ClientID%>").focus();
                  return false;
              }
              if (document.getElementById("<%=txtsponserid.ClientID%>").value == "") {

                  alert('Enter Sponser Id');
                  document.getElementById("<%=txtsponserid.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtname.ClientID%>").value == "") {

                  alert('Enter Name');
                  document.getElementById("<%=txtname.ClientID%>").focus();
                return false;
            }



            if (document.getElementById("<%=txtmobile.ClientID%>").value == "") {

                  alert('Enter Mobile');
                  document.getElementById("<%=txtmobile.ClientID%>").focus();
                return false;
            }

           // if (document.getElementById("<%=txtnomineename.ClientID%>").value == "") {

               //   alert('Enter Nominee Name');
                   //document.getElementById("<%=txtnomineename.ClientID%>").focus();
             //    return false;
            // }

            // if (document.getElementById("<%=ddrelation.ClientID%>").value == "") {

                //   alert('Select Nominee Relation');
                //   document.getElementById("<%=ddrelation.ClientID%>").focus();
               //  return false;
           //  }
              //if (validatephonenumber(document.getElementById("<%=txtmobile.ClientID%>").value) == false) {
              // alert('Invalid Mobile No');
              //  document.getElementById("<%=txtmobile.ClientID%>").focus();
              //  return false;
              //  }
              //if (document.getElementById("<%=txtemail.ClientID%>").value == "") {

              // alert('Enter Email');
              // document.getElementById("<%=txtemail.ClientID%>").focus();
              // return false;
              //  }

           //    if (validateemail(document.getElementById("<%=txtemail.ClientID%>").value) == false) {

               //    alert('Invalid Email ID');
               //    document.getElementById("<%=txtemail.ClientID%>").focus();
              //   return false;
          //   }

          //  if (document.getElementById("<%=txtaadhar.ClientID%>").value == "") {

            //      alert('Enter Adhar Number');
            //      document.getElementById("<%=txtaadhar.ClientID%>").focus();
           //       return false;
           //   }


              // if (document.getElementById("<%=ddgender.ClientID%>").value == "0") {

              //     alert('Select Gender');
              //    document.getElementById("<%=ddgender.ClientID%>").focus();
              //    return false;
              // }

              //     if (document.getElementById("<%=txtaddress.ClientID%>").value == "") {

              //        alert('Enter Address');
              //       document.getElementById("<%=txtaddress.ClientID%>").focus();
              //         return false;
              //    }

              <%--  if (document.getElementById("<%=ddcountry.ClientID%>").value == "0") {

                alert('Select Country');
                document.getElementById("<%=ddcountry.ClientID%>").focus();
            return false;
        }
        if (document.getElementById("<%=ddstate.ClientID%>").value == "0") {

                alert('Select State');
                document.getElementById("<%=ddstate.ClientID%>").focus();
               return false;
           }
           if (document.getElementById("<%=ddcity.ClientID%>").value == "0") {

                alert('Select City');
                document.getElementById("<%=ddcity.ClientID%>").focus();
              return false;
          }--%>
              //     if (document.getElementById("<%=ddcity.ClientID%>").value == "") {

              //          alert('Select City');
              //         document.getElementById("<%=ddcity.ClientID%>").focus();
              //         return false;
              // }
              //      if (document.getElementById("<%=txtareaname.ClientID%>").value == "") {

              //        alert('Enter Area');
              //      document.getElementById("<%=txtareaname.ClientID%>").focus();
              //           return false;
              //  }



              //      if (document.getElementById("<%=txtpincode.ClientID%>").value == "") {

              //         alert('Enter Pincode');
              //         document.getElementById("<%=txtpincode.ClientID%>").focus();
              //              return false;
              //     } 
              if (document.getElementById("<%=txtuserpassword.ClientID%>").value == "") {

                  alert('Enter Password');
                  document.getElementById("<%=txtuserpassword.ClientID%>").focus();
                return false;
            }


            if (document.getElementById("<%=txtconfirmpassword.ClientID%>").value == "") {

                  alert('Enter Confirm Password');
                  document.getElementById("<%=txtconfirmpassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtuserpassword.ClientID%>").value != document.getElementById("<%=txtconfirmpassword.ClientID%>").value) {

                  alert('Password Not Match');
                  document.getElementById("<%=txtuserpassword.ClientID%>").focus();
                return false;
            }
        }


        function validatephonenumber(inputtxt) {
            var phoneno = /^([6-9]{1})([0-9]{9})$/;
            if (inputtxt.match(phoneno)) {
                return true;
            }
            else {
                return false;
            }
        }

        // function validateemail(inputtxt) {
          //   var email = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
          //   if (inputtxt.match(email)) {
              //   return true;
           //  }
           //  else {
           //      return false;
            // }
        // }
        // function validatepan(inputtxt) {
          //   var panVal = $('#panNumber').val();
            // var regpan = /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/;

            // if (regpan.test(panVal)) {
                // valid pan card number
           //  } else {
                // invalid pan card number
         //    }


        // }
    </script>
</head>
<body>

 
        <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
               
                     <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="img/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 15%; left: 25%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <header id="home">
        <div class="container">
            <div class="header d-lg-flex justify-content-between align-items-center py-sm-3 py-2 px-sm-2 px-1">
                <!-- logo -->
                <div id="logo" style="text-align:center">
                     <h1><a href="index.html"><span class="brand-text-mpremium">MPremium</span></a></h1>
                       
                </div>
                <!-- //logo -->
                <!-- nav -->
           
                <!-- //nav -->
            </div>
        </div>
    </header>

                        <div class="inner-banner-w3ls d-flex flex-column justify-content-center align-items-center">
    </div>
			  <nav aria-label="breadcrumb">
        <ol class="breadcrumb d-flex justify-content-center">
            <li class="breadcrumb-item">
                <a href="index.html" class="m-0">Home</a>
            </li>
            <li class="m 0" aria-current="page">
				
				<a href="/user" class="m-0" style="color:#000">Register Here </a> </li>
        </ol>
    </nav>
			
                             <div class="center">
								  
								   
                            
                                       <section class="content">
                                              <div class="row">
                            <!--Body-->
                           <div class="col-md-6">
                                    <div class="form-group">
                                         <label class="text-end ">User Id :</label>
                                        <asp:TextBox ID="TxtUsrid"  CssClass="form-control" runat="server" placeholder="USER ID"></asp:TextBox>
                     </div>
                               </div>
                                                  </div>
                        <div class="row">
                            <!--Body-->
                           <div class="col-md-6">
                                    <div class="form-group" >
                                        <label class="text-end ">Sponsor Id :</label>
                                        <asp:TextBox ID="txtsponserid" AutoPostBack="true" OnTextChanged="txtsponserid_TextChanged" CssClass="form-control" runat="server" placeholder="Sponsor ID"></asp:TextBox>
</div>
                                </div>
                            

                                 <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Sponsor Name :</label>
                                        <asp:TextBox ID="txtsponsername" Enabled="false" CssClass="form-control" runat="server" placeholder="Sponsor Name"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
							     <div class="col-md-6" style="display:none">
                                <div class="form-group">
									 <div class="input-group">
                                    <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                    <asp:TextBox ID="txtparentname" Enabled="false" CssClass="form-control" runat="server" placeholder="Parental Name"></asp:TextBox> </div>
                                </div>
                            </div>
							<div class="col-md-6" style="display:none">
                                <div class="form-group">
									    <div class="input-group">
                                    <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                    <asp:TextBox ID="txtparentid" AutoPostBack="true" CssClass="form-control" runat="server" OnTextChanged="txtparentid_TextChanged" placeholder="Parental ID"></asp:TextBox>
                                </div>
									 </div>
                            </div>
                        
                       

                        <div class="row" >
                            <!--Body-->
                            <div class="col-md-6" style="display:none;">
                                <div class="form-group">
                                    <div class="input-group">
                                        <div class="input-group-addon bg-light"><i class="fa fa-key text-primary"></i></div>
                                        <asp:TextBox ID="txtepin" CssClass="form-control" runat="server" placeholder="E-Pin" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6" style="display:none;">
                                <div class="form-group">
                                    <div class="input-group">
                                        <div class="input-group-addon bg-light"><i class="fa fa-inr text-primary"></i></div>
                                        <asp:TextBox ID="txtamount" Enabled="false" CssClass="form-control" runat="server" placeholder="Amount"></asp:TextBox>
                                    </div>
                                </div>
                        </div>
                        </div>
                        <div class="form-row" style="display: none;">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:RadioButton ID="RdBtnFree" runat="server" Text="Free Regitration" GroupName="A" AutoPostBack="true" OnCheckedChanged="RdBtnFree_CheckedChanged" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:RadioButton ID="RdBtnEpin" runat="server" Text="E-Pin Regitration" GroupName="A" AutoPostBack="true" OnCheckedChanged="RdBtnEpin_CheckedChanged" />
                                </div>
                            </div>
                            <div class="col-md-6">
                            </div>
                        </div>
                        <asp:Panel ID="pnlpin" Visible="false" runat="server">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Select Plan :</label>
                                        <asp:DropDownList ID="DDLstPlan" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLstPlan_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="display:none;">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Select E-Pin :</label>
                                        <asp:DropDownList ID="ddepin" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddepin_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                        </asp:Panel>
                       <div class="row" style="display:none" >
                            <div class="col-md-1">
                                <div class="form-group custom-radio">
                                    <asp:RadioButton ID="RdBtnLeft" runat="server" Text="Left" GroupName="B" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group custom-radio">
                                    <asp:RadioButton ID="RdBtnRight" runat="server" Text="Right" GroupName="B" />
                                </div>
                            </div>
                            <div class="col-md-6">
                            </div>
                        </div> 


              <div class="row">
							<div class="col-md-6">
                                <div class="form-group">
                                    <label class="text-end ">Select Position :</label>
                                    <div class="input-group">
                                        <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                        <asp:DropDownList ID="ddposition" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select Position</asp:ListItem>
                                            <asp:ListItem Value="Left">Left</asp:ListItem>
                                            <asp:ListItem Value="Right">Right</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <!--Body-->
							<div class="col-md-6" >
							
								
								 <div class="form-group">
                                       <label class="text-end ">Name :</label>
                                    <div class="input-group">
                                        <div class="input-group-addon bg-light" >
											
									
 <asp:DropDownList ID="ddpp" CssClass="" runat="server" style="border:none">
                                            <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                            <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                            <asp:ListItem Value="Miss">Miss</asp:ListItem>
                                        </asp:DropDownList>
  
								</div>
                                        <asp:TextBox ID="txtname" CssClass="form-control" runat="server" placeholder="Name"></asp:TextBox>

                                    </div>
                                </div></div>
                              
                            </div>
                        <div class="row">
                            <!--Body-->
							   <div class="col-md-6">
                                <div class="form-group">
                                    <label class="text-end ">Select Gender :</label>
                                    <div class="input-group">
                                        <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                        <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                             <asp:ListItem Value="Transgender">Transgender</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                             <div class="col-md-6">
                              <div class="form-group" >
                                        <label class="text-end">Email:</label>
                                        <asp:TextBox ID="txtemail" CssClass="form-control" runat="server" placeholder="Email"></asp:TextBox>

                                    </div>
                                </div>

                            <div class="col-md-6" >
                                <div class="form-group">
                                     <label class="text-end ">Mobile No. :</label>
                                    <div class="input-group">
                                        <div class="input-group-addon bg-light"><i class="fa fa-tag prefix text-primary"></i></div>
                                        <asp:TextBox ID="txtmobile" onkeypress="return isNumber(event)" CssClass="form-control" runat="server" maxlength="10" placeholder="Mobile No"></asp:TextBox>
                                    </div>
                                </div>
                            </div> 

                            <div class="col-md-6">
		  <div class="form-group">
		     <label>Date of Birth : Year-Month-Date</label>
		   
                         
                             <fieldset>
                                <div class="col-md-4 dvRow">
                                    <asp:DropDownList ID="ddlYear" CssClass="form-control" ToolTip="Year" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 dvRow">
                                    <asp:DropDownList ID="ddlMonth" CssClass="form-control" ToolTip="Month" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 dvRow">
                                    <asp:DropDownList ID="ddlDay" CssClass="form-control" ToolTip="Day" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </fieldset>
                        </div>
                                    <div class="form-group" style="display:none;">
                                        <label>Date Of Birth(dd/MM/yyyy)</label>
                                     

                                        <asp:TextBox ID="txtdob" CssClass="form-control form_date" runat="server" Placeholder="dd/MM/yyyy"></asp:TextBox>
                                    </div>
                                </div>

                            <div class="col-md-12" >
                            <div class="form-group">
                                <label class="text-end ">Address :</label>
                                <asp:TextBox ID="TextBox3" TextMode="MultiLine" CssClass="form-control" runat="server" placeholder="Address"></asp:TextBox>
                            </div>

                        </div>
							 <div class="col-md-6">
                            <div class="form-group">
                                <label class="text-end "> Select Country :</label>
                                <asp:DropDownList ID="ddcountry" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddcountry_SelectedIndexChanged">
                                    <asp:ListItem Value="0"> Select Country</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                                     </div>
                                 <div class="col-md-6" >
                            <div class="form-group">
                                <label class="text-end "> Select State :</label>
                                <asp:DropDownList ID="ddstate" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddstate_SelectedIndexChanged">
                                    <asp:ListItem Value="0"> Select State</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                                     </div>

                                 <div class="col-md-6">
                            <div class="form-group">
                                <label class="text-end "> Select City :</label>
                                <asp:DropDownList ID="ddcity" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0"> Select City</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                                     </div>

                                <div class="col-md-6" >
                            <div class="form-group">
                                <label class="text-end ">Pincode :</label>
                                <asp:TextBox ID="TextBox2" onkeypress="return isNumber(event)" CssClass="form-control" runat="server" Placeholder="Pincode"></asp:TextBox>
                                
                            </div>
                                     </div>
                                    
                                
                          
                             <div class="col-md-6">   
                                 <div class="form-group">
                                      <label class="text-end ">Nominee Name :</label>
                                        <div class="input-group">
                                        <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                            <asp:TextBox ID="txtnomineename" placeholder= "Nominee Name" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>  
                                        </div>
                                    </div> 

                             <div class="col-md-6">
                                <div class="form-group">
                                       <label class="text-end ">Select Relation :</label>
                                    <div class="input-group">
                                        <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                        <asp:DropDownList ID="ddrelation" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select Relation</asp:ListItem>
                                            <asp:ListItem Value="Husband">Husband</asp:ListItem>
                                            <asp:ListItem Value="Wife">Wife</asp:ListItem>
                                             <asp:ListItem Value="Mother">Mother</asp:ListItem>
                                             <asp:ListItem Value="Father">Father</asp:ListItem>
                                             <asp:ListItem Value="Son">Son</asp:ListItem>
                                             <asp:ListItem Value="Daughter">Daughter</asp:ListItem>
											 <asp:ListItem Value="Brother">Brother</asp:ListItem>
											 <asp:ListItem Value="Cousin">Cousin</asp:ListItem>
											 <asp:ListItem Value="Uncle">Uncle</asp:ListItem>
											 <asp:ListItem Value="Aunt">Aunt</asp:ListItem>
                                              <asp:ListItem Value="Brother-In-Law">Brother-In-Law</asp:ListItem>
											 <asp:ListItem Value="Mother-In-Law">Mother-In-Law</asp:ListItem>
											 <asp:ListItem Value="Sister-In-Law">Sister-In-Law</asp:ListItem>
											 <asp:ListItem Value="Father-In-Law">Father-In-Law</asp:ListItem>
                                                      
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                           


                          <div class="col-md-6"  style="display:none;">
                                    <div class="form-group" >
                                        <label class="text-end">Adhar Number :</label>
                                        <asp:TextBox ID="txtaadhar" CssClass="form-control" runat="server" placeholder="Aadhar Number" MaxLength="12"></asp:TextBox>

                                    </div>
                                </div>
                            </div>



 <div class="row">

      
                            	<div class="col-md-6" style="display:none;">
                                <div class="form-group">
                                    <div class="input-group">
                                        <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                        <asp:TextBox ID="txtheight" CssClass="form-control" runat="server" placeholder="Name"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
     	<div class="col-md-6" style="display:none;">
                                <div class="form-group">
                                    <div class="input-group">
                                        <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" placeholder="Name"></asp:TextBox>

                                    </div>
                                </div>
                            </div> </div>
     

 <div class="row">
                             <div class="col-md-6"  style="display:none;">
                            <div class="form-group" >
                                        <label class="text-end">Select Gender:</label>
                                        <asp:DropDownList ID="ddgender" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select Gender</asp:ListItem>
                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                          
							
							<div class="col-md-6"  style="display:none;">
                                        <div class="form-group" >
                                        <label class="text-end">Pan Number:</label>
                                            <asp:TextBox ID="txtPanNumber" runat="server" CssClass="form-control" placeholder="Pan Card Number"></asp:TextBox>
                                        </div>
                                    </div>
       </div>
                                           <div class="row">
                                  <div class="col-md-6" style="display: none;">
                                    <div class="input-group">
                                        <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                        <asp:TextBox ID="txtaccountholdername" placeholder= "Account Holder Name" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div> <br>
                              <div class="col-md-6"  style="display: none;">
                                     <div class="input-group">
                                        <div class="input-group-addon bg-light"><i class="fa fa-user text-primary"></i></div>
                                        <asp:TextBox ID="txtaccountno" placeholder= "Account Number" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                             </div>
                                        
                             <div class="row"  style="display: none;">
                                  <div class="col-md-6">
                                    <div class="input-group">
                                       <div class="input-group-addon bg-light"><i class="fa fa-tag prefix text-primary"></i></div>
                                        <asp:TextBox ID="txtifsccode" Placeholder="IFSC Code" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div> 
                                               <div class="col-md-6" >
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddbank" CssClass="form-control" runat="server"> 
                                             <asp:ListItem Value="0"> Select Bank</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 </div>
                                 <div class="row" style="display: none;">
                       
                        </div>

        
                           <div class="row" style="display:none">
                             <div class="col-md-12" >
                            <div class="form-group">
                                <asp:TextBox ID="txtaddress" TextMode="MultiLine" CssClass="form-control" runat="server" placeholder="Address"></asp:TextBox>
                            </div>

                        </div>
                                
                              </div>
                          <div class="row" style="display:none">
                               
                                
                     
                                <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox ID="txtpincode" onkeypress="return isNumber(event)" CssClass="form-control" runat="server" Placeholder="Pincode"></asp:TextBox>
                                <asp:TextBox ID="txtareaname" CssClass="form-control" runat="server" Placeholder="Area" Visible="false"></asp:TextBox>
                            </div>
                                     </div>
                               
                              </div>
                        <div class="row">
                              
                            
                             <div class="col-md-6"  style="display:none;">
                                  <div class="form-group" >
                                        <label class="text-end">Mobile Number:</label>
                                        <asp:TextBox ID="txt123mobile" onkeypress="return isNumber(event)" CssClass="form-control" runat="server" maxlength="10" placeholder="Mobile No"></asp:TextBox>
                                    </div>
                                </div>

                            
                            </div>  
                                    
                         <div class="row">
                             <div class="col-md-6"   style="display:none;">
                                         <div class="form-group" >
                                        <label class="text-end">Nominee Name:</label>
                                            <asp:TextBox ID="txt123nomineename" placeholder= "Nominee Name" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div> 

                                <div class="col-md-6">
		 <div class="form-group">
		     <label>Nominee Date of Birth : Year-Month-Date</label>
		   
                         
                             <fieldset>
                                <div class="col-md-4 dvRow">
                                    <asp:DropDownList ID="ddlYear2" CssClass="form-control" ToolTip="Year" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged2">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 dvRow">
                                    <asp:DropDownList ID="ddlMonth2" CssClass="form-control" ToolTip="Month" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged2">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 dvRow">
                                    <asp:DropDownList ID="ddlDay2" CssClass="form-control" ToolTip="Day" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </fieldset>
                        </div>
                                    <div class="form-group" style="display:none;">
                                        <label>Nominee Date Of Birth(dd/MM/yyyy)</label>
                                     

                                        <asp:TextBox ID="txtdob2" CssClass="form-control form_date" runat="server" Placeholder="dd/MM/yyyy"></asp:TextBox>
                                    </div>
                                </div>
                                    <div class="col-md-6"   style="display:none;">
                                    <div class="form-group" >
                                        <label class="text-end">Nominee Relation:</label>
                                            <asp:DropDownList ID="dd132relation" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select Relation</asp:ListItem>
                                            <asp:ListItem Value="Husband">Husband</asp:ListItem>
                                            <asp:ListItem Value="Wife">Wife</asp:ListItem>
                                             <asp:ListItem Value="Mother">Mother</asp:ListItem>
                                             <asp:ListItem Value="Father">Father</asp:ListItem>
                                             <asp:ListItem Value="Son">Son</asp:ListItem>
                                             <asp:ListItem Value="Daughter">Daughter</asp:ListItem>
												 <asp:ListItem Value="Brother">Brother</asp:ListItem>
                                             <asp:ListItem Value="Sister">Sister</asp:ListItem>
                                             <asp:ListItem Value="Father-In-Law">Father-In-Law</asp:ListItem>
                                             <asp:ListItem Value="Mother-In-Law">Mother-In-Law</asp:ListItem>
                                              <asp:ListItem Value="Other">Other</asp:ListItem>
                                                      
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                               
                        </div>
                        <br>
                                         
                        <div class="row">
                            <!--Body-->
                            <div class="col-md-6">
                              <div class="form-group" >
                                        <label class="text-end">Password:</label>
                                        <asp:TextBox ID="txtuserpassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Password"></asp:TextBox><br><input type="checkbox" onclick="Toggle()">
    <strong>Show Password</strong>

                                    </div>
                                </div>
                           

                            <div class="col-md-6">
                                <div class="form-group" >
                                        <label class="text-end">Confirm Password:</label>
                                        <asp:TextBox ID="txtconfirmpassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Confirm Password"></asp:TextBox>
                                    </div>
                                </div>
                           
                        </div>
                    

                        <div class="form-row" style="display: none;">
                            <div class="form-group col-md-6">
                            </div>
                            <div class="form-group col-md-6">
                                <div class="col-md-4 dvRow">
                                   
                                    
                                </div>
                                <div class="col-md-4 dvRow">
                                 
                                 
                                </div>
                                <div class="col-md-4 dvRow">
                                   
                                </div>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-12" style="margin-bottom: 0 !important;">
                            </div>
                        </div>
						
						<br>

                                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <div class="form-group">
                                        
                                    <p style="color:#000">
                                        <asp:CheckBox ID="CheckBox1" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged"  runat="server" /> 
                                        I agree to the<a href="Terms_Conditions.html" class="thembo text-primary" target="_blank">Terms & Condition</a></p>
                                        </div>
                                    </div>
                                 </div>	<br>
                        <div class="row">
							
							<div class="col-sm-5"></div>
                            <div class="col-sm-2">
                                <div class="text-center">

                                    <asp:Button ID="btnSubmit" OnClientClick="return validate();" CssClass="btn btn-primary btn-block rounded-0 py-2" runat="server" Text="Submit" OnClick="btnSubmit_Click" Enabled="false" />
                                </div>
                            </div>
							
							
							<div class="col-sm-5"></div>
                        </div>
</section>
                            
                    </ContentTemplate>
                </asp:UpdatePanel>
            </form>
    

    <script src="../bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap 3.3.7 -->
<script src="../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
   

</body>
</html>
