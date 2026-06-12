(function () {
    function isFullscreenActive() {
        return !!(document.fullscreenElement ||
            document.webkitFullscreenElement ||
            document.mozFullScreenElement ||
            document.msFullscreenElement);
    }

    function requestAppFullscreen() {
        var el = document.documentElement;
        var method = el.requestFullscreen ||
            el.webkitRequestFullscreen ||
            el.mozRequestFullScreen ||
            el.msRequestFullscreen;

        if (!method) {
            return Promise.reject();
        }

        return method.call(el);
    }

    function exitAppFullscreen() {
        var method = document.exitFullscreen ||
            document.webkitExitFullscreen ||
            document.mozCancelFullScreen ||
            document.msExitFullscreen;

        if (!method) {
            return Promise.reject();
        }

        return method.call(document);
    }

    function updateFullscreenButton(btn) {
        if (!btn) return;

        var active = isFullscreenActive();
        var icon = btn.querySelector('i');

        btn.classList.toggle('is-active', active);
        btn.setAttribute('title', active ? 'Exit fullscreen' : 'Fullscreen');
        btn.setAttribute('aria-label', active ? 'Exit fullscreen' : 'Enter fullscreen');

        if (icon) {
            icon.classList.toggle('fa-expand', !active);
            icon.classList.toggle('fa-compress', active);
        }
    }

    function initAdminTopbar() {
        var fullscreenBtn = document.getElementById('adminFullscreenBtn');

        if (fullscreenBtn && fullscreenBtn.getAttribute('data-bound') !== '1') {
            fullscreenBtn.setAttribute('data-bound', '1');
            fullscreenBtn.addEventListener('click', function (event) {
                event.preventDefault();

                if (isFullscreenActive()) {
                    exitAppFullscreen().catch(function () { });
                    return;
                }

                requestAppFullscreen().catch(function () { });
            });
        }

        document.addEventListener('fullscreenchange', function () {
            updateFullscreenButton(fullscreenBtn);
        });
        document.addEventListener('webkitfullscreenchange', function () {
            updateFullscreenButton(fullscreenBtn);
        });
        document.addEventListener('mozfullscreenchange', function () {
            updateFullscreenButton(fullscreenBtn);
        });
        document.addEventListener('MSFullscreenChange', function () {
            updateFullscreenButton(fullscreenBtn);
        });

        updateFullscreenButton(fullscreenBtn);

        document.querySelectorAll('.admin-topbar-actions .dropdown').forEach(function (dropdown) {
            dropdown.addEventListener('show.bs.dropdown', function () {
                document.querySelectorAll('.admin-topbar-actions .dropdown.open').forEach(function (openDropdown) {
                    if (openDropdown !== dropdown) {
                        openDropdown.classList.remove('open');
                    }
                });
            });
        });
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initAdminTopbar);
    } else {
        initAdminTopbar();
    }
})();
