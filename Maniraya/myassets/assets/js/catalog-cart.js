(function () {
  var endpoint = "index.aspx/AddToCart";

  document.addEventListener("click", function (event) {
    var target = event.target;
    if (!target || !target.closest) {
      return;
    }

    var btn = target.closest(".catalog-add-cart");
    if (!btn) {
      return;
    }

    event.preventDefault();
    event.stopPropagation();
    if (btn.disabled) {
      return;
    }

    var productId = (btn.getAttribute("data-productid") || "").trim();
    var franchiseeId = (btn.getAttribute("data-franchiseeid") || "").trim();
    if (!productId) {
      showToast("Product not found.", true);
      return;
    }

    btn.disabled = true;
    fetch(endpoint, {
      method: "POST",
      headers: { "Content-Type": "application/json; charset=utf-8" },
      credentials: "same-origin",
      body: JSON.stringify({
        productId: productId,
        franchiseeId: franchiseeId,
        returnUrl: window.location.pathname + window.location.search
      })
    })
      .then(function (res) {
        if (!res.ok) {
          throw new Error("Request failed");
        }
        return res.json();
      })
      .then(function (data) {
        var result = data && data.d ? data.d : data;
        if (!result) {
          showToast("Unable to add this product to cart.", true);
          return;
        }
        if (result.login) {
          window.location.href = result.loginUrl || "Login.aspx";
          return;
        }
        if (result.ok) {
          updateCartCount(result.count);
          showToast(result.message || "Product added to cart.");
          return;
        }
        showToast(result.message || "Unable to add this product to cart.", true);
      })
      .catch(function () {
        showToast("Unable to add this product to cart.", true);
      })
      .then(function () {
        btn.disabled = false;
      });
  });

  function updateCartCount(count) {
    var nodes = document.querySelectorAll(".cart-count");
    var text = String(count == null ? 0 : count);
    for (var i = 0; i < nodes.length; i++) {
      nodes[i].textContent = text;
    }
  }

  function showToast(message, isError) {
    var toast = document.getElementById("catalogCartToast");
    if (!toast) {
      toast = document.createElement("div");
      toast.id = "catalogCartToast";
      toast.className = "catalog-cart-toast";
      toast.setAttribute("role", "status");
      toast.setAttribute("aria-live", "polite");
      document.body.appendChild(toast);
    }

    toast.textContent = message;
    toast.classList.toggle("is-error", !!isError);
    toast.classList.add("is-visible");
    window.clearTimeout(showToast.timer);
    showToast.timer = window.setTimeout(function () {
      toast.classList.remove("is-visible");
    }, 2800);
  }
})();
