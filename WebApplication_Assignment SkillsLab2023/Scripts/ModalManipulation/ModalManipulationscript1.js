async function PopulateModal(userId, training) {
    var isEnrolled = await isUserAlreadyEnrolled(userId, training);
    var modalBody = document.getElementById("modalBody");
    var preReqList = await getTrainingPrerequisite(training);
    var prereqCount = preReqList.length;
    var trainingStatus = training.TrainingStatus;
    modalBody.innerHTML = "";
    var registrationDeadline = new Date(parseInt(training.TrainingRegistrationDeadline.substr(6)));
    var formattedDeadline = registrationDeadline.toLocaleString();
    var detailsContainer = document.createElement("div");
    detailsContainer.innerHTML =
        "<p><strong>Name:</strong> " + training.TrainingName + "</p>" +
        "<p><strong>Description:</strong> " + training.TrainingDescription + "</p>" +
        "<p><strong>Status:</strong> " + training.TrainingStatus + "</p>" +
        "<p><strong>Deadline:</strong> " + formattedDeadline + "</p>";
    if (trainingStatus != 'Open') {
        var enrolledMessageContainer = document.createElement("div");
        enrolledMessageContainer.innerHTML = "<p>Unfortunately training is not open.</p>";
        modalBody.appendChild(enrolledMessageContainer);
        return false;
    }
    if (isEnrolled) {
        var enrolledMessageContainer = document.createElement("div");
        enrolledMessageContainer.innerHTML = "<p>You are already enrolled in this training.</p>";
        modalBody.appendChild(enrolledMessageContainer);
        return false;
    } 
    else {
    var preRequisiteContainer = document.createElement("div");
    if (preReqList && Array.isArray(preReqList)) {
        preReqList.forEach(preReq => {
            var prereqContainer = document.createElement("div");
            prereqContainer.className = 'form-group';
            prereqContainer.innerHTML = "<label for='fileInput'>" + preReq.PrerequisiteDescription + "</label><input type='file' class='form-control-file' id='fileInput'>";
            preRequisiteContainer.appendChild(prereqContainer);
        });
    } else {
        console.error("preReqList is undefined or not an array");
    }
    modalBody.appendChild(detailsContainer);
        modalBody.appendChild(preRequisiteContainer);
        modalBody.innerHTML += "<button type='button' class='btn btn-primary' data-dismiss='modal' onclick='EnrolEmployee(" + userId + ", " + JSON.stringify(training) + ", " + prereqCount + " ) ' >Submit</button>";
    }
}

async function isUserAlreadyEnrolled(userId, training) {
    try {
        var result = await fetch('/Enrolment/isUserAlreadyRegisteredForTrainingAsync', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                trainingId: training.TrainingId,
                userId: userId
            })
        });
        if (result.ok) {
            var data = await result.json();
            return data.result;
        } else {
            toastr.error("Error in checking user enrolment");
        }
    } catch (error) {
        toastr.error("Sorry internal server");
        throw error;
    }
}
