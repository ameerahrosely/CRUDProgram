var stat = 0;
var pageSize = 0;
var selectedPage = 1;
var pieChartData = [];
var chart;

$(function () {
    getPieChartData();
    loadPieChart();
    displayToken();
})

var Pagination = {
    code: '',
    Extend: function (data) {
        data = data || {};
        Pagination.size = data.size || 300;
        Pagination.page = data.page || 1;
        Pagination.step = data.step || 3;
    },
    Add: function (s, f) {
        for (var i = s; i < f; i++) {
            Pagination.code += '<a>' + i + '</a>';
        }
    },
    Last: function () {
        Pagination.code += '<i>...</i><a>' + Pagination.size + '</a>';
    },
    First: function () {
        Pagination.code += '<a>1</a><i>...</i>';
    },
    Click: function () {
        Pagination.page = +this.innerHTML;
        Pagination.Start();
        selectedPage = Pagination.page;
        loadInspectTableData();
    },
    Prev: function () {
        Pagination.page--;
        if (Pagination.page < 1) {
            Pagination.page = 1;
        }
        Pagination.Start();
        selectedPage = Pagination.page;
        loadInspectTableData();
    },
    Next: function () {
        Pagination.page++;
        if (Pagination.page > Pagination.size) {
            Pagination.page = Pagination.size;
        }
        Pagination.Start();
        selectedPage = Pagination.page;
        loadInspectTableData();
    },
    Bind: function () {
        var a = Pagination.e.getElementsByTagName('a');
        for (var i = 0; i < a.length; i++) {
            if (+a[i].innerHTML === Pagination.page) a[i].className = 'current';
            a[i].addEventListener('click', Pagination.Click, false);
        }
    },
    Finish: function () {
        Pagination.e.innerHTML = Pagination.code;
        Pagination.code = '';
        Pagination.Bind();
    },
    Start: function () {
        if (Pagination.size < Pagination.step * 2 + 6) {
            Pagination.Add(1, Pagination.size + 1);
        }
        else if (Pagination.page < Pagination.step * 2 + 1) {
            Pagination.Add(1, Pagination.step * 2 + 4);
            Pagination.Last();
        }
        else if (Pagination.page > Pagination.size - Pagination.step * 2) {
            Pagination.First();
            Pagination.Add(Pagination.size - Pagination.step * 2 - 2, Pagination.size + 1);
        }
        else {
            Pagination.First();
            Pagination.Add(Pagination.page - Pagination.step, Pagination.page + Pagination.step + 1);
            Pagination.Last();
        }
        Pagination.Finish();
    },
    Buttons: function (e) {
        var nav = e.getElementsByTagName('a');
        nav[0].addEventListener('click', Pagination.Prev, false);
        nav[1].addEventListener('click', Pagination.Next, false);
    },
    Create: function (e) {
        var html = [
            '<a>&#9668;</a>', // previous button
            '<span></span>',  // pagination container
            '<a>&#9658;</a>'  // next button
        ];

        e.innerHTML = html.join('');
        Pagination.e = e.getElementsByTagName('span')[0];
        Pagination.Buttons(e);
    },
    Init: function (e, data) {
        Pagination.Extend(data);
        Pagination.Create(e);
        Pagination.Start();
    }
};

function init() {
    Pagination.Init(document.getElementById('pagination'), {
        size: pageSize, // pages size
        page: 1,  // selected page
        step: 1   // pages before and after current
    });
}

function resetForm() {
    $("#name").val("");
    $("#symbol").val("");
    $("#contact").val("");
    $("#supply").val("");
    $("#holder").val("");
}

function postToken() {
    $.ajax({
        type: "post",
        url: "Default.aspx/SetToken",
        contentType: "application/json; charset=utf-8",
        data: '{name:"' + $("#name").val() + '",symbol:"' + $("#symbol").val() + '",contact:"' + $("#contact").val() + '",supply:"' + $("#supply").val() + '",holder:"' + $("#holder").val() + '"}',
        dataType: "json",
        success: function (data) {
            alert("Token added.");
            displayToken();
        },
        failure: function (data) {
            alert("Error in calling Ajax");
        }
    });
}

