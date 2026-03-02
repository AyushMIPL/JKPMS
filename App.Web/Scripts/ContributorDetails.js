$("#EmployerID").change(function () {
    EmployerChange();
})
function EmployerChange() {
    $("#PhoneOffice").val("");
    var id = $("#EmployerID").val();
    if (id != "") {
        $.ajax({
            url: '/ContributorPersonalDetails/getOfficePhoneAjax?id=' + id,
            type: 'Get',
            dataType: 'json'
        }).done(function (result) {
            $(PhoneOffice).val(result.Mobile);
            if (result.EmployerTypeID == "1")//for police
            {
                var date = new Date();
                var startDate = (date.getMonth() + 1) + "/" + date.getDate() + "/" + (date.getFullYear() - 55);
                $('#Date').datepicker('setStartDate', startDate);

            }
            else {//for public
                var date = new Date();
                var startDate = (date.getMonth() + 1) + "/" + date.getDate() + "/" + (date.getFullYear() - 65);
                $('#Date').datepicker('setStartDate', startDate);

            }

        })
    }
}

$(document).ready(function () {
    $('.input-group.date').datepicker({
        format: 'mm/dd/yyyy',
        autoclose: true,
        todayHighlight: true
        //startView: 1
        //,startDate: '-0d'
    });
    //for date of birth
    $('select').select2({
        placeholder: "---Select---",
        allowClear: true
    });
    var date = new Date();
    var endDate = (date.getMonth() + 1) + "/" + date.getDate() + "/" + (date.getFullYear() - 17);
    var startDate = (date.getMonth() + 1) + "/" + date.getDate() + "/" + (date.getFullYear() - 65);
    $('#Date').datepicker('setEndDate', endDate);

    //for 1st appointment and last appointment
    $("#LastDate").on("change", function (ev) {
        debugger
        var Edate = new Date($("#LastAppointmentDate").val());
        var Sdate = new Date($("#FirstAppointmentDate").val());
        if ((Sdate != '' && Edate != '' && Edate < Sdate)) {
            toasterErrorMessage("Please ensure that the End Date is greater than or equal to the Start Date.");
            $("#LastAppointmentDate").val("")
            return false;
        }
        else if (!isNaN(Sdate.getTime())) {
            toasterErrorMessage("Please check Appointment Date");
            $("#LastAppointmentDate").val("");
            return false;
        }

    });


    ////for mobile
    //$('#Mobile').keypress(function (event) {
    //    if ((event.which > 31 && (event.which < 48 || event.which > 57))) {
    //        event.preventDefault();
    //    }
    //})
    //for social security number
    //$("#SocialSecurityNo").mask("999-99-9999");

    //$('#SocialSecurityNo').keypress(function (event) {
    //    if ((event.which > 31 && (event.which < 48 || event.which > 57))) {
    //        event.preventDefault();
    //    }
    //})

    $("#SocialSecurityNo").on('blur', function () {
        debugger
        var PersonID = $("#PersonID").val();
        var OldPersonID = $("#OldPersonID").val();
        var number = $("#SocialSecurityNo").val();
        if (number != "" && number.length <= 6 && number.length >= 5) {
            debugger          
            $.ajax({
                type: 'Get',
                url: '/ContributorPersonalDetails/SocialSecurityNoAjax?Number=' + number + "&personID=" + PersonID + "&oldPersonID=" + OldPersonID,
                dataType: 'json'
            }).done(function (result) {
                if (result != "0") {
                    toasterErrorMessage("This Social Security Number already exist");
                    $("#SocialSecurityNo").val("");
                }
            });
        }
        else {
            toasterErrorMessage("Social Security Number Should be Between 5 to 6 Character");
            $("#SocialSecurityNo").val("");
        }
    })



    //$("#SocialSecurityNo").on('blur', function () {
    //    debugger
    //    var PersonID = $("#PersonID").val();
    //    var OldPersonID = $("#OldPersonID").val();
    //    var number = $("#SocialSecurityNo").val();
    //    var firstName = $("#FirstName").val();
    //    var lastName = $("#LastName").val();
    //    var DateOfBirth = $("#DateOfBirth").val();
    //    if (number != "" && number.length <= 6 && number.length >= 5) {
    //        debugger
    //        if (firstName != "" && lastName != "") {
    //            var params = {
    //                PersonID: PersonID,
    //                OldPersonID: OldPersonID,
    //                number: number,
    //                firstName: firstName,
    //                lastName: lastName,
    //                DateOfBirth: DateOfBirth
    //            };
    //            $.ajax({
    //                type: "POST",
    //                url: "/ContributorPersonalDetails/SocialSecurityNoAjax",
    //                dataType: 'json',
    //                contentType: 'application/json; charset=UTF-8',
    //                data: JSON.stringify(params),
    //                success: function (result) {
    //                    if (result != "0") {
    //                        toasterErrorMessage("This Social Security Number already exist");
    //                        $("#SocialSecurityNo").val("");
    //                    }
    //                }
    //            })
    //        }
    //        else {
    //            toasterErrorMessage("Please Enter First Name , Last Name and DateOfBirth first.");
    //            $("#SocialSecurityNo").val("");
    //        }
    //        //$.ajax({
    //        //    type: 'Get',
    //        //    url: '/ContributorPersonalDetails/SocialSecurityNoAjax?Number=' + number + "&personID=" + PersonID + "&oldPersonID=" + OldPersonID,
    //        //    dataType: 'json'
    //        //}).done(function (result) {
    //        //    if (result != "0") {
    //        //        toasterErrorMessage("This Social Security Number already exist");
    //        //        $("#SocialSecurityNo").val("");
    //        //    }
    //        //});
    //    }
    //    else {
    //        toasterErrorMessage("Social Security Number Should be Between 5 to 6 Character");
    //        $("#SocialSecurityNo").val("");
    //    }
    //})

    //for salary amount
    //$('#SalaryAmount').keypress(function (event) {
    //    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
    //        event.preventDefault();
    //    }
    //});

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
            toasterErrorMessage("Please Select PF field.");
            $("#PFRate").val("");
        }
    });

    //for email


    //for marriage date
    $("#MarriageDate").on('changeDate', function (selected) {
        $("#MarriageEndDate").val("");
        var startDate = new Date(selected.date.valueOf());
        $('#EndDate').datepicker('setStartDate', startDate);
        $('#EndDate').focus();
    }).on('clearDate', function (selected) {
        $('#EndDate').datepicker('setStartDate', null);
    });
    $("#EndDate").on('changeDate', function (selected) {
        var startDate = new Date(selected.date.valueOf());
        $('#MarriageDate').datepicker('setEndDate', startDate);

    }).on('clearDate', function (selected) {
        $('#MarriageDate').datepicker('setEndDate', null);
    });

    //$("#EndDate").on("changeDate", function (ev) {
    //    var Edate = new Date($("#MarriageEndDate").val());
    //    var Sdate = new Date($("#MarriageBeginDate").val());
    //    var pdate = $("#MarriageBeginDate").val();
    //    debugger
    //    if (pdate == "") {
    //        toasterErrorMessage("Please Enter Marriage Begin Date First");
    //        $("#MarriageEndDate").val("");
    //        return false;
    //    }
    //        else if ((Sdate != '' && Edate != '' && Edate < Sdate)) {
    //        toasterErrorMessage("Please ensure that the End Date is greater than the Start Date.");
    //        $("#MarriageEndDate").val("")
    //        return false;
    //    }
    //});

    //$("#ExpectedRetirementDate").attr("readonly", true);
    //$("#ExpectedRetirementDate").prop("disabled", "disabled");
    //$('#JobStatusID').val(1);
    //$('#EmployerContributionAmount').val((0.00).toFixed(2));
    //$('#ContributorContributionAmount').val((0.00).toFixed(2));
    //$("#PFRate").val((0.00).toFixed(2));
    //var date = new Date($("#DateOfBirth").val());

    //var id = $('select#PFRateID option:selected').val();
    //id = id.length != 0 ? id : "0";
    //$.ajax({
    //    url: '/Employers/CheckPF?Id=' + id,
    //    type: 'Get',
    //    dataType: 'json'
    //}).done(function (result) {
    //    $("#PFRate").val(result);
    //    var SalaryAmount = parseFloat($("#SalaryAmount").val());
    //    var rate = parseFloat(result);
    //    $('#EmployerContributionAmount').val((SalaryAmount * rate / 100).toFixed(2));
    //});

    //$.ajax({
    //    url: '/ContributorPersonalDetails/CheckPF?Id=' + id,
    //    type: 'Get',
    //    dataType: 'json'
    //}).done(function (result) {
    //    $("#PFRate").val(result);
    //    var SalaryAmount = parseFloat($("#SalaryAmount").val());
    //    var rate = parseFloat(result);
    //    $('#ContributorContributionAmount').val((SalaryAmount * rate / 100).toFixed(2));
    //});

    //if (!isNaN(date.getTime())) {
    //    var d = date.getMonth() + "/" + date.getDate() + "/" + (date.getFullYear() + 55);
    //    $("#ExpectedRetirementDate").val(d);
    //}

});
//$("#PFRateID").change(function () {
//    var id = $(this).val();
//    if (id != "") {
//        if ($("#SalaryAmount").val() == "")
//        { toasterErrorMessage("Pls Enter Salary Amount"); }
//        else
//        {
//            $.ajax({
//                url: '/Employers/CheckPF?Id=' + id,
//                type: 'Get',
//                dataType: 'json'
//            }).done(function (result) {
//                $("#PFRate").val(result);
//                var SalaryAmount = parseFloat($("#MonthlySalary").val());
//                var rate = parseFloat(result);
//                $('#EmployerContributionAmount').val((SalaryAmount * rate / 100).toFixed(2));
//            });
//            $.ajax({
//                url: '/ContributorPersonalDetails/CheckPF?Id=' + id,
//                type: 'Get',
//                dataType: 'json'
//            }).done(function (result) {
//                $("#PFRate").val(result);
//                var SalaryAmount = parseFloat($("#MonthlySalary").val());
//                var rate = parseFloat(result);
//                $('#ContributorContributionAmount').val((SalaryAmount * rate / 100).toFixed(2));
//            });
//        }
//    }
//    else {
//        toasterErrorMessage("Pls Select PF field.");
//        $('#EmployerContributionAmount').val((0.00).toFixed(2));
//        $('#ContributorContributionAmount').val((0.00).toFixed(2));
//        $("#PFRate").val("");
//    }

