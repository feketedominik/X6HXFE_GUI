let leagues = [];

fetch('http://localhost:8739/league')
    .then(x => x.json())
    .then(y => {
        leagues = y;
        console.log(leagues);
        display();
    });

function display() {
    leagues.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.leagueId + "</td><td>"
            + t.name + "</td></tr>";
        console.log(t.name);
    })
}