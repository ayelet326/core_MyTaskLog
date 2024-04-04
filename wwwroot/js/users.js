const url = "/api/user";
const token = localStorage.getItem("current-token");

function isAdmin() {
  fetch(url, {
    method: 'GET',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      'Authorization': "Bearer " + token
    },
  }).then(response => response.json())
    .then(user => {
      if (user.typeUser != 1) {
        alert("you not manager");
        window.location.href = "../index.html";
      }
      else
        document.getElementById("userName").innerText = " " + user.name;

      return user;
    })
    .catch(error => console.error('Unable to get items.', error));

}