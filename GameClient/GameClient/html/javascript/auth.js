function logout() {
    var req = new Request("Auth", "LogOut",  document.getElementById("textLogin").value);
    ws.send(JSON.stringify(req));
    sessionStorage['username'] = undefined;
    sessionStorage['status'] = undefined;
    ShowAuth();
}


function inspection() {
    if (ws === undefined) {
        alert("Connection is closed...");
        return false;
    }
    value;
    if (~login.indexOf(" ") || ~password.indexOf(" ")) {
        alert("take away spaces!");
        return false;
    }
    if (login != "" && password != "") {
        alert("Fill all fields!")
        return false;
    }
    return true;
}
function login() {
    var login = document.getElementById("textLogin").value;
    var password = document.getElementById("textPassword").value;
    
    if(inspection(login, password)==true){
        sessionStorage['username'] = login;
        auth(login, password);
    }
}
function auth(login, password) {
    var req = new Request("Auth", "LogIn", new Array(login, password))
    ws.send(JSON.stringify(req))
}

function registr() {
    var login = document.getElementById("textLogin").value;
    var password = document.getElementById("textPassword").value;
    if (inspection(login, password) == true) {
        var req = new Request("Auth", "Registration", new Array(login, password))
        ws.send(JSON.stringify(req))
    }
}
function Authorization(response) {
    switch (response.Cmd) {
        case "LogIn":
            if (response.Args !== undefined) {
                sessionStorage['username'] = response.Args;
                sessionStorage['status'] = 'loggin';
            }
            ShowLobby();
            break;
    }
}
