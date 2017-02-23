using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class RequestObject
    {
        public string Module;
        public string Cmd;
        public object Args;
        public RequestObject() { }
        public RequestObject(string Module, string Cmd, object Args)
        {
            this.Module = Module;
            this.Cmd = Cmd;
            this.Args = Args;
        }
    }
}
