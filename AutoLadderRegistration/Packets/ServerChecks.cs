using DNSecurityAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoLadderRegistration.Network.Clientless;

namespace AutoLadderRegistration.Packets
{
    class ServerChecks
    {
        public static List<Packet> OnEchoCheck(Packet packet)
        {
            return new List<Packet>() { new Packet(0x02, 0x0E, packet.GetBytes()) };
        }

        public static List<Packet> OnBase64Check(Packet packet)
        {
            var key = packet.ReadUInt32();
            var data = DNSecurityAPI.Crypto.CustomBase64Decoder.Decode(packet.ReadUInt8Array(0x50), (byte)key);
            var hex = byte.Parse(packet.Opcode2.ToString("X2"));
            var p = new Packet(0x02, hex, data);
            p.WriteUInt8Array(new byte[20]);

            return new List<Packet>() { p };
        }

        public static void RegisterServerChecksDispatches(Context context)
        {
            context.PacketDispatcher.Register(0x020E, OnEchoCheck, false);
            context.PacketDispatcher.Register(0x0218, OnBase64Check, false);
            context.PacketDispatcher.Register(0x0219, OnBase64Check, false);
        }
    }
}
