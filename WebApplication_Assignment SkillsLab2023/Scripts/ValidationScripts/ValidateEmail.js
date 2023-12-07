function validateEmail(email,emailValidation) {
    const emailInput = document.getElementById(email);
    const errorMessage = document.getElementById(emailValidation);

    const emailValue = emailInput.value;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    errorMessage.innerText = '';

    if (!emailRegex.test(emailValue)) {
        errorMessage.innerText = 'Invalid email address';
    } else {
        errorMessage.innerText = '';
    }
}
