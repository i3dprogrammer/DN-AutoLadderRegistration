using DNSecurityAPI;
using DNSecurityAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoLadderRegistration.Utilities;
using static AutoLadderRegistration.Utilities.Helpers;

namespace AutoLadderRegistration.Network
{
    class Clientless
    {
        static KeyValuePair<byte, byte>[] ignoredOpcodes = new KeyValuePair<byte, byte>[]
        {
            //new KeyValuePair<byte, byte>(0x04, 0x01),
            new KeyValuePair<byte, byte>(0x04, 0x02),
            new KeyValuePair<byte, byte>(0x04, 0x03),
            new KeyValuePair<byte, byte>(0x02, 0x0E),
            new KeyValuePair<byte, byte>(0x02, 0x0D),
            new KeyValuePair<byte, byte>(0x02, 0x0C),
        };

        public class Context
        {
            public ISecurity Security;
            public Socket Socket;
            public ISecurity RelaySecurity;
            public TransferBuffer Buffer;
            public PacketDispatcher PacketDispatcher;

            public Context(ISecurity security)
            {
                Security = security;
                Socket = null;
                RelaySecurity = null;
                Buffer = new TransferBuffer(0x40000);
                PacketDispatcher = new PacketDispatcher();
            }
        }
        public static Context RemoteTCPContext;
        public static Context RemoteUDPContext;
        static byte[] TPData = new byte[] { 0x31, 0x00, 0x36, 0x00, 0x39, 0x00, 0x2E, 0x00, 0x32, 0x00, 0x35, 0x00, 0x34, 0x00, 0x2E, 0x00, 0x32, 0x00, 0x31, 0x00, 0x36, 0x00, 0x2E, 0x00, 0x37, 0x00, 0x35, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        static byte[] zero2zero3;
        public static uint uniquePlayerID;
        public async static void TcpClientless(string remoteIP, int remotePort)
        {
            Context context = new Context(new TCPSecurity());
            RemoteTCPContext = context;

            Packets.RegisterPackets.RegisterAllPackets();

            bool enter = true;
            Console.WriteLine($"Connecting to {remoteIP}:{remotePort}");
            using (context.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                context.Socket.Connect(remoteIP, remotePort);
                Console.WriteLine("Connected");
                while (true)
                {
                    if (!TCPTransferIngoingPackets(context))
                        break;

                    foreach (var packet in context.Security.TransferIncoming())
                    {
                        if (!ignoredOpcodes.ToList().Exists(x => x.Key == packet.Opcode1 && x.Value == packet.Opcode2))
                            Utility.Hexdump(packet, true);

                        foreach (var p in context.PacketDispatcher.CheckDispatch(packet))
                            SendTCPPacket(p);

                        if (packet.Opcode1 == 0x02 && packet.Opcode2 == 0x01) // Start new TCP connection
                        {
                            uniquePlayerID = packet.ReadUInt32();
                            remoteIP = packet.ReadFixedAscii(0x20);
                            remotePort = packet.ReadUInt16();
                            packet.ReadUInt16();

                            var p = new Packet(0x02, 0x01);
                            p.WriteUInt32(uniquePlayerID);
                            p.WriteUInt8Array(packet.ReadUInt8Array(0x0C));
                            p.WriteUInt8Array(TPData);

                            context.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            context.Socket.Connect(remoteIP, remotePort);

                            SendTCPPacket(p);

                            await Task.Run(() =>
                            {
                                Globals.MainWindow.Log($"Teleporting...");
                            });

                            Console.WriteLine($"Connected to {remoteIP}:{remotePort}");

                            continue;
                        }
                        else if (packet.Opcode1 == 0x02 && packet.Opcode2 == 0x03)
                        {
                            packet.ReadUInt8Array(0x05);
                            var ipbytes = packet.ReadUInt8Array(4);
                            string remIP = ipbytes[0] + "." + ipbytes[1] + "." + ipbytes[2] + "." + ipbytes[3];
                            ushort remUDPPort = packet.ReadUInt16();
                            ushort remTCPPort = packet.ReadUInt16();
                            packet.ReadUInt32();
                            zero2zero3 = packet.ReadUInt8Array(0x0C);

                            Console.WriteLine($"Should connect to {remoteIP}:{remTCPPort}");
                            new Thread(() => UdpClientless(remIP, remUDPPort)).Start();

                            context.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            context.Socket.Connect(remIP, remTCPPort);

                            await Task.Run(() =>
                            {
                                Globals.MainWindow.Log($"Teleporting...");
                            });
                        } else if(packet.Opcode1 == 0x02 && packet.Opcode2 == 0x05)
                        {
                            var p = new Packet(0x02, 0x05);
                            p.WriteUInt32(packet.ReadUInt32());
                            p.WriteUInt8Array(new byte[14 * 2]);
                            p.WriteUInt8Array(new byte[] { 0xE8, 0x57, 0xB2, 0x5B });
                            p.WriteUInt32(0x00);
                            p.WriteUInt32(0x00);
                            p.WriteUInt8Array(new byte[] { 0xF8, 0xC4, 0x69, 0x5F });
                            p.WriteUInt32(0x00);
                            p.WriteUInt32(0x01);
                            p.WriteUInt32(0x54);
                            p.WriteUInt32(0x00);
                            p.WriteUInt32(0x54);
                            SendUDPPacket(p);
                        }

                        //if (packet.Opcode1 == 0x13 && packet.Opcode2 == 0x23 && enter)
                        //{
                        //    enter = false;
                        //    Console.WriteLine("GO LADDER GO!");
                        //    Packet p = new Packet(0x13, 0x0E);
                        //    p.WriteUInt8(0x00);

                        //    context.Security.Send(p);
                        //}
                    }

                    TCPTransferOutgoingPackets(context);
                    Thread.Sleep(1);
                }
            }
        }

        public static void UdpClientless(string remoteIP, int remotePort)
        {
            Context context = new Context(new UDPSecurity());
            RemoteUDPContext = context;

            context.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            EndPoint serv_endpoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);

            bool sentData = false;

            using (context.Socket)
            {
                context.Socket.Connect(serv_endpoint);
                var p1 = new Packet(0x02, 0x03);
                p1.WriteUInt32(uniquePlayerID);
                p1.WriteUInt8Array(new byte[] { 0xC0, 0xA8, 0x01, 0xC9, 0x00, 0x00 });
                SendUDPPacket(p1);

                while (true)
                {
                    if (!TCPTransferIngoingPackets(context))
                        break;

                    foreach (var packet in context.Security.TransferIncoming())
                    {
                        if(packet.Opcode == 0x00)
                        {
                            var b = packet.GetBytes();
                            if (b.Length == 0x05 && b.SequenceEqual(new byte[] { 0x0D, 0x00, 0x70, 0x00, 0x00 }))
                            {
                                Console.WriteLine("SENT 70 PACKET");
                                SendUDPPacket(packet);

                                var p = new Packet(0x02, 0x04);
                                p.WriteUInt32(uniquePlayerID);
                                p.WriteUInt8Array(zero2zero3);
                                SendTCPPacket(p);
                            }
                            else if (b.Length == 0x05 && b.SequenceEqual(new byte[] { 0x0D, 0x00, 0x60, 0x10, 0x00 }))
                            {
                                var p = new Packet(0x0C, 0x07, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x1F, 0x27, 0x00, 0x00 });
                                SendUDPPacket(p);

                                p = new Packet(0x0C, 0x01);
                                p.WriteUInt32(uniquePlayerID);
                                SendUDPPacket(p);
                            }
                        } else if(packet.Opcode1 == 0x07 && packet.Opcode2 == 0x74 && !sentData)
                        {
                            sendudpdata();
                            sentData = true;
                        } else if(packet.Opcode1 == 0x06 && packet.Opcode2 == 0x0B && !sentData)
                        {
                            sendudpdata();
                            sentData = true;
                        }

                        Console.WriteLine("[UDP]");
                        Utility.Hexdump(packet, true);
                    }

                    if (context.Socket.Poll(0, SelectMode.SelectWrite))
                    {
                        foreach (var kvp in context.Security.TransferOutgoing())
                        {
                            Utility.Hexdump(kvp.Value, false);
                            TransferBuffer buffer = kvp.Key;
                            int count = context.Socket.SendTo(buffer.Buffer, buffer.Offset, buffer.Size, SocketFlags.None, serv_endpoint);
                        }
                    }

                    Thread.Sleep(1);
                }
            }
        }

