let leagues = [];
let connection = null;

let leagueIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:8739/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("LeagueCreated", (user, message) => {
        getdata();
    });
    connection.on("LeagueDeleted", (user, message) => {
        getdata();
    });
    connection.on("LeagueUpdated", (user, message) => {
        getdata();
    });

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
    await fetch('http://localhost:8739/league')
        .then(x => x.json())
        .then(y => {
            leagues = y;
            console.log(leagues);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    leagues.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.leagueId + "</td><td>"
            + t.name + "</td><td>" +
        `<button type="button" onclick="remove(${t.leagueId})">Delete </button>` +
        `<button type="button" onclick="showupdate(${t.leagueId})">Update </button>`
            + "</td></tr>";
    })
}

function remove(id) {
    fetch('http://localhost:8739/league/' + id, {
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
    document.getElementById('leaguenametoupdate').value = leagues.find(t => t['leagueId'] == id)['name'];
    document.getElementById('updateformdiv').style.display = 'flex';
    leagueIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = "none";
    let name = document.getElementById('leaguenametoupdate').value;
    fetch('http://localhost:8739/league', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, leagueId: leagueIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let name = document.getElementById('leaguename').value;
    fetch('http://localhost:8739/league', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name })})
        .then(response => response)
        .then(data =>
        {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });    
}