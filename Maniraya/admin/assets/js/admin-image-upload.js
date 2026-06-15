(function (window, document) {
    'use strict';

    var SLOT_SELECTOR = '.admin-product-image-slot, .admin-category-image-compact';
    var QR_SELECTOR = '.admin-qr-upload-card';

    function isImageFile(file) {
        return !!(file && file.type && file.type.indexOf('image/') === 0);
    }

    function isPlaceholderUrl(url) {
        if (!url) {
            return true;
        }
        return url === '../ProductImage/' || url.indexOf('images.png') !== -1;
    }

    function getSlotParts(slotEl) {
        if (!slotEl) {
            return null;
        }

        var previewBox = slotEl.querySelector('.admin-product-image-preview-box, .admin-category-image-compact-preview');
        var placeholder = previewBox && previewBox.querySelector('.admin-product-image-placeholder');
        var img = previewBox && previewBox.querySelector('.admin-product-image-preview-img');
        var dropzone = slotEl.querySelector('.admin-product-image-dropzone');
        var upload = dropzone && dropzone.querySelector('input[type="file"]');
        var filename = dropzone && dropzone.querySelector('.admin-product-image-filename');

        return {
            slotEl: slotEl,
            previewBox: previewBox,
            placeholder: placeholder,
            img: img,
            dropzone: dropzone,
            upload: upload,
            filename: filename
        };
    }

    function getQrParts(cardEl) {
        var previewBox = cardEl.querySelector('.admin-qr-preview-box');
        var placeholder = previewBox && previewBox.querySelector('.admin-qr-placeholder');
        var img = previewBox && previewBox.querySelector('.admin-qr-preview-img');
        var dropzone = cardEl.querySelector('.admin-qr-dropzone');
        var upload = dropzone && dropzone.querySelector('input[type="file"]');
        var filename = cardEl.querySelector('.admin-qr-filename');

        return {
            slotEl: cardEl,
            previewBox: previewBox,
            placeholder: placeholder,
            img: img,
            dropzone: dropzone,
            upload: upload,
            filename: filename
        };
    }

    function setPreviewVisible(parts, visible) {
        if (!parts || !parts.img) {
            return;
        }

        if (visible) {
            parts.img.classList.add('is-visible');
            parts.img.style.display = 'block';
        } else {
            parts.img.classList.remove('is-visible');
            parts.img.style.display = 'none';
            parts.img.removeAttribute('src');
        }

        if (parts.placeholder) {
            parts.placeholder.style.display = visible ? 'none' : 'flex';
        }
    }

    function updateFilename(parts, file, currentLabel) {
        if (!parts || !parts.filename) {
            return;
        }

        if (file) {
            parts.filename.textContent = file.name;
            parts.filename.classList.add('has-file');
            parts.filename.removeAttribute('data-current');
            return;
        }

        if (currentLabel) {
            parts.filename.textContent = currentLabel;
            parts.filename.classList.add('has-file');
            parts.filename.setAttribute('data-current', '1');
            return;
        }

        var emptyText = parts.filename.getAttribute('data-empty-text') || 'No file selected';
        parts.filename.textContent = emptyText;
        parts.filename.classList.remove('has-file');
        parts.filename.removeAttribute('data-current');
    }

    function setSlotState(parts, hasFile) {
        if (!parts) {
            return;
        }

        if (parts.dropzone) {
            parts.dropzone.classList.toggle('has-file', hasFile);
        }
        if (parts.slotEl) {
            parts.slotEl.classList.toggle('has-file', hasFile);
        }
    }

    function resetPreview(parts) {
        if (!parts) {
            return;
        }

        setPreviewVisible(parts, false);
        setSlotState(parts, false);

        if (parts.upload) {
            parts.upload.value = '';
        }

        updateFilename(parts, null, null);
    }

    function showFilePreview(parts, file) {
        if (!parts) {
            return;
        }

        if (!isImageFile(file)) {
            resetPreview(parts);
            return;
        }

        var reader = new FileReader();
        reader.onload = function (ev) {
            if (parts.img) {
                parts.img.src = ev.target.result;
            }
            setPreviewVisible(parts, true);
            setSlotState(parts, true);
        };
        reader.readAsDataURL(file);
        updateFilename(parts, file, null);
    }

    function showUrlPreview(parts, url, currentLabel) {
        if (!parts) {
            return;
        }

        if (isPlaceholderUrl(url)) {
            resetPreview(parts);
            return;
        }

        if (parts.upload) {
            parts.upload.value = '';
        }

        if (parts.img) {
            parts.img.src = url;
        }

        setPreviewVisible(parts, true);
        setSlotState(parts, true);
        updateFilename(parts, null, currentLabel || 'Current image');
    }

    function assignFileToInput(upload, file) {
        try {
            var dt = new DataTransfer();
            dt.items.add(file);
            upload.files = dt.files;
            return true;
        } catch (ex) {
            return false;
        }
    }

    function bindDragDrop(parts) {
        if (!parts || !parts.dropzone || parts.dropzone._adminDragBound) {
            return;
        }

        parts.dropzone._adminDragBound = true;

        ['dragenter', 'dragover'].forEach(function (name) {
            parts.dropzone.addEventListener(name, function (e) {
                e.preventDefault();
                e.stopPropagation();
                parts.dropzone.classList.add('is-dragover');
            });
        });

        ['dragleave', 'drop'].forEach(function (name) {
            parts.dropzone.addEventListener(name, function (e) {
                e.preventDefault();
                e.stopPropagation();
                parts.dropzone.classList.remove('is-dragover');
            });
        });

        parts.dropzone.addEventListener('drop', function (e) {
            var file = e.dataTransfer && e.dataTransfer.files ? e.dataTransfer.files[0] : null;
            if (!file || !parts.upload) {
                return;
            }
            if (assignFileToInput(parts.upload, file)) {
                showFilePreview(parts, file);
            }
        });
    }

    function bindUpload(parts) {
        if (!parts || !parts.upload || parts.upload._adminUploadBound) {
            return;
        }

        parts.upload._adminUploadBound = true;
        parts.upload.addEventListener('change', function (e) {
            var file = e.target.files && e.target.files[0];
            if (file) {
                showFilePreview(parts, file);
            } else {
                resetPreview(parts);
            }
        });

        bindDragDrop(parts);
    }

    function hydrateExistingPreview(parts) {
        if (!parts || !parts.img) {
            return;
        }

        var src = parts.img.getAttribute('src');
        if (!src || isPlaceholderUrl(src)) {
            return;
        }

        setPreviewVisible(parts, true);
        setSlotState(parts, true);

        if (parts.filename && parts.filename.getAttribute('data-current') === '1') {
            parts.filename.classList.add('has-file');
        }
    }

    function initSlots() {
        var slots = document.querySelectorAll(SLOT_SELECTOR);
        var i;

        for (i = 0; i < slots.length; i++) {
            var slotParts = getSlotParts(slots[i]);
            bindUpload(slotParts);
            hydrateExistingPreview(slotParts);
        }

        var qrCards = document.querySelectorAll(QR_SELECTOR);
        for (i = 0; i < qrCards.length; i++) {
            var qrParts = getQrParts(qrCards[i]);
            bindUpload(qrParts);
            hydrateExistingPreview(qrParts);
        }
    }

    function resolveSlotElement(slotRef) {
        if (!slotRef) {
            return null;
        }
        if (typeof slotRef === 'string') {
            return document.getElementById(slotRef);
        }
        if (slotRef.slotId) {
            return document.getElementById(slotRef.slotId);
        }
        if (slotRef.nodeType === 1) {
            return slotRef;
        }
        return null;
    }

    function getPartsForSlot(slotRef) {
        var el = resolveSlotElement(slotRef);
        if (!el) {
            return null;
        }
        if (el.classList.contains('admin-qr-upload-card')) {
            return getQrParts(el);
        }
        return getSlotParts(el);
    }

    var AdminImageUpload = {
        init: initSlots,
        reset: function (slotRef) {
            resetPreview(getPartsForSlot(slotRef));
        },
        setUrlPreview: function (slotRef, url, label) {
            showUrlPreview(getPartsForSlot(slotRef), url, label);
        }
    };

    window.AdminImageUpload = AdminImageUpload;

    window.syncSlotPreview = function (slotRef, imageUrl, currentLabel) {
        AdminImageUpload.setUrlPreview(slotRef, imageUrl, currentLabel);
    };

    window.syncSliderImagePreview = function (imageUrl) {
        AdminImageUpload.setUrlPreview('sliderImageSlot1', imageUrl, 'Current image');
    };

    window.syncGalleryImagePreview = function (imageUrl) {
        AdminImageUpload.setUrlPreview('galleryImageSlot1', imageUrl, 'Current image');
    };

    window.syncEditProductImages = function (url1, url2, url3, url4) {
        var urls = [url1, url2, url3, url4];
        var ids = ['editImageSlot1', 'editImageSlot2', 'editImageSlot3', 'editImageSlot4'];
        var index;

        for (index = 0; index < ids.length; index++) {
            AdminImageUpload.setUrlPreview(ids[index], urls[index] || '', 'Current image');
        }
    };

    function scheduleInit() {
        initSlots();
    }

    if (window.Sys && Sys.Application) {
        Sys.Application.add_load(scheduleInit);
        if (Sys.WebForms && Sys.WebForms.PageRequestManager) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (!prm._adminImageUploadHooked) {
                prm._adminImageUploadHooked = true;
                prm.add_endRequest(scheduleInit);
            }
        }
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', scheduleInit);
    } else {
        scheduleInit();
    }
})(window, document);
