let teams = [];

fetch('http://localhost:8739/team')
    .then(x => x.json())
    .then(y => {
        teams = y;
        console.log(y);
        display();
    });

function display() {
    document.getElementById('resultarea').innerHTML = "";
    teams.forEach(t => {
        document.getElementById('resultarea').innerHTML += 
            "<tr><td>" + t.teamId + "</td><td>"
        + t.name + "</td><td>" + t.leagueId + "</td><td>"
        + t.stadium + "</td><td>" + t.headCoach + "</td><td>"
        + t.founded + "</td><td>" +
            `<button type="button" onclick="remove(${t.teamId})">Delete </button>` +
            `<button type="button" onclick="showupdate(${t.teamid})">Update </button>`
            + "</td></tr>";
    })
}

function remove(id) {
    alert(id);
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
    display();
}