
//playersList = document.getElementById("playersList");
var playerMove;
var roomNumber;
var userName;

function listener(response)
{
    var req = JSON.parse(response);
    switch(req.Module)
    {
        case "Auth":
            Authorization(req);
            break;
        case "Lobby":
            Lobby(req);
            break;
        case "HandShake":
            HandShake(req);
            break;
        case "Game":
            Game(req);
            break;
            

    }
}


function HandShake(response) {
    switch (response.Cmd) {
        case "Invited":
            var r = confirm("User " + response.Args[0] + " wants to play with you");
            if (r == true) {
                goPlaying(response.Args);
            } else {
               
            }
         
            break;
        case "Wait":
            alert("Wait");
            break;
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

        case "Over":
            ShowLobby();
            break;
        case "Move":
            moveBtn(response.Args);

            break;

    }
}

function Authorization(response)
{
    switch(response.Cmd)
    {
        case "LogIn":
            if (response.Args !== undefined)
                sessionStorage['username'] = "Your name: " + response.Args;
            ShowLobby();
            break;
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

    switch (coord) {
        case 1:

            move(new Array(roomNumber[0], 0, 0));

            break;
        case 2:

            move(new Array(roomNumber[0], 0, 1));

            break;
        case 3:

            move(new Array(roomNumber[0], 0, 2));

            break;
        case 4:
            move(new Array(roomNumber[0], 1, 0));

            break;
        case 5:
            move(new Array(roomNumber[0], 1, 1));

            break;
        case 6:
            move(new Array(roomNumber[0], 1, 2));

            break;
        case 7:
            move(new Array(roomNumber[0], 2, 0));

            break;
        case 8:
            move(new Array(roomNumber[0], 2, 1));

            break;
        case 9:
            move(new Array(roomNumber[0], 2, 2));

            break;
        default:
            return;
    }

}

function moveBtn(args) {

    if (args[1] == 0 && args[2] == 0) {
        b1.value = args[0];
    }
    else if (args[1] == 0 && args[2] == 1) {
        b2.value = args[0];
    }
    else if (args[1] == 0 && args[2] == 2) {
        b3.value = args[0];
    }
    else if (args[1] == 1 && args[2] == 0) {
        b4.value = args[0];
    }
    else if (args[1] == 1 && args[2] == 1) {
        b5.value = args[0];
    }
    else if (args[1] == 1 && args[2] == 2) {
        b6.value = args[0];
    }
    else if (args[1] == 2 && args[2] == 0) {
        b7.value = args[0];
    }
    else if (args[1] == 2 && args[2] == 1) {
        b8.value = args[0];
    }
    else if (args[1] == 2 && args[2] == 2) {
        b9.value = args[0];
    }


    console.log(args);
}