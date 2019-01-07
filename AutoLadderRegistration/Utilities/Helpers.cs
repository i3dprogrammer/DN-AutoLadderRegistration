using DNSecurityAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AutoLadderRegistration.Utilities
{
    class Helpers
    {
        public static int FreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }

        public static Packet FormLoginPacket(string username, string password, string ip, string mac)
        {
            if (username.Length >= 32)
                throw new Exception("Username too large.");
            if (password.Length > 20)
                throw new Exception("Password too large.");

            Packet p = new Packet(0x01, 0x28);
            p.WriteFixedAscii(username, 32);
            p.WriteFixedAscii(password, 21);
            p.WriteUInt8(0x00);
            p.WriteUInt16(0x19);
            p.WriteUInt8Array(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x01, 0x7C, 0x00 });
            p.WriteFixedAscii(ip, 22);
            p.WriteUInt16(0x19);
            p.WriteUInt32(0x00);
            p.WriteUInt8Array(new byte[] { 0x8B, 0x7C, 0x49, 0x00, 0x00 });
            p.WriteFixedAscii(mac, 20);
            return p;
        }
    }
}