//})
//$("#PFRateID").change(function () {
//    debugger
//    var id = $(this).val();
//    if (id != "") {
//        $.ajax({
//            url: '/ContributorPersonalDetails/CheckPF?Id=' + id,
//            type: 'Get',
//            dataType: 'json'
//        }).done(function (result) {
//            $("#PFRate").val(result);
//        });
//    }
//    else {
//        toasterErrorMessage("Pls Select PF field.");
//        $("#PFRate").val("");
//    }
//})
//    $('#EmployerContributionAmount').val((0.00).toFixed(2));
//    $('#ContributorContributionAmount').val((0.00).toFixed(2));
//    $("#PFRate").val((0.00).toFixed(2));
//    var id = $(this).val();
//    if (id != "") {
//        if ($("#SalaryAmount").val()=="")
//        { toasterErrorMessage("Pls Enter Salary Amount"); }
//        id = id.length != 0 ? id : "0";
//        $.ajax({
//            url: '/Employers/CheckPF?Id=' + id,
//            type: 'Get',
//            dataType: 'json'
//        }).done(function (result) {
//            $("#PFRate").val(result);
//            var SalaryAmount = parseFloat($("#SalaryAmount").val());
//            var rate = parseFloat(result);
//            $('#EmployerContributionAmount').val((SalaryAmount * rate / 100).toFixed(2));
//        });

