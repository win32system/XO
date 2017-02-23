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
    switch(response.Cmd)
    {
        case "LogIn":
            document.getElementById("label").innerHTML = response.Args[0];
            break;
    }

}
function Lobby(response)
{
    switch (response.Cmd)
    {
        case "refreshClients":
            if (response.Args.length > 0)
            {
                for (var i = 0; i < response.Args.length; i++)
                {
                    var addOpt = new Option(response.Args[i], response.Args[i]);
                    document.getElementById("clients").options[clientsCount++] = addOpt;
                }
            }
            break;
        case "Notification":
            alert(response.Args);
            break;
    }
}