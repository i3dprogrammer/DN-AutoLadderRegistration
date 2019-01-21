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
        public static Packet OnEchoCheck(Packet packet)
        {
            return new Packet(0x02, 0x0E, packet.GetBytes());
        }

        public static Packet OnBase64Check(Packet packet)
        {
            var key = packet.ReadUInt32();
            var data = DNSecurityAPI.Crypto.CustomBase64Decoder.Decode(packet.ReadUInt8Array(0x50), (byte)key);
            var hex = byte.Parse(packet.Opcode2.ToString("X2"));
            var p = new Packet(0x02, hex, data);
            p.WriteUInt8Array(new byte[20]);

            return p;
        }

        public static void RegisterServerChecksDispatches(Context context)
        {
            context.PacketDispatcher.Register(0x020E, (Func<Packet, Packet>)OnEchoCheck);
            context.PacketDispatcher.Register(0x0218, (Func<Packet, Packet>)OnBase64Check);
            context.PacketDispatcher.Register(0x0219, (Func<Packet, Packet>)OnBase64Check);
        }
    }
}
