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
        allie.getGameObject.name = "River";
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
        string fileName = SceneManager.GetActiveScene().name + " " + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Year + " " + global.inventories[factory.getQuestItem.name].count.ToString() + " questItems" + " ";
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

            helmetNames = global.inventories[factory.getHelmet.name].GetNames();
            helmetCounts = global.inventories[factory.getHelmet.name].GetCounts();

            earringNames = global.inventories[factory.getHelmet.name].GetNames();
            earringCounts = global.inventories[factory.getHelmet.name].GetCounts();

            glassesNames = global.inventories[factory.getHelmet.name].GetNames();
            glassesCounts = global.inventories[factory.getHelmet.name].GetCounts();

            maskNames = global.inventories[factory.getHelmet.name].GetNames();
            maskCounts = global.inventories[factory.getHelmet.name].GetCounts();

            meleeWeapon1HNames = global.inventories[factory.getHelmet.name].GetNames();
            meleeWeapon1HCounts = global.inventories[factory.getHelmet.name].GetCounts();

            meleeWeapon2HNames = global.inventories[factory.getHelmet.name].GetNames();
            meleeWeapon2HCounts = global.inventories[factory.getHelmet.name].GetCounts();

            capeNames = global.inventories[factory.getHelmet.name].GetNames();
            capeCounts = global.inventories[factory.getHelmet.name].GetCounts();

            armorNames = global.inventories[factory.getHelmet.name].GetNames();
            armorCounts = global.inventories[factory.getHelmet.name].GetCounts();

            shieldNames = global.inventories[factory.getHelmet.name].GetNames();
            shieldCounts = global.inventories[factory.getHelmet.name].GetCounts();

            bowNames = global.inventories[factory.getHelmet.name].GetNames();
            bowCounts = global.inventories[factory.getHelmet.name].GetCounts();

            scrollNames = global.inventories[factory.getHelmet.name].GetNames();
            scrollCounts = global.inventories[factory.getHelmet.name].GetCounts();

            supplyNames = global.inventories[factory.getHelmet.name].GetNames();
            supplyCounts = global.inventories[factory.getHelmet.name].GetCounts();

            questItemNames = global.inventories[factory.getHelmet.name].GetNames();
            questItemCounts = global.inventories[factory.getHelmet.name].GetCounts();

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
            global.inventories[factory.getHelmet.name].SetNames(helmetNames);
            global.inventories[factory.getHelmet.name].SetCounts(helmetCounts);

            global.inventories[factory.getEarring.name].SetNames(earringNames);
            global.inventories[factory.getEarring.name].SetCounts(earringCounts);

            global.inventories[factory.getGlasses.name].SetNames(glassesNames);
            global.inventories[factory.getGlasses.name].SetCounts(glassesCounts);

            global.inventories[factory.getMask.name].SetNames(maskNames);
            global.inventories[factory.getMask.name].SetCounts(maskCounts);

            global.inventories[factory.getMelee1H.name].SetNames(meleeWeapon1HNames);
            global.inventories[factory.getMelee1H.name].SetCounts(meleeWeapon1HCounts);

            global.inventories[factory.getMelee2H.name].SetNames(meleeWeapon2HNames);
            global.inventories[factory.getMelee2H.name].SetCounts(meleeWeapon2HCounts);

            global.inventories[factory.getCape.name].SetNames(capeNames);
            global.inventories[factory.getCape.name].SetCounts(capeCounts);

            global.inventories[factory.getArmor.name].SetNames(armorNames);
            global.inventories[factory.getArmor.name].SetCounts(armorCounts);

            global.inventories[factory.getShield.name].SetNames(shieldNames);
            global.inventories[factory.getShield.name].SetCounts(shieldCounts);

            global.inventories[factory.getBow.name].SetNames(bowNames);
            global.inventories[factory.getBow.name].SetCounts(bowCounts);

            global.inventories[factory.getScroll.name].SetNames(scrollNames);
            global.inventories[factory.getScroll.name].SetCounts(scrollCounts);

            global.inventories[factory.getBasic.name].SetNames(supplyNames);
            global.inventories[factory.getBasic.name].SetCounts(supplyCounts);

            global.inventories[factory.getQuestItem.name].SetNames(questItemNames);
            global.inventories[factory.getQuestItem.name].SetCounts(questItemCounts);

            global.getCompletedQuests.SetNames(completedQuestNames);
            global.getCompletedQuests.SetCounts(completedQuestCounts);

            global.getCompletedIds.SetNames(completedIDNames);
            global.getCompletedIds.SetCounts(completedIDCounts);

            foreach (AllieData allieData in allieDatas)
            {
                IActor allie = Instantiate(factory.GetAllie(allieData.name)).GetComponent<IActor>();
                allie.getGameObject.name = allieData.name;
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
