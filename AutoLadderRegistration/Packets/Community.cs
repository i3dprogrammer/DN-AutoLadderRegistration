using DNSecurityAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoLadderRegistration.Network.Clientless;

namespace AutoLadderRegistration.Packets
{
    class Community
    {

        static uint suid = 0;

        public static List<Packet> DispawnCharacter(Packet packet)
        {
            var p = new Packet(0x02, 0x11);
            p.WriteUInt32(packet.ReadUInt32());
            return new List<Packet>() { p };
        }

        public static List<Packet> OnCharacterMove(Packet packet)
        {
            return new List<Packet>();
            var uid = packet.ReadUInt32();
            if (uid == suid)
            {
                Console.WriteLine("Trying to move..");
                var p2 = new Packet(0x04, 0x01);
                p2.WriteUInt32(Network.Clientless.uniquePlayerID);
                packet.ReadUInt32();
                p2.WriteUInt32(Environment.TickCount);
                p2.WriteUInt16(packet.ReadUInt16());
                var b1 = packet.ReadUInt8Array(0x09);
                p2.WriteUInt8Array(b1);
                p2.WriteUInt32(packet.ReadUInt32());
                p2.WriteUInt32(packet.ReadUInt32());
                p2.WriteUInt8(packet.ReadUInt8());
                p2.WriteUInt32(0x03FF);

                var p = new Packet(0x04, 0x02);
                p.WriteUInt32(Network.Clientless.uniquePlayerID);
                p.WriteUInt32(Environment.TickCount);
                p.WriteUInt8Array(b1);
                p.WriteUInt8Array(new byte[] { 0x00, 0x01 });

                return new List<Packet>() { p2, p };
            }
            Console.WriteLine($"{uid} -- should be {suid}");
            return new List<Packet>();
        }

        public static List<Packet> OnCharacterSpawn(Packet packet)
        {
            var uid = packet.ReadUInt32();
            packet.ReadUInt32();
            var charName = Encoding.Unicode.GetString(packet.ReadUInt8Array(0x24));
            Console.WriteLine(charName);

            if(charName.StartsWith("Menhera-Chan"))
            {
                suid = uid;
            }

            return new List<Packet>();
        }

        public static void RegisterCommunityDispatches(Context context)
        {
            context.PacketDispatcher.Register(0x0216, DispawnCharacter, false);
            context.PacketDispatcher.Register(0x0303, OnCharacterSpawn, false);
            context.PacketDispatcher.Register(0x0401, OnCharacterMove, false);
        }
    }
}
