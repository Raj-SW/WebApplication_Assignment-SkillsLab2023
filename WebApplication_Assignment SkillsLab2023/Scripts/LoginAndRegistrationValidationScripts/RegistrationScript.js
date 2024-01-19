async function Register() {
    let form = document.querySelector('form');

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });

    if (await RegistrationValidation() === false) {
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
        RawPassword: password
    }
    var RegistrationModel = {
        userModel: UserModel,
        credentialModel: CredentialModel
    };

    try {
        const response = await fetch("/Authentication/RegisterUserAsync", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(RegistrationModel),
        });

        if (response.ok) {
            const responseData = await response.json();

            if (responseData.GlobalErrors.length > 0) {
                responseData.GlobalErrors.forEach(error => {
                    toastr.error(error);
                });
            } else if (responseData.FieldErrors) {
                for (const key in responseData.FieldErrors) {
                    if (responseData.FieldErrors.hasOwnProperty(key)) {
                        const errors = responseData.FieldErrors[key];
                        errors.forEach(error => {
                            toastr.error(`Field: ${key}, Error: ${error}`);
                        });
                    }
                }
            } else {
                toastr.success(responseData.message);
                window.location = responseData.url;
            }
        } else {
            console.error("Failed to fetch:", response);
        }
    } catch (error) {
        console.error("Fetch error:", error);
    }
}
