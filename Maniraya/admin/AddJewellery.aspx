<%@ Page Title="Add Jewellery" Language="C#" MasterPageFile="adminmaster.master" AutoEventWireup="true" CodeFile="AddJewellery.aspx.cs" Inherits="AddJewellery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .jewellery-rate-card {
            border: 1px solid #e8edf3;
            border-radius: 10px;
            padding: 12px 14px;
            margin-bottom: 10px;
            background: #fafbfd;
        }
        .jewellery-rate-card.gold { border-left: 4px solid #e5a906; }
        .jewellery-rate-card.silver { border-left: 4px solid #94a3b8; }
        .jewellery-rate-card.diamond { border-left: 4px solid #38bdf8; }
        .jewellery-rate-card strong { display: block; color: #1e293b; }
        .jewellery-rate-card span { font-size: 12px; color: #64748b; }
        .jewellery-breakdown {
            background: #f8fafc;
            border: 1px solid #e2e8f0;
            border-radius: 10px;
            padding: 14px 16px;
        }
        .jewellery-breakdown-row {
            display: flex;
            justify-content: space-between;
            padding: 6px 0;
            border-bottom: 1px dashed #e2e8f0;
            font-size: 13px;
        }
        .jewellery-breakdown-row:last-child { border-bottom: 0; }
        .jewellery-breakdown-row.total {
            font-weight: 700;
            font-size: 15px;
            color: #0f172a;
            padding-top: 10px;
        }
        .jewellery-breakdown-label { color: #64748b; }
        .jewellery-breakdown-value { color: #0f172a; font-weight: 600; }
        .jewellery-price-readonly {
            background: #f1f5f9 !important;
            font-weight: 700;
            color: #0f172a;
        }
        .jewellery-metal-field { display: none; }
        .jewellery-metal-field.is-visible { display: block; }
        .jewellery-inline-link {
            font-size: 12px;
            margin-left: 8px;
            white-space: nowrap;
        }
        .jewellery-size-panel {
            border: 1px solid #e2e8f0;
            border-radius: 10px;
            padding: 14px;
            background: #fafbfd;
        }
        .jewellery-size-checkboxes {
            display: flex;
            flex-wrap: wrap;
            gap: 10px 18px;
            margin-bottom: 14px;
            max-height: 180px;
            overflow-y: auto;
        }
        .jewellery-size-checkboxes table {
            border: 0;
            width: 100%;
        }
        .jewellery-size-checkboxes label {
            font-weight: 500;
            margin: 0;
            cursor: pointer;
        }
        .jewellery-size-add-row {
            display: flex;
            gap: 10px;
            align-items: flex-start;
            flex-wrap: wrap;
        }
        .jewellery-size-add-row .form-control {
            flex: 1;
            min-width: 200px;
        }
    </style>
    <script type="text/javascript">
        function parseJewelleryNum(val) {
            var n = parseFloat(val);
            return isNaN(n) ? 0 : n;
        }

        function formatJewelleryMoney(val) {
            return '₹ ' + parseJewelleryNum(val).toFixed(2);
        }

        function getSelectedMetalType() {
            var ddl = document.getElementById('<%= ddMetalType.ClientID %>');
            return ddl ? ddl.value : '0';
        }

        function metalNeedsGold(metal) {
            return metal === 'Gold' || metal === 'GoldDiamond' || metal === 'GoldSilver' || metal === 'GoldSilverDiamond';
        }

        function metalNeedsSilver(metal) {
            return metal === 'Silver' || metal === 'GoldSilver' || metal === 'SilverDiamond' || metal === 'GoldSilverDiamond';
        }

        function metalNeedsDiamond(metal) {
            return metal === 'Diamond' || metal === 'GoldDiamond' || metal === 'SilverDiamond' || metal === 'GoldSilverDiamond';
        }

        function toggleMetalFields() {
            var metal = getSelectedMetalType();
            var showGold = metalNeedsGold(metal);
            var showSilver = metalNeedsSilver(metal);
            var showDiamond = metalNeedsDiamond(metal);

            setMetalFieldVisible('jewelleryGoldField', showGold);
            setMetalFieldVisible('jewellerySilverField', showSilver);
            setMetalFieldVisible('jewelleryDiamondField', showDiamond);

            if (!showGold) {
                document.getElementById('<%= txtGoldWeight.ClientID %>').value = '0';
            }
            if (!showSilver) {
                document.getElementById('<%= txtSilverWeight.ClientID %>').value = '0';
            }
            if (!showDiamond) {
                document.getElementById('<%= txtDiamondCarat.ClientID %>').value = '0';
            }

            document.getElementById('jewelleryBreakdownGold').style.display = showGold ? '' : 'none';
            document.getElementById('jewelleryBreakdownSilver').style.display = showSilver ? '' : 'none';
            document.getElementById('jewelleryBreakdownDiamond').style.display = showDiamond ? '' : 'none';

            recalculateJewelleryPrices();
        }

        function setMetalFieldVisible(id, visible) {
            var el = document.getElementById(id);
            if (!el) {
                return;
            }
            if (visible) {
                el.classList.add('is-visible');
            } else {
                el.classList.remove('is-visible');
            }
        }

        function recalculateJewelleryPrices() {
            var metal = getSelectedMetalType();
            var goldRate = parseJewelleryNum(document.getElementById('<%= hfGoldRate.ClientID %>').value);
            var silverRate = parseJewelleryNum(document.getElementById('<%= hfSilverRate.ClientID %>').value);
            var diamondRate = parseJewelleryNum(document.getElementById('<%= hfDiamondRate.ClientID %>').value);
            var markup = parseJewelleryNum(document.getElementById('<%= hfMrpMarkup.ClientID %>').value);

            var goldWeight = metalNeedsGold(metal) ? parseJewelleryNum(document.getElementById('<%= txtGoldWeight.ClientID %>').value) : 0;
            var silverWeight = metalNeedsSilver(metal) ? parseJewelleryNum(document.getElementById('<%= txtSilverWeight.ClientID %>').value) : 0;
            var diamondCarat = metalNeedsDiamond(metal) ? parseJewelleryNum(document.getElementById('<%= txtDiamondCarat.ClientID %>').value) : 0;
            var makingCharges = parseJewelleryNum(document.getElementById('<%= txtMakingCharges.ClientID %>').value);
            var gstPercent = parseJewelleryNum(document.getElementById('<%= txtGstPercent.ClientID %>').value);

            var goldAmount = goldWeight * goldRate;
            var silverAmount = silverWeight * silverRate;
            var diamondAmount = diamondCarat * diamondRate;
            var subtotal = goldAmount + silverAmount + diamondAmount + makingCharges;
            var gstAmount = subtotal * gstPercent / 100;
            var price = subtotal + gstAmount;
            var mrp = Math.ceil(price * (1 + markup / 100) / 10) * 10;

            document.getElementById('lblGoldAmount').innerText = formatJewelleryMoney(goldAmount);
            document.getElementById('lblSilverAmount').innerText = formatJewelleryMoney(silverAmount);
            document.getElementById('lblDiamondAmount').innerText = formatJewelleryMoney(diamondAmount);
            document.getElementById('lblSubtotal').innerText = formatJewelleryMoney(subtotal);
            document.getElementById('lblGstAmount').innerText = formatJewelleryMoney(gstAmount);
            document.getElementById('<%= txtPrice.ClientID %>').value = price.toFixed(2);
            document.getElementById('<%= txtMRP.ClientID %>').value = mrp.toFixed(2);
        }

        function hasSelectedJewellerySize() {
            var container = document.getElementById('<%= cblSizes.ClientID %>');
            if (!container) {
                return false;
            }
            var boxes = container.getElementsByTagName('input');
            for (var i = 0; i < boxes.length; i++) {
                if (boxes[i].type === 'checkbox' && boxes[i].checked) {
                    return true;
                }
            }
            return false;
        }

        function validateJewellery() {
            if (document.getElementById('<%= txtTitle.ClientID %>').value.trim() === '') {
                alert('Enter jewellery title');
                return false;
            }
            if (document.getElementById('<%= ddMetalType.ClientID %>').value === '0') {
                alert('Select metal type');
                return false;
            }
            if (document.getElementById('<%= ddJewelleryType.ClientID %>').value === '0') {
                alert('Select jewellery type');
                return false;
            }
            if (!hasSelectedJewellerySize()) {
                alert('Select at least one size, or add new sizes on this page');
                return false;
            }
            if (document.getElementById('<%= txtBV.ClientID %>').value.trim() === '') {
                alert('Enter Business Volume (BV)');
                return false;
            }
            toggleMetalFields();
            recalculateJewelleryPrices();
            return true;
        }

        function bindJewelleryCalcEvents() {
            var ddlMetal = document.getElementById('<%= ddMetalType.ClientID %>');
            if (ddlMetal) {
                ddlMetal.onchange = toggleMetalFields;
            }
            var ids = [
                '<%= txtGoldWeight.ClientID %>',
                '<%= txtSilverWeight.ClientID %>',
                '<%= txtDiamondCarat.ClientID %>',
                '<%= txtMakingCharges.ClientID %>',
                '<%= txtGstPercent.ClientID %>'
            ];
            for (var i = 0; i < ids.length; i++) {
                var el = document.getElementById(ids[i]);
                if (el) {
                    el.oninput = recalculateJewelleryPrices;
                    el.onchange = recalculateJewelleryPrices;
                }
            }
            recalculateJewelleryPrices();
            toggleMetalFields();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPageHeading" Runat="Server">
    <section class="content-header">
        <h1>Add Jewellery</h1>
        <ol class="breadcrumb">
            <li><a href="Dashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li><a href="#">Product Management</a></li>
            <li class="active">Add Jewellery</li>
        </ol>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentpageData" Runat="Server">
    <asp:HiddenField ID="hfGoldRate" runat="server" />
    <asp:HiddenField ID="hfSilverRate" runat="server" />
    <asp:HiddenField ID="hfDiamondRate" runat="server" />
    <asp:HiddenField ID="hfMrpMarkup" runat="server" />

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row" style="margin-bottom: 16px;">
                <div class="col-md-12">
                    <div class="box box-solid" style="margin-bottom: 0;">
                        <div class="box-body text-center">
                            <a href="MetalPriceMaster.aspx" class="btn btn-default"><i class="fa fa-line-chart"></i> Metal Prices</a>
                            <a href="AddJewellery.aspx" class="btn btn-primary"><i class="fa fa-diamond"></i> Add Jewellery</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Jewellery Details</h3>
                        </div>
                        <div class="box-body admin-product-form">
                            <p class="admin-section-hint">Add jewellery with composition (gold, silver, diamond), making charges and GST. Price and MRP are calculated from current metal rates in the database.</p>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-diamond"></i> Basic Information</h5>
                                <div class="row">
                                    <div class="col-md-8 col-sm-12">
                                        <div class="form-group">
                                            <label for="<%= txtTitle.ClientID %>">Title</label>
                                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="e.g. 22K Diamond Gold Ring" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtHSN.ClientID %>">HSN Code</label>
                                            <asp:TextBox ID="txtHSN" runat="server" CssClass="form-control" placeholder="e.g. 7113" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= ddMetalType.ClientID %>">Metal Type</label>
                                            <asp:DropDownList ID="ddMetalType" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= ddJewelleryType.ClientID %>">Jewellery Type</label>
                                            <asp:DropDownList ID="ddJewelleryType" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Sizes (select multiple)</label>
                                            <div class="jewellery-size-panel">
                                                <p class="admin-section-hint" style="margin-top:0;">Check all sizes available for this jewellery. Add new sizes below without leaving this page.</p>
                                                <div class="jewellery-size-checkboxes">
                                                    <asp:CheckBoxList ID="cblSizes" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" />
                                                </div>
                                                <div class="jewellery-size-add-row">
                                                    <asp:TextBox ID="txtNewSize" runat="server" CssClass="form-control" placeholder="Add size: 12  or  multiple: 12, 14, 16" />
                                                    <asp:Button ID="btnAddSize" runat="server" Text="Add Size" CssClass="btn btn-default" OnClick="btnAddSize_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="<%= txtShortDescription.ClientID %>">Short Description</label>
                                            <asp:TextBox ID="txtShortDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Brief summary for listings" />
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="<%= txtDescription.ClientID %>">Description</label>
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Full product description" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-balance-scale"></i> Composition &amp; Charges</h5>
                                <p class="admin-section-hint">Select metal type first. Only relevant weight fields will appear (Gold/Silver in grams, Diamond in carat).</p>
                                <div class="row">
                                    <div class="col-md-4 col-sm-6 jewellery-metal-field" id="jewelleryGoldField">
                                        <div class="form-group">
                                            <label for="<%= txtGoldWeight.ClientID %>">Gold Weight (gram)</label>
                                            <asp:TextBox ID="txtGoldWeight" runat="server" CssClass="form-control" TextMode="Number" step="0.001" placeholder="0.000" Text="0" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6 jewellery-metal-field" id="jewellerySilverField">
                                        <div class="form-group">
                                            <label for="<%= txtSilverWeight.ClientID %>">Silver Weight (gram)</label>
                                            <asp:TextBox ID="txtSilverWeight" runat="server" CssClass="form-control" TextMode="Number" step="0.001" placeholder="0.000" Text="0" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6 jewellery-metal-field" id="jewelleryDiamondField">
                                        <div class="form-group">
                                            <label for="<%= txtDiamondCarat.ClientID %>">Diamond (carat)</label>
                                            <asp:TextBox ID="txtDiamondCarat" runat="server" CssClass="form-control" TextMode="Number" step="0.001" placeholder="0.000" Text="0" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtMakingCharges.ClientID %>">Making Charges (₹)</label>
                                            <asp:TextBox ID="txtMakingCharges" runat="server" CssClass="form-control" TextMode="Number" step="0.01" placeholder="0.00" Text="0" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtGstPercent.ClientID %>">GST (%)</label>
                                            <asp:TextBox ID="txtGstPercent" runat="server" CssClass="form-control" TextMode="Number" step="0.01" placeholder="e.g. 3" Text="3" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="admin-form-section">
                                <h5 class="admin-form-section-title"><i class="fa fa-inr"></i> Pricing &amp; Volume</h5>
                                <div class="row">
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtPrice.ClientID %>">Price (₹) <small class="text-muted">Auto</small></label>
                                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control jewellery-price-readonly" ReadOnly="true" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtMRP.ClientID %>">MRP (₹) <small class="text-muted">Auto</small></label>
                                            <asp:TextBox ID="txtMRP" runat="server" CssClass="form-control jewellery-price-readonly" ReadOnly="true" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6">
                                        <div class="form-group">
                                            <label for="<%= txtBV.ClientID %>">Business Volume (BV)</label>
                                            <asp:TextBox ID="txtBV" runat="server" CssClass="form-control" TextMode="Number" step="0.01" placeholder="Enter BV manually" />
                                        </div>
                                    </div>
                                </div>
                                <p class="admin-section-hint"><asp:Literal ID="litPriceHint" runat="server" /></p>
                            </div>

                            <div class="admin-form-section admin-form-section-last">
                                <h5 class="admin-form-section-title"><i class="fa fa-picture-o"></i> Product Images</h5>
                                <p class="admin-section-hint">Upload 4 images. Image 1 is the primary photo.</p>
                                <div class="admin-product-images-grid">
                                    <div class="admin-product-image-slot is-primary">
                                        <div class="admin-product-image-slot-head">
                                            <p class="admin-product-image-slot-title">Image 1</p>
                                            <span class="admin-product-image-slot-badge">Primary</span>
                                        </div>
                                        <asp:FileUpload ID="fuImage1" runat="server" CssClass="form-control" accept="image/*" />
                                    </div>
                                    <div class="admin-product-image-slot">
                                        <div class="admin-product-image-slot-head">
                                            <p class="admin-product-image-slot-title">Image 2</p>
                                        </div>
                                        <asp:FileUpload ID="fuImage2" runat="server" CssClass="form-control" accept="image/*" />
                                    </div>
                                    <div class="admin-product-image-slot">
                                        <div class="admin-product-image-slot-head">
                                            <p class="admin-product-image-slot-title">Image 3</p>
                                        </div>
                                        <asp:FileUpload ID="fuImage3" runat="server" CssClass="form-control" accept="image/*" />
                                    </div>
                                    <div class="admin-product-image-slot">
                                        <div class="admin-product-image-slot-head">
                                            <p class="admin-product-image-slot-title">Image 4</p>
                                        </div>
                                        <asp:FileUpload ID="fuImage4" runat="server" CssClass="form-control" accept="image/*" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Save Jewellery" CssClass="btn btn-primary" OnClientClick="return validateJewellery();" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Current Metal Rates</h3>
                        </div>
                        <div class="box-body">
                            <div class="jewellery-rate-card gold">
                                <strong>Gold</strong>
                                <span>₹ <asp:Literal ID="litGoldRate" runat="server" /> / gram</span>
                            </div>
                            <div class="jewellery-rate-card silver">
                                <strong>Silver</strong>
                                <span>₹ <asp:Literal ID="litSilverRate" runat="server" /> / gram</span>
                            </div>
                            <div class="jewellery-rate-card diamond">
                                <strong>Diamond</strong>
                                <span>₹ <asp:Literal ID="litDiamondRate" runat="server" /> / carat</span>
                            </div>
                            <p class="admin-section-hint">Rates from <a href="MetalPriceMaster.aspx">Add Commodities Price</a>. Update there when market rates change.</p>
                        </div>
                    </div>

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Price Breakdown</h3>
                        </div>
                        <div class="box-body">
                            <div class="jewellery-breakdown">
                                <div class="jewellery-breakdown-row" id="jewelleryBreakdownGold">
                                    <span class="jewellery-breakdown-label">Gold Amount</span>
                                    <span class="jewellery-breakdown-value" id="lblGoldAmount">₹ 0.00</span>
                                </div>
                                <div class="jewellery-breakdown-row" id="jewelleryBreakdownSilver">
                                    <span class="jewellery-breakdown-label">Silver Amount</span>
                                    <span class="jewellery-breakdown-value" id="lblSilverAmount">₹ 0.00</span>
                                </div>
                                <div class="jewellery-breakdown-row" id="jewelleryBreakdownDiamond">
                                    <span class="jewellery-breakdown-label">Diamond Amount</span>
                                    <span class="jewellery-breakdown-value" id="lblDiamondAmount">₹ 0.00</span>
                                </div>
                                <div class="jewellery-breakdown-row">
                                    <span class="jewellery-breakdown-label">Subtotal (incl. making)</span>
                                    <span class="jewellery-breakdown-value" id="lblSubtotal">₹ 0.00</span>
                                </div>
                                <div class="jewellery-breakdown-row">
                                    <span class="jewellery-breakdown-label">GST Amount</span>
                                    <span class="jewellery-breakdown-value" id="lblGstAmount">₹ 0.00</span>
                                </div>
                                <div class="jewellery-breakdown-row total">
                                    <span class="jewellery-breakdown-label">Calculated Price</span>
                                    <span class="jewellery-breakdown-value" id="lblCalculatedPrice">See Price field</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentScript" Runat="Server">
    <script type="text/javascript">
        Sys.Application.add_load(function () {
            bindJewelleryCalcEvents();
        });
    </script>
</asp:Content>
