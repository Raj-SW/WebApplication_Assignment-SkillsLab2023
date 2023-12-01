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
    // Replace 'https://api.example.com/data' with the actual API endpoint you want to fetch data from
   
    // Basic GET request
    fetch( "/Authentication/Register", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            // Add any other headers if needed (e.g., authorization token)
        },
        //body: userData
       
    })
        .then(response => {
            console.log(response)
            if (response.ok) {
                return response.json();
            } else {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
        })
        .then(data => {
            console.log(data)
            //  token upon successful login
            //const authToken = data.token;
            // Save the token to use for subsequent authenticated requests if needed
            // Redirect to the desired page
            if (data.result) {
                 window.location = data.url
            }
        })
        .catch(error => {
            console.error('Login error:', error);
        });
}