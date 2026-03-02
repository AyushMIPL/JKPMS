$(function () {

    $("#validateFrom").datepicker({
        format: 'mm/dd/yyyy',
        autoclose: true,
        todayHighlight: true
    }).on('changeDate', function (selected) {
        var startDate = new Date(selected.date.valueOf());
        $('#validateTo').datepicker('setStartDate', startDate);
        $('#validateTo').focus();
    }).on('clearDate', function (selected) {
        $('#validateTo').datepicker('setStartDate', null);
    });
    $("#validateTo").datepicker({
        format: 'mm/dd/yyyy',
        autoclose: true,
        todayHighlight: true
    }).on('changeDate', function (selected) {
        var startDate = new Date(selected.date.valueOf());
        $('#validateFrom').datepicker('setEndDate', startDate);

    }).on('clearDate', function (selected) {
        $('#validateFrom').datepicker('setEndDate', null);
    });

    var Date1 = $("#From").val();
    var Date2 = $("#to").val();
    if (Date1 == "" && Date2 == "") {
        var date = new Date();
        var LastDate = new Date(date.getFullYear(), date.getMonth() + 1, 0);
        var Date1 = (LastDate.getMonth() + 1) + "/" + 01 + "/" + (LastDate.getFullYear());
        var Date2 = (LastDate.getMonth() + 1) + "/" + LastDate.getDate() + "/" + (LastDate.getFullYear());
        $("#From").val(Date1);

        $("#to").val(Date2);
        $('#validateTo').datepicker('setStartDate', Date1);
        $('#validateFrom').datepicker('setEndDate', Date2);
    }
});