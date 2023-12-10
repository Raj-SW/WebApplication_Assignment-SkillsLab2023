async function PopulateModal(training) {
    // Access the modal body element
    var modalBody = document.getElementById("modalBody");
    var trainingId = training.TrainingId;
    var preReqList = await getTrainingPrerequisite(training);
    // Clear existing content
    modalBody.innerHTML = "";
    // Convert the timestamp to a JavaScript Date object
    var registrationDeadline = new Date(parseInt(training.TrainingRegistrationDeadline.substr(6)));
    // Format the date as a string (adjust the format as needed)
    var formattedDeadline = registrationDeadline.toLocaleString();
    // Create elements to display training details
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
            // Create a new container for each prerequisite
            var prereqContainer = document.createElement("div");
            prereqContainer.className = 'form-group';
            prereqContainer.innerHTML = "<label for='fileInput'>" + preReq.PrerequisiteDescription + "</label><input type='file' class='form-control-file' id='fileInput'>";
            // Append the new container to the main preRequisiteContainer
            preRequisiteContainer.appendChild(prereqContainer);
        });
    } else {
        console.error("preReqList is undefined or not an array");
    }
    // Append the details container to the modal body
    modalBody.appendChild(detailsContainer);
    // Append the prerequisite containers to the modal body
    modalBody.appendChild(preRequisiteContainer);
    // Append the submit button to the modal body
    //modalBody.innerHTML += "<button type='submit' class='btn btn-primary'>Submit</button>";
}
