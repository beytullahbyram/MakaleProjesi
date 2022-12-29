$(function () {
    var notid_dizi = [];

    $("div[data-notid]").each(function (i, e) {
        notid_dizi.push($(e).data("notid"));
    });
    $.ajax({
        method: "POST",
        url: "/Makaleler/Begeni",
        data: { arr: notid_dizi }
    }).done(function (data) {
        if (data.sonuc != null && data.sonuc.length > 0) {
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
  


        var btn = $(this);//butona ulaştık.
        var btnlike = btn.data("like");//bool türündedir.
        var btnnotid = btn.data("notid");//makalenin id değeri tutulur.
        var spankalp = btn.find("span.like");//kalp butonunu değiştirmek için
        var spanlikesayi = btn.find("span.begenisayisi");//beğeni saysını değiştirmek için

        $.ajax({
            method: "POST",
            url: "/Makaleler/LikeDuzenle",
            data: {
                like: !btnlike,
                notid: btnnotid
            }
        }).done((data) => {
            if (data.hata) {
                alert("Beğeni işlemi gerçekleşmedi");
                if (data.res == 0) {
                    window.location.href = '/Home/Login'
                }
            }
            else {
                btnlike = !btnlike; //butonun bool değerini değiştiriyoruz çünkü değerin tersini yolluyoruz controllera ama onu almıyoruz o yüzden burda değerini değiştirdik
                btn.data("like", btnlike);
                spanlikesayi.text(data.res);

                spankalp.removeClass("glyphicon-heart-empty");
                spankalp.removeClass("glyphicon-heart");

                if (btnlike) {
                    spankalp.addClass("glyphicon-heart");
                }
                else {
                    spankalp.addClass("glyphicon-heart-empty");
                }
            }
        }).fail(() => {
            alert("Beğeni işlemi sırasında sunucu bağlantısı başarısız oldu Login sayfasına yönlendiriliyorsunuz");
            //$(location).attr('href', 'https://localhost:44315/Home/Login');
            window.location.href='/Home/Login'

        });
    });
});