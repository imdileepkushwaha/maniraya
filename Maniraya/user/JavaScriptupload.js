
     $(function () {
         $('#example1').DataTable()
         $('#example2').DataTable({
             'paging': true,
             'lengthChange': false,
             'searching': false,
             'ordering': true,
             'info': true,
             'autoWidth': false
         })
     })


function Uploadimageofsign() {
    var fileUpload = $('#<%=ImageUpload.ClientID %>').get(0);
    var files = fileUpload.files;

    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append($('#<%=HdFiled.ClientID%>').val() + files[0].name, files[0]);
        ($('#<%=HDFilename.ClientID%>').val($('#<%=HdFiled.ClientID%>').val() + files[0].name));

    }

    $.ajax({
        url: "UploadImage.ashx",
        type: "POST",
        data: data,
        contentType: false,
        processData: false,
        success: function (result) { },
        error: function (err) {
            //alert(err.statusText)  
        }
    });
    // alert('../ProductImage/' + ($('#<%=HDFilename.ClientID%>').val()));


    document.getElementById("<%=LblMsg.ClientID%>").innerHTML = 'file save successfullly';
    // $("#ImgPhoto").attr('src', '../ProductImage/'+($('#<%=HDFilename.ClientID%>').val()));
}
