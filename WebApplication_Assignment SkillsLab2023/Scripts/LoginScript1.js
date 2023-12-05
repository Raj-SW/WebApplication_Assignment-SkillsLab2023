
function login() {

    let form = document.querySelector('form');

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });

    var email = $("#email").val().toString();
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
                console.log(response.user);
                window.location = response.url;
            }
            else {
                window.location = response.url;
            }
        },
        failure: function (response) {
        },
        error: function (response) {
        }
    });

}