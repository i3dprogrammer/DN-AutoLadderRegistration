using DNSecurityAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoLadderRegistration.Network.Clientless;

namespace AutoLadderRegistration.Packets
{
    class Ladder
    {
        public static List<Packet> Step1Ladder(Packet packet)
        {
            var list = new List<Packet>();
            list.Add(new Packet(0x20, 0x02, new byte[] { 0x10, 0x00 }));
            list.Add(new Packet(0x20, 0x02, new byte[] { 0x10, 0x00 }));
            list.Add(new Packet(0x20, 0x02, new byte[] { 0x10, 0x00 }));

            return list;
        }

        public static List<Packet> Step2Ladder(Packet packet)
        {
            return new List<Packet>() { new Packet(0x13, 0x04, new byte[11]) };
        }

        public static List<Packet> Step3Ladder(Packet packet)
        {
            var list = new List<Packet>();
            list.Add(new Packet(0x13, 0x0C, new byte[] { 0x06, 0x00, 0x00, 0x00 }));
            list.Add(new Packet(0x13, 0x04, new byte[11]));

            return list;
        }

        public static void RegisterLadderDispatches(Context context)
        {
            context.PacketDispatcher.Register(0x134A, Step1Ladder, false);
            context.PacketDispatcher.Register(0x134B, Step2Ladder, false);
            context.PacketDispatcher.Register(0x1303, Step3Ladder, false);
        }
    }
}
