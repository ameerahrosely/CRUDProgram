function openPost() {
    $("#openSesame").show();
    $(".jumbotron").hide();
    displayClub();
}

function postClub() {
    $.ajax({
        type: "post",
        url: "Default.aspx/SetClub",
        contentType: "application/json; charset=utf-8",
        data: '{name:"' + $("#clubname").val() + '",member:"' + $("#memberAmount").val() + '",date:"' + $("#dateReg").val() + '"}',
        dataType: "json",
        success: function (data) {
            alert("Club added.");
            displayClub();
        },
        failure: function (data) {
            alert("Error in calling Ajax");
        }
    });
}

function displayClub() {
    $.ajax({
        type: "post",
        url: "Default.aspx/GetClub",
        contentType: "application/json; charset=utf-8",
        data: '{}',
        dataType: "json",
        success: function (data) {
            var clubBody = $("#clubTableBody");
            clubBody.empty();
            $("#clubTableBody").trigger('destroy');

            $(data.d).each(function (index, item) {
                clubBody.append("<tr>" +
                 "<td>" + item.clubID + "</td>" +
                    "<td>" + item.clubName + "</td>" +
                    "<td>" + item.amountOfMembers + "</td>" +
                    "<td>" + item.dateofReg + "</td>" +
                    "<td><img src='/Images/edit.png' style='width:15%'/> &nbsp" +
                    "<img src='/Images/remove.png' style='width:15%'/></td>" +
                    "</tr>");
            });
        },
        failure: function (data) {
            alert("Error in calling Ajax");
        }
    });
}