//        $.ajax({
//            url: '/ContributorPersonalDetails/CheckPF?Id=' + id,
//            type: 'Get',
//            dataType: 'json'
//        }).done(function (result) {
//            $("#PFRate").val(result);
//            var SalaryAmount = parseFloat($("#SalaryAmount").val());
//            var rate = parseFloat(result);
//            $('#ContributorContributionAmount').val((SalaryAmount * rate / 100).toFixed(2));
//        });

//    }
//    else {
//        toasterErrorMessage("Pls Select PF field.");
//    }
//});

function RefreshView() {
    var url = '/ContributorPersonalDetails/Index';
    window.location.href = url;
    return false;
}



//function salary() {
//    var Salary = $("#SalaryAmount").val();
//    if (Salary != null && Salary.length != 0) {
//        var monSal = Math.round((parseFloat(Salary) / 12), 2);
//        $("#MonthlySalary").val(monSal);
//        if ($("#PFRateID").val() != "") {
//            $.ajax({
//                url: '/Employers/CheckPF?Id=' + id,
//                type: 'Get',
//                dataType: 'json'
//            }).done(function (result) {
//                $("#PFRate").val(result);
//                var SalaryAmount = parseFloat($("#MonthlySalary").val());
//                var rate = parseFloat(result);
//                $('#EmployerContributionAmount').val((SalaryAmount * rate / 100).toFixed(2));
//            });
//            $.ajax({
//                url: '/ContributorPersonalDetails/CheckPF?Id=' + id,
//                type: 'Get',
//                dataType: 'json'
//            }).done(function (result) {
//                $("#PFRate").val(result);
//                var SalaryAmount = parseFloat($("#MonthlySalary").val());
//                var rate = parseFloat(result);
//                $('#ContributorContributionAmount').val((SalaryAmount * rate / 100).toFixed(2));
//            });
//        }
//    }
//    else {
//        $("#PFRateID").val("");
//        $("#MonthlySalary").val("");
//        $('#ContributorContributionAmount').val("");
//        $('#EmployerContributionAmount').val("");
//        $('#PFRate').val("");
//    }
//}




