function listener(response)
{
    var req=new Request();
    req=JSON.parse(response, function(k, v)
    {
        return v;
    });
    switch(req.Module)
    {
        case "Auth":
            Authorization(req);
            break;
        case "Lobby":
            Lobby(req);
            break;
    }
}

function Authorization(response)
{
    switch(response.Command)
    {
        case "LogIn":
            document.getElementById("label").innerHTML=response.Message[0];
            break;
    }

}
function Lobby(response)
{
    switch(response.Command)
    {
        case "refreshClients":
            var str_clients="";
            if(response.Message.length>0)
            {
                for(var i=0; i<response.Message.length; i++)
                {
                    var addOpt=new Option(response.Message[i], response.Message[i]);
                    document.getElementById("clients").options[clientsCount++] = addOpt;
                }
            }
            break;

        case "Notification":
            alert(response.Message[0]);
            break;
    }
}


function login()
{
    var req=new Request("Auth","LogIn",new Array(document.getElementById("textLogin").value,document.getElementById("textPassword").value));
    console.log(req);
    ws.send(JSON.stringify(req));
}
function registr()
{
    var req=new Request("Auth","Registration",new Array(document.getElementById("textLogin").value,document.getElementById("textPassword").value));
    console.log(req);
    var re= JSON.stringify(req);
    console.log(re);
    ws.send(re);
}