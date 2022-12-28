const { data } = require("jquery");

$(function () {
    alert("1");
    var notid_dizi = [];

    $("div[data-notid]").each(function (i, e) {
        notid_dizi.push($(e).data("notid"));
    });

    $.ajax({
        method: "POST",
        url: "/Makaleler/Begeni",
        data: { arr: notid_dizi }
    }).done(function (data) {
        alert("done")
        if (data.sonuc != null && data.sonuc.length > 0) {
            alert("ifffff");
            for (var i = 0; i < data.sonuc.length; i++) {
                var id = data.sonuc[i];
                var div = $("div[data-notid=" + id + "]");
                var btn = div.find("button[data-like]");
                btn.data("like", true);
                var span = btn.children().first();
                span.removeClass("glyphicon-heart-empty");
                span.addClass("glyphicon-heart");
            }
        }
    }).fail(function () {
        alert("faill")
    });

    $("button[data-like]").click(function () {
        var btn = $(this);
        var btnlike = btn.data("like");
        var btnnotid = btn.data("notid");
        var spankalp = btn.find("span.like");
        var spanlikesayi = btn.find("span.begenisayisi");

        $.ajax({
            method: "POST",
            url: "/Makaleler/LikeDuzenle",
            data: {
                like=!btnlike,
                notid=btnnotid
            }
        }).done((data) => {

        });
    });
});


