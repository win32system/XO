function login()
{
    var req = new Request("Auth", "LogIn", new Array(
        document.getElementById("textLogin").value,
        document.getElementById("textPassword").value)
        );
    ws.send(JSON.stringify(req));
}
function registr()
{
    var req = new Request("Auth", "Registration", new Array(
        document.getElementById("textLogin").value,
        document.getElementById("textPassword").value)
        );
    ws.send(JSON.stringify(req));
}

function clientsList() {
    var req = new Request("Lobby", "refreshClients", null);
    ws.send(JSON.stringify(req));
}

function OnInvite() {
    var player = GetSelectedPlayer();
    var req = new Request("HandShake", "Invite", new Array(player, "XO"));
    ws.send(JSON.stringify(req));   
}

function goPlaying() {
    var player = GetSelectedPlayer();
    var req = new Request("HandShake", "Ok", new Array(player, document.getElementById("textLogin").value, "XO"));
    ws.send(JSON.stringify(req));
}