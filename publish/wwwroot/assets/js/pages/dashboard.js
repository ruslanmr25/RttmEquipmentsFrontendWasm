window.Start = function (chartData) {


    // chartData object ichida series, categories, labels va h.k. keladi

    // 1. Line/Bar chart options
    // var optionsLine = {
    //     series: chartData.lineSeries,
    //     chart: { height: 313, type: "line", toolbar: { show: false } },
    //     xaxis: { categories: chartData.categories },
    //     colors: ["#1a80f8", "#17c553", "#7942ed"]
    // };
    // var chartLine = new ApexCharts(document.querySelector("#dash-performance-chart"), optionsLine);
    // chartLine.render();

    // 2. Donut chart options
    var optionsDonut = {
        chart: { height: 320, type: "donut" },
        series: chartData.donutSeries,
        labels: chartData.donutLabels,
        colors: ["#6658dd", "#4fc6e1", "#4a81d4", "#00b19d", "#f1556c"]
    };
    var chartDonut = new ApexCharts(document.querySelector('#apex-pie-2'), optionsDonut);
    chartDonut.render();


    options = {
        chart: { height: 380, type: "bar", toolbar: { show: !1 } },
        plotOptions: { bar: { horizontal: !0 } },
        dataLabels: { enabled: !1 },
        series: [{ data: chartData.departmentSeries }],
        colors: (colors = ["#1abc9c"]),
        xaxis: {
            categories: chartData.departmentLabels,
        },
        states: { hover: { filter: "none" } },
        grid: { borderColor: "#f1f3fa" },
    };
    (chart = new ApexCharts(
        document.querySelector("#apex-bar-1"),
        options
    )).render();


}
