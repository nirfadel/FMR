function GetCelebs() {
    $.ajax({
        url: "/api/Celebs/GetTop100Celebrities",
        type: "GET",
        success: function (data) {
            buildCelebsTable(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
 
}

function deleteCelebritie(obj) {
    try {
        var celebId = obj.getAttribute("celebid");
        $.ajax({
            url: "/api/Celebs/DeleteCelebById/" + celebId,
            type: "DELETE",
            success: function (data) {
                if (data.Deleted)
                    that.closest("tr").remove();
                else
                    alert("הסלב לא נמחק!");
            },  
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
    } catch (e) {
        console.error(e);
    }
}

function clearCelebs() {
    try {
        $.ajax({
            url: "/api/Celebs/ClearTop100Celebrities",
            type: "GET",
            success: function (data) {
                $('#celebs-table tbody').empty();
                buildCelebsTable(data);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
    } catch (e) {
        console.error(e);
    }
}

function buildCelebsTable(data) {
    jQuery.each(data, function (index, celeb) {
        $('#celebs-table tbody').append("<tr><th>" + celeb.Id + "</th>" +
            "<td>" + celeb.Name + "</td><td>" + celeb.DateOfBirth + "</td><td>" +
            celeb.Gender + "</td><td>" + celeb.Role + "</td><td><img src='" + celeb.Photo + "' />" +
            "</td><td><img onclick='deleteCelebritie(this)' celebid='" + celeb.Id + "' src='./style/delete.png' title='מחק' class='delete_celeb'></img></td></tr>");
    });
}