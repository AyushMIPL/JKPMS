//mobile number accept only number
//$('#Mobile').keypress(function (event) {
//  if ((event.which > 31 && (event.which < 48 || event.which > 57))) {
//    event.preventDefault();
//  }
//})

//for pf rate

function pfRate(pfRateID) {
    debugger
    if (pfRateID != "") {
        var id = parseInt(pfRateID);
        $.ajax({
            url: '/Employers/PFAjax?Id=' + id,
            type: 'Get',
            dataType: 'json'
        }).done(function (result) {
            $("#PFRate").val(result);
        });
    }
    else {
        toasterErrorMessage("Please Select PF field.");
        $("#PFRate").val("");
    }
}

function countryChange(countryId) {
    $("#CityID").select2("val", "");
    $("#CityID").empty();
    $('#CityID').append($('<option/>').attr("value", "").text("---Select---"));
    if (countryId != "") {
        var id = parseInt(countryId);
        $.ajax({
            type: 'Get',
            url: '/Employers/CityAjax?Id=' + id,
            dataType: 'json'
        }).done(function (result) {
            $.each(result, function (i, city) {
                $('#CityID').append($('<option/>').attr("value", city.Value).text(city.Text));
            });
        });
    }
    else {
        toasterErrorMessage("Please Select Country.");
    }
}

function DisableContrls() {
    $("#CountryID").prop("disabled", "disabled");
    $("#CityID").prop("disabled", "disabled");
    $("#PFRateID").prop("disabled", "disabled");
    $("#EmployerTypeID").prop("disabled", "disabled");
    $("#btnSaveInfo").css("display", "none");
    $("#btnEditInfo").css("display", "");
    $(".txtEdit").each(function () {
        $(this).prop("disabled", "disabled");
    });
    return false;
}

function DoEdit() {
    $("#btnSaveInfo").css("display", "");
    $("#btnEditInfo").css("display", "none");
    $("#CountryID").prop("disabled", false);
    $("#CityID").prop("disabled", false);
    $("#PFRateID").prop("disabled", false);
    $("#EmployerTypeID").prop("disabled", false);
    $(".txtEdit").each(function () {
        $(this).prop("disabled", false);
    });
    var id = $("#EId").val();
    debugger
    if (id != "") {
        $("#EmployerTypeID").prop("disabled", "disabled");
    }
    return false;

}

$("#PFRateID").change(function () {
    var id = $("#PFRateID").val();
    pfRate(id);
});

//on country selection
$("#CountryID").change(function () {
    var id = $("#CountryID").val();
    countryChange(id);
});
