<%@ Page Title="" Language="C#" MasterPageFile="~/user/WebsiteMaster.master" AutoEventWireup="true" CodeFile="Event.aspx.cs" Inherits="user_Event" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <section class="slider-section">
            <div class="home-slides owl-carousel owl-theme">
                <%for(int i=0;i<dt.Rows.Count;i++){ %>
                <div class="home-single-slide" data-background="<%=dt.Rows[i]["Img_Url"].ToString().Replace("~/user/","") %>">
                    <div class="home-slide-overlay"></div>
                    <div class="home-single-slide-inner">
                       
                    </div>
                </div>
              <%} %>
            </div>
        </section>
    <div> <br> <br> </div>
   <div class="container">
            <div class="row">
				<div class="col-lg-4 col-md-4" style="border: 1px black solid;">
                    <div style="background-color:red; color:yellow; text-align:center; margin-left:-15px; margin-right:-15px; font-weight:bold;"> EVENT </div>
                    <div style="height:500px; width:100%; overflow-y:scroll;">
					<%for(int i=0; i<dt.Rows.Count; i++) {%>

					<p style="color:black;"><%=(i+1) %>- <a onClick="myFunction(<%=dt.Rows[i]["Id"].ToString() %>);"><%=dt.Rows[i]["Title"].ToString() %></a>
                        <input type="hidden"  id='<%=dt.Rows[i]["Id"].ToString() %>' value="<%=dt.Rows[i]["Content"].ToString() %>"/>
					</p>
									<%} %>
                    </div>
				</div>
				<div class="col-lg-1 col-md-4">
				</div>
				<div class="col-lg-6 col-md-4" style="border: 1px black solid;">
                     <div style="background-color:red; color:yellow; text-align:center; margin-left:-15px; margin-right:-15px; font-weight:bold;"> DESCRIPTION </div>
                    <div  id="idcomment" style="height:500px; width:100%; color:black; overflow-y:scroll;">

                    </div>
					


				</div>
				
			</div>
            </div>

     <script>
         function myFunction(SID) {
             var strid = document.getElementById(SID).value;
             document.getElementById("idcomment").innerHTML = strid;

         }
    </script>
    <div> <br> <br></div>
</asp:Content>

