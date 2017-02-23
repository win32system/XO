using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class XO: IGame
    {
        string client1Name;
        string client2Name;
        string turn;

        int[,] feild = new int[3, 3];

        public XO(string client1Name, string client2Name)
        {
            this.client1Name = client1Name;
            this.client2Name = client2Name;

            turn = client1Name;
        }

        public bool IsTurn(string name)
        {
            if (name == turn)
                return true;

            return false;
        }
        public string Move(object message)
        {
           
            RequestObject info = new RequestObject("Game","Move", message);

            object[] args = JsonConvert.DeserializeObject<object[]>(message.ToString());
            int x = Convert.ToInt32(args[1]);
            int y = Convert.ToInt32(args[2]);
            if (feild[x, y] == 0)
            {
                if(turn==client1Name)
                {
                    feild[x, y] = 1;
                    turn = client2Name;
                    info.Args = "X";
                    string strInfo = JsonConvert.SerializeObject(info);
                    return strInfo;
                }
                else
                {
                    feild[x, y] = -1;
                    turn = client1Name;
                    info.Args = "O";
                    string strInfo = JsonConvert.SerializeObject(info);
                    return strInfo;
                }
            }
            return null;
        }

        public bool IsOver()
        {
            int count = 0;
            int fieldvalue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (feild[i, j] != 0)
                        count++;
                }
            }
            if (count == 9)
            {
                return true;
            }

            for (int i = 0; i < 3; i++)
            {
                count = 0;
                if (feild[i, 0] == 0)
                    continue;
                fieldvalue = feild[i, 0];
                for (int j = 1; j < 3; j++)
                {
                    if (feild[i, j] == fieldvalue)
                        count++;
                }
                if (count == 2)
                {
                    return true;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                count = 0;
                if (feild[0, i] == 0)
                    continue;
                fieldvalue = feild[0, i];
                for (int j = 1; j < 3; j++)
                {
                    if (feild[j, i] == fieldvalue)
                        count++;
                }
                if (count == 2)
                {
                    return true;
                }
            }

            if (feild[0, 0] != 0)
            {
                fieldvalue = feild[0, 0];
                if (feild[1, 1] == fieldvalue && feild[2, 2] == fieldvalue)
                {
                    return true;
                }
            }

            if (feild[2, 0] != 0)
            {
                fieldvalue = feild[2, 0];
                if (feild[1, 1] == fieldvalue && feild[0, 2] == fieldvalue)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
