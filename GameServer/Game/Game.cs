﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
   public class Game
    {
        Rooms rooms;

        public Game(Rooms rooms)
        {
            this.rooms = rooms;
        }

        public void Dispacher(Client client, RequestObject info)
        {
            object[] args = JsonConvert.DeserializeObject<object[]>(info.Args.ToString());
            int index = Convert.ToInt32(args[0]);
            Room room = rooms.rooms[index];
            switch(info.Cmd)
            {
                case "Move":
                    Move(room, client, info.Args);
                    break;
            }
            
        }
        void Move(Room room, Client client, object args)
        {
            room.Move(client.name, args.ToString());
            if (room.IsOver())
            {  
                rooms.Remove(room);
            }
        }
    }
}
