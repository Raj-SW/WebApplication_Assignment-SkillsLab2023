function validatePasswords(passwordId, passwordIdValidation, confirmPasswordId, confirmPasswordIdValidation) {
    const password = document.getElementById(passwordId);
    const passwordValidation = document.getElementById(passwordIdValidation);

    const confirmPassword = document.getElementById(confirmPasswordId);
    const confirmPasswordValidation = document.getElementById(confirmPasswordIdValidation);

    const passwordValue = password.value;
    const confirmPasswordValue = confirmPassword.value;

    const passwordRegex = /[\s~`!@#$%^&*()_+={}[\]:;'",.<>?\\/]/g;

    passwordValidation.innerText = "";
    confirmPasswordValidation.innerText = "";

    if (passwordValue.length < 8) {
        passwordValidation.innerText = "Password should be at least 8 characters long";
    }
    if (passwordRegex.test(passwordValue)) {
        passwordValidation.innerText = 'Password should not contain spaces or special characters';
    } 
    if (confirmPasswordValue.length < 8) {
        confirmPasswordValidation.innerText = "Password should be at least 8 characters long";
    }
    if (passwordRegex.test(confirmPasswordValue)) {
        confirmPasswordValidation.innerText = 'Password should not contain spaces or special characters';
    }
    if (passwordValue != confirmPasswordValue)
    {
        confirmPasswordValidation.innerText = 'Passwords do not match';
        passwordValidation.innerText = 'Passwords do not match';
    }
}