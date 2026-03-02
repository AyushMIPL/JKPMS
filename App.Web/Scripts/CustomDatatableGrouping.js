
function GroupDataTable(api, groupColumnIndex, totalNoOfColumns) {
  debugger
  var rows = api.rows({ page: 'current' }).nodes();
  var last = null;
  api.column(groupColumnIndex, { page: 'current' })
    .data()
    .each(function (group, i) {
      if (last !== group) {
        $(rows)
          .eq(i)
          .before(
            '<tr class="group"><td colspan="' + totalNoOfColumns + '" class="groupingBackgroundColor">' +
            group +
            '</td></tr>'
          );

        last = group;
      }
    });
}

function GroupDataTable2(oSettings, groupColumnIndex, totalNoOfColumns) {
  debugger
  var totalNoOfColumns = oSettings.aoColumns.length; // Total number of columns
  var rows = oSettings.aoData; // Access all row data
  var lastGroup = null;

  // Iterate over the visible rows
  oSettings.aiDisplay.forEach(function (rowIndex) {
    var rowData = rows[rowIndex]._aData; // Access row data
    var group = rowData[groupColumnIndex]; // Group column value

    // Check if the group has changed
    if (lastGroup !== group) {
      var rowNode = rows[rowIndex].nTr; // Get the row node

      // Insert the grouping row
      $(rowNode).before(
        '<tr class="group"><td colspan="' + totalNoOfColumns + '" class="groupingBackgroundColor">' +
        group +
        '</td></tr>'
      );

      lastGroup = group;
    }
  });
} 