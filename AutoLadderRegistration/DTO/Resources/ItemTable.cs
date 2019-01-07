using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLadderRegistration.DTO.Resources
{
    class ItemTable
    {
        private static Dictionary<ulong, object[]> Table = new Dictionary<ulong, object[]>();

        //RowId,_NameID,_DescriptionID,_NameIDParam,_DescriptionIDParam,_Type,_TypeParam1,_TypeParam2,_TypeParam3,_LevelLimit,_NeedJobClass,_Rank,_OverlapCount,_IconImageIndex,_Amount,_SellAmount,_State1,_State1_GenProb,_State1_Min,_State1_Max,_State2,_State2_GenProb,_State2_Min,_State2_Max,_State3,_State3_GenProb,_State3_Min,_State3_Max,_State4,_State4_GenProb,_State4_Min,_State4_Max,_State5,_State5_GenProb,_State5_Min,_State5_Max,_State6,_State6_GenProb,_State6_Min,_State6_Max,_State7,_State7_GenProb,_State7_Min,_State7_Max,_State8,_State8_GenProb,_State8_Min,_State8_Max,_State9,_State9_GenProb,_State9_Min,_State9_Max,_State10,_State10_GenProb,_State10_Min,_State10_Max,_Skill_GenProb,_SkillID,_SkillLevel,_SkillUsingType,_Reversion,_SealCount,_IsAuthentication,_IsDestruction,_IsCash,_RootSound,_DragSound,_UseSound,_DropSkinName,_DropAniName,_DropActName,_EnchantID,_DisjointorType,_Disjointamount,_DisjointDrop1,_AllowMapTypes,_ShowAlarmDialog,_NeedPvPRank,_SealID,_IsSealed,_CashTradeCount,_Compose,_ComposeList,_ApplicableValue,_IsCollectingEvent,_ExchangeType,_UseLevelLimit,_IsRebuyable,_ItemCategoryType,_AbleWStorage,_isUse,_dragonjeweltype,_CashOverLabBuy,_ShowOldLabel,_TierName,_GearScore,_overlablimit,_AbleStorage
        private static string[] Columns;
        public static void LoadItemTable(string filePath)
        {
            var fileLines = File.ReadAllLines(filePath);
            if (fileLines.Length < 1)
                throw new Exception("Couldn't find columns row");

            Columns = fileLines[0].Split(',');

            for(int i = 1; i < fileLines.Length; i++)
            {
                var data = fileLines[i].Split(',');
                var uid = ulong.Parse(data[0]);
                if (!Table.ContainsKey(uid))
                {
                    Table.Add(uid, data);
                }
            }
        }

        public static bool CheckTableItemExists(ulong id)
        {
            return Table.ContainsKey(id);
        }

        public static object[] GetTableItem(ulong id)
        {
            if (CheckTableItemExists(id))
                return Table[id];
            throw new Exception("Item ID not foind.");
        }

        public static object GetTableItemColumn(ulong id, string columnName)
        {
            if (!CheckTableItemExists(id))
                throw new Exception("Item ID not found.");

            int index = -1;
            for (int i = 0; i < Columns.Length; i++)
            {
                if (Columns[i] == columnName)
                    index = i;
            }

            if (index == -1)
                throw new Exception("Column name not found.");

            return GetTableItem(id)[index];
        }

        public static string GetItemTableFullName(ulong id)
        {
            string name = "";
            var nameID = GetTableItemColumn(id, "_NameID").ToString();
            var nameIDParams = GetTableItemColumn(id, "_NameIDParam").ToString().Split(';');

            for(int i = 0; i < nameIDParams.Length; i++) {
                var temp = nameIDParams[i].Trim(new char[] { '}', '{', ' ' });
                if (temp.Length == 0)
                    continue;
                var paramId = ulong.Parse(temp);
                if (UITranslator.CheckID(paramId))
                {
                    nameIDParams[i] = UITranslator.GetString(paramId);
                }
            }

            var uid = ulong.Parse(nameID);
            if (UITranslator.CheckID(uid)) { 
                name = UITranslator.GetString(uid);
                name = string.Format(name, nameIDParams);
            }

            Console.WriteLine("#" + id + " - " + name);
            return name;
        }
    }
}
