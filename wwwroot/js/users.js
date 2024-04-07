const url = "/api/user";
let users = [];
// Retrieve token from local storage
const token = localStorage.getItem("current-token");

// Headers for server 'GET' requests
const headerGetReq = {
  method: 'GET',
  headers: {
    'Accept': 'application/json',
    'Content-Type': 'application/json',
    'Authorization': "Bearer " + token
  },
};

// Check if token exists and not expired, if not redirect to login page
function isThereToken() {
  const token = localStorage.getItem("current-token");
  if (!token||isTokenExpired(token)) {
      alert("You dont have a token or your token has expired, please login")
      localStorage.setItem("current-token", "");
      window.location.href = '../html/login.html';
  }
}

//decode a JSON Web Token (JWT) and extract its payload.
function decodeJwt(token) {
  const tokenParts = token.split('.');// splitting the token into header, payload, and signature   
  const payloadBase64 = tokenParts[1].replace(/-/g, '+').replace(/_/g, '/');// extracting the payload (in base64)
  const payload = JSON.parse(decodeURIComponent(escape(atob((payloadBase64))))); // decoding the payload from base64 and parsing to json include hebrew chars
  return payload;
}

// Compares the current date in milliseconds and the expiration date in the token,
// and returns the result of the comparison.
// If the expiration date is less than the current date, the token is marked as expired.
function isTokenExpired(token) {
  return (decodeJwt(token).exp < Date.now() / 1000) ;
}

// Checks if the current user is an administrator
function isAdmin() {
  fetch(url, headerGetReq)
    .then(response => response.json())
    .then(user => {
      if (user.typeUser != 1) {
        alert("you not manager");
        window.location.href = "../index.html";
      }
      else
        document.getElementById("userName").innerText += " " + user.name;

      return user;
    })
    .catch(error => console.error('Unable to get users.', error));

}

// Get list of users from the server
function getUsers() {
  isThereToken();
  fetch('/getAll', headerGetReq)
    .then(response => response.json())
    .then(data => {
      console.log(data);
      _displayusers(data)
    })
    .catch(error => console.log('Unable to get users.', error));
}

// Add a new user
function addUser() {
  isThereToken();
  const addNameTextbox = document.getElementById('add-name');
  const addPassword = document.getElementById('add-password');
  const addUserType = document.getElementById("userType");

  let user = {
    id: 0,
    name: addNameTextbox.value.trim(),
    password: addPassword.value.trim(),
    typeUser: (addUserType.value === "Admin") ? 1 : 0
  };

  fetch(url, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      'Authorization': "Bearer " + token
    },
    body: JSON.stringify(user)
  })
    .then(response => response.json())
    .then(() => {
      getUsers();
      addNameTextbox.value = '';
      addPassword.value = "";
    })
    .catch(error => console.log('Unable to add item.', error));
}

// Delete a user
function deleteUser(id) {
  isThereToken();
  fetch(`${url}/${id}`, {
    method: 'DELETE',
    headers: {
      'Authorization': "Bearer " + token
    }
  }).then(() => getUsers())
    .catch(error => console.error('Unable to delete user.', error));
}


// Display the count of users
function _displayCount(userCount) {
  const name = (userCount === 1) ? 'user' : 'users';

  document.getElementById('counter').innerText = `${userCount} ${name}`;
}

// Display all tasks
function _displayusers(data) {
  const tBody = document.getElementById('users');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(user => {

    const ifAdmin = (user.typeUser == 1) ? "Admin" : "User";

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteUser(${user.id})`);

    let tr = tBody.insertRow();

    let td = tr.insertCell(0);
    let textNode = document.createTextNode(user.id);
    td.appendChild(textNode);

    let td1 = tr.insertCell(1);
    let textNode1 = document.createTextNode(user.name);
    td1.appendChild(textNode1);

    let td2 = tr.insertCell(2);
    let textNode2 = document.createTextNode(user.password);
    td2.appendChild(textNode2);

    let td3 = tr.insertCell(3);
    let textNode3 = document.createTextNode(ifAdmin)
    td3.appendChild(textNode3);

    let td4 = tr.insertCell(4);
    td4.appendChild(deleteButton);

  });

  users = data;
}