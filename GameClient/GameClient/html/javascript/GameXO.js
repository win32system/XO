
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
    switch (response.Command) {
        case "Invited":
            var r = confirm("User" + response.Message[0] + "wants to play with you");
            if (r == true) {
                goPlaying();
            } else {
               
            }
         
            break;
        case "Wait":
            alert(response.Message[0]);
            break;
    }
}

function Authorization(response)
{
    switch(response.Command)
    {
        case "LogIn":
            document.getElementById("label").innerHTML = response.Message[0];
            ShowLobby();
            break;
    }
}

function Lobby(response)
{
    switch(response.Command)
    {
        case "refreshClients":
            if(response.Message.length>0)
            {
                //for(var i=0; i<response.Message.length; i++)
                //{
                //    var addOpt = new Option(response.Message[i], response.Message[i]);
                //    document.getElementById("clients").options[clientsCount++] = addOpt;
                //}

                playersList.innerHTML = "";

                for (var i = 0; i < response.Message.length; i++) {
                    //if (Message[i] === userName.value) {
                    //    continue;
                    //}
                    playersList.innerHTML += "<input type='radio' name='players' id='" + response.Message[i] + "' />" + response.Message[i] + "<br />";
                }
            }
            break;
        case "Notification":
            alert(response.Message[0]);
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

