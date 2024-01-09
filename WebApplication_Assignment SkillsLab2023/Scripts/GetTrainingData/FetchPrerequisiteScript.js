function getTrainingPrerequisite(training) {
    return new Promise((resolve, reject) => {
        fetch('/Training/GetTrainingPrerequisitebyTrainingIdAsync', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ trainingId: training.TrainingId })
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    reject(new Error("Error: " + response.status));
                }
            })
            .then(data => {
                if (data.result) {
                    console.log("pre req response successful");
                    resolve(data.preReqList);
                } else {
                    reject(new Error("Error in pre req response"));
                }
            })
            .catch(error => {
                reject(error);
            });
    });
}
