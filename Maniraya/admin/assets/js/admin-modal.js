/**
 * Bootstrap 3 modals with ASP.NET UpdatePanel async postbacks.
 * Moves modal to <body> so it stacks above the backdrop.
 */
(function ($) {
    'use strict';

    function resetBackdrop() {
        $('body').removeClass('modal-open').css('padding-right', '');
        $('.modal-backdrop').remove();
    }

    window.showAdminModal = function (modalId) {
        modalId = modalId || 'myModal';

        window.setTimeout(function () {
            resetBackdrop();

            // Close mobile sidebar so it does not compete with the modal layer
            document.body.classList.remove('admin-sidebar-open');

            // Remove stale modal copies left on body from earlier opens
            $('body > #' + modalId).remove();

            var $modal = $('#' + modalId).last();
            if (!$modal.length) {
                return;
            }

            if ($modal.data('bs.modal')) {
                $modal.data('bs.modal', null);
            }

            // Escape UpdatePanel stacking context — backdrop is on body
            if (!$modal.parent().is('body')) {
                $modal.appendTo('body');
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

    window.Closepopup = function () {
        window.closeAdminModal('myModal');
    };
})(jQuery);
