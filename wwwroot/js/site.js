const uri = '/api/todo';
let tasks = [];
// Retrieve token from local storage
const token = localStorage.getItem("current-token");

// Headers for server requests
let headerReq = {
    method: 'GET',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + token
    },
};

// Check if token exists, if not redirect to login page
function isThereToken() {
    if (!token)
        window.location.href = '../html/login.html';
}

// Get current user's data from the server
function getMyUser() {
    isThereToken();
    
    return fetch('api/user', headerReq)
        .then(response => response.json())
        .then(user => {
            return user;
        })
        .catch(error => console.log('Unable to get items.', error));
}

// Get list of tasks from the server
function getItems() {
    fetch(uri, headerReq)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.log('Unable to get items.', error));
}

// Add a new task
async function addItem() {
    isThereToken();
    const addNameTextbox = document.getElementById('add-name');
    const addDateToDo = document.getElementById('add-dateToDo');

    let item = {
        userId: 0,
        isDone: false,
        name: addNameTextbox.value.trim(),
        dateToDo: addDateToDo.value.toString(),
        id: 0
    };

    // Get current user's data to add the task for them
    await getMyUser().then(user => item.userId = user.id);

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': "Bearer " + token
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
            addDateToDo.value = "";
        })
        .catch(error => console.log('Unable to add item.', error));
}

// Delete a task
function deleteItem(id) {
    isThereToken();
    fetch(`${uri}/${id}`, {
        method: 'DELETE',
        headers: {
            'Authorization': "Bearer " + token
        }
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

// Display form for editing a task
function displayEditForm(id) {
    const item = tasks.find(item => item.id === id);

    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-isDone').checked = item.isDo;
    document.getElementById('editForm').style.display = 'block';
    document.getElementById('edit-date').value = item.dateToDo;
}

// Update a task
function updateItem() {
    isThereToken();
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        isDo: document.getElementById('edit-isDone').checked,
        name: document.getElementById('edit-name').value.trim(),
        dateToDo: document.getElementById('edit-date').value.trim()
    };

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': "Bearer " + token
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

// Close input form
function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

// Display the count of items
function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'task' : 'task kinds';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

// Display all tasks
function _displayItems(data) {
    const tBody = document.getElementById('tasks');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let isDoneCheckbox = document.createElement('input');
        isDoneCheckbox.type = 'checkbox';
        isDoneCheckbox.disabled = true;
        isDoneCheckbox.checked = item.isDo;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isDoneCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        let textNode2 = document.createTextNode(item.dateToDo);
        td3.appendChild(textNode2);

        let td4 = tr.insertCell(3);
        td4.appendChild(editButton);

        let td5 = tr.insertCell(4);
        td5.appendChild(deleteButton);
    });

    tasks = data;
}

