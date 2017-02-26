function clientsList() {
    if (ws === undefined) {
        alert("Connection is closed...");
        return;
    }
    var req = new Request("Lobby", "refreshClients", null);
    ws.send(JSON.stringify(req));
}

function OnInvite() {
    if (ws === undefined) {
        alert("Connection is closed...");
        return;
    }
    var player = GetSelectedPlayer();
    var req = new Request("HandShake", "Invite", new Array(player, "XO"));
    ws.send(JSON.stringify(req));   
}

function goPlaying(play) {
    if (ws === undefined) {
        alert("Connection is closed...");
        return;
    }
    var req = new Request("HandShake", "Ok", new Array(play[0], "XO"));
    ws.send(JSON.stringify(req));
}

function move(args) {
    if (ws === undefined) {
        alert("Connection is closed...");
        return;
    }
    var req = new Request("Game", "Move", args);
    ws.send(JSON.stringify(req));
}

function start(args) {
    if (ws === undefined) {
        alert("Connection is closed...");
        return;
    }
    var req = new Request("Game", "Start", args);
    ws.send(JSON.stringify(req));
}