//$('#SameAsMailingAddress').change(function () {
//    if ($(this).is(":checked")) {
//        $("#PermanentAddress").val($("#MailingAddress").val());
//        $("#PermanentZipCode").val($("#ZipCode").val());
//        $('#PermAddCountry').empty();
//        $('#PermAddState').empty();
//        $('#PermanentCityID').empty();
//        $('#PhyAddCountry option').clone().appendTo('#PermAddCountry');
//        $('#PhyAddState option').clone().appendTo('#PermAddState');
//        $('#CityID option').clone().appendTo('#PermanentCityID');
//        $('#PermAddCountry').val($('select#PhyAddCountry option:selected').val()).attr("selected", "selected");
//        $('#PermAddState').val($('select#PhyAddState option:selected').val()).attr("selected", "selected");
//        $('#PermanentCityID').val($('select#CityID option:selected').val()).attr("selected", "selected");
//    }
//    else {
//        $("#PermanentAddress").val('');
//        $("#PermanentZipCode").val('');
//        $('#PermAddCountry').empty();
//        $('#PermAddState').empty();
//        $('#PermanentCityID').empty();
//        $('#PhyAddCountry option').clone().appendTo('#PermAddCountry');
//    }
//});


//$(function () {
//$("#JobStatusID").prop("disabled", "disabled");
//$("#CountryID").change(function () {
//    var id = $(this).val();
//    if (id != "") {
//        $.ajax({
//            type: 'Get',
//            url: '/ContributorPersonalDetails/GetCity?Id=' + id,
//            dataType: 'json'
//        }).done(function (result) {
//            $("#PermanentCityID").empty();
//            $('#PermanentCityID').append($('<option/>').attr("value", "").text("---Select---"));
//            $.each(result, function (i, city) {
//                debugger
//                $("#PermanentCityID").append('<option value="'
//              + city.Value + '">'
//              + city.Text + '</option>');
//            });
//        });
//    }
//    else {
//        toasterErrorMessage("Pls Select Country.");
//        $("#PermanentCityID").empty();
//        $('#PermanentCityID').append($('<option/>').attr("value", "").text("---Select---"));
//    }
//});

