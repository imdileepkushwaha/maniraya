<%@ Page Title="Contact Settings" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="ContactAdd.aspx.cs" Inherits="ContactAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Contact Settings</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Website Management</a></li>
            <li class="active">Contact Settings</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-primary contact-settings-page">
                <div class="box-header with-border">
                    <h3 class="box-title"><i class="fa fa-cog"></i> Website &amp; Invoice Settings</h3>
                </div>
                <div class="box-body">
                    <div class="contact-settings-tabbar">
                        <ul class="nav nav-tabs contact-settings-tabs" role="tablist">
                            <li class="active" role="presentation">
                                <a href="#tabContact" data-toggle="tab" role="tab">
                                    <span class="contact-settings-tab-icon is-contact"><i class="fa fa-phone"></i></span>
                                    <span class="contact-settings-tab-text">
                                        <strong>Add Contact</strong>
                                        <small>Phone, email, address, website</small>
                                    </span>
                                </a>
                            </li>
                            <li role="presentation">
                                <a href="#tabGst" data-toggle="tab" role="tab">
                                    <span class="contact-settings-tab-icon is-gst"><i class="fa fa-file-text-o"></i></span>
                                    <span class="contact-settings-tab-text">
                                        <strong>Add GST</strong>
                                        <small>Company GSTIN for invoices</small>
                                    </span>
                                </a>
                            </li>
                            <li role="presentation">
                                <a href="#tabSign" data-toggle="tab" role="tab">
                                    <span class="contact-settings-tab-icon is-sign"><i class="fa fa-pencil"></i></span>
                                    <span class="contact-settings-tab-text">
                                        <strong>Add Sign</strong>
                                        <small>Authorised signature image</small>
                                    </span>
                                </a>
                            </li>
                        </ul>
                    </div>

                    <div class="tab-content contact-settings-tab-content">
                        <div class="tab-pane active" id="tabContact" role="tabpanel">
                            <div class="row">
                                <div class="col-md-5 col-sm-6">
                                    <div class="box box-primary contact-settings-inner-box">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Add / Update Contact</h3>
                                        </div>
                                        <div class="box-body">
                                            <p class="admin-section-hint">Manage phone, email, address, and website shown on the public website, footer, and contact page.</p>
                                            <div class="form-group">
                                                <label for="<%= ddlContactType.ClientID %>">Contact Type</label>
                                                <asp:DropDownList ID="ddlContactType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="Phone">Phone</asp:ListItem>
                                                    <asp:ListItem Value="Email">Email</asp:ListItem>
                                                    <asp:ListItem Value="Address">Address</asp:ListItem>
                                                    <asp:ListItem Value="Website">Website</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label for="<%= txtTitle.ClientID %>">Title / Label</label>
                                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="e.g. Customer Support, Head Office"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="<%= txtContactValue.ClientID %>">Value</label>
                                                <asp:TextBox ID="txtContactValue" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Phone number, email, address, or website"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="<%= txtDisplayOrder.ClientID %>">Display Order</label>
                                                <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="form-control" Text="1" TextMode="Number"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="<%= chkPrimary.ClientID %>">Primary (shown on header/footer)</label>
                                                <asp:CheckBox ID="chkPrimary" runat="server" />
                                            </div>
                                            <div class="form-group">
                                                <label for="<%= chkStatus.ClientID %>">Status (Active)</label>
                                                <asp:CheckBox ID="chkStatus" runat="server" Checked="true" />
                                            </div>
                                        </div>
                                        <div class="box-footer">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-7 col-sm-6">
                                    <div class="box box-primary contact-settings-inner-box">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Contact List</h3>
                                        </div>
                                        <div class="box-body table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover dataTable" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ContactType" HeaderText="Type" />
                                                    <asp:BoundField DataField="Title" HeaderText="Title" />
                                                    <asp:TemplateField HeaderText="Value">
                                                        <ItemTemplate>
                                                            <span style="word-break: break-word;"><%# Eval("ContactValue") %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DisplayOrder" HeaderText="Order" />
                                                    <asp:TemplateField HeaderText="Primary">
                                                        <ItemTemplate><%# Convert.ToBoolean(Eval("IsPrimary")) ? "Yes" : "No" %></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate><%# Convert.ToBoolean(Eval("Status")) ? "Active" : "Inactive" %></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lnkEdit_Click" CssClass="admin-grid-edit-btn" ToolTip="Edit"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lnkDelete_Click" CssClass="admin-grid-delete-btn" OnClientClick="return confirm('Are you sure to delete this contact?');" ToolTip="Delete"><i class="icon fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane" id="tabGst" role="tabpanel">
                            <div class="row">
                                <div class="col-md-5 col-sm-6">
                                    <div class="box box-success contact-settings-inner-box">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Company GST Settings</h3>
                                        </div>
                                        <div class="box-body">
                                            <p class="admin-section-hint">Manage the company GSTIN shown on tax invoices. Mark one GST number as primary for invoice printing.</p>
                                            <div class="form-group">
                                                <label for="<%= txtGstTitle.ClientID %>">Label</label>
                                                <asp:TextBox ID="txtGstTitle" runat="server" CssClass="form-control" placeholder="e.g. Company GSTIN"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="<%= txtGstNumber.ClientID %>">GST Number</label>
                                                <asp:TextBox ID="txtGstNumber" runat="server" CssClass="form-control" placeholder="e.g. 29AARCM8049H1ZQ" MaxLength="15"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="<%= txtGstDisplayOrder.ClientID %>">Display Order</label>
                                                <asp:TextBox ID="txtGstDisplayOrder" runat="server" CssClass="form-control" Text="1" TextMode="Number"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="<%= chkGstPrimary.ClientID %>">Primary (shown on invoices)</label>
                                                <asp:CheckBox ID="chkGstPrimary" runat="server" Checked="true" />
                                            </div>
                                            <div class="form-group">
                                                <label for="<%= chkGstStatus.ClientID %>">Status (Active)</label>
                                                <asp:CheckBox ID="chkGstStatus" runat="server" Checked="true" />
                                            </div>
                                        </div>
                                        <div class="box-footer">
                                            <asp:Button ID="btnGstSubmit" runat="server" Text="Save GST" CssClass="btn btn-success" OnClick="btnGstSubmit_Click" />
                                            <asp:Button ID="btnGstCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnGstCancel_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-7 col-sm-6">
                                    <div class="box box-success contact-settings-inner-box">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">GST Number List</h3>
                                        </div>
                                        <div class="box-body table-responsive">
                                            <asp:GridView ID="gvGst" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover dataTable" Width="100%" EmptyDataText="No GST number added yet.">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Title" HeaderText="Label" />
                                                    <asp:TemplateField HeaderText="GST Number">
                                                        <ItemTemplate>
                                                            <span style="word-break: break-word; font-weight: 600;"><%# Eval("ContactValue") %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DisplayOrder" HeaderText="Order" />
                                                    <asp:TemplateField HeaderText="Primary">
                                                        <ItemTemplate><%# Convert.ToBoolean(Eval("IsPrimary")) ? "Yes" : "No" %></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate><%# Convert.ToBoolean(Eval("Status")) ? "Active" : "Inactive" %></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkGstEdit" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lnkGstEdit_Click" CssClass="admin-grid-edit-btn" ToolTip="Edit"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkGstDelete" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lnkGstDelete_Click" CssClass="admin-grid-delete-btn" OnClientClick="return confirm('Are you sure to delete this GST number?');" ToolTip="Delete"><i class="icon fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane" id="tabSign" role="tabpanel">
                            <div class="row">
                                <div class="col-md-5 col-sm-6">
                                    <div class="box box-warning contact-settings-inner-box">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Invoice Signature Settings</h3>
                                        </div>
                                        <div class="box-body">
                                            <p class="admin-section-hint">Upload the authorised signatory image shown on tax invoices. Use a clear PNG/JPG signature on white background.</p>
                                            <asp:HiddenField ID="hfSignImage" runat="server" />
                                            <div class="form-group">
                                                <label for="<%= txtSignTitle.ClientID %>">Label</label>
                                                <asp:TextBox ID="txtSignTitle" runat="server" CssClass="form-control" placeholder="e.g. Authorised Signatory"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="<%= fuSignImage.ClientID %>">Signature Image</label>
                                                <asp:FileUpload ID="fuSignImage" runat="server" CssClass="form-control" accept="image/*" />
                                                <p class="help-block" style="margin-top: 6px;">Allowed: JPG, PNG, WEBP, GIF. Recommended size: 300 x 120 px.</p>
                                            </div>
                                            <div class="form-group">
                                                <label>Current Preview</label>
                                                <div>
                                                    <asp:Image ID="imgSignPreview" runat="server" Style="max-height: 90px; max-width: 220px; border: 1px solid #e2e8f0; padding: 6px; background: #fff;" Visible="false" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="<%= txtSignDisplayOrder.ClientID %>">Display Order</label>
                                                <asp:TextBox ID="txtSignDisplayOrder" runat="server" CssClass="form-control" Text="1" TextMode="Number"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="<%= chkSignPrimary.ClientID %>">Primary (shown on invoices)</label>
                                                <asp:CheckBox ID="chkSignPrimary" runat="server" Checked="true" />
                                            </div>
                                            <div class="form-group">
                                                <label for="<%= chkSignStatus.ClientID %>">Status (Active)</label>
                                                <asp:CheckBox ID="chkSignStatus" runat="server" Checked="true" />
                                            </div>
                                        </div>
                                        <div class="box-footer">
                                            <asp:Button ID="btnSignSubmit" runat="server" Text="Save Signature" CssClass="btn btn-warning" OnClick="btnSignSubmit_Click" />
                                            <asp:Button ID="btnSignCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnSignCancel_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-7 col-sm-6">
                                    <div class="box box-warning contact-settings-inner-box">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Signature List</h3>
                                        </div>
                                        <div class="box-body table-responsive">
                                            <asp:GridView ID="gvSign" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover dataTable" Width="100%" EmptyDataText="No signature uploaded yet.">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Title" HeaderText="Label" />
                                                    <asp:TemplateField HeaderText="Preview">
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgSignThumb" runat="server" ImageUrl='<%# "~/InvoiceSign/" + Eval("ContactValue") %>' Style="max-height: 50px; max-width: 140px;" AlternateText="Signature preview" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DisplayOrder" HeaderText="Order" />
                                                    <asp:TemplateField HeaderText="Primary">
                                                        <ItemTemplate><%# Convert.ToBoolean(Eval("IsPrimary")) ? "Yes" : "No" %></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate><%# Convert.ToBoolean(Eval("Status")) ? "Active" : "Inactive" %></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSignEdit" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lnkSignEdit_Click" CssClass="admin-grid-edit-btn" ToolTip="Edit"><i class="icon fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkSignDelete" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lnkSignDelete_Click" CssClass="admin-grid-delete-btn" OnClientClick="return confirm('Are you sure to delete this signature?');" ToolTip="Delete"><i class="icon fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSignSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
    <script type="text/javascript">
        function activateContactTab(tabId) {
            if (!tabId) {
                return;
            }

            var $tabLink = $('a[href="' + tabId + '"]');
            if ($tabLink.length) {
                $tabLink.tab('show');
            }
        }

        Sys.Application.add_load(function () {
            var activeTab = '<%= ActiveTabHref %>';
            activateContactTab(activeTab);
        });
    </script>
</asp:Content>
