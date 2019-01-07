using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLadderRegistration.Packets
{
    class RegisterPackets
    {
        public static void RegisterAllPackets()
        {
            var context = Network.Clientless.RemoteTCPContext;
            Ladder.RegisterLadderDispatches(context);
            LoginServer.RegisterLoginServerDispatches(context);
            ServerChecks.RegisterServerChecksDispatches(context);
            Teleports.RegisterTeleportDispatches(context);
            Community.RegisterCommunityDispatches(context);
            Character.RegisterCharacterDispatches(context);
        }
    }
}
