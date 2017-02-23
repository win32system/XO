using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class Rooms
    {
        public List<Room> rooms;

        public Rooms()
        {
            rooms = new List<Room>();
        }

        public void Add(List<Client> clients, string gameName)
        {
            rooms.Add(new Room(clients, gameName));
        }
        public void Remove(Room room)
        {
            for(int i=0; i<room.clients.Count; i++)
            {
                room.clients[i].inGame = false;
            }
            rooms.Remove(room);
        }

    }
}
