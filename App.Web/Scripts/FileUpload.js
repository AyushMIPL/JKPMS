
function ShowUpload(uploadControl) {
    //debugger;
    //$("#fileUploadTaxi").click();
    $("#" + uploadControl + "").click();
}
function ShowImagePreview(input, ctrlToMap, maxSizeInKB) {
    debugger;
    //validate if file is image only
  var id = input.id;
  var files = input.files;

 
    var file = files[0];

    $($(input)[0].files).each(function () {

        var length = $(this)[0].name.split('.').length;
        var extension = "." + $(this)[0].name.split('.')[length - 1];
        var audio = /\.(jpeg|jpg|gif|png|JPEG|JPG|GIF|PNG)$/i;
        if (audio.test(extension)) {
            $("#fileValidate").text("");
            return true;
        }
        else {
            $("#fileValidate").text("Please enter valid file format");
            //$("#fileUploadTaxi").val('');
            $("#" + id + "").val('');
            return false;
        }
    });

  // Check if the file size is above the specified maximum limit in KB
  var maxSizeInBytes = maxSizeInKB * 1024;
  debugger;
  if (file.size > maxSizeInBytes) {
    // Resize the image and display the resized image
    resizeImage(file, maxSizeInBytes, function (resizedBlob) {
      debugger;
      // Use the resized image blob to display
      displayResizedImage(resizedBlob, ctrlToMap);
    });
    return;
  }

  $("#fileValidate").text("");

    //show selected image
    var str = "";
    var id = 0;
    if (input.files && input.files[0]) {

        $($(input)[0].files).each(function () {
            var filerdr = new FileReader();
            var name = $(this)[0].name;
            filerdr.onload = function (e) {

                //$('#imgClientNew').attr('src', e.target.result);
                $('#' + ctrlToMap + '').attr('src', e.target.result);
            }
            filerdr.readAsDataURL($(this)[0]);

        });
    }
}


//function ShowImagePreview(input, ctrlToMap, maxSizeInKB) {
//  debugger;

//  // validate if file is an image and check size
//  var id = input.id;
//  var files = input.files;

//  if (files.length > 0) {
//    var file = files[0];

//    // Check if the file is an image
//    var extension = "." + file.name.split('.').pop();

//    var imageExtensions = /\.(jpeg|jpg|gif|png|JPEG|JPG|GIF|PNG)$/i;

//    if (!imageExtensions.test(extension)) {
//      $("#fileValidate").text("Please enter a valid image file format");
//      $("#" + id + "").val('');
//      return;
//    }

//    // Check if the file size is above the specified maximum limit in KB
//    var maxSizeInBytes = maxSizeInKB * 1024;
//    if (file.size < maxSizeInBytes) {
//      // Resize the image and display the resized image
//      resizeImage(file, maxSizeInBytes, function (resizedBlob) {
//        debugger;
//        // Use the resized image blob to display
//        displayResizedImage(resizedBlob, ctrlToMap);
//      });
//      return;
//    }

//    $("#fileValidate").text("");

//    // Show selected image
//    var filerdr = new FileReader();
//    filerdr.onload = function (e) {
//      $('#' + ctrlToMap + '').attr('src', e.target.result);
//    };
//    filerdr.readAsDataURL(file);
//  }
//}

function resizeImage(file, maxSizeInBytes, callback) {
  debugger;
  var reader = new FileReader();
  reader.onload = function (event) {
    var img = new Image();
    img.onload = function () {
      var canvas = document.createElement('canvas');
      var ctx = canvas.getContext('2d');

      // Resize the image while maintaining its aspect ratio
      var scaleFactor = Math.min(1, maxSizeInBytes / file.size);
      canvas.width = img.width * scaleFactor;
      canvas.height = img.height * scaleFactor;
      ctx.drawImage(img, 0, 0, canvas.width, canvas.height);

      // Convert the resized image back to Blob
      canvas.toBlob(function (blob) {
        callback(blob);
      }, file.type);
    };
    img.src = event.target.result;
  };
  reader.readAsDataURL(file);
}

function displayResizedImage(blob, ctrlToMap) {
  var img = new Image();
  img.onload = function () {
    $('#' + ctrlToMap + '').attr('src', img.src);
  };
  img.src = URL.createObjectURL(blob);
}