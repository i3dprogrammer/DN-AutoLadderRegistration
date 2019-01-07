using DNSecurityAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoLadderRegistration.Network.Clientless;

namespace AutoLadderRegistration.Packets
{
    class Teleports
    {
        static int semiCounter = 0;

        public static List<Packet> On0202Echo(Packet packet)
        {
            return new List<Packet>() { new Packet(0x02, 0x02, new byte[] { 0x01, 0x00 }) };
        }

        public static List<Packet> On0301Echo(Packet packet)
        {
            return new List<Packet>() { new Packet(0x03, 0x01) };
        }

        public static List<Packet> OnCitySemiLoad(Packet packet)
        {
            var list = new List<Packet>();
            list.Add(new Packet(0x29, 0x05, new byte[1]));

            list.Add(new Packet(0x20, 0x02, new byte[2]));

            list.Add(new Packet(0x26, 0x05));

            list.Add(new Packet(0x20, 0x02, new byte[2]));
            list.Add(new Packet(0x20, 0x02, new byte[2]));

            list.Add(new Packet(0x2B, 0x01, new byte[1]));

            list.Add(new Packet(0x20, 0x02, new byte[2]));
            list.Add(new Packet(0x20, 0x02, new byte[2]));
            list.Add(new Packet(0x20, 0x02, new byte[2]));

            list.Add(new Packet(0x20, 0x02, new byte[2]));
            list.Add(new Packet(0x20, 0x02, new byte[2]));
            list.Add(new Packet(0x20, 0x02, new byte[2]));

            list.Add(new Packet(0x20, 0x02, new byte[2]));
            list.Add(new Packet(0x20, 0x02, new byte[2]));
            list.Add(new Packet(0x20, 0x02, new byte[2]));

            list.Add(new Packet(0x20, 0x02, new byte[2]));
            list.Add(new Packet(0x20, 0x02, new byte[2]));
            list.Add(new Packet(0x20, 0x02, new byte[2]));
            return list;
        }

        public static List<Packet> OnTeleportSemiLoad(Packet packet)
        {
            var list = new List<Packet>();
            semiCounter++;

            if(semiCounter == 3)
            {
                list.Add(new Packet(0x0F, 0x07, new byte[0]));
                list.Add(new Packet(0x14, 0x00, new byte[0]));
                list.Add(new Packet(0x03, 0x08, new byte[0]));
                list.Add(new Packet(0x0D, 0x08, new byte[0]));
            }

            return list;
        }

        public static List<Packet> OnAlmostLoad(Packet packet)
        {
            var list = new List<Packet>();
            list.Add(new Packet(0x17, 0x04, new byte[] { 0x01 }));
            list.Add(new Packet(0x1E, 0x01));
            return list;
        }

        public static List<Packet> OnFullLoad(Packet packet)
        {
            return new List<Packet>() { new Packet(0x20, 0x02, new byte[2]) };
        }

        public static void RegisterTeleportDispatches(Context context)
        {
            context.PacketDispatcher.Register(0x0202, On0202Echo, false);
            context.PacketDispatcher.Register(0x0301, On0301Echo, false);
            context.PacketDispatcher.Register(0x0307, OnCitySemiLoad, false);
            context.PacketDispatcher.Register(0x0768, OnTeleportSemiLoad, false);
            context.PacketDispatcher.Register(0x0D0F, OnAlmostLoad, false);
            context.PacketDispatcher.Register(0x1F03, OnFullLoad, false);
        }
    }
}
