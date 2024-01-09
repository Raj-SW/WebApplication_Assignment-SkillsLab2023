async function login() {
    let form = document.querySelector('form');
    form.addEventListener('submit', async (e) => {
        e.preventDefault();

        if (await LoginValidation() === false) {
            return false;
        }

        var email = $("#email").val().toString().toLowerCase();
        var password = $("#password").val().toString();

        var loginObject = {
            Email: email,
            Password: password,
        };

        try {
            const response = await fetch("/Authentication/LoginUserAsync", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(loginObject),
            });

            if (response.ok) {
                const responseData = await response.json();

                if (responseData.result) {
                    alert(responseData.message);
                    window.location = responseData.url;
                    console.log(responseData.user);
                } else {
                    alert(responseData.message);
                    window.location = responseData.url;
                }
            } else {
                console.error("Failed to fetch:", response);
            }
        } catch (error) {
            console.error("Fetch error:", error);
        }
    });
}
