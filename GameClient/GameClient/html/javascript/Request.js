function login()
{
    var req=new Request("Auth","LogIn",new Array(document.getElementById("textLogin").value,document.getElementById("textPassword").value));
    ws.send(JSON.stringify(req));
}
function registr()
{
    var req=new Request("Auth","Registration",new Array(document.getElementById("textLogin").value,document.getElementById("textPassword").value));
    ws.send(JSON.stringify(req));
}