        private static void sendudpdata()
        {
            SendUDPPacket(new Packet(0x00, 0x00, new byte[] { 0x0D, 0x00, 0x68, 0x08, 0x00 }));

            var p = new Packet(0x00, 0x00, new byte[] { 0x0D, 0x00, 0x60, 0x10, 0x00 });
            SendUDPPacket(p);

            p = new Packet(0x0F, 0x07);
            SendUDPPacket(p);

            p = new Packet(0x14, 0x0);
            SendUDPPacket(p);

            p = new Packet(0x04, 0x5A);
            p.WriteUInt32(uniquePlayerID);
            p.WriteUInt8(0x01);
            SendUDPPacket(p);

        }
        private static bool TCPTransferIngoingPackets(Context context)
        {
            if (context.Socket.Poll(0, SelectMode.SelectRead))
            {
                try
                {
                    int count = context.Socket.Receive(context.Buffer.Buffer);
                    if (count == 0)
                    {
                        Task.Run(() =>
                        {
                            Globals.MainWindow.Log("Disconnected.");
                        });
                        context.Socket.Dispose();
                        return false;
                    }
                    context.Security.Recv(context.Buffer.Buffer, 0, count);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    context.Socket.Dispose();
                    return false;
                }
            }
            return true;
        }
        private static void TCPTransferOutgoingPackets(Context context)
        {
            if (context.Socket.Poll(0, SelectMode.SelectWrite))
            {
                foreach (var kvp in context.Security.TransferOutgoing())
                {
                    Utility.Hexdump(kvp.Value, false);
                    TransferBuffer buffer = kvp.Key;
                    do
                    {
                        int count = context.Socket.Send(buffer.Buffer, buffer.Offset, buffer.Size, SocketFlags.None);
                        buffer.Offset += count;
                        Thread.Sleep(1);
                    } while (buffer.Offset != buffer.Size);
                }
            }
        }


        private static void EnterLadder(object state)
        {
            //var p = new Packet(0x13, 0x22);
            var p = new Packet(0x06, 0x0E);
            SendTCPPacket(p);
        }

        public static void SendTCPPacket(Packet p)
        {
            RemoteTCPContext.Security.Send(p);
        }

        public static void SendUDPPacket(Packet p)
        {
            RemoteUDPContext.Security.Send(p);
        }

    }
}
