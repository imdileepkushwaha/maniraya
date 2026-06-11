const products = typeof featuredProducts !== "undefined" ? featuredProducts : [];

const productGrid = document.getElementById("productGrid");
const searchInput = document.getElementById("searchInput");
const categorySelect = document.getElementById("categorySelect");
const cartBtn = document.getElementById("cartBtn");
const closeCartBtn = document.getElementById("closeCartBtn");
const cartDrawer = document.getElementById("cartDrawer");
const cartItems = document.getElementById("cartItems");
const cartCount = document.getElementById("cartCount");
const cartTotal = document.getElementById("cartTotal");
const shippingLeft = document.getElementById("shippingLeft");
const shippingProgress = document.getElementById("shippingProgress");
const headerMenu = document.querySelector(".header-menu");
const mobileMenuBtn = document.getElementById("mobileMenuBtn");
const siteHeader = document.querySelector(".site-header");
const headerSearchWrap = document.querySelector(".header-search-wrap");
const mobileSearchToggle = document.getElementById("mobileSearchToggle");
const overlay = document.getElementById("overlay");
const offerPopupBackdrop = document.getElementById("offerPopupBackdrop");
const closeOfferPopup = document.getElementById("closeOfferPopup");
const offerTimer = offerPopupBackdrop ? offerPopupBackdrop.querySelector(".offer-timer") : null;
const offerTimerDigits = offerTimer ? Array.from(offerTimer.querySelectorAll("span:not(.offer-colon)")) : [];
const selectedCarousel = document.getElementById("selectedCarousel");
const selectedCarouselTrack = selectedCarousel ? selectedCarousel.querySelector(".selected-carousel-track") : null;
const selectedSlides = selectedCarousel ? Array.from(selectedCarousel.querySelectorAll(".selected-slide")) : [];
const selectedDots = selectedCarousel ? Array.from(selectedCarousel.querySelectorAll(".selected-dot")) : [];
const heroCarouselPrev = selectedCarousel ? selectedCarousel.querySelector(".hero-carousel-prev") : null;
const heroCarouselNext = selectedCarousel ? selectedCarousel.querySelector(".hero-carousel-next") : null;
const heroSlideCurrent = selectedCarousel ? selectedCarousel.querySelector(".hero-slide-current") : null;
const siteLoader = document.getElementById("siteLoader");
const passwordToggleButtons = Array.from(document.querySelectorAll(".password-toggle[data-target]"));
const forgotPasswordLink = document.getElementById("forgotPasswordLink");
const forgotPopupBackdrop = document.getElementById("forgotPopupBackdrop");
const closeForgotPopup = document.getElementById("closeForgotPopup");
const cancelForgotPopup = document.getElementById("cancelForgotPopup");
const forgotPasswordForm = document.getElementById("forgotPasswordForm");
const forgotEmailInput = document.getElementById("forgotEmailInput");

let cart = [];
const FREE_SHIPPING_TARGET = 2000;
const OFFER_TIMER_START_SECONDS = 29 * 60 + 18;
let offerTimerRemainingSeconds = OFFER_TIMER_START_SECONDS;
let offerTimerIntervalId = null;
let selectedSlideIndex = 0;
let selectedCarouselIntervalId = null;
const SELECTED_CAROUSEL_INTERVAL_MS = 3000;

const formatCurrency = (value) => `\u20B9 ${Number(value).toLocaleString("en-IN")}`;

const productFavIcon = `<svg aria-hidden="true" width="16" height="16" viewBox="0 0 24 24" fill="none"><path d="M12 20.25C12 20.25 4.5 14.8 4.5 9.45C4.5 7.16 6.34 5.25 8.63 5.25C10.07 5.25 11.37 5.96 12 7.07C12.63 5.96 13.93 5.25 15.37 5.25C17.66 5.25 19.5 7.16 19.5 9.45C19.5 14.8 12 20.25 12 20.25Z" stroke="currentColor" stroke-width="1.8" stroke-linejoin="round"/></svg>`;

function getProductDiscount(product) {
  const oldPrice = product.oldPrice || product.price * 1.1;
  return Math.max(1, Math.round(((oldPrice - product.price) / oldPrice) * 100));
}

