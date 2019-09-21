function filterMonths() {
    $.ajax({
        url: "/dashboard/GetExpenses",
        data: {
            monthId: $("#month").val()
        },
        success: function (data) {
            console.log(data)
            let series = []
            for (var i = 0; i < data.type.length; i++) {
                series.push({
                    name: data.type[i],
                    y: data.amount[i]
                })
            }
            console.log(series)
            var myChart = Highcharts.chart('expensesPerType', {
                chart: {
                    type: 'pie'
                },
                title: {
                    text: 'Expenses per type'
                },
                xAxis: {
                    categories: data.type
                },
                series: [{
                    name: 'Amount',
                    colorByPoint: true,
                    data: series
                    
                }]
            });
        }
    })

    $.ajax({
        url: "/dashboard/GetIncomes",
        data: {
            monthId: $("#month").val()
        },
        success: function (data) {
            console.log(data)
            let series = []
            for (var i = 0; i < data.type.length; i++) {
                series.push({
                    name: data.type[i],
                    y: data.amount[i]
                })
            }
            console.log(series)
            var myChart = Highcharts.chart('incomesPerType', {
                chart: {
                    type: 'pie'
                },
                title: {
                    text: 'Incomes per type'
                },
                xAxis: {
                    categories: data.type
                },
                series: [{
                    name: 'Amount',
                    colorByPoint: true,
                    data: series

                }]
            });
        }
    })

    $.ajax({
        url: "/dashboard/GetMonthResult",
        data: {
            monthId: $("#month").val()
        },
        success: function (data) {
            let series = [
                {
                    name: 'Incomes',
                    y: data.incomes

                },
                {
                    name: 'Expenses',
                    y: data.expenses
                }
            ]
            var myChart = Highcharts.chart('incomeXexpense', {
                chart: {
                    type: 'pie'
                },
                title: {
                    text: 'Incomes x Expenses'
                },
                series: [{
                    name: 'Amount',
                    colorByPoint: true,
                    data: series

                }]
            });
        }
    })
}