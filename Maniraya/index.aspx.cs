using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadCategory();
            loadProduct(1);
            BindSiteContacts();
            BindSitePopup();
        }
    }

    void BindSitePopup()
    {
        DataTable dt = SitePopupHelper.GetActivePopups();
        DataTable displayTable = BuildDisplayPopups(dt);

        if (displayTable == null || displayTable.Rows.Count == 0)
        {
            pnlSitePopup.Visible = false;
            return;
        }

        rptSitePopups.DataSource = displayTable;
        rptSitePopups.DataBind();
        pnlSitePopup.Visible = true;
        RegisterPopupScript();
    }

    static DataTable BuildDisplayPopups(DataTable source)
    {
        DataTable displayTable = new DataTable();
        displayTable.Columns.Add("Title", typeof(string));
        displayTable.Columns.Add("PopupContent", typeof(string));
        displayTable.Columns.Add("PopupImage", typeof(string));

        if (source == null)
        {
            return displayTable;
        }

        foreach (DataRow row in source.Rows)
        {
            string content = Convert.ToString(row["PopupContent"]);
            string imagePath = SitePopupHelper.GetPopupImagePath(row);
            bool hasContent = !string.IsNullOrWhiteSpace(content);
            bool hasImage = !string.IsNullOrWhiteSpace(imagePath);

            if (!hasContent && !hasImage)
            {
                continue;
            }

            displayTable.Rows.Add(
                Convert.ToString(row["Title"]),
                content,
                imagePath);
        }

        return displayTable;
    }

    protected void rptSitePopups_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        {
            return;
        }

        DataRowView row = e.Item.DataItem as DataRowView;
        if (row == null)
        {
            return;
        }

        string title = Convert.ToString(row["Title"]);
        string content = Convert.ToString(row["PopupContent"]);
        string imagePath = Convert.ToString(row["PopupImage"]);

        bool hasContent = !string.IsNullOrWhiteSpace(content);
        bool hasImage = !string.IsNullOrWhiteSpace(imagePath);
        bool hasTitle = !string.IsNullOrWhiteSpace(title);

        HtmlGenericControl popupCard = e.Item.FindControl("popupCard") as HtmlGenericControl;
        HtmlGenericControl popupTitle = e.Item.FindControl("popupTitle") as HtmlGenericControl;
        HtmlGenericControl popupMessage = e.Item.FindControl("popupMessage") as HtmlGenericControl;
        HtmlGenericControl popupImageWrap = e.Item.FindControl("popupImageWrap") as HtmlGenericControl;
        HtmlImage popupImage = e.Item.FindControl("popupImage") as HtmlImage;

        if (popupCard != null)
        {
            popupCard.Attributes["class"] = hasImage && !hasContent
                ? "site-popup site-popup--image-only"
                : "site-popup";
        }

        if (popupTitle != null)
        {
            popupTitle.Visible = hasTitle;
            popupTitle.InnerText = title;
        }

        if (popupMessage != null)
        {
            popupMessage.Visible = hasContent;
            popupMessage.InnerHtml = SitePopupHelper.FormatPopupHtml(content);
        }

        if (popupImageWrap != null)
        {
            popupImageWrap.Visible = hasImage;
        }

        if (popupImage != null && hasImage)
        {
            popupImage.Src = ResolveUrl(imagePath);
            popupImage.Alt = hasTitle ? title : "Announcement";
        }
    }

    void RegisterPopupScript()
    {
        string script = @"
(function () {
    var backdrop = document.getElementById('" + pnlSitePopup.ClientID + @"');
    if (!backdrop) {
        return;
    }

    var popupStack = backdrop.querySelector('.site-popup-stack');
    if (!popupStack) {
        return;
    }

    function getVisiblePopups() {
        return Array.prototype.filter.call(
            popupStack.querySelectorAll('.site-popup'),
            function (popup) {
                return !popup.classList.contains('is-hidden');
            }
        );
    }

    function syncLayerOrder() {
        var visiblePopups = getVisiblePopups();
        var layerOffset = 16;
        var maxHeight = 0;

        visiblePopups.forEach(function (popup, index) {
            var isTop = index === visiblePopups.length - 1;
            popup.style.zIndex = String(index + 1);
            popup.style.transform = 'translate(' + (index * layerOffset) + 'px, ' + (index * layerOffset) + 'px)';
            popup.classList.toggle('is-top-layer', isTop);

            var popupHeight = popup.offsetHeight;
            var totalHeight = popupHeight + (index * layerOffset);
            if (totalHeight > maxHeight) {
                maxHeight = totalHeight;
            }
        });

        popupStack.style.height = visiblePopups.length > 0 ? (maxHeight + 'px') : '0px';
    }

    function syncBackdropState() {
        var visiblePopups = getVisiblePopups();
        if (visiblePopups.length > 0) {
            backdrop.classList.add('is-open');
            backdrop.setAttribute('aria-hidden', 'false');
            document.body.style.overflow = 'hidden';
            syncLayerOrder();
            return;
        }

        backdrop.classList.remove('is-open');
        backdrop.setAttribute('aria-hidden', 'true');
        document.body.style.overflow = '';
        popupStack.style.height = '0px';
    }

    function closePopup(popup) {
        if (!popup) {
            return;
        }

        popup.classList.add('is-hidden');
        popup.classList.remove('is-top-layer');
        syncBackdropState();
    }

    popupStack.addEventListener('click', function (event) {
        var closeBtn = event.target.closest('.site-popup-close-btn');
        if (!closeBtn) {
            return;
        }

        event.preventDefault();
        event.stopPropagation();
        closePopup(closeBtn.closest('.site-popup'));
    });

    document.addEventListener('keydown', function (event) {
        if (event.key !== 'Escape' || !backdrop.classList.contains('is-open')) {
            return;
        }

        var visiblePopups = getVisiblePopups();
        if (visiblePopups.length > 0) {
            closePopup(visiblePopups[visiblePopups.length - 1]);
        }
    });

    window.addEventListener('load', syncLayerOrder);

    popupStack.querySelectorAll('img').forEach(function (img) {
        if (!img.complete) {
            img.addEventListener('load', syncLayerOrder);
        }
    });

    syncBackdropState();
})();";

        ClientScript.RegisterStartupScript(GetType(), "sitePopupScript", script, true);
    }

    void BindSiteContacts()
    {
        string phone = SiteContactHelper.GetPrimaryPhone();
        string email = SiteContactHelper.GetPrimaryEmail();

        if (lnkAboutContact != null)
        {
            lnkAboutContact.HRef = SiteContactHelper.BuildMailtoHref(email);
        }

        if (lnkWhatsAppFloat != null)
        {
            lnkWhatsAppFloat.HRef = SiteContactHelper.BuildWhatsAppHref(phone, "Hi Maniraya, I need help");
        }
    }

    void loadCategory()
    {
        ddcountry.Items.Clear();
        DataTable dt = CatalogHelper.BuildDisplayCategories();
        ddcountry.DataSource = dt;
        ddcountry.DataTextField = "CategoryName";
        ddcountry.DataValueField = "CategoryId";
        ddcountry.DataBind();
        ddcountry.Items.Insert(0, new ListItem("All Categories", "0"));

        rptcategory.DataSource = dt;
        rptcategory.DataBind();
    }

    void loadProduct(int pageIndex)
    {
        string categoryId = null;
        if (ddcountry.SelectedIndex != 0)
        {
            categoryId = ddcountry.SelectedValue;
        }

        DataTable dt = CatalogHelper.LoadProducts(pageIndex, CatalogHelper.CatalogProductPageSize, categoryId);
        rptProducts.DataSource = dt;
        rptProducts.DataBind();
    }

    protected void ddcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadProduct(1);
    }

    protected void rptCategories_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "SelectCategory")
        {
            int categoryId = Convert.ToInt32(e.CommandArgument);

            if (categoryId <= 0)
            {
                ddcountry.SelectedIndex = 0;
            }
            else
            {
                ListItem item = ddcountry.Items.FindByValue(categoryId.ToString());
                if (item != null)
                {
                    ddcountry.SelectedValue = categoryId.ToString();
                }
            }

            loadProduct(1);
        }
    }
}