function renderProducts(items) {
  if (!productGrid) return;
  if (!items.length) {
    productGrid.innerHTML = "<p class=\"products-empty\">No products found.</p>";
    return;
  }

  productGrid.innerHTML = items
    .map((product) => {
      const oldPrice = product.oldPrice || product.price * 1.1;
      const discount = getProductDiscount(product);
      const categoryLabel = product.categoryLabel || product.category;
      const badge = product.badge
        ? `<span class="product-badge">${product.badge}</span>`
        : `<span class="product-badge product-badge-sale">-${discount}%</span>`;
      const imageMarkup = product.image
        ? `<img src="${product.image}" alt="${product.name}" loading="lazy" />`
        : `<span class="product-image-fallback">${getProductInitials(product.name)}</span>`;

      const detailUrl = `Productdetail.aspx?productid=${product.id}&franchiseeid=0`;
      return `
      <article class="product-card">
        <div class="product-card-media">
          ${badge}
          <a class="product-image-link" href="${detailUrl}">
            <div class="product-image">${imageMarkup}</div>
            <span class="product-card-overlay" aria-hidden="true">
              <span class="product-card-overlay-btn">Quick View</span>
            </span>
          </a>
        </div>
        <div class="product-card-body">
          <div class="product-card-head">
            <span class="product-category-tag">${categoryLabel}</span>
          </div>
          <h3 class="product-title"><a href="${detailUrl}">${product.name}</a></h3>
          <div class="product-price-block">
            <div class="product-price-main">
              <span class="price">${formatCurrency(product.price)}</span>
              <span class="product-old-price">${formatCurrency(oldPrice)}</span>
            </div>
            <span class="product-discount">Save ${discount}%</span>
          </div>
          <div class="product-card-actions">
            <a class="view-btn" href="${detailUrl}">
              <svg width="15" height="15" viewBox="0 0 24 24" fill="none" aria-hidden="true"><path d="M2.6 12C3.9 8.1 7.6 5.5 12 5.5C16.4 5.5 20.1 8.1 21.4 12C20.1 15.9 16.4 18.5 12 18.5C7.6 18.5 3.9 15.9 2.6 12Z" stroke="currentColor" stroke-width="1.7"/><circle cx="12" cy="12" r="3" stroke="currentColor" stroke-width="1.7"/></svg>
              <span>View</span>
            </a>
            <button class="add-btn" type="button" data-id="${product.id}">
              <svg width="15" height="15" viewBox="0 0 24 24" fill="none" aria-hidden="true"><path d="M4 7h16l-1.4 11H5.4L4 7Z" stroke="currentColor" stroke-width="1.8" stroke-linejoin="round"/><path d="M9 11v6M15 11v6" stroke="currentColor" stroke-width="1.8" stroke-linecap="round"/></svg>
              <span>Add to Cart</span>
            </button>
          </div>
        </div>
      </article>
    `;
    })
    .join("");
}

function addToCart(id) {
  const product = products.find((item) => item.id === id);
  if (!product) return;

  const existing = cart.find((item) => item.id === id);
  if (existing) {
    existing.qty += 1;
  } else {
    cart.push({ ...product, qty: 1 });
  }
  renderCart();
}

function removeFromCart(id) {
  cart = cart.filter((item) => item.id !== id);
  renderCart();
}

function increaseCartQty(id) {
  const item = cart.find((entry) => entry.id === id);
  if (!item) return;
  item.qty += 1;
  renderCart();
}

function decreaseCartQty(id) {
  const item = cart.find((entry) => entry.id === id);
  if (!item) return;
  if (item.qty <= 1) {
    removeFromCart(id);
    return;
  }
  item.qty -= 1;
  renderCart();
}

function getProductInitials(name) {
  return name
    .split(" ")
    .slice(0, 2)
    .map((part) => part[0])
    .join("")
    .toUpperCase();
}

function renderStars(rating) {
  const filled = "★".repeat(rating);
  const empty = "☆".repeat(Math.max(0, 5 - rating));
  return `<span class="stars-filled">${filled}</span><span class="stars-empty">${empty}</span>`;
}

