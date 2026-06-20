<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Treeviewdetail.aspx.cs" Inherits="Treeviewdetail" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Advanced Tree View</title>

    <!-- Bootstrap -->
 <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

     <style>
        .tree { text-align: center; }

        .tree ul {
            padding-top: 20px;
            position: relative;
            display: flex;
            justify-content: center;
        }

        .tree li {
            list-style-type: none;
            position: relative;
            padding: 20px 5px 0 5px;
            text-align: center;
        }

        /* connector lines */
         /* connector lines */
        .tree li::before, .tree li::after {
            content: '';
            position: absolute;
            top: 0;
            right: 50%;
            border-top: 2px solid #ccc;
            width: 50%;
            height: 20px;
        }

        .tree li::after {
            right: auto;
            left: 50%;
            border-left: 2px solid #ccc;
        }

        .tree li:only-child::after, .tree li:only-child::before {
            display: none;
        }

        .tree li:only-child { padding-top: 0; }

        .tree li:first-child::before, .tree li:last-child::after {
            border: 0 none;
        }

         .tree li:last-child::before {
            border-right: 2px solid #ccc;
            border-radius: 0 5px 0 0;
        }

        .tree li:first-child::after {
            border-radius: 5px 0 0 0;
        }

         .tree ul ul::before {
            content: '';
            position: absolute;
            top: 0;
            left: 50%;
            border-left: 2px solid #ccc;
            width: 0;
            height: 20px;
        }

          /* node box */
        .node {
            display: inline-block;
            padding: 10px;
            border-radius: 10px;
            border: 1px solid #ccc;
            background: #f8f9fa;
            min-width: 120px;
            cursor: pointer;
        }

        .node img {
            width: 40px;
            height: 40px;
            border-radius: 50%;
            margin-bottom: 5px;
        }
    </style>

   
</head>
<body>
    <form id="form1" runat="server" class="container mt-4">
        <asp:HiddenField ID="hiddenParentId" runat="server" />
        <div class="tree">
        <ul id="treeRoot"></ul>
    </div>
    </form>

<script>

    // Load root nodes
    $(document).ready(function () {
        let parentId = $('#<%= hiddenParentId.ClientID %>').val();
        loadTree(parentId, $('#treeRoot'));
   });

    // Expand / Collapse + Lazy Load
    //$(document).on('click', '.node', function (e) {
    //    e.stopPropagation();

    //    let parentLi = $(this).closest('li');
    //    let childUl = parentLi.children('ul');
    //    let userId = parentLi.data('id');
    //    let toggle = $(this).find('.toggle');

    //    if (childUl.length > 0) {
    //        childUl.toggle();
    //        toggle.text(childUl.is(':visible') ? '-' : '+');
    //    } else {
    //        let ul = $('<ul></ul>');
    //        parentLi.append(ul);
    //        loadChildren(userId, ul);
    //        toggle.text('-');
    //    }
    //});

    // AJAX loader
    function loadTree(parentId, container) {
        $.ajax({
            url: 'Treeviewdetail.aspx/GetChildren',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ parentId: parentId }),
            success: function (res) {
                let data = res.d;

                $.each(data, function (i, item) {

                    let li = $('<li></li>');

                    let node = $(`
                    <div class="node">
                        <img src="${item.photoimage}" />
                        <div>${item.username}</div>
                        <small>${item.status}</small>
                          $<small>${item.topupamount}</small>
                    </div>
                `);

                    let childUL = $('<ul style="display:none"></ul>');

                    node.click(function (e) {
                        e.stopPropagation();

                        if (childUL.children().length === 0) {
                            loadTree(item.userid, childUL);
                        }

                        childUL.toggle();
                    });

                    li.append(node);
                    li.append(childUL);
                    container.append(li);
                });
            },
            error: function (err) {
                console.log(err.responseText);
            }
        });
    }

</script>


 



</body>
</html>


