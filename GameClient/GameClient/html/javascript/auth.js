function logout() {
    var req = new Request("Auth", "LogOut",  document.getElementById("textLogin").value);
    ws.send(JSON.stringify(req));
   
    ShowAuth();
}

function registr() {
    if (ws === undefined) {
        alert("Соединение прервано...");
        return;
    }
    var login = document.getElementById("textLogin").value;
    var password = document.getElementById("textPassword").value;
    var email = document.getElementById("textEmail").value;

    if (~login.indexOf(" ") || ~password.indexOf(" ") || ~email.indexOf(" ")) {
        alert("убери пробел!");
    }

    if (!~email.indexOf("@")) {
        alert("Поле Email должно быть в формате example@exmp.com");
    }
    else if (login != "" && password != "" && email != "") {
        var req = new Request("Auth", "Registration", new Array(login, password, email))
        ws.send(JSON.stringify(req))
    }

    else {
        alert("Заполни все поля");
    }
}

function login() {
    if (ws === undefined) {
        alert("Соединение прервано...");
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
        alert("Заполни все поля");
    }
}

function forget(args) {
    var login = document.getElementById("textLogin").value;
    if (~login.indexOf(" ") || login == "") {
        alert("Заполни поле Login");
    }
    var req = new Request("Auth", "Forget",  new Array(login));
    ws.send(JSON.stringify(req));
}