function renderCart() {
  if (!cartItems || !cartCount || !cartTotal) return;
  if (!cart.length) {
    cartItems.innerHTML = "<p class='cart-empty'>Your cart is empty.</p>";
    cartCount.textContent = "0";
    cartTotal.textContent = formatCurrency(0);
    if (shippingLeft) shippingLeft.textContent = formatCurrency(FREE_SHIPPING_TARGET);
    if (shippingProgress) shippingProgress.style.width = "0%";
    return;
  }

  cartItems.innerHTML = cart
    .map(
      (item) => `
      <div class="cart-item">
        <div class="cart-item-media">${item.image ? `<img src="${item.image}" alt="" />` : getProductInitials(item.name)}</div>
        <div class="cart-item-content">
          <strong>${item.name}</strong>
          <div class="cart-item-rating">${renderStars(item.rating || 4)}</div>
          <div class="cart-item-price">
            <span class="new-price">${formatCurrency(item.price)}</span>
            <span class="old-price">${formatCurrency(item.oldPrice || item.price * 1.1)}</span>
          </div>
        </div>
        <div class="cart-item-actions">
          <button class="delete-btn" data-remove-id="${item.id}" aria-label="Delete item">

          <svg width="10" height="11" viewBox="0 0 10 11" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path opacity="0.4" d="M9.15334 4.11869C9.15334 4.15609 8.86022 7.86351 8.69279 9.4238C8.58795 10.3813 7.97067 10.9621 7.04475 10.9786C6.33333 10.9946 5.63689 11 4.95167 11C4.22421 11 3.51278 10.9946 2.82222 10.9786C1.92733 10.9572 1.30951 10.3648 1.21002 9.4238C1.03778 7.85801 0.750005 4.15609 0.744656 4.11869C0.739307 4.00594 0.775681 3.8987 0.849497 3.8118C0.922244 3.7315 1.02709 3.68311 1.13728 3.68311H8.76607C8.87573 3.68311 8.97522 3.7315 9.05385 3.8118C9.12713 3.8987 9.16404 4.00594 9.15334 4.11869Z" fill="white"/>
          <path d="M9.9 2.18727C9.9 1.96123 9.72188 1.78414 9.50791 1.78414H7.90427C7.57798 1.78414 7.29448 1.55205 7.22174 1.22481L7.13187 0.823871C7.00617 0.339338 6.57236 0 6.0856 0H3.81493C3.32282 0 2.89329 0.339338 2.76278 0.85027L2.6788 1.22536C2.60552 1.55205 2.32202 1.78414 1.99626 1.78414H0.392619C0.178123 1.78414 0 1.96123 0 2.18727V2.39627C0 2.61681 0.178123 2.7994 0.392619 2.7994H9.50791C9.72188 2.7994 9.9 2.61681 9.9 2.39627V2.18727Z" fill="white"/>
          </svg>
          
          </button>
          <button class="qty-minus-btn" data-dec-id="${item.id}" aria-label="Decrease quantity">-</button>
          <span class="cart-item-qty">${item.qty}</span>
          <button class="qty-plus-btn" data-inc-id="${item.id}" aria-label="Increase quantity">+</button>
        </div>
      </div>
    `
    )
    .join("");

  const count = cart.reduce((total, item) => total + item.qty, 0);
  const total = cart.reduce((sum, item) => sum + item.qty * item.price, 0);
  const remaining = Math.max(0, FREE_SHIPPING_TARGET - total);
  const progress = Math.min(100, (total / FREE_SHIPPING_TARGET) * 100);

  cartCount.textContent = String(count);
  cartTotal.textContent = formatCurrency(total);
  if (shippingLeft) shippingLeft.textContent = formatCurrency(remaining);
  if (shippingProgress) shippingProgress.style.width = `${progress}%`;
}

function openCart() {
  if (!cartDrawer || !overlay) return;
  cartDrawer.classList.add("open");
  cartDrawer.setAttribute("aria-hidden", "false");
  overlay.hidden = false;
  document.body.classList.add("cart-open");
}

function closeCart() {
  if (!cartDrawer || !overlay) return;
  cartDrawer.classList.remove("open");
  cartDrawer.setAttribute("aria-hidden", "true");
  overlay.hidden = true;
  document.body.classList.remove("cart-open");
}

