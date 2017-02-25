var ws;
function connection() {
    ws = sessionStorage['ws'];
    if (ws === undefined) {
        ws = new WebSocket("ws://localhost:8888");
        var clientsCount = 0;
        var roomsCount = 0;
        ws.onopen = function () {
            sessionStorage['detailPage'] = true;
            sessionStorage['ws'] = ws;
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
        ws = sessionStorage['ws'];
    }
}