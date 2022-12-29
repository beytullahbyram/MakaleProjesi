
var notid = -1;//notid undefined olur. tarih bilgisi güncelleme işlemi düzeltildi
$(function () {
    $('#Modal2').on('show.bs.modal', function (e) {//e=> buton bilgilerini tutar
        var button = $(e.relatedTarget);
        notid = button.data("notid");
        $('#Modal2_body').load("/Makaleler/DevamGoster/" + notid);//Action partial page döner
    });
});