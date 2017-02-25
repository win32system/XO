function logout() {
    var req = new Request("Auth", "LogOut",  document.getElementById("textLogin").value);
    ws.send(JSON.stringify(req));
   
    ShowAuth();
}

function registr() {
    if (ws === undefined) {
        alert("Connection is closed...");
        return;
    }
    var login = document.getElementById("textLogin").value;
    var password = document.getElementById("textPassword").value;
    if (~login.indexOf(" ") || ~password.indexOf(" ")) {
        alert("убери пробел!");
    }
    else if (login != "" && password != "") {
        var req = new Request("Auth", "Registration", new Array(login, password))
        ws.send(JSON.stringify(req))
    }

    else {
        alert("Fill all fields!");
    }
}

function login() {
    if (ws === undefined) {
        alert("Connection is closed...");
        return;
    }
    auth();
}
function auth() {
    var login = document.getElementById("textLogin").value;
    var password = document.getElementById("textPassword").value;
    if (~login.indexOf(" ") || ~password.indexOf(" ")) {
        alert("убери пробелы!");
    }
    else if (login != "" && password != "") {
        var req = new Request("Auth", "LogIn", new Array(login, password))
        ws.send(JSON.stringify(req))
    }

    else {
        alert("Fill all fields!");
    }
}