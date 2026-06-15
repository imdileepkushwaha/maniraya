(function () {
    function pickIconClass(title, breadcrumb) {
        var bcIcon = breadcrumb && breadcrumb.querySelector('i.fa');
        if (bcIcon) {
            return Array.prototype.slice.call(bcIcon.classList)
                .filter(function (c) { return c !== 'fa'; })
                .join(' ') || 'fa-dashboard';
        }

        var t = (title || '').toLowerCase();
        if (t.indexOf('dashboard') >= 0) return 'fa-tachometer';
        if (t.indexOf('user') >= 0) return 'fa-users';
        if (t.indexOf('report') >= 0) return 'fa-bar-chart';
        if (t.indexOf('product') >= 0) return 'fa-cube';
        if (t.indexOf('account') >= 0 || t.indexOf('transaction') >= 0) return 'fa-money';
        if (t.indexOf('network') >= 0 || t.indexOf('downline') >= 0 || t.indexOf('tree') >= 0) return 'fa-sitemap';
        if (t.indexOf('news') >= 0) return 'fa-newspaper-o';
        if (t.indexOf('bank') >= 0) return 'fa-university';
        if (t.indexOf('country') >= 0 || t.indexOf('state') >= 0 || t.indexOf('city') >= 0) return 'fa-globe';
        if (t.indexOf('franchisee') >= 0) return 'fa-building';
        if (t.indexOf('vendor') >= 0) return 'fa-truck';
        if (t.indexOf('kyc') >= 0) return 'fa-id-card';
        if (t.indexOf('message') >= 0 || t.indexOf('inbox') >= 0 || t.indexOf('mail') >= 0) return 'fa-envelope-o';
        return 'fa-file-text-o';
    }

    function enhanceHeader(header) {
        if (!header || header.getAttribute('data-admin-ch-enhanced') === '1') return;
        if (header.querySelector(':scope > .admin-ch-layout')) return;

        var h1 = header.querySelector(':scope > h1');
        if (!h1) return;

        var breadcrumb = header.querySelector(':scope > .breadcrumb, :scope > ol.breadcrumb');
        var iconClass = pickIconClass(h1.textContent, breadcrumb);

        var layout = document.createElement('div');
        layout.className = 'admin-ch-layout';

        var icon = document.createElement('div');
        icon.className = 'admin-ch-icon';
        icon.innerHTML = '<i class="fa ' + iconClass + '" aria-hidden="true"></i>';

        var copy = document.createElement('div');
        copy.className = 'admin-ch-copy';

        var eyebrow = document.createElement('span');
        eyebrow.className = 'admin-ch-eyebrow';
        eyebrow.textContent = document.body.classList.contains('franchisee-app') ? 'Franchisee Panel' : 'Admin Panel';

        copy.appendChild(eyebrow);
        copy.appendChild(h1);

        layout.appendChild(icon);
        layout.appendChild(copy);

        if (breadcrumb) {
            var trail = document.createElement('div');
            trail.className = 'admin-ch-trail';
            trail.appendChild(breadcrumb);
            layout.appendChild(trail);
        }

        header.appendChild(layout);
        header.setAttribute('data-admin-ch-enhanced', '1');
    }

    function initAdminHeaders() {
        document.querySelectorAll('.admin-app .admin-page-header-slot.content-header, .admin-app .admin-page-header-slot > .content-header, .franchisee-app .admin-page-header-slot.content-header, .franchisee-app .admin-page-header-slot > .content-header').forEach(enhanceHeader);
        document.querySelectorAll('.admin-app .content-header, .franchisee-app .content-header').forEach(function (header) {
            if (header.classList.contains('admin-page-header-slot')) return;
            if (header.parentElement && header.parentElement.classList.contains('content-header')) return;
            if (!header.closest('.admin-page-header-slot')) {
                enhanceHeader(header);
            }
        });
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initAdminHeaders);
    } else {
        initAdminHeaders();
    }
})();
