using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class Game
    {
        Rooms rooms;

        public Game(Rooms rooms)
        {
            this.rooms = rooms;
        }

        public void Dispacher(Client client, Info info)
        {
            int index = Convert.ToInt32(info.Message[0]);
            Room room = rooms.rooms[index];
            switch(info.Command)
            {
                case "Move":
                    Move(room, client, info.Message);
                    break;
            }
            
        }
        void Move(Room room, Client client, List<string> message)
        {
            room.Move(client.name, message);

            if (room.IsOver())
            {
                rooms.Remove(room);
            }
        }
    }
}
