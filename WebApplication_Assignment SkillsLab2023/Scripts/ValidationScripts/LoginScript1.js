function login() {

    let form = document.querySelector('form');

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
    if (LoginValidation() === false) {
        return false;
    }

    var email = $("#email").val().toString().toLowerCase();
    var Password = $("#password").val().toString();

    var loginObject = {
        Email: email,
        Password: Password,
    };
    $.ajax({
        type: "POST",
        url: "/Authentication/LoginUser",
        data: loginObject,
        dataType: "json",
        success: function (response) {
            if (response.result) {
                alert(response.message);
                window.location = response.url;
                console.log(response.user);
            }
            else {
                alert(response.message);
                window.location = response.url;
            }
        },
        failure: function (response) {
        },
        error: function (response) {
        }
    });

}