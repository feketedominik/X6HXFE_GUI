let players = [];
let connection = null;

let playersIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:8739/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("PlayerCreated", (user, message) => {
        getdata();
    });
    //connection.on("PlayerDeleted", (user, message) => {
    //    getdata();
    //});
    //connection.on("PlayerUpdated", (user, message) => {
    //    getdata();
    //});

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:8739/player')
        .then(x => x.json())
        .then(y => {
            players = y;
            console.log(players);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    players.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.playerId + "</td><td>"
        + t.name + "</td><td>" +
        + t.teamId + "</td><td>" + t.nationality + "</td><td>" +
            t.position +"</td><td>" + t.born + "</td><td>" +
        `<button type="button" onclick="remove(${t.playerId})">Delete </button>` +
        `<button type="button" onclick="showupdate(${t.playerId})">Update </button>`
            + "</td></tr>";
    })
}
/*
function remove(id) {
    fetch('http://localhost:8739/player/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function showupdate(id) {
    document.getElementById('playernametoupdate').value = players.find(t => t['playerId'] == id)['name'];
    document.getElementById('updateformdiv').style.display = 'flex';
    playerIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = "none";
    let name = document.getElementById('playernametoupdate').value;
    fetch('http://localhost:8739/player', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, playerId: playerIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
*/
function create() {
    let name = document.getElementById('playername').value;
    let teamId = document.getElementById('playerteam').value;
    let nationality = document.getElementById('playernationality').value;
    let position = document.getElementById('playerposition').value;
    let born = document.getElementById('playerborn').value;
    fetch('http://localhost:8739/player', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name,
                teamId: teamId,
                nationality: nationality,
                position: position,
                born: born
            })
    })
        .then(response => response)
        .then(data =>
        {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });    
}