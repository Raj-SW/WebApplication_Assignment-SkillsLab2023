async function PopulateModal(userId,training) {
    var modalBody = document.getElementById("modalBody");
    var preReqList = await getTrainingPrerequisite(training);
    modalBody.innerHTML = "";
    var registrationDeadline = new Date(parseInt(training.TrainingRegistrationDeadline.substr(6)));
    var formattedDeadline = registrationDeadline.toLocaleString();
    var detailsContainer = document.createElement("div");
    detailsContainer.innerHTML = "<h3>Training Details</h3>" +
        "<p><strong>Name:</strong> " + training.TrainingName + "</p>" +
        "<p><strong>Description:</strong> " + training.TrainingDescription + "</p>" +
        "<p><strong>Status:</strong> " + training.TrainingStatus + "</p>" +
        "<p><strong>Deadline:</strong> " + formattedDeadline + "</p>" +
        "<p><strong>Seats Available:</strong> " + training.SeatsAvailable + "</p>";
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
    modalBody.innerHTML += "<button type='button' class='btn btn-primary' onclick='EnrolEmployee(" + userId + ", " + JSON.stringify(training) + ")' >Submit</button>";
}
