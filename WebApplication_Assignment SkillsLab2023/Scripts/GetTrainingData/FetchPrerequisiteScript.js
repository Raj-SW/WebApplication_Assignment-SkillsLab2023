async function getTrainingPrerequisite(training) {
    try {
        const response = await fetch('/Training/GetTrainingPrerequisitebyTrainingIdAsync', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ trainingId: training.TrainingId })
        });
        if (!response.ok) {
            throw new Error("Error: " + response.status);
        }
        const data = await response.json();
        if (data.result) {
            return data.preReqList;
        } else {
            toastr.error("Error in pre req response");
        }
    } catch (error) {
        toastr.error("Internal server error");
    }
}
