
function Register()
{
    let form = document.querySelector('form');

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
    if (RegistrationValidation()===false) {
        return false;
    }
   
    var firstName = document.getElementById("FirstName").value;
    var lastName = document.getElementById("LastName").value;
    var nic = document.getElementById("nic").value;
    var mobileNum = document.getElementById("MobileNum").value;
    var email = document.getElementById("email").value.toLowerCase();
    var password = document.getElementById("password").value;

    var UserModel = {
        NIC: nic,
        UserFirstName: firstName,
        UserLastName: lastName,
        MobileNum: mobileNum
    }
    var CredentialModel = {
        Email: email,
        Password: password
    }
    var RegistrationModel = {
        userModel: UserModel,
        credentialModel: CredentialModel
    };

    fetch( "/Authentication/RegisterUserAsync", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(RegistrationModel)
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
            if (data.result) {
                window.location = data.url;
                alert(data.message);
            }
            alert(data.message);
        })
        .catch(error => {
            alert("Sorry we're having some trouble with you're registration, We'll get back to you", error)
        });
}