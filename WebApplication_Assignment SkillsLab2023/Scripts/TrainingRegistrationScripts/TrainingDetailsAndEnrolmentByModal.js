function showTrainingDetails(trainingId) {
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

