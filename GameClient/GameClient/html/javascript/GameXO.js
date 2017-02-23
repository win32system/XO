
//playersList = document.getElementById("playersList");

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

    }
}


function HandShake(response) {
    switch (response.Cmd) {
        case "Invited":
            var r = confirm("User" + response.Args + "wants to play with you");
            if (r == true) {
                goPlaying();
            } else {
               
            }
         
            break;
        case "Wait":
            alert(response.Args);
            break;
    }
}

function Authorization(response)
{
    switch(response.Cmd)
    {
        case "LogIn":
            document.getElementById("label").innerHTML = response.Args;
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
                //for(var i=0; i<response.Message.length; i++)
                //{
                //    var addOpt = new Option(response.Message[i], response.Message[i]);
                //    document.getElementById("clients").options[clientsCount++] = addOpt;
                //}

                playersList.innerHTML = "";
                var personlist = new Array(response.Args);
                for (var i = 0; i < personlist[0].length; i++) {
                    //if (Message[i] === userName.value) {
                    //    continue;
                    //}
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

