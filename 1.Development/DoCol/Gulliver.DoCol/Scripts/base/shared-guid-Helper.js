var OnLoadSetWindow = function (arg) {
    var dataLayoutShared = '<div id="hidden-tab"><input type="hidden" id="hfldUniqueTabSession" /></div>';
    $('body').append(dataLayoutShared);

    //check exist control
    var idGui = $('#hfldUniqueTabSession');
    if (idGui.length) {
        idGui.val(arg);
        setTabIdToFormsOnLoad(idGui.val());
    }
}

function setTabIdToFormsOnLoad(tabVal) {
    $('form').each(function () {
        var iHidden = $(this).find($(':hidden#hfldUniqueTabSession'));
        if (iHidden.length) {
            iHidden.val(tabVal);
        }
        else {
            $('<input>').attr({
                type: 'hidden',
                id: 'hfldUniqueTabSession',
                name: 'hfldUniqueTabSession',
                value: tabVal
            }).appendTo($(this));
        }
    });
}

function queryParameters(a) {
    if (a == "") return {};
    a = a.split('?')[1];
    a = a.split('&');
    var b = {};
    for (var i = 0; i < a.length; ++i) {
        var p = a[i].split('=');
        if (p.length != 2) continue;
        b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
    }
    return b;
};

function submitFakePost(url, qs, val) {
    var my_form = document.createElement('FORM');
    my_form.name = 'multiTabForm';
    my_form.method = 'POST';
    my_form.action = url;

    // insert hidden ID value
    var my_val = document.createElement('INPUT');
    my_val.type = 'hidden';
    my_val.name = 'hfldUniqueTabSession';
    my_val.value = val;
    my_form.appendChild(my_val);

    // IE8 compability
    if (!Object.keys) {
        Object.keys = function (obj) {
            var keys = [];

            for (var i in obj) {
                if (obj.hasOwnProperty(i)) {
                    keys.push(i);
                }
            }

            return keys;
        };
    }

    // insert all parameters
    var key = [];
    var qsLength = Object.keys(qs).length;

    for (i = 0; i < qsLength; i++) {
        var my_tb = document.createElement('INPUT');
        my_tb.type = 'hidden';
        my_tb.name = Object.keys(qs)[i];
        my_tb.value = qs[my_tb.name];
        my_form.appendChild(my_tb);
    }
    document.body.appendChild(my_form);
    my_form.submit();
}

$(function () {
    $(".linkbuttonsubmit").click(function (e) {
        e.preventDefault();
        var val = $("#hfldUniqueTabSession").val();
        submitFakePost(this.href, queryParameters(this.href), $("#hfldUniqueTabSession").val());
    });
});