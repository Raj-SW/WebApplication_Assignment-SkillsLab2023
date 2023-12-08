// jsfile.js

function showTrainingDetails(trainingId) {
    // Assuming there is a global variable or function to fetch training details based on ID
    var trainingDetails = getTrainingDetails(trainingId);

    // Update modal content based on training details
    $('#trainingName').text(trainingDetails.name);
    $('#description').text(trainingDetails.description);

    // Clear existing prerequisites
    $('#prerequisites').empty();

    // Append prerequisites to the modal
    for (var i = 0; i < trainingDetails.prerequisites.length; i++) {
        var prerequisite = trainingDetails.prerequisites[i];

        // Create a list item for each prerequisite
        var listItem = $('<li>').text(prerequisite.name);

        // Add file upload input for each prerequisite
        var fileInput = $('<input>').attr({
            type: 'file',
            id: 'fileInput_' + i,   
            name: 'fileInput_' + i  
        });

        listItem.append(fileInput);
        $('#prerequisites').append(listItem);
    }
    $('#trainingModal').modal('show');
}

// Example function to fetch training details (replace with your implementation)
function getTrainingDetails(trainingId) {
    return {
        name: 'Training ' + trainingId,
        description: 'Description for Training ' + trainingId,
        prerequisites: [
            { name: 'Prerequisite 1' },
            { name: 'Prerequisite 2' },
        ]
    };
}
function closeModal() {
    console.log("Closing modal");
    $('#closeButton').modal('hide');
}

