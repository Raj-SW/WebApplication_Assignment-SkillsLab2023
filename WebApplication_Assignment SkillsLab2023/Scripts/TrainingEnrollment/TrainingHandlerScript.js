function EnrolEmployee(userId, training)
{
    let form = document.querySelector('form');

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });

    console.log("User ID: ", userId, " Training ID: ", training.TrainingId);

    var fileInputs = document.querySelectorAll('#fileInput');
    fileInputs.forEach(function (fileInput, index) {
        var file = fileInput.files[0];
        if (file) {
            console.log("File " + (index + 1) + ": " + file.name);
        } else {
            console.log("No file selected for input " + (index + 1));
        }
    });
    console.log("Handling submission");
    const url = '/Training/EnrolEmployeeIntoTraining';
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    })
        .then(response => response.json())
        .then(result => {
            console.log(result);
        })
        .catch(error => {
            console.error('Error:', error);
        });
    return true;
}