using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using System.Linq;

namespace TCOY.Canvas
{
    public class EquipmentDisplay : MonoBehaviour
    {
        IGlobal global;
        IFactory factory;

        [SerializeField] Button buttonPrefab;

        [SerializeField] AudioClip open;
        [SerializeField] AudioClip close;
        [SerializeField] AudioClip equip;
        [SerializeField] AudioClip unequip;
        [SerializeField] AudioClip cyclePartyMembers;
        [SerializeField] AudioClip cycleEquipmentParts;

        [SerializeField] Camera detailedActorViewCamera;

        [SerializeField] RectTransform grid;

        [SerializeField] Button helmetsTab;
        [SerializeField] Button earringsTab;
        [SerializeField] Button glassesTab;
        [SerializeField] Button meleeWeapons1HTab;
        [SerializeField] Button meleeWeapons2HTab;
        [SerializeField] Button capesTab;
        [SerializeField] Button armorTab;
        [SerializeField] Button shieldsTab;
        [SerializeField] Button bowsTab;
    
        [SerializeField] Text itemName;
        [SerializeField] Image itemSprite;
        [SerializeField] Text itemInfo;

        [SerializeField] Text partyMemberName;
        [SerializeField] Text partyMemberStats;
        [SerializeField] Text partyMemberValues;
        [SerializeField] Text partyMemberIncreases;

        [SerializeField] Image helmetSlot;
        [SerializeField] Image earringSlot;
        [SerializeField] Image glassesSlot;
        [SerializeField] Image meleeWeapon1HSlot;
        [SerializeField] Image meleeWeapon2HSlot;
        [SerializeField] Image capeSlot;
        [SerializeField] Image armorSlot;
        [SerializeField] Image shieldSlot;
        [SerializeField] Image bowsSlot;

        [SerializeField] Sprite helmetSprite;
        [SerializeField] Sprite earringSprite;
        [SerializeField] Sprite glassesSprite;
        [SerializeField] Sprite meleeWeapon1HSprite;
        [SerializeField] Sprite meleeWeapon2HSprite;
        [SerializeField] Sprite capeSprite;
        [SerializeField] Sprite armorSprite;
        [SerializeField] Sprite shieldSprite;
        [SerializeField] Sprite bowsSprite;
        [SerializeField] Sprite EmptySprite;

        Dictionary<ItemTypeBase, Image> slots = new Dictionary<ItemTypeBase, Image>();

        ItemTypeBase currentPart;
        int allieIndex = 0;

        IActor partyMember;

        IInventory equipment; 
        IStats stats;

        IItem previousItem;
        IItem currentItem;

        List<Modifier> oldModifiers;
        List<Modifier> newModifiers;

        Modifier oldModifier;
        Modifier newModifier;
        int oldModifierValue = 0;
        int newModifierValue = 0;
        int modifierValue = 0;

        InventoryUI globalInventoryUI;
        
        void OnEnable()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();

            globalInventoryUI = new InventoryUI(factory);

            helmetsTab.onClick.RemoveAllListeners();
            earringsTab.onClick.RemoveAllListeners();
            glassesTab.onClick.RemoveAllListeners();
            meleeWeapons1HTab.onClick.RemoveAllListeners();
            meleeWeapons2HTab.onClick.RemoveAllListeners();
            capesTab.onClick.RemoveAllListeners();
            armorTab.onClick.RemoveAllListeners();
            shieldsTab.onClick.RemoveAllListeners();
            bowsTab.onClick.RemoveAllListeners();

