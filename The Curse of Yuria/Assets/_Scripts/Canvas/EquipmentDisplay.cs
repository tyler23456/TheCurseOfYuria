using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using System.Linq;

namespace TCOY.Canvas
{
    public class EquipmentDisplay : MenuBase
    {
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
        
        protected new void OnEnable()
        {
            base.OnEnable();

            globalInventoryUI = new InventoryUI();

            helmetsTab.onClick.RemoveAllListeners();
            earringsTab.onClick.RemoveAllListeners();
            glassesTab.onClick.RemoveAllListeners();
            meleeWeapons1HTab.onClick.RemoveAllListeners();
            meleeWeapons2HTab.onClick.RemoveAllListeners();
            capesTab.onClick.RemoveAllListeners();
            armorTab.onClick.RemoveAllListeners();
            shieldsTab.onClick.RemoveAllListeners();
            bowsTab.onClick.RemoveAllListeners();

            helmetsTab.onClick.AddListener(() => { RefreshEquipmentPart(Factory.instance.getHelmet); Global.instance.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            earringsTab.onClick.AddListener(() => { RefreshEquipmentPart(Factory.instance.getEarring); Global.instance.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            glassesTab.onClick.AddListener(() => { RefreshEquipmentPart(Factory.instance.getGlasses); Global.instance.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            meleeWeapons1HTab.onClick.AddListener(() => { RefreshEquipmentPart(Factory.instance.getMelee1H); Global.instance.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            meleeWeapons2HTab.onClick.AddListener(() => { RefreshEquipmentPart(Factory.instance.getMelee2H); Global.instance.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            capesTab.onClick.AddListener(() => { RefreshEquipmentPart(Factory.instance.getCape); Global.instance.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            armorTab.onClick.AddListener(() => { RefreshEquipmentPart(Factory.instance.getArmor); Global.instance.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            shieldsTab.onClick.AddListener(() => { RefreshEquipmentPart(Factory.instance.getShield); Global.instance.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            bowsTab.onClick.AddListener(() => { RefreshEquipmentPart(Factory.instance.getBow); Global.instance.getAudioSource.PlayOneShot(cycleEquipmentParts); });

            helmetsTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => Factory.instance.GetItem(i).itemType == Factory.instance.getHelmet)); };
            earringsTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => Factory.instance.GetItem(i).itemType == Factory.instance.getEarring)); };
            glassesTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => Factory.instance.GetItem(i).itemType == Factory.instance.getGlasses)); };
            meleeWeapons1HTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => Factory.instance.GetItem(i).itemType == Factory.instance.getMelee1H)); };
            meleeWeapons2HTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => Factory.instance.GetItem(i).itemType == Factory.instance.getMelee2H)); };
            capesTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => Factory.instance.GetItem(i).itemType == Factory.instance.getCape)); };
            armorTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => Factory.instance.GetItem(i).itemType == Factory.instance.getArmor)); };
            shieldsTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => Factory.instance.GetItem(i).itemType == Factory.instance.getShield)); };
            bowsTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => Factory.instance.GetItem(i).itemType == Factory.instance.getBow)); };

            slots.Clear();
            slots.Add(Factory.instance.getHelmet, helmetSlot);
            slots.Add(Factory.instance.getEarring, earringSlot);
            slots.Add(Factory.instance.getGlasses, glassesSlot);
            slots.Add(Factory.instance.getMelee1H, meleeWeapon1HSlot);
            slots.Add(Factory.instance.getMelee2H, meleeWeapon2HSlot);
            slots.Add(Factory.instance.getCape, capeSlot);
            slots.Add(Factory.instance.getArmor, armorSlot);
            slots.Add(Factory.instance.getShield, shieldSlot);
            slots.Add(Factory.instance.getBow, bowsSlot);

            allieIndex = 0;
            RefreshEquipmentPart(Factory.instance.getHelmet);
            RefreshPartyMember();

            Global.instance.getAudioSource.PlayOneShot(open);

            Global.instance.gameState = Global.GameState.Paused;
        }

        protected new void OnDisable()
        {
            base.OnDisable();
        }

        public void RefreshEquipmentPart(ItemTypeBase part)
        {
            currentPart = part;
            globalInventoryUI.grid = grid;
            globalInventoryUI.buttonPrefab = buttonPrefab;
            globalInventoryUI.inventory = Global.instance.inventories[currentPart.name];
            globalInventoryUI.OnClick = (itemName) => OnEquip(itemName);
            globalInventoryUI.onPointerEnter = (itemName) => OnPointerEnter(itemName);
            globalInventoryUI.onPointerExit = (itemName) => OnPointerExit(itemName);
            globalInventoryUI.Display();
        }

        public void RefreshPartyMember()
        {
            partyMember = Global.instance.allies[allieIndex];

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

            ItemTypeBase itemType = Factory.instance.getHelmet;

            for (int i = 0; i < equipment.count; i++)
            {
                itemType = Factory.instance.GetItem(equipment.GetName(i)).itemType;
                slots[itemType].sprite = Factory.instance.GetItem(equipment.GetName(i)).icon;
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

            currentItem = Factory.instance.GetItem(itemName);

            Global.instance.inventories[currentItem.itemType.name].Add(itemName);
            currentItem.Unequip(partyMember);
            Global.instance.getAudioSource.PlayOneShot(unequip);

            RefreshPartyMember();
            RefreshEquipmentPart(currentItem.itemType);
        }

        public void OnEquip(string itemName)
        {
            currentItem = Factory.instance.GetItem(itemName);

            string partyMemberItem = equipment.Find(i => Factory.instance.GetItem(i).itemType == currentItem.itemType);

            if (partyMemberItem == null)
            {
                Global.instance.inventories[currentPart.name].Remove(itemName);
            }
            else
            {
                Global.instance.inventories[currentPart.name].Add(partyMemberItem);
                Global.instance.inventories[currentPart.name].Remove(itemName);     
            }

            currentItem.Equip(partyMember);
            Global.instance.getAudioSource.PlayOneShot(equip);

            RefreshPartyMember();
            RefreshEquipmentPart(currentPart);
        }

        public void OnPointerEnter(string itemName)
        {
            currentItem = Factory.instance.GetItem(itemName);

            string previousItemName = equipment.Find(i => Factory.instance.GetItem(i).itemType == currentItem.itemType);

            if (previousItemName == null)
                previousItem = Factory.instance.GetItem("Empty");
            else
                previousItem = Factory.instance.GetItem(previousItemName);
            

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
                allieIndex = Mathf.Clamp(allieIndex, 0, Global.instance.allies.count - 1);
                Global.instance.getAudioSource.PlayOneShot(cyclePartyMembers);
                RefreshPartyMember();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                allieIndex++;
                allieIndex = Mathf.Clamp(allieIndex, 0, Global.instance.allies.count - 1);
                Global.instance.getAudioSource.PlayOneShot(cyclePartyMembers);
                RefreshPartyMember();
            }

            //move Detailed Actor View Camera to the new character
            detailedActorViewCamera.transform.position = partyMember.getGameObject.transform.position + new Vector3(0f, 1f, -2.5f);
            //---------------------------------------------------
        }
    }
}