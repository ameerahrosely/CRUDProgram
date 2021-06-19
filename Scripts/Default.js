var stat = 0;

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
                    "<td><input id='c" + item.clubID + "' class='form-control' type='text' value='" + item.clubName + "' disabled/></td>" +
                    "<td><input id='a" + item.clubID + "' class='form-control' type='number' value='" + item.amountOfMembers + "' disabled/></td>" +
                    "<td>" + item.dateofReg + "</td>" +
                    "<td><img src='/Images/edit.png' style='width:15%; cursor:pointer' onclick='editClub(" + item.clubID + "); return false;'/> &nbsp" +
                    "<img src='/Images/remove.png' style='width:15%; cursor:pointer' onclick='deleteClub(" + item.clubID + "); return false;'/> &nbsp" +
                    "<img id='img" + item.clubID + "' src='/Images/check.png' style='width:15%; cursor:pointer; display:none' onclick='updateClub(" + item.clubID + "); return false;'/></td>" +
                    "</tr>");
            });
        },
        failure: function (data) {
            alert("Error in calling Ajax");
        }
    });
}

function editClub(id) {
    
    if (stat == 0) {
        $("#c" + id).prop("disabled", false);
        $("#a" + id).prop("disabled", false);
        $("#img" + id).show();
        stat = 1;
    }
    else if (stat == 1)    {
        $("#c" + id).prop("disabled", true);
        $("#a" + id).prop("disabled", true);
        stat = 0;
        $("#img" + id).hide();
    }
}

function updateClub(id) {
    if (confirm("Confirm to save this information?")) {
        //alert($("#c" + id).val() + $("#a" + id).val());
    } else {
        displayClub();
        stat = 0;
    }
}

function deleteClub(id) {
    if (confirm("Are you sure to delete this club?")) {
        $.ajax({
            type: "post",
            url: "Default.aspx/DeleteClub",
            contentType: "application/json; charset=utf-8",
            data: '{id:"' + id + '"}',
            dataType: "json",
            success: function (data) {
                alert("Data successfully deleted.");
                displayClub();
            },
            failure: function (data) {
                alert("Error in calling Ajax");
            }
        });
    } else {
        displayClub();
    }
}