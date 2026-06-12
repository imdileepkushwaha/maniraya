(function () {
    function normalizePath(path) {
        if (!path) return '';
        var parts = path.split('/');
        return (parts[parts.length - 1] || '').toLowerCase();
    }

    function initAdminSidebar() {
        var body = document.body;
        var toggle = document.querySelector('.admin-sidebar-toggle');
        var overlay = document.getElementById('adminSidebarOverlay');
        var current = normalizePath(window.location.pathname);

        document.querySelectorAll('.admin-side-toggle').forEach(function (btn) {
            if (btn.getAttribute('data-bound') === '1') return;
            btn.setAttribute('data-bound', '1');
            btn.addEventListener('click', function () {
                var group = btn.closest('.admin-side-group');
                if (group) {
                    group.classList.toggle('is-open');
                }
            });
        });

        document.querySelectorAll('.admin-side-link, .admin-side-submenu a').forEach(function (link) {
            var href = link.getAttribute('href');
            if (!href || href === '#' || href.indexOf('javascript') === 0) return;

            if (normalizePath(href) === current) {
                link.classList.add('is-active');
                var group = link.closest('.admin-side-group');
                if (group) {
                    group.classList.add('is-open');
                    var toggleBtn = group.querySelector('.admin-side-toggle');
                    if (toggleBtn) {
                        toggleBtn.classList.add('is-active');
                    }
                }
            }
        });

        if (current === 'dashboard.aspx') {
            var dash = document.querySelector('.admin-side-link[href*="Dashboard.aspx"]');
            if (dash) dash.classList.add('is-active');
        }

        function closeSidebar() {
            body.classList.remove('admin-sidebar-open');
        }

        if (toggle && !toggle.getAttribute('data-bound')) {
            toggle.setAttribute('data-bound', '1');
            toggle.addEventListener('click', function () {
                body.classList.toggle('admin-sidebar-open');
            });
        }

        if (overlay && !overlay.getAttribute('data-bound')) {
            overlay.setAttribute('data-bound', '1');
            overlay.addEventListener('click', closeSidebar);
        }
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initAdminSidebar);
    } else {
        initAdminSidebar();
    }
})();
