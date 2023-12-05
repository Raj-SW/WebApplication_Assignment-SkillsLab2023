function RegistrationValidation() {
    var firstName = document.getElementById("FirstName").value;
    var firstNameValidation = document.getElementById("firstNameValidation");

    var lastName = document.getElementById("LastName").value;
    var nic = document.getElementById("nic").value;
    var mobileNum = document.getElementById("MobileNum").value;

    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;


    // Check if the first name is empty
    if (firstName.trim() === "") {
        firstNameValidation.innerText = "Please enter your First Name.";
        return false;
    }

    // Check if the first name contains only alphabets and has a length greater than one
    if (!/^[a-zA-Z]+$/.test(firstName) || firstName.length <= 1) {
        firstNameValidation.textContent = "First Name should contain only alphabets and have more than one character.";
        return false; 
    }
    return true;

}