<%@ Page Title="" Language="C#" MasterPageFile="~/user/WebsiteMaster.master" AutoEventWireup="true" CodeFile="Gallery.aspx.cs" Inherits="user_Gallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

         <section class="col-sm-12 banner-content text-center" data-background="new_assets/images/bg/banner-bg.jpg">
            <div class="container">
                <div class="row">
                    <div class="banner-main">
                        <h1>Image Gallery</h1>
                        <h6>elaborated code and creative design</h6>
                        <p>Duis sed odio sit amet nibh vulputate cursus a sit amet mauris. Morbi accumsan ipsuy veli. Nam nec tellus a odio tincidunt auctor </p>
                    </div>
                </div>
            </div>
        </section>
   

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <section class="products space">
        <div class="container">
            <div class="row">
                 <%for(int i=0;i<dt.Rows.Count;i++){ %>
                <div class="product-base no-padding col-md-4" style="float:left;">
                    <div class="col-md-12 product-block">
                        <div class="product-inner text-center">
                            <div class="product-image">
                                <img style="height:250px; width:400px;" src="<%=dt.Rows[i]["Img_Url"].ToString().Replace("~/user/","") %>" alt="Products">
                            </div>
                            <div class="product-text">     
                                <h5><%=dt.Rows[i]["Title"].ToString() %></h5>    
                            </div>
                        </div>
                    </div>
                </div>
                <%} %>

            </div>
        </div>
    </section>

    </asp:Content>