function clientsList() {
    var req = new Request("Lobby", "refreshClients", null);
    ws.send(JSON.stringify(req));
}

function OnInvite() {
    var player = GetSelectedPlayer();
    var req = new Request("HandShake", "Invite", new Array(player, "XO"));
    ws.send(JSON.stringify(req));   
}

function goPlaying(play) {
    var req = new Request("HandShake", "Ok", new Array(play[0], "XO"));
    ws.send(JSON.stringify(req));
}

function move(args) {
    var req = new Request("Game", "Move", args);
    ws.send(JSON.stringify(req));
}

function start(args) {
    var req = new Request("Game", "Start", args);
    ws.send(JSON.stringify(req));
}