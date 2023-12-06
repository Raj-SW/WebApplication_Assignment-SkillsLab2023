function ValidateMobileNum(inputId, messageId)
{
    const input = document.getElementById(inputId);
    const errorMessage = document.getElementById(messageId);
    const inputValue = input.value;
    const numberRegex = /^[0-9]{8}$/;
;

    if (!numberRegex.test(inputValue))
    {
        errorMessage.innerText = "Please enter a valid 8-digit phone number (only numbers allowed)";
    }
    else {
        errorMessage.innerText = "";
    }
}