//    $('#PhyAddCountry').change(function () {
//        $('#PhyAddState').empty();
//        $('#CityID').empty();
//        $.getJSON('/ContributorPersonalDetails/StateList/' + $('#PhyAddCountry').val(), function (data) {
//            var items = '<option>Select State</option>';
//            $.each(data, function (i, state) {
//                items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
//            });
//            $('#PhyAddState').append(items);
//            if ($("#SameAsMailingAddress").is(":checked")) {
//                $('#SameAsMailingAddress').trigger('change');
//            }
//        });
//    });

//    $('#PhyAddState').change(function () {
//        $('#CityID').empty();
//        $.getJSON('/ContributorPersonalDetails/Citylist/' + $('#PhyAddState').val(), function (data) {
//            var items = '<option>Select City</option>';
//            $.each(data, function (i, city) {
//                items += "<option value='" + city.Value + "'>" + city.Text + "</option>";
//            });
//            $('#CityID').append(items);
//            if ($("#SameAsMailingAddress").is(":checked")) {
//                $('#SameAsMailingAddress').trigger('change');
//            }
//        });
//    });

//    $('#CityID').change(function () {
//        if ($("#SameAsMailingAddress").is(":checked")) {
//            $('#SameAsMailingAddress').trigger('change');
//        }
//    });

//    $("#MailingAddress").on('input', function () {
//        if ($("#SameAsMailingAddress").is(":checked")) {
//            $('#SameAsMailingAddress').trigger('change');
//        }
//    });

//    $("#MailingAddress").bind('paste', function () {
//        if ($("#SameAsMailingAddress").is(":checked")) {
//            $('#SameAsMailingAddress').trigger('change');
//        }
//    });

//    $("#ZipCode").on('input', function () {
//        if ($("#SameAsMailingAddress").is(":checked")) {
//            $('#SameAsMailingAddress').trigger('change');
//        }
//    });
//    $("#ZipCode").bind('paste', function () {
//        if ($("#SameAsMailingAddress").is(":checked")) {
//            $('#SameAsMailingAddress').trigger('change');
//        }
//    });

//    $('#PermAddCountry').change(function () {
//        $('#PermAddState').empty();
//        $('#PermanentCityID').empty();
//        $.getJSON('/ContributorPersonalDetails/StateList/' + $('#PermAddCountry').val(), function (data) {
//            var items = '<option>Select State</option>';
//            $.each(data, function (i, state) {
//                items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
//            });
//            $('#PermAddState').append(items);
//        });
//    });

//    $('#PermAddState').change(function () {
//        $('#PermanentCityID').empty();
//        $.getJSON('/ContributorPersonalDetails/Citylist/' + $('#PermAddState').val(), function (data) {
//            var items = '<option>Select City</option>';
//            $.each(data, function (i, city) {
//                items += "<option value='" + city.Value + "'>" + city.Text + "</option>";
//            });
//            $('#PermanentCityID').append(items);
//        });
//    });

//});


//$(document).on('change', '#DateOfBirth', function () {
//    var date = new Date($("#DateOfBirth").val());
//    debugger
//    if (!isNaN(date.getTime())) {
//        var c = date.getMonth() == 0 ? 12 : date.getMonth();
//        var d = c + "/" + date.getDate() + "/" + (date.getFullYear() + 55);
//        $("#ExpectedRetirementDate").val(d);

//    } else {
//        toasterErrorMessage("Invalid Date");
//    }

//});

function country() {
    $("#PermanentCityID").select2("val", "");
    $("#PermanentCityID").empty();
    $('#PermanentCityID').append($('<option/>').attr("value", "").text("---Select---"));
    var id = $("#CountryID").val();
    if (id != "") {
        $.ajax({
            type: 'Get',
            url: '/Employers/CityAjax?Id=' + id,
            dataType: 'json'
        }).done(function (result) {
            $.each(result, function (i, city) {
                $('#PermanentCityID').append($('<option/>').attr("value", city.Value).text(city.Text));
            });
        });
    }
}
$("#CountryID").change(function () {
    country();
})
