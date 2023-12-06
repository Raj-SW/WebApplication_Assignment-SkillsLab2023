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