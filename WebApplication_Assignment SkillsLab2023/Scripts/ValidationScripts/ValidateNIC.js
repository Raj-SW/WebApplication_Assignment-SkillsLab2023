function ValidateNIC(inputId, messageId) {
    const input = document.getElementById(inputId);
    const errorMessage = document.getElementById(messageId);
    const inputValue = input.value;
    const inputRegex = /^[a-zA-Z0-9]{4,}$/;

    if (input.value.length <= 3)
    {
        errorMessage.innerText = "NIC should be more than 4 characters"
    }
    else if (!inputRegex.test(inputValue))
    {
        errorMessage.innerText = inputId + " should only have alphanumerics";
    }
    else
    {
        errorMessage.innerText = "";
    }
}