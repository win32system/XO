var ws;
function connection() {
    if (ws === undefined) {
       
        ws = new WebSocket("ws://127.0.0.1:8888");//192.168.1.100
       
        var clientsCount = 0;
        var roomsCount = 0;
        ws.onopen = function () {
            sessionStorage['detailPage'] = true;
            if (sessionStorage['status'] === "loggin"){
                var req = new Request("Auth", "status", new Array(sessionStorage["username"], sessionStorage["password"]));
                ws.send(JSON.stringify(req));
            }
        };
        ws.onmessage = function (evt) {
            listener(evt.data);
        };
        ws.onclose = function () {
            sessionStorage['detailPage'] = undefined;
            ws = undefined;
            ShowAuth();
            alert("Connection is closed...");
        };
    }
};
function Request(Module,Cmd,Args) {
    this.Module=Module;
    this.Cmd=Cmd;
    this.Args = Args;
}

window.onload = function () {
    if (sessionStorage['status'] === "loggin") {
        ShowLobby();
    }
    else if (sessionStorage['status'] === "logout") {
        ShowAuth();
    }
    connection();
}

function listener(response) {
    var req = JSON.parse(response);
    switch (req.Module) {
        case "Auth": Authorization(req); break;
        case "Lobby": Lobby(req); break;
        case "HandShake": HandShake(req); break;
        case "Game": Game(req); break;
    }
}
function sendMessage(info) { 
    if (ws === undefined) {
        alert("Connection is closed...");
        return;
    }
    ws.send(JSON.stringify(info));
}

/*window.onbeforeunload = function (e) {
    if (document.getElementById("game").style.display === 'flex')
        move(new Array(roomNumber[0], sessionStorage['username']));
    //sendMessage(new Request("Game", "Registration", new Array(login, password, email)));

  //  logout();
};*/