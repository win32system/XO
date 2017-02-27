var playerMove;
var roomNumber;
var userName;

function HandShake(response) {
    switch (response.Cmd) {
        case "Invited":
            var r = confirm("User " + response.Args[0] + " wants to play with you");
            if (r == true)  goPlaying(response.Args);
            break;
        case "Wait": alert("Wait"); break;
    }
}

function Game(response)
{
    switch (response.Cmd) {
        case "Start":
            roomNumber = response.Args;
            start(roomNumber);
            ShowGame();
            break;
        case "Over": alert(response.Args); ShowLobby();  break;
        case "Move": moveBtn(response.Args); break;
        case "Role": statusPlay(response.Args); break; 
    }
}
var gamestorage;
function statusPlay(args) {
    if (args === sessionStorage["username"]) {
        gamestorage = "X";
        course.innerHTML = "play: "+gamestorage+" go";
    }
    else {
        gamestorage = "O";
        course.innerHTML = "play: " + gamestorage+" wait";
    }
}

function Lobby(response)
{
    switch(response.Cmd)
    {
        case "refreshClients":
            if (response.Args.length > 0)
            {
                playersList.innerHTML = "";
                var personlist = new Array(response.Args);
                for (var i = 0; i < personlist[0].length; i++) {
                    playersList.innerHTML += "<input type='radio' name='players' id='" + personlist[0][i] + "' />" + personlist[0][i] + "<br />";
                }
            }
            break;
        case "Notification":
            alert(response.Args);
            break;
    }
}

function AddToplayersList(names) {
    playersList.innerHTML = "";

    for (var i = 0; i < names.length; i++) {
        if (names[i] === userName.value) {
            continue;
        }
        playersList.innerHTML += "<input type='radio' name='players' id='" + names[i] + "' />" + names[i] + "<br />";
    }
}

function GetSelectedPlayer() {
    
    for (var i = 0; i < playersList.childNodes.length; i++) {
        if (playersList.childNodes[i].checked) {
            return playersList.childNodes[i + 1].nodeValue;
        }
    }
}

function OnButtonClicked(coord) {
    move(new Array(roomNumber[0],gamestorage, coord, sessionStorage['username']));
}

function moveBtn(args) {
    if (gamestorage === args[0]) {
        course.innerHTML = "play: " + gamestorage + " wait";
    }
    else {
        course.innerHTML = "play: " + gamestorage + " go";
    }
    
    if (args[1] == 0) {
        b0.value = args[0];
    }
    else if (args[1] == 1) {
        b1.value = args[0];
    }
    else if (args[1] == 2) {
        b2.value = args[0];
    }
    else if (args[1] == 3) {
        b3.value = args[0];
    }
    else if (args[1] == 4) {
        b4.value = args[0];
    }
    else if (args[1] == 5) {
        b5.value = args[0];
    }
    else if (args[1] == 6) {
        b6.value = args[0];
    }
    else if (args[1] == 7) {
        b7.value = args[0];
    }
    else if (args[1] == 8) {
        b8.value = args[0];
    }
    
    console.log(args);
}