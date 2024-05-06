using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

namespace TCOY.Canvas
{
    public class SaveDisplay : MonoBehaviour
    {
        

        void Start()
        {

        }

        void OnSave()
        {

        }

        void OnLoad()
        {
            
        }

        class InventoryData
        {
            public string[] helmetNames;
            public int[] helmetCounts;

            public string[] earringNames;
            public int[] earringCounts;

            public string[] glassesNames;
            public int[] glassesCounts;

            public string[] maskNames;
            public int[] maskCounts;

            public string[] meleeWeapon1HNames;
            public int[] meleeWeapon1HCounts;

            public string[] meleeWeapon2HNames;
            public int[] meleeWeapon2HCounts;

            public string[] capeNames;
            public int[] capeCounts;

            public string[] armorNames;
            public int[] armorCounts;

            public string[] shieldNames;
            public int[] shieldCounts;

            public string[] bowNames;
            public int[] bowCounts;

            public string[] scrollNames;
            public int[] scrollCounts;

            public string[] supplyNames;
            public int[] supplyCounts;

            public string[] questItemNames;
            public int[] questItemCounts;

            public string[] completedQuestNames;
            public int[] completedQuestCounts;

            public string[] completedIDNames;
            public int[] completedIDCounts;

            List<PartyMemberData> partyMemberDatas;

            public void Save(IGlobal global)
            {
                helmetNames = global.inventories[IItem.Category.helmets].GetNames();
                helmetCounts = global.inventories[IItem.Category.helmets].GetCounts();

                earringNames = global.inventories[IItem.Category.earrings].GetNames();
                earringCounts = global.inventories[IItem.Category.earrings].GetCounts();

                glassesNames = global.inventories[IItem.Category.glasses].GetNames();
                glassesCounts = global.inventories[IItem.Category.glasses].GetCounts();

                maskNames = global.inventories[IItem.Category.masks].GetNames();
                maskCounts = global.inventories[IItem.Category.masks].GetCounts();

                meleeWeapon1HNames = global.inventories[IItem.Category.meleeWeapons1H].GetNames();
                meleeWeapon1HCounts = global.inventories[IItem.Category.meleeWeapons1H].GetCounts();

                meleeWeapon2HNames = global.inventories[IItem.Category.meleeWeapons2H].GetNames();
                meleeWeapon2HCounts = global.inventories[IItem.Category.meleeWeapons2H].GetCounts();

                capeNames = global.inventories[IItem.Category.capes].GetNames();
                capeCounts = global.inventories[IItem.Category.capes].GetCounts();

                armorNames = global.inventories[IItem.Category.armor].GetNames();
                armorCounts = global.inventories[IItem.Category.armor].GetCounts();

                shieldNames = global.inventories[IItem.Category.shields].GetNames();
                shieldCounts = global.inventories[IItem.Category.shields].GetCounts();

                bowNames = global.inventories[IItem.Category.bows].GetNames();
                bowCounts = global.inventories[IItem.Category.bows].GetCounts();

                scrollNames = global.inventories[IItem.Category.scrolls].GetNames();
                scrollCounts = global.inventories[IItem.Category.scrolls].GetCounts();

                supplyNames = global.inventories[IItem.Category.supplies].GetNames();
                supplyCounts = global.inventories[IItem.Category.supplies].GetCounts();

                questItemNames = global.inventories[IItem.Category.questItems].GetNames();
                questItemCounts = global.inventories[IItem.Category.questItems].GetCounts();

                completedQuestNames = global.getCompletedQuests.GetNames();
                completedQuestCounts = global.getCompletedQuests.GetCounts();

                completedIDNames = global.getCompletedIds.GetNames();
                completedIDCounts = global.getCompletedIds.GetCounts();

                PartyMemberData partyMemberData = new PartyMemberData();
                partyMemberDatas = new List<PartyMemberData>();
                for (int i = 0; i < global.getParty.Count; i++)
                {
                    partyMemberData = new PartyMemberData();
                    partyMemberData.Save(global.getParty[i]);
                    partyMemberDatas.Add(partyMemberData);
                }
            }

            public void Load(IGlobal global)
            {
                global.inventories[IItem.Category.helmets].SetNames(helmetNames);
                global.inventories[IItem.Category.helmets].SetCounts(helmetCounts);

                global.inventories[IItem.Category.earrings].SetNames(earringNames);
                global.inventories[IItem.Category.earrings].SetCounts(earringCounts);

                global.inventories[IItem.Category.glasses].SetNames(glassesNames);
                global.inventories[IItem.Category.glasses].SetCounts(glassesCounts);

                global.inventories[IItem.Category.masks].SetNames(maskNames);
                global.inventories[IItem.Category.masks].SetCounts(maskCounts);

                global.inventories[IItem.Category.meleeWeapons1H].SetNames(meleeWeapon1HNames);
                global.inventories[IItem.Category.meleeWeapons1H].SetCounts(meleeWeapon1HCounts);

                global.inventories[IItem.Category.meleeWeapons2H].SetNames(meleeWeapon2HNames);
                global.inventories[IItem.Category.meleeWeapons2H].SetCounts(meleeWeapon2HCounts);

                global.inventories[IItem.Category.capes].SetNames(capeNames);
                global.inventories[IItem.Category.capes].SetCounts(capeCounts);

                global.inventories[IItem.Category.armor].SetNames(armorNames);
                global.inventories[IItem.Category.armor].SetCounts(armorCounts);

                global.inventories[IItem.Category.shields].SetNames(shieldNames);
                global.inventories[IItem.Category.shields].SetCounts(shieldCounts);

                global.inventories[IItem.Category.bows].SetNames(bowNames);
                global.inventories[IItem.Category.bows].SetCounts(bowCounts);

                global.inventories[IItem.Category.scrolls].SetNames(scrollNames);
                global.inventories[IItem.Category.scrolls].SetCounts(scrollCounts);

                global.inventories[IItem.Category.supplies].SetNames(supplyNames);
                global.inventories[IItem.Category.supplies].SetCounts(supplyCounts);

                global.inventories[IItem.Category.questItems].SetNames(questItemNames);
                global.inventories[IItem.Category.questItems].SetCounts(questItemCounts);

                global.getCompletedQuests.SetNames(completedQuestNames);
                global.getCompletedQuests.SetCounts(completedQuestCounts);

                global.getCompletedIds.SetNames(completedIDNames);
                global.getCompletedIds.SetCounts(completedIDCounts);


                PartyMemberData partyMemberData = new PartyMemberData();
                partyMemberDatas = new List<PartyMemberData>();
                for (int i = 0; i < global.getParty.Count; i++)
                {
                    partyMemberData = new PartyMemberData();
                    partyMemberData.Save(global.getParty[i]);
                    partyMemberDatas.Add(partyMemberData);
                }
            }
        }

        class PartyMemberData
        {
            public string name;
            public string[] equipmentNames;
            public int[] equipmentCounts;
            public string[] skillNames;
            public int[] skillCounts;

            public void Save(IPlayer player)
            {
                name = player.getGameObject.name;
                equipmentNames = player.getEquipment.GetNames();
                equipmentCounts = player.getEquipment.GetCounts();
                skillNames = player.getSkills.GetNames();
                skillCounts = player.getSkills.GetCounts();
            }

            public void Load()
            {

            }
        }

    }
}