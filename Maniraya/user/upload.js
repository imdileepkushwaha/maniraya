document.getElementById("uploadBtn").addEventListener("click", function () {
    var fileInput = document.getElementById("fileInput");
    if (fileInput.files.length === 0) {
        alert("Please select a file.");
        return;
    }

    var file = fileInput.files[0];
    var reader = new FileReader();

    reader.onload = function (e) {
        var base64File = e.target.result.split(',')[1];  // Get the base64 string without the data URL
        var fileName = file.name;

        // Send the base64 file to the server
        uploadFileToServer(base64File, fileName);
    };

    reader.readAsDataURL(file);
});

function uploadFileToServer(base64File, fileName) {
    // Make AJAX call to server
    fetch("UploadForm.aspx/UploadFile", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            base64File: base64File,
            fileName: fileName
        })
    })
    .then(response => response.json())
    .then(data => {
        alert(data.d);  // Show the server response
})
.catch(error => {
    console.error("Error:", error);
alert("An error occurred during the file upload.");
});
}
