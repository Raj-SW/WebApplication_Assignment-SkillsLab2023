async function login() {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
    if (LoginValidation() === false) {
        return false;
    }
    var email = $("#email").val().toString().toLowerCase();
    var password = $("#password").val().toString();

    var loginObject = {
        Email: email,
        RawPassword: password,
    };
    try {
        const response = await fetch("/Authentication/LoginUserAsync", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(loginObject),
        });

        const responseData = await response.json();

        if (responseData.GlobalErrors && responseData.GlobalErrors.length > 0) {
            toastr.error(responseData.GlobalErrors.join('\n'), '', { timeOut: 1000 });
        } else if (responseData.FieldErrors) {
            console.log(responseData.FieldErrors);
        } else {
            toastr.success(responseData.message, '', { timeOut: 1000, });
            await new Promise(resolve => setTimeout(resolve, 1000));
            window.location = responseData.url;
        }
    } catch (error) {
        console.error("Fetch error:", error);
        // Display the fetch error using Toastr with timeout
        toastr.error("An error occurred during login. Please try again.", '', { timeOut: 1000 });
    }
}