function updateOfferTimerDisplay() {
  if (offerTimerDigits.length < 4) return;
  const minutes = Math.floor(offerTimerRemainingSeconds / 60);
  const seconds = offerTimerRemainingSeconds % 60;
  const value = `${String(minutes).padStart(2, "0")}${String(seconds).padStart(2, "0")}`;
  offerTimerDigits.forEach((digit, index) => {
    digit.textContent = value[index] || "0";
  });
}

function stopOfferTimer() {
  if (offerTimerIntervalId) {
    window.clearInterval(offerTimerIntervalId);
    offerTimerIntervalId = null;
  }
}

function startOfferTimer() {
  if (!offerPopupBackdrop || !offerTimer) return;
  stopOfferTimer();
  offerTimerRemainingSeconds = OFFER_TIMER_START_SECONDS;
  updateOfferTimerDisplay();
  offerTimerIntervalId = window.setInterval(() => {
    if (offerTimerRemainingSeconds <= 0) {
      stopOfferTimer();
      return;
    }
    offerTimerRemainingSeconds -= 1;
    updateOfferTimerDisplay();
  }, 1000);
}

function showOfferPopup() {
  if (!offerPopupBackdrop) return;
  offerPopupBackdrop.hidden = false;
  startOfferTimer();
}

function hideOfferPopup() {
  if (!offerPopupBackdrop) return;
  offerPopupBackdrop.hidden = true;
  stopOfferTimer();
}

function showForgotPopup() {
  if (!forgotPopupBackdrop) return;
  forgotPopupBackdrop.hidden = false;
  requestAnimationFrame(() => {
    forgotPopupBackdrop.classList.add("is-open");
  });
  window.setTimeout(() => {
    if (forgotEmailInput) forgotEmailInput.focus();
  }, 120);
}

function hideForgotPopup() {
  if (!forgotPopupBackdrop) return;
  forgotPopupBackdrop.classList.remove("is-open");
  window.setTimeout(() => {
    if (forgotPopupBackdrop && !forgotPopupBackdrop.classList.contains("is-open")) {
      forgotPopupBackdrop.hidden = true;
    }
  }, 220);
}

function showSelectedSlide(nextIndex) {
  if (!selectedCarouselTrack || !selectedSlides.length) return;
  selectedSlideIndex = (nextIndex + selectedSlides.length) % selectedSlides.length;
  selectedCarouselTrack.style.transform = `translateX(-${selectedSlideIndex * 100}%)`;
  selectedSlides.forEach((slide, index) => {
    slide.classList.toggle("is-active", index === selectedSlideIndex);
  });
  selectedDots.forEach((dot, index) => {
    dot.classList.toggle("is-active", index === selectedSlideIndex);
  });
  if (heroSlideCurrent) {
    heroSlideCurrent.textContent = String(selectedSlideIndex + 1).padStart(2, "0");
  }
}

function startSelectedCarouselAutoplay() {
  if (!selectedCarouselTrack || selectedSlides.length < 2) return;
  if (selectedCarouselIntervalId) window.clearInterval(selectedCarouselIntervalId);
  selectedCarouselIntervalId = window.setInterval(() => {
    showSelectedSlide(selectedSlideIndex + 1);
  }, SELECTED_CAROUSEL_INTERVAL_MS);
}

function setupSelectedCarousel() {
  if (!selectedCarousel || !selectedCarouselTrack || selectedSlides.length < 2) return;
  showSelectedSlide(0);
  startSelectedCarouselAutoplay();
  selectedDots.forEach((dot, index) => {
    dot.addEventListener("click", () => {
      showSelectedSlide(index);
      startSelectedCarouselAutoplay();
    });
  });
  if (heroCarouselPrev) {
    heroCarouselPrev.addEventListener("click", () => {
      showSelectedSlide(selectedSlideIndex - 1);
      startSelectedCarouselAutoplay();
    });
  }
  if (heroCarouselNext) {
    heroCarouselNext.addEventListener("click", () => {
      showSelectedSlide(selectedSlideIndex + 1);
      startSelectedCarouselAutoplay();
    });
  }
}

function setupHeaderScroll() {
  if (!siteHeader) return;

  const toggleHeaderState = () => {
    siteHeader.classList.toggle("is-scrolled", window.scrollY > 12);
  };

  toggleHeaderState();
  window.addEventListener("scroll", toggleHeaderState, { passive: true });
}

