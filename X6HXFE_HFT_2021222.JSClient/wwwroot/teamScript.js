let teams = [];
let connection = null;

let teamIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:8739/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("TeamCreated", (user, message) => {
        getdata();
    });
    connection.on("TeamDeleted", (user, message) => {
        getdata();
    });
    connection.on("TeamUpdated", (user, message) => {
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
    await fetch('http://localhost:8739/team')
        .then(x => x.json())
        .then(y => {
            teams = y;
            console.log(teams);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    teams.forEach(t => {
        document.getElementById('resultarea').innerHTML += 
            "<tr><td>" + t.teamId + "</td><td>"
        + t.name + "</td><td>" + t.leagueId + "</td><td>"
        + t.stadium + "</td><td>" + t.headCoach + "</td><td>"
        + t.founded + "</td><td>" +
            `<button type="button" onclick="remove(${t.teamId})">Delete </button>` +
            `<button type="button" onclick="showupdate(${t.teamId})">Update </button>`
            + "</td></tr>";
    })
}

function remove(id) {
    fetch('http://localhost:8739/team/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function create() {
    let name = document.getElementById('teamname').value;
    let leagueId = document.getElementById('teamleague').value;
    let stadium = document.getElementById('teamstadium').value;
    let headCoach = document.getElementById('teamheadcoach').value;
    let founded = document.getElementById('teamfounded').value;
    fetch('http://localhost:8739/team', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name,
                leagueId: leagueId,
                stadium: stadium,
                headCoach: headCoach,
                founded: founded
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });    
}

function showupdate(id) {
    document.getElementById('teamnametoupdate').value = teams.find(t => t['teamId'] == id)['name'];
    document.getElementById('teamleaguetoupdate').value = teams.find(t => t['teamId'] == id)['leagueId'];
    document.getElementById('teamstadiumtoupdate').value = teams.find(t => t['teamId'] == id)['stadium'];
    document.getElementById('teamheadcoachtoupdate').value = teams.find(t => t['teamId'] == id)['headCoach'];
    document.getElementById('teamfoundedtoupdate').value = teams.find(t => t['teamId'] == id)['founded'];
    document.getElementById('updateformdiv').style.display = 'flex';
    teamIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = "none";
    let name = document.getElementById('teamnametoupdate').value;
    let leagueId = document.getElementById('teamleaguetoupdate').value;
    let stadium = document.getElementById('teamstadiumtoupdate').value;
    let headCoach = document.getElementById('teamheadcoachtoupdate').value;
    let founded = document.getElementById('teamfoundedtoupdate').value;
    fetch('http://localhost:8739/team', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name, teamId: teamIdToUpdate,
                leagueId: leagueId,
                stadium: stadium,
                headCoach: headCoach,
                founded: founded
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}