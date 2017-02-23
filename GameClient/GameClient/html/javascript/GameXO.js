function listener(response)
{
    var req = JSON.parse(response);
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
            if(response.Message.length>0)
            {
                for(var i=0; i<response.Message.length; i++)
                {
                    var addOpt = new Option(response.Message[i], response.Message[i]);
                    document.getElementById("clients").options[clientsCount++] = addOpt;
                }
            }
            break;
        case "Notification":
            alert(response.Message[0]);
            break;
    }
}