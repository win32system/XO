var ws;
function connection() {
    if (ws === undefined) {
        ws = new WebSocket("ws://localhost:8888");
        var clientsCount = 0;
        var roomsCount = 0;
        ws.onopen = function () {
            sessionStorage['detailPage'] = true;
            if (sessionStorage['status'] === "loggin") {                
                var req = new Request("Auth", "status", sessionStorage[username]);
                ws.send(JSON.stringify(req))
            }
        };
        ws.onmessage = function (evt) {
            listener(evt.data);
        };
        ws.onclose = function () {
            sessionStorage['detailPage'] = undefined;
            ws = undefined;
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
    if (sessionStorage['detailPage'] === undefined) {
        connection();
        ShowAuth();
    }
    else {
        ShowLobby();
    }
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
