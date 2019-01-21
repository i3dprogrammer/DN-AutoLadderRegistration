using DNSecurityAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoLadderRegistration.Utilities;
using AutoLadderRegistration.Network;

namespace AutoLadderRegistration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Globals.MainWindow = this;
        }

        public List<uint> CharList = new List<uint>();

        private void Button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            var b = Globals.PublicRand.Next(0, 255);
            var h = b.ToString("X2");
            var p = Helpers.FormLoginPacket(txt_username.Text, txt_pw.Text, $"{b}.{b}.{b}.{b}", $"{h}-{h}-{h}-{h}-{h}-{h}");
            Clientless.SendTCPPacket(p);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            nud_ladderCount.Maximum = 99;
            cmb_server.SelectedIndex = 0;
            Log("Initializing store keys.");
            DNSecurityAPI.Keys.Initialize(LocalKeys.XTEAKey, LocalKeys.UDPCryptoKey, LocalKeys.UDPDecryptKey, LocalKeys.UDPEncryptKey, LocalKeys.CustomeBase64Table);
            Log("Initialized.");
            Log("Loading UIString.");
            //DTO.Resources.UITranslator.LoadUIString("uistring.xml");
            Log("Loading game tables.");
            //DTO.Resources.GameTables.LoadItemEntry("ItemTable.txt", DTO.Resources.GameTables.ItemTable);
            Console.WriteLine(DTO.Resources.GameTables.ItemTable.Count);
            //DTO.Resources.GameTables.LoadItemEntry("WeaponTable.txt", DTO.Resources.GameTables.WeaponTable);
            Log("Data loaded.");
        }

        public void Log(string text, params string[] data)
        {
            var line = $"[{DateTime.Now.Hour.ToString().PadLeft(2, '0')}-{DateTime.Now.Minute.ToString().PadLeft(2, '0')}-{DateTime.Now.Second.ToString().PadLeft(2, '0')}] ";
            line = line + string.Format(text, data) + "\n";
            rtb_log.AppendText(line);
            Console.Write(line);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cmb_server.SelectedIndex == -1)
                return;
            Globals.LoginServerIP = cmb_server.SelectedItem.ToString().Split(':')[0];
            Globals.LoginServerPort = int.Parse(cmb_server.SelectedItem.ToString().Split(':')[1]);

            Log($"Connecting to server {Globals.LoginServerIP}:{Globals.LoginServerPort}");
            new Thread(() => Clientless.TcpClientless(Globals.LoginServerIP, Globals.LoginServerPort)).Start();
            button3.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmb_chars.SelectedIndex == -1)
                return;
            button2.Enabled = false;

            var p = new Packet(0x01, 0x06); //Request channels list
            p.WriteUInt32(CharList.ElementAt(cmb_chars.SelectedIndex));
            p.WriteUInt32Array(new uint[] { 0x00, 0x00, 0x00, 0x00, 0x00 });
            Clientless.SendTCPPacket(p);
        }

        private void nud_ladderCount_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var p = new Packet(0x20, 0x1D);
            p.WriteUInt8Array(new byte[] { 0xB8, 0x5C, 0x19, 0x00, 0x00, 0x00, 0x00, 0x00, 0xA9, 0x39, 0x00, 0x00 });
            Clientless.SendTCPPacket(p);
            //var p = new Packet(0x13, 0x14);
            //p.WriteUInt8(0x10);
            //p.WriteUInt32(0x06);
            //Clientless.SendTCPPacket(p);
        }
    }
}
