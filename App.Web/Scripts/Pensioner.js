
$(document).ready(function () {
    var value = $('select#EmplStatus option:selected').val();
    //debugger
    //if (value == 'Y') {
    //    $("#dtpic").hide();
    //    $("#Terminated").val("");
    //} else {
    //    $("#dtpic").show();
    //}
     
    //expected retirement date & job status to active contributor
    $("#ExpectedRetirementDate").prop("readonly", "readonly");
    $("#RetirementOrResignationDate").prop("readonly", "readonly");   
    //for social security number
    //$('#SocialSecurityNo').keypress(function (event) {
    //    if ((event.which > 31 && (event.which < 48 || event.which > 57))) {
    //        event.preventDefault();
    //    }
    //})

    $("#SocialSecurityNo").on('blur', function () {
        var number = $("#SocialSecurityNo").val();
        if (number != "" && number.length<=6) {
            $.ajax({
                type: 'Get',
                url: '/ContributorPersonalDetails/SocialSecurityNoAjax?Number=' + number,
                dataType: 'json'
            }).done(function (result) {
                if (result != "0") {
                    toasterErrorMessage("This Social Security Number Already exist...Enter Another one");
                    $("#SocialSecurityNo").val("");
                }
            });
        }
        else {
            toasterErrorMessage("Social Security should not be greater than 6 character");

        }
    })


    
    //for country

    $("#CountryID").change(function () {
        var id = $(this).val();
        if (id != "") {
            $.ajax({
                type: 'Get',
                url: '/Employers/CityAjax?Id=' + id,
                dataType: 'json'
            }).done(function (result) {
                $("#PermanentCityID").empty();
                $('#PermanentCityID').append($('<option/>').attr("value", "").text("---Select---"));
                $.each(result, function (i, city) {
                    $('#PermanentCityID').append($('<option/>').attr("value", city.Value).text(city.Text));
                });
            });
        }
        else {
            toasterErrorMessage("Pls Select Country.");
            $("#PermanentCityID").empty();
            $('#PermanentCityID').append($('<option/>').attr("value", "").text("---Select---"));
        }
    })

    //for pf rate

    //for pf rate
    $("#PFRateID").change(function () {
        var id = $(this).val();
        if (id != "") {
            $.ajax({
                url: '/Employers/PFAjax?Id=' + id,
                type: 'Get',
                dataType: 'json'
            }).done(function (result) {
                $("#PFRate").val(result);
            });
        }
        else {
            toasterErrorMessage("Pls Select PF field.");
            $("#PFRate").val("");
        }
    });

    //for email
    $("#Email").on('blur', function () {
        var email = $("#Email").val();
        if (email != "") {
            $.ajax({
                type: 'Get',
                url: '/ContributorPersonalDetails/EmailAjax?Email=' + email,
                dataType: 'json'
            }).done(function (result) {
                if (result != "0") {
                    toasterErrorMessage(email + " is Already exist...Enter Another one");
                    $("#Email").val("");
                }
            });
        }
        else {
            toasterErrorMessage("Please enter EmailId");

        }
    })

})


$(document).on('change', '#DateOfBirth', function () {
    var date = new Date($("#DateOfBirth").val());
    debugger
    if (!isNaN(date.getTime())) {
        var c = date.getMonth() == 0 ? 12 : date.getMonth();
        var d = c + "/" + date.getDate() + "/" + (date.getFullYear() + 55);
        $("#ExpectedRetirementDate").val(d);

    } else {
        toasterErrorMessage("Invalid Date");
    }
});


$(document).on('change', '#EmplStatus', function () {
    var value = $('select#EmplStatus option:selected').val();
    debugger
    //if (value == 'Y') {
    //    $("#dtpic").hide();
    //    $("#Terminated").val("");
    //} else {
    //    $("#dtpic").show();
    //}
});