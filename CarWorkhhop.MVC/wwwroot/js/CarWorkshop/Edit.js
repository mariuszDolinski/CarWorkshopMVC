//upewniamy si�, �e dokument jest dost�pny, je�li tak funkcja function b�dzie wykonana
$(document).ready(function () {
    //implememntacja metod przeniesiona do pliku site.js mozna wydzielli� do oddzielnego 
    //pliku w folderze carWorkshop gdyby takich metod by�o du�o
    LoadCarWorkshopServices()

    //nadpisujemy to co zostanie wykonane po klikni�ciu w Ok(przycisk typu submit)
    $("#createCarWorkshopServiceModal form").submit(function (event) {
        event.preventDefault(); //zatrzymujemy domy�lne wykonanie akcji po submit

        $.ajax({//this to nasz formularz
            url: $(this).attr('action'), //podajemy na jaki adres b�dzie wyslane zapytanie poprzez atrybut action
            type: $(this).attr('method'), //przekazujemy typ akcji, u nas HttpPost
            data: $(this).serialize(), //serialize serializuje wszystkie inputy z formularza i wysy�a na backend
            success: function (data) { //info zwrotne na podstawie zwracanych w kontrolerze kod�w (badrequest lub ok)
                toastr["success"]("Created carworkshop service")
                LoadCarWorkshopServices()
            },
            error: function (xhr, status, error) {
                console.log(xhr);
                toastr["error"]("Something went wrong")
            }

        })
    });
});