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
        public static void OnInventoryItemsList(Packet p)
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
        }

        public static void OnStorageItemList(Packet p)
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
        }

        public static void OnProfileItemsList(Packet p)
        {
            var t1 = p.ReadUInt16();
            var t2 = p.ReadUInt16();
            var t3 = p.ReadUInt16();
            p.ReadUInt8Array(0x2C);

            Utilities.Globals.MainProfileItems.Items.Clear();
            for (int i = 0; i < t1; i++) //Face/Hair > equipment
                Utilities.Globals.MainProfileItems.AddItem(p);
            Utilities.Globals.MainProfileItems.Items.Clear();
            for (int i = 0; i < t2; i++) //Heraldry
                Utilities.Globals.MainProfileItems.AddItem(p);
            Utilities.Globals.MainProfileItems.Items.Clear();
            for (int i = 0; i < t3; i++) //Talisman
                Utilities.Globals.MainProfileItems.AddItem(p);
        }

        public static void RegisterCharacterDispatches(Context context)
        {
            context.PacketDispatcher.Register(Opcodes.STORAGE_ITEMS_LIST, (Action<Packet>)OnStorageItemList);
            context.PacketDispatcher.Register(Opcodes.INVENTORY_ITEMS_LIST, (Action<Packet>)OnInventoryItemsList);
            context.PacketDispatcher.Register(Opcodes.PROFILE_ITEMS_LIST, (Action<Packet>)OnProfileItemsList);
        }
    }
}
