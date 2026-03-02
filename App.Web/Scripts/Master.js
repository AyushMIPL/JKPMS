$("#IsActive").attr("checked", true);
$("#IsActive").on("click", function () {
      if (!this.checked) {
        toasterErrorMessage("If you Uncheck This field..Then this Record is treated as Inactive Record");
      }
    })