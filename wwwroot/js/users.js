const url="/api/user";
const token = localStorage.getItem("current-token");

function isAdmin(){
    console.log(token)
    fetch(url,{
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization':"Bearer "+token
        },
    }).then(response => response.json())
    .then(user => {
        if(user.TypeUser!=1)
      {
        alert("you not manager");
        window.location.href="../index.html";
      }
      else
        alert("hello"+user.Name);
    return user;
    })
    .catch(error => console.error('Unable to get items.', error));

}