function ShowAuth() {
    document.getElementById("auth").style.display = 'flex';
    document.getElementById("lobby").style.display = 'none';
    document.getElementById("game").style.display = 'none';
    document.getElementById("playersList").style.display = 'none';
}
function ShowLobby() {
    document.getElementById("auth").style.display = 'none';
    document.getElementById("lobby").style.display = 'flex';
    document.getElementById("game").style.display = 'none';
    document.getElementById("playersList").style.display = 'flex';
}
function ShowGame() {
    document.getElementById("auth").style.display = 'none';
    document.getElementById("lobby").style.display = 'none';
    document.getElementById("game").style.display = 'flex';
    document.getElementById("playersList").style.display = 'none';
    
    $(".gameField").val(function (index, x) { return ""; });
}
window.onload = function () {
    sessionStorage.
    if (ws !== undefined) {
        ShowLobby();
    }
}