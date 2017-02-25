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
                    Registration(client, info.Args);
                    break;
                case "LogIn":
                    LogIn(client, info.Args);
                    break;
                case "LogOut":
                    LogOut(client);
                    break;
            }
        }
        private void Registration(Client client, object args)
        {
            object[] arg = JsonConvert.DeserializeObject<object[]>(args.ToString());
            User user = new  User(arg[0].ToString(), arg[1].ToString());
            LinkedList<User> users = GetPersonList();

            foreach (User record in users)
            {
                if (record.name == user.name)
                {
                    lobby.SendNotification("Пользователь существует", client);
                    return;
                }
            }
            users.AddLast(user);
            AppendRecord(user);
            lobby.SendNotification("Вы зарегистрировались", client);
        }

        private void LogIn(Client client, object args)
        {
            object[] arg = JsonConvert.DeserializeObject<object[]>(args.ToString());
            User user = new User(arg[0].ToString(), arg[1].ToString());
            if (clients.clientsList.Find(c => c.name == arg[0].ToString()) != null)
            {
                
                lobby.SendNotification("С таким логином уже вошел в систему", client);
                return;
            }
            LinkedList<User> users = GetPersonList();
            if (users != null)
            {
                foreach (User record in users)
                {
                    if (record.name == user.name)
                    {
                       if(record.password == user.password)
                       {
                            client.name = user.name;
                            client.Write(JsonConvert.SerializeObject(new RequestObject("Auth", "LogIn", user.name)));
                            return;
                        }
                    }
                }
            }
            lobby.SendNotification("Неверное имя пользоваеля или пароль", client);
            
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
