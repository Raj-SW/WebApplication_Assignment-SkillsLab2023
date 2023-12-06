function RegistrationValidation() {

    let form = document.querySelector('form');

    var inputFieldsArray = form.querySelectorAll('input');
    var validationMessagesArray = form.querySelectorAll('p');

    var firstName = document.getElementById("FirstName").value;
    var lastName = document.getElementById("LastName").value;
    var nic = document.getElementById("nic").value;
    var mobileNum = document.getElementById("MobileNum").value;
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var confirmPassword = document.getElementById("confirm-password").value;

    const nameRegex = /^[a-zA-Z]{2,}$/;
    const nicRegex = /^[a-zA-Z0-9]{4,}$/;
    const mobileNumregex = /^\d{8}$/;
    const emailregex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const passwordregex = /^[a-zA-Z0-9]{8,}$/;

    const validationString = "";

    inputFieldsArray.forEach((input,index) => {
        if (input.value.trim() === "") {
            validationMessagesArray[index].innerText = "Please do not leave field empty.";
            validationString = validationString + "\n Input: " + input.id + " at index: " + index;
        }
    });
    //if (!nameRegex.test(firstName) || !nameRegex.test(lastName) || !nicRegex.test(nic) || !mobileNumregex.test(mobileNum) || !emailregex.test(email) || !passwordregex.test(password)
    //    || !passwordregex.test(confirmPassword) || (password != confirmPassword))
    //{
    //    return false;
    //}

    if (validationString.length > 0)
    {
        console.log(validationString);
        return false;
    }

    return true;
}