using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using FirstGearGames.SmoothCameraShaker;

public class SaveManager : MonoBehaviour, ISaveManager
{
    IFactory factory;
    IGlobal global;

    [SerializeField] CameraShaker mainCameraShaker;

    public void Start()
    {
        factory = GetComponent<IFactory>();
        global = GetComponent<IGlobal>();
    }

    public void OnNewGame()
    {
        ClearNonPersistentData();

        IActor allie = Instantiate(factory.GetAllie("River")).GetComponent<IActor>();
        allie.Initialize();
        global.allies.Add(allie);
        
        global.sceneIDToLoad = 3;
        global.scenePositionToStart = new Vector2(0, 0);
        global.sceneEulerAngleZToStart = 0;
        global.ToggleDisplay(IGlobal.Display.LoadingDisplay);
    }

    public void OnNewSave()
    {
        SaveData saveData = new SaveData();
        saveData.Save(global, factory);
        string json = JsonUtility.ToJson(saveData);
        string fileName = SceneManager.GetActiveScene().name + " " + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Year + " " + global.inventories[IItem.Category.questItems].count.ToString() + " questItems" + " ";
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
        saveData.Save(global, factory);
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
        saveData.Load(global, factory);

        global.sceneIDToLoad = saveData.level;
        global.scenePositionToStart = new Vector2(saveData.position[0], saveData.position[1]);
        global.sceneEulerAngleZToStart = 0;
        global.ToggleDisplay(IGlobal.Display.LoadingDisplay);
    }

    public void ClearNonPersistentData()
    {
        global.StopAllCoroutines();

        global.ClearAllInventories();
        global.allies.DestroyAndClear();
        global.enemies.DestroyAndClear();
        global.ClearAllPopups();

        global.aTBGuageFilledQueue.Clear();
        global.pendingCommands.Clear();
        global.successfulCommands.Clear();;
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

        public void Save(IGlobal global, IFactory factory)
        {
            level = SceneManager.GetActiveScene().buildIndex;

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

            AllieData allieData = new AllieData();
            allieDatas = new List<AllieData>();
            global.allies.ForEach(i =>
            {
                allieData = new AllieData();
                allieData.Save(i);
                allieDatas.Add(allieData);

            });

            position = new float[] { global.allies.GetPositionAt(0).x, global.allies.GetPositionAt(0).y };
            eulerAnglesZ = global.allies.GetEulerAngleZAt(0);
        }

        public void Load(IGlobal global, IFactory factory)
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

            foreach (AllieData allieData in allieDatas)
            {
                IActor allie = Instantiate(factory.GetAllie(allieData.name)).GetComponent<IActor>();
                global.allies.Add(allie);
                allieData.Load(allie, factory);
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
            skillNames = partyMember.getSkills.GetNames();
            skillCounts = partyMember.getSkills.GetCounts();
            statusEffectNames = partyMember.getStatusEffects.GetNames();
            statusEffectAccumulators = partyMember.getStatusEffects.GetAccumulators();
        }

        public void Load(IActor allie, IFactory factory)
        {
            allie.Initialize();

            allie.getGameObject.name = name;

            allie.getStats.HP = HP;
            allie.getStats.MP = MP;

            foreach (string name in allie.getEquipment.GetNames())
                factory.GetItem(name).Unequip(allie);

            foreach (string name in allie.getSkills.GetNames())
                factory.GetItem(name).Unequip(allie);

            foreach (string name in equipmentNames)
                factory.GetItem(name).Equip(allie);

            foreach (string name in skillNames)
                factory.GetItem(name).Equip(allie);

            foreach (string name in statusEffectNames)
                factory.GetItem(name).Equip(allie);

            foreach (string name in skillNames)
                factory.GetItem(name).Equip(allie);

            allie.getStatusEffects.SetNamesAndAccumulators(statusEffectNames, statusEffectAccumulators);
        }
    }
}
