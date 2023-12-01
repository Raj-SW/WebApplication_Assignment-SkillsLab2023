function Register()
{
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
    // Inputs - using FormData()
    const userData = {
        username: 'exampleUser',
        email: 'user@example.com',
        password: 'securePassword',
    };
    fetch( "/Authentication/RegisterUser", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            //( authorization token)
        },
        //body: userData
    })
        .then(response => {
            console.log(response)
            if (response.ok) {
                return response.json();
            } else {
                //toastr.success(`Internal Server error  ${ response.status }`);
                //throw new Error(`HTTP error! Status: ${response.status}`);
            }
        })
        .then(data => {
            //authtoken logic happens here
            if (data.result) {
                //toastr.success('User Registered Please Login Now');
                window.location = data.url
            }
        })
        .catch(error => {
            //toastr.success('Registration error ', error);

        });
}