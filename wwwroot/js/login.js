const loginUrl = '/api/Login';
const form_login = document.getElementById("form_login");

//sign in by login-form
form_login.onsubmit=(event)=>{
    event.preventDefault();
    const name = document.getElementById("name").value;
    const password = document.getElementById("password").value;
    login(name,password);
}
// login by name and password
function login(name,password) {
  
    const user = {
        Id: 0,
        Name: name,
        Password: password,
        TypeUser: null
    };

    fetch(loginUrl, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
        .then(response => {
            if (response.status === 401)
                throw new Error();
            return response.json();
        }
        )
        .then((token) => {
            saveInLocalStorage(token);
            window.location.href = "../index.html";
        })
        .catch((error) => {
            alert('user not exist');
            location.href = `login.html`;

        }
        ).finally(() => {
            name.value = " ";
            password.value = " ";
        });

};

function saveInLocalStorage(token) {
    localStorage.setItem("current-token", token);
}

//sign in by google account
//handle the google button
handleCredentialResponse = (response) => {
    if (response.credential) {
        var idToken = response.credential;
        var decodedToken = decodeJwt(idToken);  
        var userName = decodedToken.name; // User Name
        var userPassword = decodedToken.email; // User Password=> only for users who registered their email as their password

        login(userName,userPassword);

    } else {
        alert('Google Sign-In was cancelled.');
    }
}



//decode a JSON Web Token (JWT) and extract its payload.
function decodeJwt(token) {
    const tokenParts = token.split('.');// splitting the token into header, payload, and signature   
    const payloadBase64 = tokenParts[1].replace(/-/g, '+').replace(/_/g, '/');// extracting the payload (in base64)
    const payload = JSON.parse(decodeURIComponent(escape(atob((payloadBase64))))); // decoding the payload from base64 and parsing to json include hebrew chars
    return payload;
}







