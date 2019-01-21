using DNSecurityAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLadderRegistration.DTO.Character
{
    public class Inventory
    {
        public byte TotalInventorySlots { get; set; }
        public byte UsedInventorySlots { get; set; }
        public byte FreeInventorySlots
        {
            get
            {
                return (byte)(TotalInventorySlots - UsedInventorySlots);
            }
        }
        public Dictionary<byte, InventoryItem> Items = new Dictionary<byte, InventoryItem>();


        public void AddItem(Packet p)
        {
            InventoryItem item = new InventoryItem();
            item.Position = p.ReadUInt8();
            item.ItemID = p.ReadUInt32();
            item.UsageID = p.ReadUInt64();
            p.ReadUInt8Array(0x06);
            item.Count = p.ReadUInt16();
            p.ReadUInt8Array(0x1D);

            try
            {
                item.Name = Resources.GameTables.GetTableEntryFullName(item.ItemID);
                Console.WriteLine("#" + item.ItemID + " - " + item.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }

            if (!Items.ContainsKey(item.Position))
                Items.Add(item.Position, item);
            else
                throw new Exception("Item already exists at this position.");
        }

        public void SwapItems(byte oldPos, byte newPos)
        {
            var oldItem = Items[oldPos];
            Items[oldPos] = Items[newPos];
            Items[newPos] = oldItem;
        }

        public void RemoveItem(InventoryItem item)
        {
            RemoveItem(item.Position);
        }

        public void RemoveItem(byte slot)
        {
            Items.Remove(slot);
        }
    }

    public class InventoryItem
    {
        public uint ItemID { get; set; }
        public string Name { get; set; }
        public byte Position { get; set; }
        public UInt64 UsageID { get; set; }
        public ushort Count { get; set; }
    }
}
