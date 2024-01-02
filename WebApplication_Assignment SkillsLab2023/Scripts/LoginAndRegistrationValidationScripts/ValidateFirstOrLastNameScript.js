function validateFirstOrLastName(inputId,messageId) {
    const input = document.getElementById(inputId);
    const errorMessage = document.getElementById(messageId);

    const inputValue = input.value;
    const inputRegex = /^[a-zA-Z]+$/
;
    if (input.value.length <= 1)
    {
        errorMessage.innerText = "Length should be more than one alphabet"
    }
    else if (!inputRegex.test(inputValue))
    {
        errorMessage.innerText = inputId+" should only have alphabets";
    }
    else
    {
        errorMessage.innerText = "";
    }

}