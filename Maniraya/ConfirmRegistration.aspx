<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfirmRegistration.aspx.cs" Inherits="user_ConfirmRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous"/>
    
    <title></title>

</head>
    
<body>
    <form id="form1" runat="server">
  
 
   <div class="container cont" style="width:620px; height:1020px">
		<div class="card text-center">

            
		
		  <div class="card-body">


              <div class="row">
<div class="col-md-12" style="display:none">
    <div class="logo" >
               
               <%--  <asp:image id="Image1" runat="server" style="width:80%; border-radius:150px; height:60px" />--%>
     
			
			</div>


</div>

                  </div>
               <div class="row">
                  <div class="col-md-12">
					  
					  
					 <img src="myassets/assets/images/logo.png"><br>
				
					  
					 <img src="user/img/thankyou.png">
					 
<h6 style="font-size:30px;">
Welcome To  <%= clsUtility.ProjectName %>, Dear  <span style="font-size:35px; color:#ee7509"><asp:Label ID="lblName" runat="server"></asp:Label></span><br>You are now registered with <%= clsUtility.ProjectName %>, Your User Id is <span style="font-family:arial black;font-size:35px;color:#ee7509;"><b><asp:Label ID="LblLoginId" runat="server"> </asp:Label></b></span><br>
and Password is <span style="font-family:arial black;font-size:35px;color:#ee7509;"><b> <asp:Label ID="LblPassword" runat="server"></asp:Label></b></span> 

	</h6>
<a href="index.aspx" class="btn btn-primary">Login</a>
                   
                    
				
				<asp:Label ID="LblSponsorName" runat="server" style="display:none"></asp:Label>
			

                     


                     
				
		

</div>
                   </div>
                  
             <asp:Label ID="LblSponsorId" runat="server" style="display:none"></asp:Label>
                        
					
			
                  




			
			
			
		
	
		  
		</div>
	</div>

    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
<style>
		body{
		line-height:30px;
		}
		.thank{
			position:relative;
			margin-top:50px;
			font-size:24px;
		}
		.thank span{
			color:darkgreen;
			font-family:arial black;
		}
		.cont{
			position:relative;
			margin:0 auto;
			top:75px;
			
			overflow:auto;
          
		}
		.logo{
			position:relative;
			width:100px;
			height:100px;
			float:left;
			border-radius:150px;
			
			padding-top:10px;
            overflow:auto;
			
		}
		.menu{
			position:relative;
			float:right;
			top:-100px;
		}
		.congra{
			position:relative;
			top:20px;
			left:-40px;
			
		}
		.left{
			position:relative;
			top:30px;
			left:60px;
			
		}
		
		.box1{
			position:relative;
			float:left;
			margin:5px;
			padding-top:10px;
			width:130px;
			height:80px;
			border:3px solid red;	
		}
		.box2{
			position:relative;
			
			margin:5px;
			padding-top:10px;
			width:165px;
			height:80px;
			border:3px solid black;			
		}
		
		
	</style>

	   </form>
      
   
</body>
</html>
