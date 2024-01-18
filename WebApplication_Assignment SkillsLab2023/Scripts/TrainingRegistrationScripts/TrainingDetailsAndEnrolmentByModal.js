function showTrainingDetails(trainingId) {
    var trainingDetails = getTrainingDetails(trainingId);
    $('#trainingName').text(trainingDetails.name);
    $('#description').text(trainingDetails.description);
    $('#prerequisites').empty();
    for (var i = 0; i < trainingDetails.prerequisites.length; i++) {
        var prerequisite = trainingDetails.prerequisites[i];
        var listItem = $('<li>').text(prerequisite.name);
        var fileInput = $('<input>').attr({
            type: 'file',
            id: 'fileInput_' + i,   
            name: 'fileInput_' + i,
            accept: ".pdf, .png, .jpeg, .jpg, .doc, .docx"
        });
        listItem.append(fileInput);
        $('#prerequisites').append(listItem);
    }
    $('#trainingModal').modal('show');
}

