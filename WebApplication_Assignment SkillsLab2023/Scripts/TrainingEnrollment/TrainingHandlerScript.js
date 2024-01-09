async function EnrolEmployee(userId, training, prereqCount) {
    let form = document.querySelector('form');

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });

    var formData = new FormData();

    formData.append('userId', userId);
    formData.append('trainingId', training.TrainingId);

    // Add files to the FormData
    var fileInputs = document.querySelectorAll('#fileInput');
    var filesSelected = false;

    fileInputs.forEach(function (fileInput, index) {
        var file = fileInput.files[0];
        if (file) {
            filesSelected = true;
            console.log("File " + (index + 1) + ": " + file.name);
            formData.append('files' + index, file);
        } else {
            filesSelected = false;
            console.log("No file selected for input " + (index + 1));
        }
    });
    if (!filesSelected && prereqCount > 0) {
        // No files selected, throw an error and notify the user
        console.error('Error: No files selected');
        alert('Error: Please upload at least one file.');
        return false; // Prevent further execution
    }
    var EnrolmentObject = {
        TrainingId: training.TrainingId,
        UserId: userId,
        Files: Array.from(fileInputs)
    }
    const url = '/Enrolment/EnrolEmployeeIntoTrainingAsync';
    await fetch(url, {
        method: 'POST',
        body: formData 
    })
        .then(response => response.json())
        .then(result => {
            console.log("data received here", result);
            console.log(result.message);
            alert(result.message);
        })
        .catch(error => {
            console.error('Error:', error);
        });
    return true;
}
