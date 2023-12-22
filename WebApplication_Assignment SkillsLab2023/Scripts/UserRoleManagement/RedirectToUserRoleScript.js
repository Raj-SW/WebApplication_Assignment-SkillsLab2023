function RedirectToUserRole() {
    var selectedRoleId = document.querySelector('input[name="selectedRole"]:checked');

    let form = document.querySelector('form');
        form.addEventListener('submit', (e) => {
            e.preventDefault();
        });
    if (selectedRoleId) {
        var roleId = selectedRoleId.value;

        fetch('/Authentication/RedirectToUserRole', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ roleId: roleId })
        })
            .then(response => {
                console.log(response)
                if (response.ok) {
                    return response.json();
                } else {
                    alert("Sorry we're having some trouble with response", response)
                }
            })
            .then(data => {
                console.log(data.message);
                window.location = data.url;
            })
            .catch(error => alert('Error:', error));
    } else {
        alert('Please select a role before submitting.');
    }
}