//upewniamy siê, ¿e dokument jest dostêpny, jeœli tak funkcja function bêdzie wykonana
$(document).ready(function () {
    //implememntacja metod przeniesiona do pliku site.js mozna wydzielliæ do oddzielnego 
    //pliku w folderze carWorkshop gdyby takich metod by³o du¿o
    LoadCarWorkshopServices()

    //nadpisujemy to co zostanie wykonane po klikniêciu w Ok(przycisk typu submit)
    $("#createCarWorkshopServiceModal form").submit(function (event) {
        event.preventDefault(); //zatrzymujemy domyœlne wykonanie akcji po submit

        $.ajax({//this to nasz formularz
            url: $(this).attr('action'), //podajemy na jaki adres bêdzie wyslane zapytanie poprzez atrybut action
            type: $(this).attr('method'), //przekazujemy typ akcji, u nas HttpPost
            data: $(this).serialize(), //serialize serializuje wszystkie inputy z formularza i wysy³a na backend
            success: function (data) { //info zwrotne na podstawie zwracanych w kontrolerze kodów (badrequest lub ok)
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