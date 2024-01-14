function RedirectToUserRole() {
    var selectedRoleId = document.querySelector('input[name="selectedRole"]:checked');
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
    });
    if (selectedRoleId == null) {
        toastr.error("Please Select a role");
        return false;
    }
    if (selectedRoleId) {
        var roleId = selectedRoleId.value;

        fetch('/Authentication/RedirectToUserRoleAsync', {
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
                    toastr.error("Sorry, we're having some trouble with the response");
                }
            })
            .then(data => {
                toastr.success(data.message, '', { timeOut: 1000, });
                setTimeout(() => {
                    window.location = data.url;
                }, 1000);
            })
            .catch(error => {
                console.error('Error:', error);
                toastr.error("An error occurred. Please try again.");
            });
    } else {
        toastr.error('Please select a role before submitting.');
    }
}
