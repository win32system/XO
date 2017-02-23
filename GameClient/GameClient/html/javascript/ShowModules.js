function ShowAuth() {
    document.getElementById("auth").style.display = 'flex';
    document.getElementById("lobby").style.display = 'none';
    document.getElementById("game").style.display = 'none';
}
function ShowLobby() {
    document.getElementById("auth").style.display = 'flex';
    document.getElementById("lobby").style.display = 'flex';
    document.getElementById("game").style.display = 'none';
}
function ShowGame() {
    document.getElementById("auth").style.display = 'none';
    document.getElementById("lobby").style.display = 'none';
    document.getElementById("game").style.display = 'flex';
}