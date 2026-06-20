/**
 * Bootstrap 3 modals with ASP.NET UpdatePanel async postbacks.
 * Moves modal inside the WebForms <form> so postback buttons keep working,
 * while fixed positioning + z-index keep it above the backdrop.
 */
(function ($) {
    'use strict';

    function resetBackdrop() {
        $('body').removeClass('modal-open').css('padding-right', '');
        $('.modal-backdrop').remove();
    }

    function getModalContainer() {
        var $form = $('#form1');
        if (!$form.length) {
            $form = $('form').first();
        }
        return $form.length ? $form : $('body');
    }

    function removeDuplicateModals(modalId) {
        var $all = $('#' + modalId);
        if ($all.length > 1) {
            // Keep first instance (fresh UpdatePanel markup); drop orphans moved to form on prior open.
            $all.slice(1).remove();
        }
    }

    window.showAdminModal = function (modalId) {
        modalId = modalId || 'myModal';

        window.setTimeout(function () {
            resetBackdrop();
            removeDuplicateModals(modalId);

            document.body.classList.remove('admin-sidebar-open');

            var $container = getModalContainer();
            $('body > #' + modalId).remove();

            var $modal = $('#' + modalId).first();
            if (!$modal.length) {
                return;
            }

            if ($modal.data('bs.modal')) {
                $modal.data('bs.modal', null);
            }

            if (!$modal.parent().is($container)) {
                $modal.appendTo($container);
            }

            $modal.off('shown.bs.modal.admin').on('shown.bs.modal.admin', function () {
                $('body').addClass('modal-open').css('padding-right', '0');
                var $dialog = $modal.find('.modal-dialog').first();
                if ($dialog.length) {
                    $dialog.css('margin-top', '');
                }
            });

            $modal.modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
        }, 150);
    };

    window.closeAdminModal = function (modalId) {
        modalId = modalId || 'myModal';
        var $modal = $('body > #' + modalId);
        if (!$modal.length) {
            $modal = $('#' + modalId).last();
        }

        if ($modal.length) {
            $modal.modal('hide');
        }

        window.setTimeout(resetBackdrop, 300);
    };

    window.showModal = function () {
        window.showAdminModal('myModal');
    };

    window.showModal1 = function () {
        window.showAdminModal('DivPhotolarge');
    };

    window.Closepopup = function () {
        window.closeAdminModal('myModal');
    };

    function hookUpdatePanelModals() {
        if (typeof Sys === 'undefined' || !Sys.WebForms || !Sys.WebForms.PageRequestManager) {
            return;
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm._adminModalHooked) {
            return;
        }

        prm._adminModalHooked = true;
        prm.add_endRequest(function () {
            var seen = {};
            $('.modal[id]').each(function () {
                var id = this.id;
                if (!id || seen[id]) {
                    $(this).remove();
                    return;
                }
                seen[id] = true;
            });

            var $container = getModalContainer();
            $('.modal.in').each(function () {
                var $modal = $(this);
                if (!$modal.parent().is($container)) {
                    $modal.appendTo($container);
                }
            });
        });
    }

    $(hookUpdatePanelModals);
    if (typeof Sys !== 'undefined' && Sys.Application) {
        Sys.Application.add_load(hookUpdatePanelModals);
    }
})(jQuery);
