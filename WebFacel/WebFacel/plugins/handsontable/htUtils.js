
var tableCellPositive = function (value, callback) {
    if (!value || String(value).length === 0) {
        callback(false);
    } else {
        var valInt = parseInt(value);

        if (valInt > 0) {
            callback(true);
        } else {
            callback(false);
        }
    }
};

function tableCellImage(instance, td, row, col, prop, value, cellProperties) {
    var args = (value);

    if (args == null) { return false; }

    if (args.id == null) {
        //return false;        
        args.id = 0;
        args.type = "link";
    }

    if (args.type == "link") {
        var lnk = document.createElement("a");
        var img = document.createElement('IMG');

        img.src = "img/deleteIcon.jpg";
        lnk.id = "link_" + args.id;
        lnk.href = "#";
        lnk.value = args.id;
        lnk.title = "Permite eliminar el registro respectivo";
        lnk.appendChild(img);
        td.style.textAlign = "center";

        Handsontable.Dom.addEvent(lnk, 'click', function (e) {
            var vsid = this.id;

            vsid = vsid.replace("link_", "");
            alert("Eliminando registro " + vsid);

            return true;
        });

        Handsontable.Dom.empty(td);
        td.appendChild(lnk);
    } else {
        Handsontable.renderers.TextRenderer.apply(this, arguments);
    }

    return td;
};

function setCellSource(row, col, newValue) {
    var cellProp = ht.getCellMeta(row, col);

    cellProp.source = newValue;
    ht.render(); //Metodo necesario cuando se cambia el METADATA
};

function setCellType(row, col, newValue) {
    var cellProp = ht.getCellMeta(row, col);

    cellProp.type = newValue;
    ht.render(); //Metodo necesario cuando se cambia el METADATA
};

function setCellRowCol(row, col) {
    $("#txtRow").val(row);
    $("#txtCol").val(col);
};
