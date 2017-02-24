using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    class Authorization
    {
       
        private Clients clients;
        private Lobby lobby = new Lobby();
        private static string AuthFolder = "Users.json";

        public Authorization(Clients clients)
        {
            this.clients = clients;
        }

        public void Dispacher(Client client, RequestObject info)
        {
            switch (info.Cmd)
            {
                case "Registration":
                    string res = Registration(client, info.Args);
                    lobby.SendNotification(res, client);
                    break;
                case "LogIn":
                    res = LogIn(client, info.Args);
                    if (res != "")
                        lobby.SendNotification(res, client);
                    break;
                case "LogOut":
                    LogOut(client);
                    break;
            }
        }
        private string Registration(Client client, object args)
        {
            object[] arg = JsonConvert.DeserializeObject<object[]>(args.ToString());
            User user = new  User(arg[0].ToString(), arg[1].ToString());
            LinkedList<User> users = GetPersonList();

            foreach (User record in users)
            {
                if (record.name == user.name)
                {
                    return "Пользователь существует";
                }
            }
            users.AddLast(user);
            AppendRecord(user);
            return "Вы зарегистрировались";
        }

        private string LogIn(Client client, object args)
        {
            object[] arg = JsonConvert.DeserializeObject<object[]>(args.ToString());
            User user = new User(arg[0].ToString(), arg[1].ToString());
            if (clients.clientsList.Find(c => c.name == arg[0].ToString()) != null)
            {
                return "С таким логином уже вошел в системе";
            }
            LinkedList<User> users = GetPersonList();
            if (users == null)
            {
                foreach (User record in users)
                {
                    if (record.name == user.name || record.password == user.password)
                    {
                       return "Неверное имя пользоваеля или пароль";
                    }
                }
            }
            client.name = user.name;
            client.Write(JsonConvert.SerializeObject(new RequestObject("Auth", "LogIn", user.name)));
            return "";
        }
        
        private void LogOut(Client client)
        {
            clients.Dell(client);
        }
     
        private LinkedList<User> GetPersonList()
        {
            LinkedList<User> records = new LinkedList<User>();
            if (!File.Exists(AuthFolder))
            {
                return records;
            }
            string[] json = File.ReadAllLines(AuthFolder);
            if (json[0] == "") return records;

            int length = json.Length;
            for(int i=0; i < length; i++)
            {
                User rec = JsonConvert.DeserializeObject<User>(json[i]);
                records.AddLast(rec);
            }

            return records;
        }
        private void AppendRecord(User user)
        {
            File.AppendAllLines(AuthFolder, new string[] { JsonConvert.SerializeObject(user) });
        }
      
    }
}
