var notid = -1;//notid undefined olur. tarih bilgisi güncelleme işlemi düzeltildi
$(function () {
    //modal açıldıktan sonraki işlemler...
    $('#Modal1').on('show.bs.modal', function (e) {//e=> buton bilgilerini tutar
        var button = $(e.relatedTarget);
        notid = button.data("notid");
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

                    alert("siliniyor")
                    $('#Modal1_body').load("/Yorum/YorumGoster/" + notid);
                    alert("silindi")

                }
                else {
                    alert("Yorum Düzenlenemedi")
                }
            }).fail(() => {
                alert("İşlem başarısız")
            })
        }
    }
    else if (islem === "delete") {
        var mesaj = confirm("Silmet istediğinize emin misiniz?");
        if (!mesaj) {
            return false;
        }
        $.ajax({
            method: "GET",
            url: "/Yorum/Delete/" + yorumid,
        }).done((data) => {
            if (data.sonuc) {
                $('#Modal1_body').load("/Yorum/YorumGoster/" + notid);
            }
            else {
                alert("Yorum Silinemedi");
            }
        }).fail( () => {
            alert("İşlem başarısız")
        });

    }
    else if (islem === "create") {
        var yorum = $("#yorum_text_id").val();
        $.ajax({
            method: "POST",
            url: "/Yorum/Create",
            data: {
                Text: yorum,
                noteid: notid
            }
        }).done((data) => {
            if (data.sonuc) {
                $('#Modal1_body').load("/Yorum/YorumGoster/" + notid);
            }
        }).fail(() => {
            alert("İşlem başarısız");
        })
    }
}