function closeAllHeaderMenus() {
  if (!headerMenu) return;
  headerMenu.querySelectorAll(".menu-item.open").forEach((item) => item.classList.remove("open"));
  headerMenu.querySelectorAll("[data-menu-toggle]").forEach((link) => link.setAttribute("aria-expanded", "false"));
}

function closeMobileMenu() {
  if (!headerMenu) return;
  headerMenu.classList.remove("mobile-open");
  document.body.classList.remove("mobile-menu-open");
  if (mobileMenuBtn) {
    mobileMenuBtn.setAttribute("aria-expanded", "false");
    mobileMenuBtn.classList.remove("is-open");
  }
  closeAllHeaderMenus();
}

function closeMobileSearch() {
  document.body.classList.remove("mobile-search-open");
  if (mobileSearchToggle) mobileSearchToggle.classList.remove("is-open");
}

function hideSiteLoader() {
  if (!siteLoader) {
    document.body.classList.remove("is-loading");
    return;
  }
  siteLoader.classList.add("is-hidden");
  document.body.classList.remove("is-loading");
  window.setTimeout(() => {
    siteLoader.remove();
  }, 380);
}

function setupHeaderMenu() {
  if (!headerMenu) return;

  headerMenu.addEventListener("click", (event) => {
    const target = event.target;
    if (!(target instanceof Element)) return;
    const toggleLink = target.closest("[data-menu-toggle]");
    if (!toggleLink) return;

    event.preventDefault();

    const menuItem = toggleLink.closest(".menu-item");
    if (!menuItem) return;

    const isOpen = menuItem.classList.contains("open");
    closeAllHeaderMenus();
    if (!isOpen) {
      menuItem.classList.add("open");
      toggleLink.setAttribute("aria-expanded", "true");
    }
  });

  headerMenu.addEventListener("click", (event) => {
    const target = event.target;
    if (!(target instanceof Element)) return;
    const normalLink = target.closest(".menu-link");
    if (!normalLink) return;
    if (normalLink.hasAttribute("data-menu-toggle")) return;
    if (window.innerWidth <= 768) closeMobileMenu();
  });

  document.addEventListener("click", (event) => {
    if (!(event.target instanceof Node)) return;
    if (headerMenu.contains(event.target)) return;
    closeAllHeaderMenus();
    if (window.innerWidth <= 768 && siteHeader && !siteHeader.contains(event.target)) {
      closeMobileMenu();
      closeMobileSearch();
    }
  });

  if (mobileMenuBtn) {
    mobileMenuBtn.addEventListener("click", () => {
      const isOpen = headerMenu.classList.toggle("mobile-open");
      mobileMenuBtn.setAttribute("aria-expanded", isOpen ? "true" : "false");
      mobileMenuBtn.classList.toggle("is-open", isOpen);
      document.body.classList.toggle("mobile-menu-open", isOpen);
      if (!isOpen) {
        closeAllHeaderMenus();
      }
      if (isOpen) closeMobileSearch();
    });
  }

  if (mobileSearchToggle && headerSearchWrap) {
    mobileSearchToggle.addEventListener("click", () => {
      const isOpen = !document.body.classList.contains("mobile-search-open");
      document.body.classList.toggle("mobile-search-open", isOpen);
      mobileSearchToggle.classList.toggle("is-open", isOpen);
      if (isOpen) {
        closeMobileMenu();
        window.setTimeout(() => {
          const mobileSearchInput = headerSearchWrap.querySelector(".header-search-input");
          if (mobileSearchInput instanceof HTMLInputElement) mobileSearchInput.focus();
        }, 30);
      }
    });
  }

  window.addEventListener("resize", () => {
    if (window.innerWidth > 768) {
      closeMobileMenu();
      closeMobileSearch();
    }
  });
}

function setupPasswordToggle() {
  if (!passwordToggleButtons.length) return;
  passwordToggleButtons.forEach((toggleButton) => {
    const targetId = toggleButton.getAttribute("data-target");
    if (!targetId) return;
    const targetInput = document.getElementById(targetId);
    if (!(targetInput instanceof HTMLInputElement)) return;

    toggleButton.addEventListener("click", () => {
      const isPassword = targetInput.type === "password";
      targetInput.type = isPassword ? "text" : "password";
      toggleButton.classList.toggle("is-password-visible", isPassword);
      const labelPrefix = targetInput.name === "confirmPassword" ? "confirm password" : "password";
      toggleButton.setAttribute("aria-label", isPassword ? `Hide ${labelPrefix}` : `Show ${labelPrefix}`);
    });
  });
}

