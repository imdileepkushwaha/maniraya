<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/WebMasterPage.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="page-header">
        <div class="container">
            <h1 class="page-title">Contact Us</h1>
            <p>Questions about orders, products, or your account? Our team is ready to help you.</p>
        </div>
    </div>

    <section class="contact-page">
        <div class="container contact-page-inner">
            <div class="contact-layout">
                <aside class="contact-aside" aria-label="Contact information">
                    <div class="contact-aside-intro">
                        <span class="contact-eyebrow">We're here for you</span>
                        <h2>Let's start a conversation</h2>
                        <p>Reach out by phone, email, or WhatsApp — we're happy to help.</p>
                    </div>

                    <div class="contact-channel-list">
                        <a id="lnkContactPhone" runat="server" href="tel:+918884448586" class="contact-channel-card">
                            <span class="contact-channel-icon contact-channel-icon--phone" aria-hidden="true">
                                <svg width="22" height="22" viewBox="0 0 24 24" fill="none"><path d="M6.6 3H8.4L9.2 7.8L7.6 8.8C8.5 11.1 9.9 13.5 12.2 15.8L13.2 14.2L18 15V16.8C18 17.5 17.5 18 16.8 18.1C10.1 18.8 5.2 13.9 5.9 7.2C6 6.5 6.5 6 6.6 3Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/></svg>
                            </span>
                            <span class="contact-channel-body">
                                <strong>Call us</strong>
                                <span id="lblContactPhone" runat="server">+91 888 444 8586</span>
                                <small>Mon – Sat, 9:00 AM – 6:00 PM</small>
                            </span>
                        </a>

                        <a id="lnkContactEmail" runat="server" href="mailto:info@mpremium.in" class="contact-channel-card">
                            <span class="contact-channel-icon contact-channel-icon--email" aria-hidden="true">
                                <svg width="22" height="22" viewBox="0 0 24 24" fill="none"><path d="M4 6H20V18H4V6ZM4 8L12 13L20 8" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/></svg>
                            </span>
                            <span class="contact-channel-body">
                                <strong>Email us</strong>
                                <span id="lblContactEmail" runat="server">info@mpremium.in</span>
                                <small>We reply within 24 hours</small>
                            </span>
                        </a>

                        <div class="contact-channel-card contact-channel-card--static">
                            <span class="contact-channel-icon contact-channel-icon--location" aria-hidden="true">
                                <svg width="22" height="22" viewBox="0 0 24 24" fill="none"><path d="M12 21C12 21 19 14.5 19 9.5C19 6.46 16.54 4 13.5 4C10.46 4 8 6.46 8 9.5C8 14.5 12 21 12 21Z" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/><circle cx="12" cy="9.5" r="2.5" stroke="currentColor" stroke-width="1.8"/></svg>
                            </span>
                            <span class="contact-channel-body">
                                <strong>Office</strong>
                                <span id="lblContactAddress" runat="server">Maniraya, New Delhi, India</span>
                                <small>Pan-India delivery support</small>
                            </span>
                        </div>
                    </div>

                    <a id="lnkContactWhatsApp" runat="server" href="https://wa.me/918884448586" class="contact-whatsapp-btn" target="_blank" rel="noopener">
                        <svg width="20" height="20" viewBox="0 0 24 24" fill="currentColor" aria-hidden="true"><path d="M17.472 14.382c-.297-.149-1.758-.867-2.03-.967-.273-.099-.471-.148-.67.15-.197.297-.767.966-.94 1.164-.173.199-.347.223-.644.075-.297-.15-1.255-.463-2.39-1.475-.883-.788-1.48-1.761-1.653-2.059-.173-.297-.018-.458.13-.606.134-.133.298-.347.446-.52.149-.174.198-.298.298-.497.099-.198.05-.371-.025-.52-.075-.149-.669-1.612-.916-2.207-.242-.579-.487-.5-.669-.51-.173-.008-.371-.01-.57-.01-.198 0-.52.074-.792.372-.272.297-1.04 1.016-1.04 2.479 0 1.462 1.065 2.875 1.213 3.074.149.198 2.096 3.2 5.077 4.487.709.306 1.262.489 1.694.625.712.227 1.36.195 1.871.118.571-.085 1.758-.719 2.006-1.413.248-.694.248-1.289.173-1.413-.074-.124-.272-.198-.57-.347m-5.421 7.403h-.004a9.87 9.87 0 01-5.031-1.378l-.361-.214-3.741.982.998-3.648-.235-.374a9.86 9.86 0 01-1.51-5.26c.001-5.45 4.436-9.884 9.888-9.884 2.64 0 5.122 1.03 6.988 2.898a9.825 9.825 0 012.893 6.994c-.003 5.45-4.435 9.884-9.885 9.884m8.413-18.297A11.815 11.815 0 0012.05 0C5.495 0 .16 5.335.157 11.892c0 2.096.547 4.142 1.588 5.945L.057 24l6.305-1.654a11.882 11.882 0 005.683 1.448h.005c6.554 0 11.89-5.335 11.893-11.893a11.821 11.821 0 00-3.48-8.413z"/></svg>
                        Chat on WhatsApp
                    </a>
                </aside>

                <div class="contact-visual-panel" aria-hidden="false">
                    <div class="contact-visual-frame">
                        <img
                            src="img/sidebar-cta-img.jpg"
                            alt="Friendly customer support team ready to help"
                            loading="lazy"
                            class="contact-visual-image" />
                        <span class="contact-visual-shade" aria-hidden="true"></span>
                    </div>
                    <div class="contact-visual-badge">
                        <span class="contact-visual-badge-icon" aria-hidden="true">
                            <svg width="18" height="18" viewBox="0 0 24 24" fill="none"><path d="M12 22C17.5228 22 22 17.5228 22 12C22 6.47715 17.5228 2 12 2C6.47715 2 2 6.47715 2 12C2 17.5228 6.47715 22 12 22Z" stroke="currentColor" stroke-width="1.8"/><path d="M12 7V12L15 14" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/></svg>
                        </span>
                        <div>
                            <strong>Quick response</strong>
                            <span>Mon – Sat, 9 AM – 6 PM</span>
                        </div>
                    </div>
                    <div class="contact-visual-caption">
                        <p>Your satisfaction is our priority. Connect with us anytime for orders, products, or account help.</p>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
