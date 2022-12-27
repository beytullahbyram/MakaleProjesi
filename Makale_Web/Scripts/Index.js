$(function () {
    //modal açıldıktan sonraki işlemler...
    $('#Modal1').on('show.bs.modal', function (e) {//e=> buton bilgilerini tutar
        var button = $(e.relatedTarget);
        var notid = button.data("notid");
        //modal1_body'ye yükle => yorumgoster action
        $('#Modal1_body').load("/Yorum/YorumGoster/" + notid);//Action partial page döner
    });
});
function yorumislem(btn, islem, yorumid, yorumtext) {
    var button = $(btn);
    var editmod = button.data("edit");
    if (islem === "update") {
        if (!editmod) {
            button.data("edit", true);
            button.removeClass("btn-warning");
            button.addClass("btn-success");
            var span = button.find("tspan");
            span.removeClass("glyphicon-edit");
            span.addClass("glyphicon-ok");
            $(yorumtext).attr("contenteditable", true);
        } else {
            button.data("edit", false);
            button.removeClass("btn-success");
            button.addClass("btn-warning");
            var span = button.find("tspan");
            span.removeClass("glyphicon-ok");
            span.addClass("glyphicon-edit");
            $(yorumtext).attr("contenteditable", false);
            var yenitxt = $(yorumtext).text();
            $.ajax({
                method: "POST",
                url: "/Yorum/Edit/" + yorumid,
                data: { text: yenitxt }//text parametresini actionda alıcağız
            }).done(function (data) {
                if (data.sonuc) {
                    $('#modal1').load("/Yorum/YorumGoster/" + notid);
                }
                else {
                    alert("Yorum Düzenlenemedi")
                }
            }).fail(function () {
                alert("İşlem başarısız")
            })
        }
    }
}