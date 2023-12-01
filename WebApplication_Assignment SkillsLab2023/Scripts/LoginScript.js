
function login() {
    let form = document.querySelector('form');

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });

    //get values from input
    var Id = parseInt($("#userId").val());
    var Password = $("#password").val().toString();

    //create object to map Login Model
   
    var loginObject = {
        UserId: parseInt(Id),
        Password: Password,
        AccessId:1
    };
    //ajax POST
    $.ajax({
        type: "POST",
        url: "/Authentication/Authenticate",
        data: loginObject,
        dataType: "json",
        success: function (response) {
            if (response.result) {
                //alert(response)
                console.log(response);
                alert("Login Successfull!!")
                window.location = response.url;
            }
            else {
                //alert("Wrong")
                alert("Wrong Credentials")

                return false;
            }
        },
        failure: function (response) {
           
            toastr.error('Unable to make request!!');

        },
        error: function (response) {
            //alert(response.responseText)
            //alert("smth wrong")
            toastr.error('Something happen, Please contact Administrator!!');
        }
    });

}