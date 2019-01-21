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
        private class ListDispatch
        {
            public Func<Packet, List<Packet>> RespondFunc;
            public bool PassPacket;

            public ListDispatch(Func<Packet, List<Packet>> respondList, bool passPacket)
            {
                RespondFunc = respondList;
                PassPacket = passPacket;
            }
        }

        private class AsyncDispatch
        {
            public Func<Packet, Task<List<Packet>>> RespondFunc;
            public bool PassPacket;

            public AsyncDispatch(Func<Packet, Task<List<Packet>>> respondList, bool passPacket)
            {
                RespondFunc = respondList;
                PassPacket = passPacket;
            }
        }

        private class VoidDispatch
        {
            public Action<Packet> RespondFunc;
            public bool PassPacket;

            public VoidDispatch(Action<Packet> respondList, bool passPacket)
            {
                RespondFunc = respondList;
                PassPacket = passPacket;
            }
        }

        private class Dispatch
        {
            public Func<Packet, Packet> RespondFunc;
            public bool PassPacket;

            public Dispatch(Func<Packet, Packet> respondList, bool passPacket)
            {
                RespondFunc = respondList;
                PassPacket = passPacket;
            }
        }

        private Dictionary<ushort, List<ListDispatch>> listDelegator = new Dictionary<ushort, List<ListDispatch>>();
        private Dictionary<ushort, List<AsyncDispatch>> asyncDelegator = new Dictionary<ushort, List<AsyncDispatch>>();
        private Dictionary<ushort, List<VoidDispatch>> voidDelegator = new Dictionary<ushort, List<VoidDispatch>>();
        private Dictionary<ushort, List<Dispatch>> delegator = new Dictionary<ushort, List<Dispatch>>();

        public void Register(ushort opcode, Func<Packet, List<Packet>> fnListToSend)
        {
            if (!listDelegator.ContainsKey(opcode))
                listDelegator[opcode] = new List<ListDispatch>();
            listDelegator[opcode].Add(new ListDispatch(fnListToSend, false));
        }

        //public void Register(ushort opcode, Func<Packet, Task<List<Packet>>> fnListToSend)
        //{
        //    if (!asyncDelegator.ContainsKey(opcode))
        //        asyncDelegator[opcode] = new List<AsyncDispatch>();
        //    asyncDelegator[opcode].Add(new AsyncDispatch(fnListToSend, false));
        //}

        public void Register(ushort opcode, Func<Packet, Packet> fnListToSend)
        {
            if (!delegator.ContainsKey(opcode))
                delegator[opcode] = new List<Dispatch>();
            delegator[opcode].Add(new Dispatch(fnListToSend, false));
        }

        public void Register(ushort opcode, Action<Packet> fnListToSend)
        {
            if (!voidDelegator.ContainsKey(opcode))
                voidDelegator[opcode] = new List<VoidDispatch>();
            voidDelegator[opcode].Add(new VoidDispatch(fnListToSend, false));
        }

        public List<Packet> CheckListDispatch(Packet packet)
        {
            List<Packet> ps = new List<Packet>();
            if (listDelegator.ContainsKey(packet.Opcode))
            {
                listDelegator[packet.Opcode].ForEach(x =>
                {
                    ps.AddRange(x.RespondFunc(packet));
                });
            }
            return ps;
        }

        public List<Packet> CheckAsyncDispatch(Packet packet)
        {
            List<Packet> ps = new List<Packet>();
            if (asyncDelegator.ContainsKey(packet.Opcode))
            {
                asyncDelegator[packet.Opcode].ForEach(x =>
                {
                    Func<Task<List<Packet>>> t = async () => await x.RespondFunc(packet);
                    ps.AddRange(t().Result);
                });
            }
            return ps;
        }

        public void CheckVoidDispatch(Packet packet)
        {
            if (voidDelegator.ContainsKey(packet.Opcode))
            {
                voidDelegator[packet.Opcode].ForEach(x =>
                {
                    x.RespondFunc(packet);
                });
            }
        }

        public List<Packet> CheckPacketDispatches(Packet packet)
        {
            List<Packet> ps = new List<Packet>();
            ps.AddRange(CheckListDispatch(packet));
            ps.AddRange(CheckAsyncDispatch(packet));
            ps.AddRange(CheckDispatch(packet));
            return ps;
        }

        public List<Packet> CheckDispatch(Packet packet)
        {
            List<Packet> ps = new List<Packet>();
            if (delegator.ContainsKey(packet.Opcode))
            {
                delegator[packet.Opcode].ForEach(x =>
                {
                    ps.Add(x.RespondFunc(packet));
                });
            }
            return ps;
        }
    }
}
