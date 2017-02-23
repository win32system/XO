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
        Clients clients;
        Lobby lobby = new Lobby();
        public Authorization(Clients clients)
        {
            this.clients = clients;
        }

        public void Dispacher(Client client, Info info)
        {
            switch (info.Command)
            {
                case "Registration":
                    Registration(client, info.Message);
                    break;
                case "LogIn":
                    LogIn(client, info.Message);
                    break;
                case "LogOut":
                    LogOut(client);
                    break;
            }
        }
        private void Registration(Client client, List<string> message)
        {
            Info info = new Info();
            User user = new User(message[0], message[1]);
            List<User> users = new List<User>();
            if (!File.Exists("Users.json"))
            {
                var stream = new FileStream("Users.json", FileMode.Create);
                stream.Close();
            }
            else
            {
                string json = File.ReadAllText("Users.json");
                if (json != "")
                    users = JsonConvert.DeserializeObject<List<User>>(json);
            }
            if (users.Find(tmpuser => tmpuser.name == user.name) != null)
            {
                lobby.SendNotification("Пользователь с таким именем уже сущесвует", client);
                return;
            }
            if (user.name == "" || user.name == " " || user.password == "" || user.password == " ")
            {
                lobby.SendNotification("Имя пользователя и пароль не должны быть пустыми", client);
                return;
            }
            users.Add(user);
            string strUsers = JsonConvert.SerializeObject(users);
            File.WriteAllText("Users.json", strUsers);
            lobby.SendNotification("Вы зарегистрировались, теперь вы можете войти.", client);
        }
        private void LogIn(Client client, List<string> message)
        {
            User user = new User(message[0], message[1]);
            List<User> users = new List<User>();
            if (!File.Exists("Users.json"))
            {
                var stream = new FileStream("Users.json", FileMode.Create);
                stream.Close();
            }
            else
            {
                string json = File.ReadAllText("Users.json");
                if (json != "")
                    users = JsonConvert.DeserializeObject<List<User>>(json);
            }

            if (users.Find(tmpUser => tmpUser.name == user.name && tmpUser.password == user.password) == null)
            {
                lobby.SendNotification("Неверное имя пользоваеля или пароль", client);
                return;
            }
            if (clients.clientsList.Find(c => c.name == message[0]) != null)
            {
                lobby.SendNotification("Пользователь с таким логином уже вошел в систему", client);
                return;
            }
            Info info = new Info();
            info.Module = "Auth";
            info.Command = "LogIn";
            info.Message.Clear();
            info.Message.Add(message[0]);

            string strinfo = JsonConvert.SerializeObject(info);
            client.name = message[0];
            client.Write(strinfo);

            Thread.Sleep(100);
            lobby.SendClients(client, clients.clientsList);
        }

        private void LogOut(Client client)
        {
            clients.Dell(client);

        }
    }
}
