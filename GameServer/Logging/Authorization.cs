﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

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
                case "Forget":
                    ForgotPassword(client, info.Args);
                    break;


            }
        }
        private void Registration(Client client, object args)
        {
            object[] arg = JsonConvert.DeserializeObject<object[]>(args.ToString());
            User user = new  User(arg[0].ToString(), arg[1].ToString(), arg[2].ToString());
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
            User user = new User(arg[0].ToString(), arg[1].ToString(), null);
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
            if (json.Length == 0)
                return records; 

            int length = json.Length;
            for(int i=0; i < length; i++)
            {
                User rec = JsonConvert.DeserializeObject<User>(json[i].ToString());
                records.AddLast(rec);
            }

            return records;
        }
        private void AppendRecord(User user)
        {
            File.AppendAllLines(AuthFolder, new string[] { JsonConvert.SerializeObject(user) });
        }

        public static void ForgotPassword(Client client, Object args)
        {
            //RegistredUsers registredUsers = new RegistredUsers();
            //string pass = registredUsers.GetData(login);
            //if (pass != "" && pass != null)
            //{
            //    SmtpClient Smtp = new SmtpClient("smtp.gmail.com", 587);
            //    Smtp.Credentials = new NetworkCredential("bestchat.helper@gmail.com", "bestchat");
            //    MailMessage Message = new MailMessage();
            //    Message.From = new MailAddress("bestchat.helper@gmail.com");
            //    Message.To.Add(new MailAddress(mail));
            //    Message.Subject = "Пароль";
            //    Message.Body = "Ваш пароль : " + pass;
            //    Smtp.EnableSsl = true;
            //    Smtp.Send(Message);

            //}
        }

    }
    


 /* public static void ForgotPassword(string login, string mail)
        {
            RegistredUsers registredUsers = new RegistredUsers();
            string pass = registredUsers.GetData(login);
            if (pass != "" && pass != null)
            {
                SmtpClient Smtp = new SmtpClient("smtp.gmail.com", 587);
                Smtp.Credentials = new NetworkCredential("bestchat.helper@gmail.com", "bestchat");
                MailMessage Message = new MailMessage();
                Message.From = new MailAddress("bestchat.helper@gmail.com");
                Message.To.Add(new MailAddress(mail));
                Message.Subject = "Пароль";
                Message.Body = "Ваш пароль : " + pass;
                Smtp.EnableSsl = true;
                Smtp.Send(Message);

           }
   }*/


}

