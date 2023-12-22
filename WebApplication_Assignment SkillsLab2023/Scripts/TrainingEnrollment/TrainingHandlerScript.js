function EnrolEmployee(userId, training) {
    let form = document.querySelector('form');

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });

    console.log("User ID: ", userId, " Training ID: ", training.TrainingId);

    var formData = new FormData();

    // Add user ID and training ID to the FormData
    formData.append('userId', userId);
    formData.append('trainingId', training.TrainingId);

    // Add files to the FormData
    var fileInputs = document.querySelectorAll('#fileInput');
    fileInputs.forEach(function (fileInput, index) {
        var file = fileInput.files[0];
        if (file) {
            console.log("File " + (index + 1) + ": " + file.name);
            formData.append('files' + index, file);
        } else {
            console.log("No file selected for input " + (index + 1));
        }
    });
    var EnrolmentObject = {
        TrainingId: training.TrainingId,
        UserId: userId,
        Files: Array.from(fileInputs)
    }
    console.log("Handling submission");
    const url = '/Training/EnrolEmployeeIntoTraining';
    fetch(url, {
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
