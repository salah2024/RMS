function TreeView(Data, plugin) {
    $('#jstree').jstree('destroy');
    $('#jstree').jstree({
        'plugins': plugin,
        'core': {
            //'check_callback': true,
            'data': Data,
            'animation': false,
            //'expand_selected_onload': true,
            'themes': {
                'icons': false,
            }
        },
        'search': {
            'show_only_matches': true,
            'show_only_matches_children': true
        }
    });

    //$("#jstree").jstree(true).refresh();

    $('#search').on("keyup change", function () {
        $('#jstree').jstree(true).search($(this).val())
    })

    $('#clear').click(function (e) {
        $('#search').val('').change().focus()
    })

    $('#jstree').on('changed.jstree', function (e, data) {
        var objects = data.instance.get_selected(true)
        var leaves = $.grep(objects, function (o) { return data.instance.is_leaf(o) })
        var list = $('#output')
        list.empty()
        $.each(leaves, function (i, o) {
            $('<li/>').text(o.text).appendTo(list)
        })
    });
}


            var gParents = [];
function createTree(data, parentId) {
    var html = "";
    var children = data.filter(function (item) {
        return item.parentId == parentId;
    });

    if (children.length > 0) {
        children.forEach(function (item) {
            var id = item.id;
            var parentId = item.parentId;
            var operationName = item.operationName;
            var funcCall = item.functionCall.trim();
            var fbShomareh = item.itemsFBShomareh.trim();
            var sharh = item.sharh;

            if (fbShomareh === "") {
                if (funcCall !== "") {
                    html += "<li><a onclick=\"" + funcCall + "('" + id + "')\" id=\"a" + id + "\">" + operationName +
                        "<span id=\"span" + id + "\"></span></a>" +
                        "<span id=\"spanOpShomareh" + id + "\"></span>" +
                        "<ul style='margin-top:5px' id='ula" + id + "'></ul></li>";
                } else {
                    html += "<li><a id=\"a" + id + "\">" + operationName +
                        "<span id=\"span" + id + "\"></span></a>" +
                        "<span id=\"spanOpShomareh" + id + "\"></span>" +
                        "<ul id=\"ula" + id + "\">";
                    html += createTree(data, id); // Recursive call
                    html += "</ul></li>";
                }
            } else {
     //           if (!gParents.includes(parentId)) {
     //               if (gParents.length != 0) {
     //                   debugger;
     //                   html += `</tbody></table>`;
     //               }
     //               gParents.push(parentId);
     //               html += `
     //               <table border="1" style="width:100%; border-collapse: collapse;">
     //           <thead>
     //               <tr>
     //                   <th>شماره</th>
     //                   <th>شرح</th>
     //                   <th>عملیات</th>
     //                   <th>عملیات</th>
     //               </tr>
     //           </thead>
     //           <tbody>`;

     //           }

     //           html += `
     //               <tr>
     //               <td>${fbShomareh}</td>
     //               <td><a id="a${id}" onclick="OperationClick('${id}')">${sharh}</a></td>
     //               <td><span id="span${id}"></span></td>
     //               <td><span id="spanOpShomareh${fbShomareh}"></span></td>
     //               </tr>
					//`;

     //           html += "<tr><td><div id=\"ula" + id + "\" class=\"row\" style=\"display: none; margin:10px\">" +
     //               "<div id=\"uldiva" + id + "\" class=\"col-md-12\" style=\"border:1px solid #79c7ea;" +
     //               "padding-left:0px;padding-right:0px;text-align:center;border-radius:5px !important;\">" +
     //               "</div></div></td></tr>";

               

                



                html += "<li><a id=\"a" + id + "\" onclick=\"OperationClick('" + id + "')\">" +
                	fbShomareh + " - " + sharh + "<span id=\"span" + id + "\"></span></a>" +
                	"<span id=\"spanOpShomareh" + fbShomareh + "\"></span></li>";

                html += "<div id=\"ula" + id + "\" class=\"row\" style=\"display: none; margin:10px\">" +
                	"<div id=\"uldiva" + id + "\" class=\"col-md-12\" style=\"border:1px solid #79c7ea;" +
                	"padding-left:0px;padding-right:0px;text-align:center;border-radius:5px !important;\">" +
                	"</div></div>";
            }
        });
    }

    return html;
}
