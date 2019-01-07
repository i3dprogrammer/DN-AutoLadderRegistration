using DNSecurityAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoLadderRegistration.Network.Clientless;

namespace AutoLadderRegistration.Packets
{
    class Character
    {
        public static List<Packet> OnInventoryItemsList(Packet p)
        {
            Utilities.Globals.MainCharInventory.Items.Clear();
            byte invSlots = p.ReadUInt8();
            byte usedSlots = p.ReadUInt8();

            Utilities.Globals.MainCharInventory.TotalInventorySlots = invSlots;
            Utilities.Globals.MainCharInventory.UsedInventorySlots = usedSlots;

            for (int i = 0; i < usedSlots; i++)
            {
                Utilities.Globals.MainCharInventory.AddItem(p);
            }

            return new List<Packet>();
        }

        public static List<Packet> OnStorageItemList(Packet p)
        {
            Utilities.Globals.MainCharStorage.Items.Clear();
            p.ReadUInt64();

            byte storageSlots = p.ReadUInt8();
            byte usedSlots = p.ReadUInt8();

            Utilities.Globals.MainCharStorage.TotalInventorySlots = storageSlots;
            Utilities.Globals.MainCharStorage.UsedInventorySlots = usedSlots;

            for (int i = 0; i < usedSlots; i++)
            {
                Utilities.Globals.MainCharStorage.AddItem(p);
            }

            return new List<Packet>();
        }

        public static void RegisterCharacterDispatches(Context context)
        {
            context.PacketDispatcher.Register(Opcodes.STORAGE_ITEMS, OnInventoryItemsList, false);
            context.PacketDispatcher.Register(Opcodes.INVENTORY_ITEMS, OnInventoryItemsList, false);
        }
    }
}
