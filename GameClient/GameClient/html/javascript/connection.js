var ws;
window.onload = function () {
    ws = new WebSocket("ws://localhost:8888");
    var clientsCount=0;
    var roomsCount=0;
    ws.onopen = function () {};
    ws.onmessage = function (evt) {
        listener(evt.data);
    };
    ws.onclose = function () {
        alert("Connection is closed...");
    };
};
function Request(Module,Command,Message) {
    this.Module=Module;
    this.Command=Command;
    this.Message=Message;
}