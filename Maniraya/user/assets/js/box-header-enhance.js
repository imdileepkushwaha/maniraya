(function () {
	"use strict";

	var iconRules = [
		{ pattern: /search|criteria|crteria|filter/i, icon: "fa-search" },
		{ pattern: /detail|report|ledger|statement/i, icon: "fa-list-alt" },
		{ pattern: /personal|profile|user info/i, icon: "fa-user" },
		{ pattern: /nominee/i, icon: "fa-users" },
		{ pattern: /bank|account|paytm/i, icon: "fa-university" },
		{ pattern: /upload|photo|image|proof|pan|gst/i, icon: "fa-camera" },
		{ pattern: /mail|message|ticket|inbox|sent/i, icon: "fa-envelope" },
		{ pattern: /income|bonus|payout|commission/i, icon: "fa-line-chart" },
		{ pattern: /add|compose|activate|joining|topup|register|verify/i, icon: "fa-plus-circle" },
		{ pattern: /epin|e-pin|pin/i, icon: "fa-key" },
		{ pattern: /wallet|transfer|money|recharge|withdraw|deposit|cash/i, icon: "fa-money" },
		{ pattern: /tree|downline|direct|team|binary|pool/i, icon: "fa-sitemap" },
		{ pattern: /purchase|item|package|repurchase|shop/i, icon: "fa-shopping-cart" },
		{ pattern: /password|security/i, icon: "fa-lock" },
		{ pattern: /award|rank|vacation/i, icon: "fa-trophy" }
	];

	function resolveIcon(title) {
		var i;
		for (i = 0; i < iconRules.length; i++) {
			if (iconRules[i].pattern.test(title)) {
				return iconRules[i].icon;
			}
		}
		return "fa-file-text-o";
	}

	var toneCount = 8;

	function resolveTone(title, index) {
		var hash = 0;
		var i;

		for (i = 0; i < title.length; i++) {
			hash = ((hash << 5) - hash) + title.charCodeAt(i);
			hash |= 0;
		}

		return Math.abs((hash + index) % toneCount);
	}

	function enhanceBoxHeader(header, index) {
		if (!header || header.classList.contains("box-header-enhanced")) {
			return;
		}

		var title = header.querySelector(".box-title, h3");
		if (!title) {
			return;
		}

		var titleText = title.textContent.replace(/\s+/g, " ").trim();
		if (!titleText) {
			return;
		}

		header.classList.add("box-header-enhanced");
		header.classList.add("box-header-tone-" + resolveTone(titleText, index));

		var main = document.createElement("div");
		main.className = "box-header-main";

		var iconWrap = document.createElement("span");
		iconWrap.className = "box-header-icon";
		iconWrap.setAttribute("aria-hidden", "true");
		iconWrap.innerHTML = '<i class="fa ' + resolveIcon(titleText) + '"></i>';

		var textWrap = document.createElement("div");
		textWrap.className = "box-header-text";

		main.appendChild(iconWrap);
		textWrap.appendChild(title);
		main.appendChild(textWrap);

		var tools = header.querySelector(".box-tools");
		header.insertBefore(main, header.firstChild);

		if (tools) {
			main.appendChild(tools);
		}
	}

	function enhanceAllBoxHeaders() {
		var headers = document.querySelectorAll(".box.box-primary > .box-header");
		var i;

		for (i = 0; i < headers.length; i++) {
			enhanceBoxHeader(headers[i], i);
		}
	}

	if (document.readyState === "loading") {
		document.addEventListener("DOMContentLoaded", enhanceAllBoxHeaders);
	} else {
		enhanceAllBoxHeaders();
	}

	if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
		Sys.WebForms.PageRequestManager.getInstance().add_endRequest(enhanceAllBoxHeaders);
	}
})();
