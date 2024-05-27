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

        IActor allie = Instantiate(Factory.instance.GetAllie("River")).GetComponent<IActor>();
        allie.getGameObject.name = "River";
        allie.Initialize();
        Global.instance.allies.Add(allie);
        
        Global.instance.sceneIDToLoad = 3;
        Global.instance.scenePositionToStart = new Vector2(0, 0);
        Global.instance.sceneEulerAngleZToStart = 0;
        Global.instance.ToggleDisplay(Global.Display.Loading);
    }

    public void OnNewSave()
    {
        SaveData saveData = new SaveData();
        saveData.Save();
        string json = JsonUtility.ToJson(saveData);
        string fileName = SceneManager.GetActiveScene().name + " " + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Year + " " + Global.instance.inventories[Factory.instance.getQuestItem.name].count.ToString() + " questItems" + " ";
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

        Global.instance.sceneIDToLoad = saveData.level;
        Global.instance.scenePositionToStart = new Vector2(saveData.position[0], saveData.position[1]);
        Global.instance.sceneEulerAngleZToStart = 0;
        Global.instance.ToggleDisplay(Global.Display.Loading);
    }

    public void ClearNonPersistentData()
    {
        Global.instance.ClearAllInventories();
        Global.instance.allies.DestroyAndClear();
        Global.instance.enemies.DestroyAndClear();
        Global.instance.ClearAllPopups();

        Global.instance.aTBGuageFilledQueue.Clear();
        Global.instance.pendingCommands.Clear();
        Global.instance.successfulCommands.Clear();;
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

            helmetNames = Global.instance.inventories[Factory.instance.getHelmet.name].GetNames();
            helmetCounts = Global.instance.inventories[Factory.instance.getHelmet.name].GetCounts();

            earringNames = Global.instance.inventories[Factory.instance.getHelmet.name].GetNames();
            earringCounts = Global.instance.inventories[Factory.instance.getHelmet.name].GetCounts();

            glassesNames = Global.instance.inventories[Factory.instance.getHelmet.name].GetNames();
            glassesCounts = Global.instance.inventories[Factory.instance.getHelmet.name].GetCounts();

            maskNames = Global.instance.inventories[Factory.instance.getHelmet.name].GetNames();
            maskCounts = Global.instance.inventories[Factory.instance.getHelmet.name].GetCounts();

            meleeWeapon1HNames = Global.instance.inventories[Factory.instance.getHelmet.name].GetNames();
            meleeWeapon1HCounts = Global.instance.inventories[Factory.instance.getHelmet.name].GetCounts();

            meleeWeapon2HNames = Global.instance.inventories[Factory.instance.getHelmet.name].GetNames();
            meleeWeapon2HCounts = Global.instance.inventories[Factory.instance.getHelmet.name].GetCounts();

            capeNames = Global.instance.inventories[Factory.instance.getHelmet.name].GetNames();
            capeCounts = Global.instance.inventories[Factory.instance.getHelmet.name].GetCounts();

            armorNames = Global.instance.inventories[Factory.instance.getHelmet.name].GetNames();
            armorCounts = Global.instance.inventories[Factory.instance.getHelmet.name].GetCounts();

            shieldNames = Global.instance.inventories[Factory.instance.getHelmet.name].GetNames();
            shieldCounts = Global.instance.inventories[Factory.instance.getHelmet.name].GetCounts();

            bowNames = Global.instance.inventories[Factory.instance.getHelmet.name].GetNames();
            bowCounts = Global.instance.inventories[Factory.instance.getHelmet.name].GetCounts();

            scrollNames = Global.instance.inventories[Factory.instance.getHelmet.name].GetNames();
            scrollCounts = Global.instance.inventories[Factory.instance.getHelmet.name].GetCounts();

            supplyNames = Global.instance.inventories[Factory.instance.getHelmet.name].GetNames();
            supplyCounts = Global.instance.inventories[Factory.instance.getHelmet.name].GetCounts();

            questItemNames = Global.instance.inventories[Factory.instance.getHelmet.name].GetNames();
            questItemCounts = Global.instance.inventories[Factory.instance.getHelmet.name].GetCounts();

            completedQuestNames = Global.instance.getCompletedQuests.GetNames();
            completedQuestCounts = Global.instance.getCompletedQuests.GetCounts();

            completedIDNames = Global.instance.getCompletedIds.GetNames();
            completedIDCounts = Global.instance.getCompletedIds.GetCounts();

            AllieData allieData = new AllieData();
            allieDatas = new List<AllieData>();
            Global.instance.allies.ForEach(i =>
            {
                allieData = new AllieData();
                allieData.Save(i);
                allieDatas.Add(allieData);

            });

            position = new float[] { Global.instance.allies.GetPositionAt(0).x, Global.instance.allies.GetPositionAt(0).y };
            eulerAnglesZ = Global.instance.allies.GetEulerAngleZAt(0);
        }

        public void Load()
        {
            Global.instance.inventories[Factory.instance.getHelmet.name].SetNames(helmetNames);
            Global.instance.inventories[Factory.instance.getHelmet.name].SetCounts(helmetCounts);

            Global.instance.inventories[Factory.instance.getEarring.name].SetNames(earringNames);
            Global.instance.inventories[Factory.instance.getEarring.name].SetCounts(earringCounts);

            Global.instance.inventories[Factory.instance.getGlasses.name].SetNames(glassesNames);
            Global.instance.inventories[Factory.instance.getGlasses.name].SetCounts(glassesCounts);

            Global.instance.inventories[Factory.instance.getMask.name].SetNames(maskNames);
            Global.instance.inventories[Factory.instance.getMask.name].SetCounts(maskCounts);

            Global.instance.inventories[Factory.instance.getMelee1H.name].SetNames(meleeWeapon1HNames);
            Global.instance.inventories[Factory.instance.getMelee1H.name].SetCounts(meleeWeapon1HCounts);

            Global.instance.inventories[Factory.instance.getMelee2H.name].SetNames(meleeWeapon2HNames);
            Global.instance.inventories[Factory.instance.getMelee2H.name].SetCounts(meleeWeapon2HCounts);

            Global.instance.inventories[Factory.instance.getCape.name].SetNames(capeNames);
            Global.instance.inventories[Factory.instance.getCape.name].SetCounts(capeCounts);

            Global.instance.inventories[Factory.instance.getArmor.name].SetNames(armorNames);
            Global.instance.inventories[Factory.instance.getArmor.name].SetCounts(armorCounts);

            Global.instance.inventories[Factory.instance.getShield.name].SetNames(shieldNames);
            Global.instance.inventories[Factory.instance.getShield.name].SetCounts(shieldCounts);

            Global.instance.inventories[Factory.instance.getBow.name].SetNames(bowNames);
            Global.instance.inventories[Factory.instance.getBow.name].SetCounts(bowCounts);

            Global.instance.inventories[Factory.instance.getScroll.name].SetNames(scrollNames);
            Global.instance.inventories[Factory.instance.getScroll.name].SetCounts(scrollCounts);

            Global.instance.inventories[Factory.instance.getBasic.name].SetNames(supplyNames);
            Global.instance.inventories[Factory.instance.getBasic.name].SetCounts(supplyCounts);

            Global.instance.inventories[Factory.instance.getQuestItem.name].SetNames(questItemNames);
            Global.instance.inventories[Factory.instance.getQuestItem.name].SetCounts(questItemCounts);

            Global.instance.getCompletedQuests.SetNames(completedQuestNames);
            Global.instance.getCompletedQuests.SetCounts(completedQuestCounts);

            Global.instance.getCompletedIds.SetNames(completedIDNames);
            Global.instance.getCompletedIds.SetCounts(completedIDCounts);

            foreach (AllieData allieData in allieDatas)
            {
                IActor allie = Instantiate(Factory.instance.GetAllie(allieData.name)).GetComponent<IActor>();
                allie.getGameObject.name = allieData.name;
                Global.instance.allies.Add(allie);
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
            skillNames = partyMember.getSkills.GetNames();
            skillCounts = partyMember.getSkills.GetCounts();
            statusEffectNames = partyMember.getStatusEffects.GetNames();
            statusEffectAccumulators = partyMember.getStatusEffects.GetAccumulators();
        }

        public void Load(IActor allie)
        {
            allie.Initialize();

            allie.getGameObject.name = name;

            allie.getStats.HP = HP;
            allie.getStats.MP = MP;

            foreach (string name in allie.getEquipment.GetNames())
                Factory.instance.GetItem(name).Unequip(allie);

            foreach (string name in allie.getSkills.GetNames())
                Factory.instance.GetItem(name).Unequip(allie);

            foreach (string name in equipmentNames)
                Factory.instance.GetItem(name).Equip(allie);

            foreach (string name in skillNames)
                Factory.instance.GetItem(name).Equip(allie);

            foreach (string name in statusEffectNames)
                Factory.instance.GetItem(name).Equip(allie);

            foreach (string name in skillNames)
                Factory.instance.GetItem(name).Equip(allie);

            allie.getStatusEffects.SetNamesAndAccumulators(statusEffectNames, statusEffectAccumulators);
        }
    }
}
