function uploadFile() {
    var fileInput = document.getElementById('fileInput');
    var file = fileInput.files[0];

    var formData = new FormData();
    formData.append('file', file);

    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/YourController/UploadFile'); // Replace with your controller and action method names
    xhr.onload = function () {
        if (xhr.status === 200) {
            console.log('File uploaded successfully!');
        } else {
            console.log('Error occurred while uploading the file.');
        }
    };
    xhr.send(formData);
}