function validateFirstOrLastName() {
    const input = document.getElementById('FirstName');
    const errorMessage = document.getElementById('firstNameValidation');

    //emailInput.addEventListener('change', validateEmail);

    const inputValue = input.value;
    const inputRegex = !/^[a-zA-Z]+$/;

    if (inputRegex.test(inputValue)) {
        errorMessage.innerText = '';
    } else {
        errorMessage.innerText = 'Name should only have alphabet and more than one alphabet ';
    }
}
function validateNIC() {
    const emailInput = document.getElementById('email');
    const errorMessage = document.getElementById('emailValidation');

    //emailInput.addEventListener('change', validateEmail);

    const emailValue = emailInput.value;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (emailRegex.test(emailValue)) {
        errorMessage.innerText = '';
    } else {
        errorMessage.innerText = 'Invalid email address';
    }
}

function validateMobileNum() {
    const emailInput = document.getElementById('email');
    const errorMessage = document.getElementById('emailValidation');

    //emailInput.addEventListener('change', validateEmail);

    const emailValue = emailInput.value;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (emailRegex.test(emailValue)) {
        errorMessage.innerText = '';
    } else {
        errorMessage.innerText = 'Invalid email address';
    }
}
function validateEmail() {
    const emailInput = document.getElementById('email');
    const errorMessage = document.getElementById('emailValidation');

    //emailInput.addEventListener('change', validateEmail);

    const emailValue = emailInput.value;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (emailRegex.test(emailValue)) {
        errorMessage.innerText = '';
    } else {
        errorMessage.innerText = 'Invalid email address';
    }
}
function validatePasswords() {
    const emailInput = document.getElementById('email');
    const errorMessage = document.getElementById('emailValidation');

    //emailInput.addEventListener('change', validateEmail);

    const emailValue = emailInput.value;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (emailRegex.test(emailValue)) {
        errorMessage.innerText = '';
    } else {
        errorMessage.innerText = 'Invalid email address';
    }
}