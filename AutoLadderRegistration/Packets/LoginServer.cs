using AutoLadderRegistration.Utilities;
using DNSecurityAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoLadderRegistration.Network.Clientless;

namespace AutoLadderRegistration.Packets
{
    class LoginServer
    {
        static bool firstTimer = false;
        public static List<Packet> OnConnectionSucceed(Packet packet)
        {
            if (firstTimer)
                return new List<Packet>();
            firstTimer = true;
            List<Packet> list = new List<Packet>();
            list.Add(new Packet(0x01, 0x01,
                new byte[] { 0x5B, 0x06, 0x01, 0x01, 0x00, 0x16, 0x05 })); //Request Dragon Nest Server Info

            Task.Run(() =>
            {
                Globals.MainWindow.Log("Connected to server, please log in.");
                Globals.MainWindow.button1.Enabled = true;
            });

            return list;
        }

        public static List<Packet> OnLoginResponse(Packet packet)
        {
            List<Packet> list = new List<Packet>();

            packet.ReadUInt32();
            var b = packet.ReadUInt8();
            switch (b)
            {
                case 0x00:
                    list.Add(new Packet(0x01, 0x03));
                    Task.Run(() =>
                    {
                        Globals.MainWindow.Log("Logged in successfully, requesting server list.");
                    });
                    break;
                default:
                    Console.WriteLine("An error occurred while logging in.");
                    Task.Run(() =>
                    {
                        Globals.MainWindow.button1.Enabled = true;
                        Globals.MainWindow.Log("Error occurred while logging in, please login again.");
                    });
                    break;
            }

            return list;
        }

        public static List<Packet> OnServerListResponse(Packet packet)
        {
            Task.Run(() =>
            {
                Globals.MainWindow.Log("Requesting characters list.");
            });
            var p = new Packet(0x01, 0x04); //Request char list
            p.WriteUInt8(0x01);
            return new List<Packet>() { p };
        }

        public static List<Packet> OnCharList(Packet packet)
        {
            Task.Run(() =>
            {
                Globals.MainWindow.Log("Select your character now.");
                Globals.MainWindow.CharList.Clear();
                Globals.MainWindow.cmb_chars.Items.Clear();
                Globals.MainWindow.button2.Enabled = true;
            });
            packet.ReadUInt32(); //unk
            var maxLevelCharsCount = packet.ReadUInt32(); //unk
            var heroLevel = packet.ReadUInt32();
            var totalFreeChars = packet.ReadUInt8();
            var totalCreatedChars = packet.ReadUInt8();
            for (int i = 0; i < totalCreatedChars; i++)
            {
                packet.ReadUInt8(); //01 - Deletion maybe?
                packet.ReadUInt8(); //01 - Deletion maybe?
                var bytes = packet.ReadUInt16Array(0x11);
                List<byte> nameBytes = new List<byte>();
                foreach (var b in bytes)
                {
                    if (b == 0x00)
                        break;
                    nameBytes.Add((byte)b);
                }
                var name = Encoding.ASCII.GetString(nameBytes.ToArray());
                var charID = packet.ReadUInt32();
                packet.ReadUInt32();
                var charLevel = packet.ReadUInt8();
                var bytes2 = packet.ReadUInt8Array(0x105);
                Console.WriteLine($"#{charID} {Encoding.ASCII.GetString(nameBytes.ToArray())} - Level {charLevel}");
                Globals.MainWindow.CharList.Add(charID);
                Globals.MainWindow.cmb_chars.Items.Add($"#{charLevel.ToString().PadLeft(2, '0')} - {name}");
            }

            var p = new Packet(0x01, 0x05); //Request channels list
            p.WriteUInt32(Globals.MainWindow.CharList.Last());
            p.WriteUInt32Array(new uint[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            return new List<Packet>() { p };
        }

        public static List<Packet> OnChannelsResponse(Packet packet)
        {
            var list = new List<Packet>();
            Task.Run(() =>
            {
                Globals.MainWindow.Log("Selecting channel");
            });

            packet.ReadUInt32();
            packet.ReadUInt8();
            var channelCount = packet.ReadUInt8();


            var p = new Packet(0x01, 0x08);
            p.WriteUInt32(packet.ReadUInt32()); //Always join the 1st server
            list.Add(p);

            list.Add(new Packet(0x01, 0x0D, new byte[] { 0x01 }));

            return list;
        }

        public static void RegisterLoginServerDispatches(Context context)
        {
            context.PacketDispatcher.Register(0x0217, OnConnectionSucceed, false);
            context.PacketDispatcher.Register(0x0102, OnLoginResponse, false);
            context.PacketDispatcher.Register(0x0103, OnServerListResponse, false);
            context.PacketDispatcher.Register(0x0104, OnCharList, false);
            context.PacketDispatcher.Register(0x0105, OnChannelsResponse, false);
        }
    }
}
