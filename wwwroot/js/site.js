const uri = '/todo';
let pizzas = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}
const x=0;
function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addDateTextbox = document.getElementById('add-date');
    const item = {
        Id:x+1,
        Name: addNameTextbox.value.trim(),
        DateToDo:addDateTextbox.value.trim(),
        IsDo: false,
        
    };

    fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
            method: 'DELETE'
        })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = pizzas.find(item => item.id === id);

    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-isdone').checked = item.isdone;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        isdone: document.getElementById('edit-isdone').checked,
        name: document.getElementById('edit-name').value.trim()
    };

    fetch(`${uri}/${itemId}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'task' : 'my tasks ';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    console.log(data)
    // const tBody = document.getElementById('Tasks');
    // tBody.innerHTML = '';

    // _displayCount(data.length);

    // const button = document.createElement('button');

    // data.forEach(item => {
    //     let isdoneCheckbox = document.createElement('input');
    //     isdoneCheckbox.type = 'checkbox';
    //     isdoneCheckbox.disabled = true;
    //     isdoneCheckbox.checked = item.isdone;

    //     let editButton = button.cloneNode(false);
    //     editButton.innerText = 'Edit';
    //     editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

    //     let deleteButton = button.cloneNode(false);
    //     deleteButton.innerText = 'Delete';
    //     deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

    //     let tr = tBody.insertRow();

    //     let td1 = tr.insertCell(0);
    //     td1.appendChild(isdoneCheckbox);

    //     let td2 = tr.insertCell(1);
    //     let textNode = document.createTextNode(item.name);
    //     td2.appendChild(textNode);

    //     let td3 = tr.insertCell(2);
    //     td3.appendChild(editButton);

    //     let td4 = tr.insertCell(3);
    //     td4.appendChild(deleteButton);
    // });

    // pizzas = data;
}