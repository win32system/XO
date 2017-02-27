function clientsList() {
    var req = new Request("Lobby", "refreshClients", null);
    sendMessage(req);
}

function OnInvite() {
    var player = GetSelectedPlayer();
    var req = new Request("HandShake", "Invite", new Array(player, "XO"));
    sendMessage(req);
}

function goPlaying(play) {
    var req = new Request("HandShake", "Ok", new Array(play[0], "XO"));
    sendMessage(req);
}

function move(args) {  
    var req = new Request("Game", "Move", args);
    sendMessage(req);
}
function move(args) {
    var req = new Request("Game", "Remove", args);
    sendMessage(req);
}
function start(args) {
    var req = new Request("Game", "Start", args);
    sendMessage(req);
}