function setupForgotPasswordPopup() {
  if (forgotPasswordLink) {
    forgotPasswordLink.addEventListener("click", (event) => {
      event.preventDefault();
      showForgotPopup();
    });
  }

  if (closeForgotPopup) {
    closeForgotPopup.addEventListener("click", hideForgotPopup);
  }

  if (cancelForgotPopup) {
    cancelForgotPopup.addEventListener("click", hideForgotPopup);
  }

  if (forgotPopupBackdrop) {
    forgotPopupBackdrop.addEventListener("click", (event) => {
      if (event.target === forgotPopupBackdrop) hideForgotPopup();
    });
  }

  const submitForgotPopup = document.getElementById("submitForgotPopup");
  if (submitForgotPopup) {
    submitForgotPopup.addEventListener("click", () => {
      if (!forgotEmailInput || !forgotEmailInput.value.trim()) {
        forgotEmailInput?.focus();
        forgotEmailInput?.setCustomValidity("Please enter your email address.");
        forgotEmailInput?.reportValidity();
        return;
      }
      forgotEmailInput.setCustomValidity("");
      hideForgotPopup();
    });
  }
}

function setupProductDetailGallery() {
  const gallery = document.querySelector(".pd-gallery");
  if (!gallery) return;

  const mainImage = gallery.querySelector(".pd-main-image");
  const thumbButtons = Array.from(gallery.querySelectorAll(".pd-thumb"));
  if (!(mainImage instanceof HTMLImageElement) || !thumbButtons.length) return;

  thumbButtons.forEach((thumbButton) => {
    thumbButton.addEventListener("click", () => {
      const thumbImage = thumbButton.querySelector("img");
      if (!(thumbImage instanceof HTMLImageElement)) return;

      mainImage.src = thumbImage.src.replace(/w=\d+/, "w=900");
      mainImage.alt = thumbButton.getAttribute("aria-label") || thumbImage.alt || mainImage.alt;

      thumbButtons.forEach((button) => button.classList.remove("is-active"));
      thumbButton.classList.add("is-active");
    });
  });
}

function setupProductDetailTabs() {
  const tabsWrap = document.querySelector(".pd-tabs-wrap");
  if (!tabsWrap) return;

  const tabs = Array.from(tabsWrap.querySelectorAll(".pd-tab"));
  const panels = Array.from(tabsWrap.querySelectorAll(".pd-tab-panel"));
  if (!tabs.length || !panels.length || tabs.length !== panels.length) return;

  tabs.forEach((tab, index) => {
    tab.addEventListener("click", () => {
      tabs.forEach((item) => item.classList.remove("is-active"));
      panels.forEach((panel) => panel.classList.remove("is-active"));
      tab.classList.add("is-active");
      panels[index].classList.add("is-active");
    });
  });
}

if (productGrid) {
  productGrid.addEventListener("click", (event) => {
    const target = event.target;
    if (!(target instanceof Element)) return;
    const addButton = target.closest("[data-id]");
    if (!addButton) return;
    const id = Number(addButton.getAttribute("data-id"));
    if (id) addToCart(id);
  });
}

if (cartItems) {
  cartItems.addEventListener("click", (event) => {
    const target = event.target;
    if (!(target instanceof Element)) return;
    const removeButton = target.closest("[data-remove-id]");
    if (removeButton) {
      const removeId = Number(removeButton.getAttribute("data-remove-id"));
      if (removeId) removeFromCart(removeId);
      return;
    }

    const decreaseButton = target.closest("[data-dec-id]");
    if (decreaseButton) {
      const decreaseId = Number(decreaseButton.getAttribute("data-dec-id"));
      if (decreaseId) decreaseCartQty(decreaseId);
      return;
    }

    const increaseButton = target.closest("[data-inc-id]");
    if (!increaseButton) return;
    const increaseId = Number(increaseButton.getAttribute("data-inc-id"));
    if (increaseId) increaseCartQty(increaseId);
  });
}

