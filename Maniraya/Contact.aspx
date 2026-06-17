<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/WebMasterPage.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .contact-hero {
            background-color: #f8fafc;
            padding: 60px 0;
            text-align: center;
            border-bottom: 1px solid #e2e8f0;
        }
        .contact-hero h1 {
            font-size: 36px;
            font-weight: 700;
            color: #1e293b;
            margin-bottom: 15px;
        }
        .contact-hero p {
            font-size: 18px;
            color: #64748b;
            max-width: 600px;
            margin: 0 auto;
        }
        .contact-section {
            padding: 80px 0;
        }
        .contact-info-card {
            background: #fff;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06);
            height: 100%;
            text-align: center;
        }
        .contact-icon {
            width: 60px;
            height: 60px;
            background: #e0e7ff;
            color: #4f46e5;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 24px;
            margin: 0 auto 20px;
        }
        .contact-info-card h3 {
            font-size: 20px;
            font-weight: 600;
            margin-bottom: 15px;
            color: #1e293b;
        }
        .contact-info-card p {
            color: #475569;
            margin-bottom: 5px;
        }
        .contact-form-wrap {
            background: #fff;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05);
        }
        .form-group label {
            font-weight: 600;
            color: #334155;
            margin-bottom: 8px;
            display: block;
        }
        .form-control {
            border: 1px solid #cbd5e1;
            border-radius: 8px;
            padding: 12px 16px;
            font-size: 16px;
            transition: all 0.3s ease;
        }
        .form-control:focus {
            border-color: #4f46e5;
            box-shadow: 0 0 0 3px rgba(79, 70, 229, 0.1);
        }
        .btn-submit {
            background: #4f46e5;
            color: white;
            font-weight: 600;
            padding: 14px 28px;
            border-radius: 8px;
            border: none;
            width: 100%;
            font-size: 16px;
            transition: all 0.3s ease;
        }
        .btn-submit:hover {
            background: #4338ca;
            transform: translateY(-1px);
        }
        .map-container {
            border-radius: 12px;
            overflow: hidden;
            box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1);
            margin-top: 40px;
            height: 400px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="contact-hero">
        <div class="container">
            <h1>Get in Touch</h1>
            <p>We'd love to hear from you. Please fill out the form below or reach out to us using our contact details.</p>
        </div>
    </div>

    <section class="contact-section">
        <div class="container">
            <div class="row" style="margin-bottom: 50px;">
                <div class="col-md-4 mb-4">
                    <div class="contact-info-card">
                        <div class="contact-icon">
                            <i class="fa fa-map-marker"></i>
                        </div>
                        <h3>Our Location</h3>
                        <p>123 Business Avenue</p>
                        <p>New Delhi, India 110001</p>
                    </div>
                </div>
                <div class="col-md-4 mb-4">
                    <div class="contact-info-card">
                        <div class="contact-icon">
                            <i class="fa fa-phone"></i>
                        </div>
                        <h3>Call Us</h3>
                        <p>+91 888 444 8586</p>
                        <p>Mon - Fri, 9am - 6pm</p>
                    </div>
                </div>
                <div class="col-md-4 mb-4">
                    <div class="contact-info-card">
                        <div class="contact-icon">
                            <i class="fa fa-envelope"></i>
                        </div>
                        <h3>Email Us</h3>
                        <p>info@mpremium.in</p>
                        <p>support@mpremium.in</p>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-8 col-md-offset-2">
                    <div class="contact-form-wrap">
                        <h3 style="font-weight: 700; margin-bottom: 30px; text-align: center;">Send us a Message</h3>
                        
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Your Name</label>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="John Doe"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Your Email</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="john@example.com"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label>Subject</label>
                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" placeholder="How can we help?"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>Message</label>
                            <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" placeholder="Write your message here..."></asp:TextBox>
                        </div>

                        <div style="margin-top: 30px;">
                            <asp:Button ID="btnSubmit" runat="server" Text="Send Message" CssClass="btn-submit" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="map-container">
                        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d224346.61368048706!2d77.06889969033324!3d28.52758200617607!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x390cfd5b347eb62d%3A0x52c2b7494e204dce!2sNew%20Delhi%2C%20Delhi!5e0!3m2!1sen!2sin!4v1700000000000!5m2!1sen!2sin" width="100%" height="100%" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