            helmetsTab.onClick.AddListener(() => { RefreshEquipmentPart(factory.getHelmet); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            earringsTab.onClick.AddListener(() => { RefreshEquipmentPart(factory.getEarring); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            glassesTab.onClick.AddListener(() => { RefreshEquipmentPart(factory.getGlasses); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            meleeWeapons1HTab.onClick.AddListener(() => { RefreshEquipmentPart(factory.getMelee1H); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            meleeWeapons2HTab.onClick.AddListener(() => { RefreshEquipmentPart(factory.getMelee2H); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            capesTab.onClick.AddListener(() => { RefreshEquipmentPart(factory.getCape); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            armorTab.onClick.AddListener(() => { RefreshEquipmentPart(factory.getArmor); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            shieldsTab.onClick.AddListener(() => { RefreshEquipmentPart(factory.getShield); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            bowsTab.onClick.AddListener(() => { RefreshEquipmentPart(factory.getBow); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });

            helmetsTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).itemType == factory.getHelmet)); };
            earringsTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).itemType == factory.getEarring)); };
            glassesTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).itemType == factory.getGlasses)); };
            meleeWeapons1HTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).itemType == factory.getMelee1H)); };
            meleeWeapons2HTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).itemType == factory.getMelee2H)); };
            capesTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).itemType == factory.getCape)); };
            armorTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).itemType == factory.getArmor)); };
            shieldsTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).itemType == factory.getShield)); };
            bowsTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).itemType == factory.getBow)); };

            slots.Add(factory.getHelmet, helmetSlot);
            slots.Add(factory.getEarring, earringSlot);
            slots.Add(factory.getGlasses, glassesSlot);
            slots.Add(factory.getMelee1H, meleeWeapon1HSlot);
            slots.Add(factory.getMelee2H, meleeWeapon2HSlot);
            slots.Add(factory.getCape, capeSlot);
            slots.Add(factory.getArmor, armorSlot);
            slots.Add(factory.getShield, shieldSlot);
            slots.Add(factory.getBow, bowsSlot);

            allieIndex = 0;
            RefreshEquipmentPart(factory.getHelmet);
            RefreshPartyMember();

            global.getAudioSource.PlayOneShot(open);

            IGlobal.gameState = IGlobal.GameState.Paused;
        }

        private void OnDisable()
        {
            global.getAudioSource.PlayOneShot(close);
            IGlobal.gameState = IGlobal.GameState.Playing;
        }

        public void RefreshEquipmentPart(ItemTypeBase part)
        {
            currentPart = part;
            globalInventoryUI.grid = grid;
            globalInventoryUI.buttonPrefab = buttonPrefab;
            globalInventoryUI.inventory = global.inventories[currentPart.name];
            globalInventoryUI.OnClick = (itemName) => OnEquip(itemName);
            globalInventoryUI.onPointerEnter = (itemName) => OnPointerEnter(itemName);
            globalInventoryUI.onPointerExit = (itemName) => OnPointerExit(itemName);
            globalInventoryUI.Display();
        }

        public void RefreshPartyMember()
        {
            partyMember = global.allies[allieIndex];

            detailedActorViewCamera.cullingMask = LayerMask.GetMask("Actor" + (allieIndex + 1).ToString());

            equipment = partyMember.getEquipment;
            stats = partyMember.getStats;

            helmetSlot.sprite = helmetSprite;
            earringSlot.sprite = earringSprite;
            glassesSlot.sprite = glassesSprite;
            meleeWeapon1HSlot.sprite = meleeWeapon1HSprite;
            meleeWeapon2HSlot.sprite = meleeWeapon2HSprite;
            capeSlot.sprite = capeSprite;
            armorSlot.sprite = armorSprite;
            shieldSlot.sprite = shieldSprite;
            bowsSlot.sprite = bowsSprite;

            ItemTypeBase itemType = factory.getHelmet;

            for (int i = 0; i < equipment.count; i++)
            {
                itemType = factory.GetItem(equipment.GetName(i)).itemType;
                slots[itemType].sprite = factory.GetItem(equipment.GetName(i)).icon;
            }
            partyMemberName.text = partyMember.getGameObject.name;
            partyMemberStats.text = "";
            partyMemberValues.text = "";

            int[] statValues = stats.GetAttributes();
            for (int i = 0; i < statValues.Length; i++)
            {
                partyMemberStats.text += ((IStats.Attribute)i).ToString() + "\n";
                partyMemberValues.text += statValues[i].ToString() + "\n";
            }
        }

        public void OnUnequip(string itemName)
        {
            if (itemName == null)
                return;

            currentItem = factory.GetItem(itemName);

            global.inventories[currentItem.itemType.name].Add(itemName);
            currentItem.Unequip(partyMember);
            global.getAudioSource.PlayOneShot(unequip);

            RefreshPartyMember();
            RefreshEquipmentPart(currentItem.itemType);
        }

        public void OnEquip(string itemName)
        {
            currentItem = factory.GetItem(itemName);

            string partyMemberItem = equipment.Find(i => factory.GetItem(i).itemType == currentItem.itemType);

            if (partyMemberItem == null)
            {
                global.inventories[currentPart.name].Remove(itemName);
            }
            else
            {
                global.inventories[currentPart.name].Add(partyMemberItem);
                global.inventories[currentPart.name].Remove(itemName);     
            }

            currentItem.Equip(partyMember);
            global.getAudioSource.PlayOneShot(equip);

            RefreshPartyMember();
            RefreshEquipmentPart(currentPart);
        }

        public void OnPointerEnter(string itemName)
        {
            currentItem = factory.GetItem(itemName);

            string previousItemName = equipment.Find(i => factory.GetItem(i).itemType == currentItem.itemType);

            if (previousItemName == null)
                previousItem = factory.GetItem("Empty");
            else
                previousItem = factory.GetItem(previousItemName);
            

            this.itemName.text = itemName;
            this.itemInfo.text = currentItem.getInfo;
            partyMemberIncreases.text = "";
            this.itemSprite.sprite = currentItem.icon;

            oldModifiers = previousItem.getModifiers;
            newModifiers = currentItem.getModifiers;

            if (currentItem.getCounters.Count > 0)
                this.itemInfo.text += "\n\nCounters:";

            foreach (Reactor reactor in currentItem.getCounters)
                this.itemInfo.text += "\n" + reactor.getItem.ToString() + "|" + reactor.getParty.ToString() + "|" + reactor.getReaction.ToString() + "|" + reactor.getTargeter.ToString();

            if (currentItem.getCounters.Count > 0)
                this.itemInfo.text += "\n\nInterrupts:";

            foreach (Reactor reactor in currentItem.getInterrupts)
                this.itemInfo.text += "\n" + reactor.getItem.ToString() + "|" + reactor.getParty.ToString() + "|" + reactor.getReaction.ToString() + "|" + reactor.getTargeter.ToString();

            int length = partyMember.getStats.GetAttributes().Length;

            for (int i = 0; i < length; i++)
            {
                oldModifier = oldModifiers.FirstOrDefault(e => e.getAttribute == (IStats.Attribute)i);
                newModifier = newModifiers.FirstOrDefault(e => e.getAttribute == (IStats.Attribute)i);

                if (oldModifier == null)
                    oldModifierValue = 0;
                else
                    oldModifierValue = oldModifier.getOffset;

                if (newModifier == null)
                    newModifierValue = 0;
                else
                    newModifierValue = newModifier.getOffset;

                modifierValue = newModifierValue - oldModifierValue;

                if (modifierValue == 0)
                    partyMemberIncreases.text += "<color=#555555ff>" + modifierValue.ToString() + "\n" + "</color>";             
                else if (modifierValue > 0)
                    partyMemberIncreases.text += "<color=#00ff00ff>" + "+ " + modifierValue.ToString() + "\n" + "</color>";
                else
                    partyMemberIncreases.text += "<color=#ff0000ff>" + "" + modifierValue.ToString() + "\n" + "</color>";
            }
        }

        public void OnPointerExit(string itemName)
        {
            this.itemName.text = "";
            itemInfo.text = "";
            partyMemberIncreases.text = "";
            this.itemSprite.sprite = EmptySprite;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                allieIndex--;
                allieIndex = Mathf.Clamp(allieIndex, 0, global.allies.count - 1);
                global.getAudioSource.PlayOneShot(cyclePartyMembers);
                RefreshPartyMember();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                allieIndex++;
                allieIndex = Mathf.Clamp(allieIndex, 0, global.allies.count - 1);
                global.getAudioSource.PlayOneShot(cyclePartyMembers);
                RefreshPartyMember();
            }

            //move Detailed Actor View Camera to the new character
            detailedActorViewCamera.transform.position = partyMember.getGameObject.transform.position + new Vector3(0f, 1f, -2.5f);
            //---------------------------------------------------
        }
    }
}