using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using FirstGearGames.SmoothCameraShaker;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [SerializeField] CameraShaker mainCameraShaker;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
    }

    public void OnNewGame()
    {
        ClearNonPersistentData();

        IActor allie = AllieDatabase.Instance.Instantiate("River");
        allie.getGameObject.name = "River";
        AllieManager.Instance.Add(allie);
        
        LoadingDisplay.Instance.ShowExclusivelyInParent(3, Vector2.zero, 0f);
    }

    public void OnNewSave()
    {
        SaveData saveData = new SaveData();
        saveData.Save();
        string json = JsonUtility.ToJson(saveData);
        string fileName = SceneManager.GetActiveScene().name + " " + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Year + " " + InventoryManager.Instance.questItems.count.ToString() + " questItems" + " ";
        string fullPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + fileName;
        int i = 0;
        Debug.Log(Application.persistentDataPath);
        while (true)
        {
            if (!File.Exists(fullPath + i.ToString() + ".json"))
                break;

            i++;
        }
        File.WriteAllText(fullPath + i.ToString() + ".json", json);
    }

    public void OnOverwrite(string fileName)
    {
        SaveData saveData = new SaveData();
        saveData.Save();
        string json = JsonUtility.ToJson(saveData);
        string fullPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + fileName;

        File.WriteAllText(fullPath + ".json", json); //need to save scene and position
    }   

    public void OnLoad(string fileName)
    {
        string json = string.Empty;

        if (!File.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + fileName))
            return;

        ClearNonPersistentData();

        json = File.ReadAllText(Application.persistentDataPath + Path.AltDirectorySeparatorChar + fileName);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        saveData.Load();

        LoadingDisplay.Instance.ShowExclusivelyInParent(saveData.level, new Vector2(saveData.position[0], saveData.position[1]), 0f);
    }

    public void ClearNonPersistentData()
    {
        InventoryManager.Instance.EmptyAllInventories();
        AllieManager.Instance.DestroyAll();
        EnemyManager.Instance.DestroyAll();
        PopupManager.Instance.ClearAllPopups();

        BattleManager.Instance.aTBGuageFilledQueue.Clear();
        BattleManager.Instance.pendingCommands.Clear();
        BattleManager.Instance.successfulCommands.Clear();;
    }

    class SaveData
    {
        public int level;

        public float[] position;
        public float eulerAnglesZ;

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

        public List<AllieData> allieDatas;

        public void Save()
        {
            level = SceneManager.GetActiveScene().buildIndex;

            helmetNames = InventoryManager.Instance.helmets.GetNames();
            helmetCounts = InventoryManager.Instance.helmets.GetCounts();

            earringNames = InventoryManager.Instance.earrings.GetNames();
            earringCounts = InventoryManager.Instance.earrings.GetCounts();

            glassesNames = InventoryManager.Instance.glasses.GetNames();
            glassesCounts = InventoryManager.Instance.glasses.GetCounts();

            maskNames = InventoryManager.Instance.masks.GetNames();
            maskCounts = InventoryManager.Instance.masks.GetCounts();

            meleeWeapon1HNames = InventoryManager.Instance.meleeWeapons1H.GetNames();
            meleeWeapon1HCounts = InventoryManager.Instance.meleeWeapons1H.GetCounts();

            meleeWeapon2HNames = InventoryManager.Instance.meleeWeapons2H.GetNames();
            meleeWeapon2HCounts = InventoryManager.Instance.meleeWeapons2H.GetCounts();

            capeNames = InventoryManager.Instance.capes.GetNames();
            capeCounts = InventoryManager.Instance.capes.GetCounts();

            armorNames = InventoryManager.Instance.armor.GetNames();
            armorCounts = InventoryManager.Instance.armor.GetCounts();

            shieldNames = InventoryManager.Instance.shields.GetNames();
            shieldCounts = InventoryManager.Instance.shields.GetCounts();

            bowNames = InventoryManager.Instance.bows.GetNames();
            bowCounts = InventoryManager.Instance.bows.GetCounts();

            scrollNames = InventoryManager.Instance.scrolls.GetNames();
            scrollCounts = InventoryManager.Instance.scrolls.GetCounts();

            supplyNames = InventoryManager.Instance.basic.GetNames();
            supplyCounts = InventoryManager.Instance.basic.GetCounts();

            questItemNames = InventoryManager.Instance.questItems.GetNames();
            questItemCounts = InventoryManager.Instance.questItems.GetCounts();

            completedQuestNames = InventoryManager.Instance.completedQuests.GetNames();
            completedQuestCounts = InventoryManager.Instance.completedQuests.GetCounts();

            completedIDNames = InventoryManager.Instance.completedIds.GetNames();
            completedIDCounts = InventoryManager.Instance.completedIds.GetCounts();

            AllieData allieData = new AllieData();
            allieDatas = new List<AllieData>();
            AllieManager.Instance.ForEach(i =>
            {
                allieData = new AllieData();
                allieData.Save(i);
                allieDatas.Add(allieData);

            });

            position = new float[] { AllieManager.Instance.GetPositionAt(0).x, AllieManager.Instance.GetPositionAt(0).y };
            eulerAnglesZ = AllieManager.Instance.GetEulerAngleZAt(0);
        }

        public void Load()
        {
            InventoryManager.Instance.helmets.SetNames(helmetNames);
            InventoryManager.Instance.helmets.SetCounts(helmetCounts);

            InventoryManager.Instance.earrings.SetNames(earringNames);
            InventoryManager.Instance.earrings.SetCounts(earringCounts);

            InventoryManager.Instance.glasses.SetNames(glassesNames);
            InventoryManager.Instance.glasses.SetCounts(glassesCounts);

            InventoryManager.Instance.masks.SetNames(maskNames);
            InventoryManager.Instance.masks.SetCounts(maskCounts);

            InventoryManager.Instance.meleeWeapons1H.SetNames(meleeWeapon1HNames);
            InventoryManager.Instance.meleeWeapons1H.SetCounts(meleeWeapon1HCounts);

            InventoryManager.Instance.meleeWeapons2H.SetNames(meleeWeapon2HNames);
            InventoryManager.Instance.meleeWeapons2H.SetCounts(meleeWeapon2HCounts);

            InventoryManager.Instance.capes.SetNames(capeNames);
            InventoryManager.Instance.capes.SetCounts(capeCounts);

            InventoryManager.Instance.armor.SetNames(armorNames);
            InventoryManager.Instance.armor.SetCounts(armorCounts);

            InventoryManager.Instance.shields.SetNames(shieldNames);
            InventoryManager.Instance.shields.SetCounts(shieldCounts);

            InventoryManager.Instance.bows.SetNames(bowNames);
            InventoryManager.Instance.bows.SetCounts(bowCounts);

            InventoryManager.Instance.scrolls.SetNames(scrollNames);
            InventoryManager.Instance.scrolls.SetCounts(scrollCounts);

            InventoryManager.Instance.basic.SetNames(supplyNames);
            InventoryManager.Instance.basic.SetCounts(supplyCounts);

            InventoryManager.Instance.questItems.SetNames(questItemNames);
            InventoryManager.Instance.questItems.SetCounts(questItemCounts);

            InventoryManager.Instance.completedQuests.SetNames(completedQuestNames);
            InventoryManager.Instance.completedQuests.SetCounts(completedQuestCounts);

            InventoryManager.Instance.completedIds.SetNames(completedIDNames);
            InventoryManager.Instance.completedIds.SetCounts(completedIDCounts);

            foreach (AllieData allieData in allieDatas)
            {
                IActor allie = AllieDatabase.Instance.Instantiate("River");
                allie.getGameObject.name = allieData.name;
                AllieManager.Instance.Add(allie);
                allieData.Load(allie);
            }
        }
    }

    [System.Serializable]
    public class AllieData
    {
        public string name;
        public int HP;
        public int MP;
        public string[] equipmentNames;
        public int[] equipmentCounts;
        public string[] skillNames;
        public int[] skillCounts;
        public string[] statusEffectNames;
        public float[] statusEffectAccumulators;

        public void Save(IActor partyMember)
        {
            name = partyMember.getGameObject.name;

            HP = partyMember.getStats.HP;
            MP = partyMember.getStats.MP;

            equipmentNames = partyMember.getEquipment.GetNames();
            equipmentCounts = partyMember.getEquipment.GetCounts();
            skillNames = partyMember.getScrolls.GetNames();
            skillCounts = partyMember.getScrolls.GetCounts();
            statusEffectNames = partyMember.getStatusEffects.GetNames();
            statusEffectAccumulators = partyMember.getStatusEffects.GetAccumulators();
        }

        public void Load(IActor allie)
        {
            allie.getGameObject.name = name;

            allie.getStats.HP = HP;
            allie.getStats.MP = MP;

            foreach (string name in allie.getEquipment.GetNames())
                ItemDatabase.Instance.Get(name).Unequip(allie);

            foreach (string name in allie.getScrolls.GetNames())
                ItemDatabase.Instance.Get(name).Unequip(allie);

            foreach (string name in equipmentNames)
                ItemDatabase.Instance.Get(name).Equip(allie);

            foreach (string name in skillNames)
                ItemDatabase.Instance.Get(name).Equip(allie);

            foreach (string name in statusEffectNames)
                ItemDatabase.Instance.Get(name).Equip(allie);

            allie.getStatusEffects.SetNamesAndAccumulators(statusEffectNames, statusEffectAccumulators);
        }
    }
}
