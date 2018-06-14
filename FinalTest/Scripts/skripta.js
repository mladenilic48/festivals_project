$(document).ready(function () {

    var host = window.location.host;
    var token = null;
    var headers = {};
    var festivaliEndpoint = "/api/festival/";

    // Priprema dogadjaja
    $("body").on("click", "#btnDeleteFestival", deleteFestival);


    $(window).load(function () {

        var requestURL = "http://" + host + festivaliEndpoint;
        console.log("URL zahteva:" + requestURL);

        $.getJSON(requestURL, function (data, status) {
            var bodyFestivalTable = $("#tableBodyFestivali");
            bodyFestivalTable.empty();

            if (status == "success") {
                for (i = 0; i < data.length; i++) {
                    var tr = $('<tr></tr>');

                    var td1 = $('<td></td>');
                    td1.text(data[i].Naziv);
                    tr.append(td1);

                    var td2 = $('<td></td>');
                    td2.text(data[i].Mesto.Naziv);
                    tr.append(td2);

                    var td3 = $('<td></td>');
                    td3.text(data[i].GodinaPrvogOdrzavanja);
                    tr.append(td3);

                    var td4 = $('<td></td>');
                    td4.text(data[i].CenaKarte);
                    tr.append(td4);

                    bodyFestivalTable.append(tr);
                }

                $("#prijava").css("display", "block");
                $("#registracija").css("display", "none");
                $("#kadaJePrijavljen").css("display", "none");

            } else {
                alert("Neuspesno ocitani podaci o festivalima.");
            }
        });

    });


    // prijava korisnika
    $("#frmPrijava").submit(function (e) {
        e.preventDefault();

        var email = $("#prijavaKorisnickoIme").val();
        var loz = $("#prijavaLozinka").val();

        // objekat koji se salje
        var sendData = {
            "grant_type": "password",
            "username": email,
            "password": loz
        };

        var requestURL = "http://" + host + "/Token";
        console.log("URL zahteva:" + requestURL);

        $.ajax({
            "type": "POST",
            "url": requestURL,
            "data": sendData

        }).done(function (data) {
            console.log(data);
            token = data.access_token;

            $("#ispisPrijavljenKorisnik").empty().append("Prijavljen korisnik: " + data.userName);
            $("#prijava").css("display", "none");
            $("#tblPrikazFestivala").css("display", "none");
            $("#kadaJePrijavljen").css("display", "block");

            //ucitavanje festivala
            var requestURL = "http://" + host + festivaliEndpoint;
            console.log("URL zahteva:" + requestURL);

            if (token) {
                headers.Authorization = 'Bearer ' + token;
            }

            $.ajax({
                "type": "GET",
                "url": requestURL,
                "headers": headers

            }).done(extendTabele())

                .fail(function (data) {
                    alert("Greska prililom ucitavanja festivala!" + data.statusText);
                });


        }).fail(function (data) {
            alert("Greska prilikom prijave korisnika!\n" + data.statusText);
        });
    });


    function extendTabele(data, status) {
        var bodyFestivalTable = $("#tableBodyFestivaliExtend");
        bodyFestivalTable.empty();

        if (status == "success") {
            for (i = 0; i < data.length; i++) {

                var stringId = data[i].Id.toString();

                var tr = $('<tr></tr>');

                var td1 = $('<td></td>');
                td1.text(data[i].Naziv);
                tr.append(td1);

                var td2 = $('<td></td>');
                td2.text(data[i].Mesto.Naziv);
                tr.append(td2);

                var td3 = $('<td></td>');
                td3.text(data[i].GodinaPrvogOdrzavanja);
                tr.append(td3);

                var td4 = $('<td></td>');
                td4.text(data[i].CenaKarte);
                tr.append(td4);

                var td5 = $('<td><button id="btnDeleteFestival" name="' + stringId + '" class="btn btn-danger" >Delete</button></td>');
                tr.append(td5);

                bodyFestivalTable.append(tr);
            }


        } else {
            alert("Neuspesno ocitani podaci o festivalima nakon prijave.");
        }
    };



    // odjava korisnika sa sistema
    $("#btnOdjava").click(function () {
        token = null;
        headers = {};

        $("#prijava").css("display", "block");
        $("#kadaJePrijavljen").css("display", "none");
        $("#tblPrikazFestivala").css("display", "block");
    })


    //Registracija
    $("#btnRegistracija").click(function () {
        $("#prijava").css("display", "none");
        $("#registracija").css("display", "block");
    });

    // registracija korisnika
    $("#frmRegistracija").submit(function (e) {
        e.preventDefault();

        var korisnickoIme = $("#registracijaKorisnickoIme").val();
        var loz1 = $("#registracijaLozinka").val();
        var loz2 = $("#registracijaPonoviLozinku").val();

        // objekat koji se salje
        var sendData = {
            "Email": korisnickoIme,
            "Password": loz1,
            "ConfirmPassword": loz2
        };

        var requestURL = "http://" + host + "/api/Account/Register";
        console.log("URL zahteva:" + requestURL);

        $.ajax({
            type: "POST",
            url: requestURL,
            data: sendData

        }).done(function (data) {
            $("#prijava").css("display", "block");
            $("#registracija").css("display", "none");

        }).fail(function (data) {
            alert("Greska prilikom registracije!\n" + data.statusText);
        });


    });


    // Brisanje festivala
    function deleteFestival() {
        // Izvalcimo Id
        var deleteId = this.name;

        var requestUrl = "http://" + host + festivaliEndpoint + deleteId;
        console.log("URL za brisanje: " + requestUrl);

        $.ajax({
            url: requestUrl,
            type: "DELETE",
        })
            .done(function (data, status) {
                var requestURL = "http://" + host + festivaliEndpoint;
                console.log("URL zahteva:" + requestURL);
                $.getJSON(requestURL, extendTabele());
            })
            .fail(function (data, status) {
                alert("Greska prilikom brisanja festivala!")
            });
    };





    // ucitavanje prvog proizvoda
    $("#proizvodi").click(function () {
        // korisnik mora biti ulogovan
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            "type": "GET",
            "url": "http://" + host + "/api/products/1",
            "headers": headers

        }).done(function (data) {
            $("#sadrzaj").append("Proizvod: " + data.Name);

        }).fail(function (data) {
            alert(data.status + ": " + data.statusText);
        });


    });



});