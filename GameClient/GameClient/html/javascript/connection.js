function onloadsocket() {
    if (ws === undefined) {
        var ws = new WebSocket("ws://localhost:8888");
        var clientsCount = 0;
        var roomsCount = 0;
        ws.onopen = function () {
            auth();
        };
        ws.onmessage = function (evt) {
            listener(evt.data);
        };
        ws.onclose = function () {
            alert("Connection is closed...");
        };
        return ws;
    }
};
function Request(Module,Cmd,Args) {
    this.Module=Module;
    this.Cmd=Cmd;
    this.Args = Args;
}