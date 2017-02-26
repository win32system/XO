function logout() {
    if (ws === undefined) {
        alert("Connection is closed...");
        return;
    }
    var req = new Request("Auth", "LogOut", document.getElementById("textLogin").value);
    ws.send(JSON.stringify(req));
    sessionStorage['username'] = undefined;
    sessionStorage['password'] = undefined;
    sessionStorage['status'] = undefined;
    ShowAuth();
}

function inspection(login, password) {
    if (ws === undefined) {
        alert("Connection is closed...");
        return false;
    }
    if (~login.indexOf(" ") || ~password.indexOf(" ") ) {
        alert("take away spaces!");
        return false;
    }
    if (login == "" && password == "") {
        alert("Fill all fields!")
        return false;
    }
    return true;
}
function inspectionregist(login, password, email) {
    if (ws === undefined) {
        alert("Connection is closed...");
        return false;
    }
    if (!~email.indexOf("@")) {
        alert("Поле Email должно быть в формате example@exmp.com");
        return false;
    }
    if (~login.indexOf(" ") || ~password.indexOf(" ") || ~email.indexOf(" ")) {
        alert("take away spaces!");
        return false;
    }
    if (login == "" && password == "" && email=="") {
        alert("Fill all fields!")
        return false;
    }
    return true;
}
function login() {
    var login = document.getElementById("textLogin").value;
    var password = document.getElementById("textPassword").value;
    connection();
    if(inspection(login, password)==true){
        sessionStorage['username'] = login;
        sessionStorage['password'] = password;
        auth(login, password);
    }
}

function auth(login, password) {
    if (ws === undefined) {
        alert("Connection is closed...");
        return;
    }
    var req = new Request("Auth", "LogIn", new Array(login, password))
    
    ws.send(JSON.stringify(req))
}

function registr() {
    if (ws === undefined) {
        alert("Connection is closed...");
        return;
    }
    var login = document.getElementById("textLogin").value;
    var password = document.getElementById("textPassword").value;
    var email = document.getElementById("textEmail").value;
    if (inspectionregist(login, password, email) == true){
        var req = new Request("Auth", "Registration", new Array(login, password, email));
        ws.send(JSON.stringify(req));
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

function forget(login) {
    
    if (ws === undefined) {
        alert("Connection is closed...");
        return;
    }

    if (~login.indexOf(" ") || login == "") {
        alert("Fill Login field");
        return;
    }
    var req = new Request("Auth", "Forget", login);
    ws.send(JSON.stringify(req));

}