function setupFeaturedProducts() {
  if (!productGrid || !products.length) return;

  let activeCategory = "all";
  const filterRoot = document.getElementById("productFilters");
  const headerSearchInput = document.querySelector(".header-search-input");

  const applyFeaturedFilters = () => {
    const query = headerSearchInput ? headerSearchInput.value.trim().toLowerCase() : "";
    const filtered = products.filter((product) => {
      const categoryMatch = activeCategory === "all" || product.category === activeCategory;
      const searchMatch =
        !query ||
        product.name.toLowerCase().includes(query) ||
        (product.categoryLabel || product.category).toLowerCase().includes(query);
      return categoryMatch && searchMatch;
    });
    renderProducts(filtered);
    const countEl = document.getElementById("productsCount");
    if (countEl) {
      countEl.textContent = `${filtered.length} product${filtered.length === 1 ? "" : "s"}`;
    }
  };

  if (filterRoot) {
    filterRoot.addEventListener("click", (event) => {
      const chip = event.target.closest(".product-filter-chip");
      if (!chip) return;
      activeCategory = chip.dataset.category || "all";
      filterRoot.querySelectorAll(".product-filter-chip").forEach((button) => {
        button.classList.toggle("is-active", button === chip);
      });
      applyFeaturedFilters();
    });
  }

  if (headerSearchInput) {
    headerSearchInput.addEventListener("input", applyFeaturedFilters);
  }

  renderProducts(products);
  const countEl = document.getElementById("productsCount");
  if (countEl) {
    countEl.textContent = `${products.length} products`;
  }
}
if (cartBtn) cartBtn.addEventListener("click", openCart);
if (closeCartBtn) closeCartBtn.addEventListener("click", closeCart);
if (overlay) overlay.addEventListener("click", closeCart);

if (closeOfferPopup) {
  closeOfferPopup.addEventListener("click", hideOfferPopup);
}

if (offerPopupBackdrop) {
  offerPopupBackdrop.addEventListener("click", (event) => {
    if (event.target === offerPopupBackdrop) hideOfferPopup();
  });
}

document.addEventListener("keydown", (event) => {
  if (event.key === "Escape") {
    hideOfferPopup();
    hideForgotPopup();
    closeAllHeaderMenus();
    closeMobileMenu();
    closeMobileSearch();
  }
});

if (searchInput && categorySelect) {
  const applyFilters = () => {
    const query = searchInput.value.trim().toLowerCase();
    const category = categorySelect.value;
    const filtered = products.filter((product) => {
      const categoryMatch = category === "all" || product.category === category;
      const searchMatch = product.name.toLowerCase().includes(query);
      return categoryMatch && searchMatch;
    });
    renderProducts(filtered);
  };
  searchInput.addEventListener("input", applyFilters);
  categorySelect.addEventListener("change", applyFilters);
}

function setupCouponSubscribe() {
  const form = document.querySelector(".coupon-subscribe-form");
  if (!form) return;

  const input = form.querySelector('input[type="email"]');
  const button = form.querySelector(".coupon-subscribe-btn");
  const feedback = document.querySelector(".coupon-feedback");
  if (!input || !button || !feedback) return;

  const handleSubscribe = () => {
    const email = input.value.trim();
    const isValid = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);

    feedback.classList.remove("is-success", "is-error");

    if (!isValid) {
      feedback.textContent = "Please enter a valid email address.";
      feedback.classList.add("is-error");
      input.focus();
      return;
    }

    feedback.textContent = "Thanks! Your discount coupon is on the way.";
    feedback.classList.add("is-success");
    input.value = "";
  };

  button.addEventListener("click", handleSubscribe);
  input.addEventListener("keydown", (event) => {
    if (event.key === "Enter") {
      event.preventDefault();
      handleSubscribe();
    }
  });
}

setupFeaturedProducts();
setupHeaderScroll();
setupHeaderMenu();
setupSelectedCarousel();
setupPasswordToggle();
setupForgotPasswordPopup();
setupProductDetailGallery();
setupProductDetailTabs();
setupCouponSubscribe();
renderCart();

window.addEventListener("load", () => {
  hideSiteLoader();
  window.setTimeout(showOfferPopup, 450);
});

// Fallback in case browser load event delays unexpectedly.
window.setTimeout(hideSiteLoader, 3500);
