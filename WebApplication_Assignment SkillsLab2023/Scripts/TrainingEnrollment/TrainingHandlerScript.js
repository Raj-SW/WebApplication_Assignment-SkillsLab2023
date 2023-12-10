function EnrolEmployee()
{
    //console.log('userModel: ', userModel, 'trainingModel: ', trainingModel);
    // Access uploaded files
    var fileInputs = document.querySelectorAll('#fileInput');
    fileInputs.forEach(function (fileInput, index) {
        var file = fileInput.files[0];
        console.log("File " + (index + 1) + ": ", file);
    });
    console.log("Handling submission");
    return true;
}