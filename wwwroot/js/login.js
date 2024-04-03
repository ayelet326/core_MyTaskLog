const loginUrl='/api/Login';
const form_login=document.getElementById("form_login");
////////// log in////////////
form_login.onsubmit = (event) => {
    event.preventDefault(); 
    const name = document.getElementById("name").value;
    const password = document.getElementById("password").value;

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
    .then(response =>
     {
        if (response.status===401)
            throw new Error();
        return response.json();
    }
    )
    .then((token) => {
        console.log(token)
        saveInLocalStorage(token);
        window.location.href="../index.html";
    })
    .catch((error) =>{
        alert('user not exist');
      
        location.href=`login.html`;

    } 
    ).finally(()=>{
        name.value=" ";
        password.value=" ";
    });

};
 
function saveInLocalStorage(token){
    localStorage.setItem("current-token", token);
}