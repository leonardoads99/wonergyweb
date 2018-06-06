$(document).ready(function () {

    function atualizarDefinicao() {
        $.ajax(
            {
                type: 'GET',
                url: '/Home/DashBoard',
                dataType: 'html',
                cache: false,
                async: true,
                success: function (data) {
                    $('linhaConsumo').html(data);
                }
            });
    }

    $(document).ready(function () {
        setInterval(atualizarDefinicao, 60000);
        //   $('#linhaConsumo').load("DashBoard.html");
    });

    $("#lineChart").hide();
    $("#barGrouped").hide();
    $("#pieChart").hide();
    $("#primeiroGrafico").hide();

    $("#btnH").click(function () {
        $("#barchart").hide();
        $("#lineChart").hide();
        $("#barGrouped").hide();
        $("#primeiroGrafico").show();
    });


    $("#btnD").click(function () {
        $("#barchart").hide();
        $("#lineChart").show();
        $("#barGrouped").hide();
        $("#primeiroGrafico").hide();
    });


    $("#btnM").click(function () {
        $("#barchart").show();
        $("#lineChart").hide();
        $("#barGrouped").hide();
        $("#primeiroGrafico").hide();
    });

    $("#btnY").click(function () {
        $("#barchart").hide();
        $("#lineChart").hide();
        $("#barGrouped").show();
        $("#primeiroGrafico").hide();
    });

    $.ajax({
        type: "GET",
        url: "/Home/AlimentarGraficoMes",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (chData) {

            //Grafico Mensal
            new Chart(document.getElementById("bar-chart"), {
                type: 'bar',
                data: {
                    labels: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
                    datasets: [
                        {
                            label: "Consumo geral do mês gasto",
                            backgroundColor: ["#00688B", "#00688B", "#00688B", "#00688B", "#00688B", "#00688B", "#00688B", "#00688B", "#00688B", "#00688B", "#00688B", "#00688B"],
                            data: chData
                        },
                        {
                            label: "Consumo Recomendado",
                            backgroundColor: ["#008B8B", "#008B8B", "#008B8B", "#008B8B", "#008B8B", "#008B8B", "#008B8B", "#008B8B", "#008B8B", "#008B8B", "#008B8B", "#008B8B"],
                            data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
                        }
                    ]
                },
                options: {
                    responsive: true,
                    legend: { display: true },
                    legend: { position: 'Top' },
                    title: { display: true },
                    tooltips: { enabled: true }
                },
            });
        }
    });

    //Grafico Anual
    $.ajax({
        type: "GET",
        url: "/Home/AlimentarGraficoAnual",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (chData) {
            new Chart(document.getElementById("bar-chart-grouped"), {
                type: 'bar',
                data: {
                    labels: ["2018", "2019", "2020", "2021"],

                    datasets: [
                        {
                            label: "Equipamento 1",
                            backgroundColor: "#3e95cd",
                            data: chData
                        }
                    ]
                },
                options: {
                    title: { display: true, text: 'Anual' }
                },
            });
        }
    });

    //Grafico Diario
    $.ajax({
        type: "GET",
        url: "/Home/AlimentarGraficoDiario",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (chData) {
            new Chart(document.getElementById("line-chart"), {
                type: 'line',
                data: {
                    labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30,31],
                    datasets: [{
                        data: chData,
                        label: ["Janeiro", "Fevereiro", "Marco", "Abril", "Maio", "Junho", "Julho", "Agosto", "Septembro", "Outubro", "Novembro", "Dezembro"],
                        borderColor: "#3e95cd",
                        fill: false
                    }]
                },
            });
        }
    });


    //Grafico Hora
    $.ajax({
        type: "GET",
        url: "/Home/AlimentarGraficoHora",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (chData) {
            new Chart(document.getElementById("primeiroGrafico"), {
                type: 'line',

                data: {
                    labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24],
                    datasets: [{
                        data: chData,
                        label: ["Horas"],
                        borderColor: "#3e95cd",
                        fill: false
                    }]
                },
            });
        }
    });







    //new Chart(document.getElementById("pie-chart"), {
    //    type: 'pie',
    //    data: {
    //        labels: ["Africa", "Asia", "Europe", "Latin America", "North America"],
    //        datasets: [{
    //            label: "Population (millions)",
    //            backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
    //            data: [2478, 5267, 734, 784, 433]
    //        }]
    //    },
    //    options: {
    //        title: {
    //            display: true,
    //            text: 'Predicted world population (millions) in 2050'
    //        }
    //    }
    //});


    //new Chart(document.getElementById("GraficoLine"), {
    //    type: 'graficoLine'

    //});

    //var ctx = document.getElementById("GraficoLine").getContext("2d");
    //var options = {
    //    responsive: true
    //};
    //var grafico = {
    //    labels: ["Jan", "Fev", "Mar", "Abr", "Maio", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"], // 12
    //    datasets: [
    //        {
    //            label: "Dados primários",
    //            fillColor: "rgba(220,220,220,0.3)",
    //            strokeColor: "#4d90fe",
    //            pointColor: "#4d90fe",
    //            pointStrokeColor: "#fff",
    //            pointHighlightFill: "#fff",
    //            pointHighlightStroke: "#4d90fe",
    //            data: [28, 48, 40, 19, 86, 27, 90, 200, 87, 20, 50, 20]  // 12	
    //        }
    //    ]
    //};

});