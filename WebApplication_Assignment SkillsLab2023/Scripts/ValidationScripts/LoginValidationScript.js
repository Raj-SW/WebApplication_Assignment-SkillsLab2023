function LoginValidation() {

    let form = document.querySelector('form');

    var inputFieldsArray = form.querySelectorAll('input');
    var validationMessagesArray = form.querySelectorAll('p');

    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;

    const emailregex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const passwordregex = /^[a-zA-Z0-9]{8,}$/;

    var validationString = "";

    inputFieldsArray.forEach((input, index) => {
        if (input.value.trim() === "") {
            validationMessagesArray[index].innerText = "Please do not leave field empty.";
            validationString = "Input: " + input.id + " at index: " + index;
        }
    });
    if (!emailregex.test(email) || !passwordregex.test(password))
    {
        return false;
    }

    if (validationString.length > 0) {
        console.log(validationString);
        return false;
    }

    return true;
}