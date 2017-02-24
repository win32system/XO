using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class XO : IGame
    {
        private string[] feild;

        private string[] combinations;

        public XO()
        {
            feild = new string[] { "", "", "", "", "", "", "", "", "", };
            combinations = new string[8];
        }

        public void Turn(int index, string unit)
        {
            feild[index] = unit;
        }

        public string[] GetMatrix()
        {
            return feild;
        }
        public string Result { set; get; }
        public bool IsGameOver()
        {
            string[] combinations = new string[8];
            combinations[0] = feild[0] + feild[1] + feild[2];
            combinations[1] = feild[3] + feild[4] + feild[5];
            combinations[2] = feild[6] + feild[7] + feild[8];
            combinations[3] = feild[0] + feild[3] + feild[6];
            combinations[4] = feild[1] + feild[4] + feild[7];
            combinations[5] = feild[2] + feild[5] + feild[8];
            combinations[6] = feild[0] + feild[4] + feild[8];
            combinations[7] = feild[2] + feild[4] + feild[6];

            bool draw = true;
            for (int i = 0; i < combinations.Length; i++)
            {
                if (combinations[i].Length >= 3)
                {
                    if (combinations[i].All(ch => ch == 'X') || combinations[i].All(ch => ch == '0'))
                    {
                        Result = combinations[i].First() + " win!";
                        return true;
                    }
                }
                else
                {
                    draw = false;
                }
            }

            if (draw)
            {
                Result = "Draw!";
            }
            return draw;
        }
    }
}
