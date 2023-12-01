
function login() {

    let form = document.querySelector('form');

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });

    var Id = parseInt($("#userId").val());
    var Password = $("#password").val().toString();

    var loginObject = {
        UserId: parseInt(Id),
        Password: Password,
        AccessId:1
    };
    $.ajax({
        type: "POST",
        url: "/Authentication/LoginUser",
        data: loginObject,
        dataType: "json",
        success: function (response) {
            if (response.result) {
                window.location = response.url;
                alert(response.message);
            }
            else {
                alert(response.message);
                window.location = response.url;
            }
        },
        failure: function (response) {
            console.log(response.message);
        },
        error: function (response) {
            console.log(response.message);
        }
    });

}