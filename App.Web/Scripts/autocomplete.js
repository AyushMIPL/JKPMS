$("#EmployeeCode").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: "/PensionProcessHeaders/AutoCompleteAjax",
            type: "POST",
            dataType: "json",
            data: { term: request.term },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: (item.EmplCode + " " + item.FirstName + " " + item.LastName), value: item.EmplCode };
                }))

            }
        })
    },
    select: function (event, ui) {
        var result = ui.item.label.split(" ");
        $("#FirstName").val(result[1]);
        $("#LastName").val(result[2]);
    },
    messages: {
        noResults: "", results: ""
    }
});

$("#FirstName").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: "/PensionProcessHeaders/AutoCompleteAjax",
            type: "POST",
            dataType: "json",
            data: { term: request.term },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: (item.EmplCode + " " + item.FirstName + " " + item.LastName), value: item.FirstName };
                }))

            }
        })
    },
    select: function (event, ui) {
        var result = ui.item.label.split(" ");
        $("#EmployeeCode").val(result[0]);
        $("#LastName").val(result[2]);
    },
    messages: {
        noResults: "", results: ""
    }
});

$("#LastName").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: "/PensionProcessHeaders/AutoCompleteAjax",
            type: "POST",
            dataType: "json",
            data: { term: request.term },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: (item.EmplCode + " " + item.FirstName + " " + item.LastName), value: item.LastName };
                }))

            }
        })
    },
    select: function (event, ui) {
        var result = ui.item.label.split(" ");
        $("#FirstName").val(result[1]);
        $("#EmployeeCode").val(result[0]);
    },
    messages: {
        noResults: "", results: ""
    }
});
