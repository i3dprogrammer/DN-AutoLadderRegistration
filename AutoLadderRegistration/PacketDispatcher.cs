using DNSecurityAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLadderRegistration
{
    class PacketDispatcher
    {
        private class Dispatch
        {
            public Func<Packet, List<Packet>> RespondFunc;
            public bool PassPacket;

            public Dispatch(Func<Packet, List<Packet>> respondList, bool passPacket)
            {
                RespondFunc = respondList;
                PassPacket = passPacket;
            }
        }

        private Dictionary<ushort, List<Dispatch>> delegator = new Dictionary<ushort, List<Dispatch>>();

        public void Register(ushort opcode, Func<Packet, List<Packet>> fnListToSend, bool pass)
        {
            if (!delegator.ContainsKey(opcode))
                delegator[opcode] = new List<Dispatch>();
            delegator[opcode].Add(new Dispatch(fnListToSend, pass));
        }

        public List<Packet> CheckDispatch(Packet packet)
        {
            if (delegator.ContainsKey(packet.Opcode))
            {
                List<Packet> ps = new List<Packet>();
                delegator[packet.Opcode].ForEach(x =>
                {
                    ps.AddRange(x.RespondFunc(packet));
                    if (x.PassPacket)
                        ps.Add(packet);
                });

                return ps;
            }

            return new List<Packet>();
        }
    }
}