function displayToken() {
    $.ajax({
        type: "post",
        url: "Default.aspx/GetToken",
        contentType: "application/json; charset=utf-8",
        data: '{}',
        dataType: "json",
        success: function (data) {
            var tokenBody = $("#tokenTableBody");
            tokenBody.empty();
            $("#tokenTableBody").trigger('destroy');

            $(data.d).each(function (index, item) {
                tokenBody.append("<tr>" +
                    "<td>" + item.tokenID + "</td>" +
                    "<td><input id='sym" + item.tokenID + "' class='form-control' type='text' value='" + item.symbol + "' disabled/></td>" +
                    "<td><input id='n" + item.tokenID + "' class='form-control' type='text' value='" + item.name + "' disabled/></td>" +
                    "<td>" + item.address + "</td>" +
                    "<td>" + item.holders + "</td>" +
                    "<td><input id='s" + item.tokenID + "' class='form-control' type='text' value='" + item.supply + "' disabled/></td>" +
                    "<td>" + item.percentSupply + "</td>" +
                    "<td><img src='/Images/edit.png' style='width:20px; cursor:pointer' onclick='editToken(" + item.tokenID + "); return false;'/> &nbsp" +
                    "<img src='/Images/remove.png' style='width:20px; cursor:pointer' onclick='deleteToken(" + item.tokenID + "); return false;'/> &nbsp" +
                    "<img id='img" + item.tokenID + "' src='/Images/check.png' style='width:20px; cursor:pointer; display:none' onclick='updateToken(" + item.tokenID + "); return false;'/></td>" +
                    "</tr>");
            });
            getPieChartData();
            loadPieChart();
        },
        failure: function (data) {
            alert("Error in calling Ajax");
        }
    });
}

function editToken(id) {
    
    if (stat == 0) {
        $("#sym" + id).prop("disabled", false);
        $("#n" + id).prop("disabled", false);
        $("#s" + id).prop("disabled", false);
        $("#img" + id).show();
        stat = 1;
    }
    else if (stat == 1)    {
        $("#sym" + id).prop("disabled", true);
        $("#n" + id).prop("disabled", true);
        $("#s" + id).prop("disabled", true);
        stat = 0;
        $("#img" + id).hide();
    }
}

function updateToken(id) {
    if (confirm("Confirm to save this information?")) {
        $.ajax({
            type: "post",
            url: "Default.aspx/UpdateToken",
            contentType: "application/json; charset=utf-8",
            data: '{id:"' + id + '",symbol:"' + $("#sym" + id).val() + '",name:"' + $("#n" + id).val() + '",supply:"' + $("#s" + id).val() + '"}',
            dataType: "json",
            success: function (data) {
                alert("Data successfully updated.");
                displayToken();
            },
            failure: function (data) {
                alert("Error in calling Ajax");
            }
        });
    } else {
        displayClub();
        stat = 0;
    }
}

function deleteToken(id) {
    if (confirm("Are you sure to delete this token?")) {
        $.ajax({
            type: "post",
            url: "Default.aspx/DeleteToken",
            contentType: "application/json; charset=utf-8",
            data: '{id:"' + id + '"}',
            dataType: "json",
            success: function (data) {
                alert("Data successfully deleted.");
                displayToken();
            },
            failure: function (data) {
                alert("Error in calling Ajax");
            }
        });
    } else {
        displayToken();
    }
}

function loadPieChart() {
    am4core.ready(function () {

        // Themes begin
        am4core.useTheme(am4themes_animated);

        chart = am4core.create("chartdiv", am4charts.PieChart3D);
        chart.hiddenState.properties.opacity = 0; // this creates initial fade-in
        chart.logo.disabled = true;

        var series = chart.series.push(new am4charts.PieSeries3D());
        series.dataFields.value = "supply";
        series.dataFields.category = "name";

    }); // end am4core.ready()
}

function getPieChartData() {
    $.ajax({
        type: "post",
        url: "Default.aspx/GetToken",
        contentType: "application/json; charset=utf-8",
        data: '{}',
        dataType: "json",
        success: function (data) {
            pieChartData = [];

            $(data.d).each(function (index, item) {
                pieChartData.push({
                    "name": item.name,
                    "supply": item.supply
                });
            });
            assignChartData();
        },
        failure: function (data) {
            alert("Error in calling Ajax");
        }
    });
}

function assignChartData() {
    loadPieChart();
    chart.data = pieChartData;
}