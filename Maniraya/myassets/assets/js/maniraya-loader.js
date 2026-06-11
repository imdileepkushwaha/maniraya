(function (window, document) {
  "use strict";

  var DEFAULT_MESSAGE = "Please wait...";
  var activeCount = 0;
  var loaderEl = null;
  var textEl = null;
  var defaultMessage = DEFAULT_MESSAGE;

  var LOADER_MARKUP =
    '<div id="tcAjaxLoader" class="site-loader tc-ajax-loader is-hidden" role="status" aria-live="polite" aria-label="Loading">' +
      '<div class="site-loader-card">' +
        '<div class="site-loader-cart" aria-hidden="true">' +
          '<span class="site-loader-wheel"></span>' +
          '<span class="site-loader-wheel"></span>' +
        '</div>' +
        '<p class="site-loader-brand">Maniraya</p>' +
        '<p class="site-loader-text">' + DEFAULT_MESSAGE + '</p>' +
        '<div class="site-loader-progress" aria-hidden="true">' +
          '<span class="site-loader-progress-fill"></span>' +
        '</div>' +
      '</div>' +
    '</div>';

  function ensureLoader() {
    if (loaderEl) {
      return loaderEl;
    }

    loaderEl = document.getElementById("tcAjaxLoader");
    if (!loaderEl) {
      var wrap = document.createElement("div");
      wrap.innerHTML = LOADER_MARKUP;
      loaderEl = wrap.firstElementChild;
      document.body.appendChild(loaderEl);
    }

    textEl = loaderEl.querySelector(".site-loader-text");
    return loaderEl;
  }

  function show(message) {
    ensureLoader();
    activeCount += 1;
    if (textEl) {
      textEl.textContent = message || defaultMessage;
    }
    loaderEl.classList.remove("is-hidden");
    document.body.classList.add("tc-ajax-loading");
  }

  function hide() {
    if (activeCount > 0) {
      activeCount -= 1;
    }
    if (activeCount > 0) {
      return;
    }
    activeCount = 0;
    if (loaderEl) {
      loaderEl.classList.add("is-hidden");
    }
    document.body.classList.remove("tc-ajax-loading");
  }

  function setupPageRequestManager() {
    if (typeof Sys === "undefined" || !Sys.WebForms || !Sys.WebForms.PageRequestManager) {
      return;
    }

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm._tcLoaderHooked) {
      return;
    }

    prm._tcLoaderHooked = true;
    prm.add_beginRequest(function () {
      show(defaultMessage);
    });
    prm.add_endRequest(function () {
      hide();
    });
  }

  function bindPageRequestManager() {
    if (typeof Sys !== "undefined" && Sys.Application) {
      Sys.Application.add_load(setupPageRequestManager);
    }
    setupPageRequestManager();
  }

  function init() {
    ensureLoader();
    bindPageRequestManager();
  }

  window.ManirayaLoader = {
    show: show,
    hide: hide,
    setDefaultMessage: function (message) {
      defaultMessage = message || DEFAULT_MESSAGE;
    },
    init: init
  };

  if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", init);
  } else {
    init();
  }

  window.addEventListener("load", bindPageRequestManager);
})(window, document);
