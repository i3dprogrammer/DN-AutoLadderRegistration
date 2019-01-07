using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLadderRegistration.DTO.Character;

namespace AutoLadderRegistration.Utilities
{
    class Globals
    {
        public static string LoginServerIP = "211.43.158.240";
        public static int LoginServerPort = 14300;
        public static Random PublicRand = new Random();
        public static Form1 MainWindow;

        //public static uint CharSessionUniqueID;
        public static Inventory MainCharInventory = new Inventory();
        public static Inventory MainCharStorage = new Inventory();
    }
}
