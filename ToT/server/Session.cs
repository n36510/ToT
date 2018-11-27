using System;
using System.Collections.Generic;
using System.Net.Sockets;


namespace server
{
    class Session
    {
        public TcpClient c;
        public string userName;

        public Session(TcpClient cli, string uid)
        {
            c = cli;
            userName = uid;
        }
    }
}
