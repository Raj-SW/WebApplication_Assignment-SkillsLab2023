async function EnrolEmployee(userId, training, prereqCount) {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
    var fileSizeLimit = 50 * 1024 * 1024;
    var formData = new FormData();
    formData.append('userId', userId);
    formData.append('trainingId', training.TrainingId);
    var fileInputs = document.querySelectorAll('#fileInput');
    var filesSelected = false;
    fileInputs.forEach(function (fileInput, index) {
        var file = fileInput.files[0];
        if (file) {
            if (file.size > fileSizeLimit) {
                toastr.warning(file.name + " exceeds file size limit of 50 mb");
                return false;
            }
            var fileExtension = file.name.split('.').pop().toLowerCase();
            var allowedExtensions = ['pdf', 'png', 'jpeg', 'jpg', 'doc', 'docx'];
            if (!allowedExtensions.includes(fileExtension)) {
                toastr.warning('Invalid file type for ' + file.name + '. Allowed types are: PDF, PNG, JPEG, Word');
                return false;
            }
            filesSelected = true;
            console.log("File " + (index + 1) + ": " + file.name);
            formData.append('files' + index, file);
        } else {
            filesSelected = false;
        }
    });
    if (!filesSelected && prereqCount > 0) {
        toastr.error("Please submit all required attachments");
        return false;
    }
    const url = '/Enrolment/EnrolEmployeeIntoTrainingAsync';
    await fetch(url, {
        method: 'POST',
        body: formData 
    })
        .then(response => response.json())
        .then(result => {
            if (result.result) {
                toastr.success(result.message);
                return result.result;
            }
            toastr.warning(result.message);
            return result.result;
        })
        .catch(error => {
            toastr.error("Sorry we're having trouble with your enolment. Make sure to submit all attachments ");
        });
}
