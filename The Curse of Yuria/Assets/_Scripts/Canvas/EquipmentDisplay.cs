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

        IItem.Category currentPart = IItem.Category.helmets;
        int partyMemberIndex = 0;

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

            helmetsTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.helmets, global.inventories[IItem.Category.helmets]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            earringsTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.earrings, global.inventories[IItem.Category.earrings]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            glassesTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.glasses, global.inventories[IItem.Category.glasses]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            meleeWeapons1HTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.meleeWeapons1H, global.inventories[IItem.Category.meleeWeapons1H]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            meleeWeapons2HTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.meleeWeapons2H, global.inventories[IItem.Category.meleeWeapons2H]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            capesTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.capes, global.inventories[IItem.Category.capes]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            armorTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.armor, global.inventories[IItem.Category.armor]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            shieldsTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.shields, global.inventories[IItem.Category.shields]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            bowsTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.bows, global.inventories[IItem.Category.bows]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });

            helmetsTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).category == IItem.Category.helmets)); };
            earringsTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).category == IItem.Category.earrings)); };
            glassesTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).category == IItem.Category.glasses)); };
            meleeWeapons1HTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).category == IItem.Category.meleeWeapons1H)); };
            meleeWeapons2HTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).category == IItem.Category.meleeWeapons2H)); };
            capesTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).category == IItem.Category.capes)); };
            armorTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).category == IItem.Category.armor)); };
            shieldsTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).category == IItem.Category.shields)); };
            bowsTab.GetComponent<PointerHover>().onPointerRightClick = () => { OnUnequip(equipment.Find(i => factory.GetItem(i).category == IItem.Category.bows)); };

            partyMemberIndex = 0;
            RefreshEquipmentPart(IItem.Category.helmets, global.inventories[(int)IItem.Category.helmets]);
            RefreshPartyMember();

            global.getAudioSource.PlayOneShot(open);
        }

        private void OnDisable()
        {
            global.getAudioSource.PlayOneShot(close);
        }

        public void RefreshEquipmentPart(IItem.Category part, IInventory inventory)
        {
            currentPart = part;
            globalInventoryUI.grid = grid;
            globalInventoryUI.buttonPrefab = buttonPrefab;
            globalInventoryUI.inventory = inventory;
            globalInventoryUI.OnClick = (itemName) => OnEquip(itemName);
            globalInventoryUI.onPointerEnter = (itemName) => OnPointerEnter(itemName);
            globalInventoryUI.onPointerExit = (itemName) => OnPointerExit(itemName);
            globalInventoryUI.Display();
        }

        public void RefreshPartyMember()
        {
            partyMember = global.GetPartyMember(partyMemberIndex);

            detailedActorViewCamera.cullingMask = LayerMask.GetMask("Actor" + (partyMemberIndex + 1).ToString());

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

            IItem.Category category = IItem.Category.helmets;

            for (int i = 0; i < equipment.count; i++)
            {
                category = factory.GetItem(equipment.GetName(i)).category;

                switch (category)
                {
                    case IItem.Category.helmets:
                        helmetSlot.sprite = factory.GetItem(equipment.GetName(i)).icon;
                        break;
                    case IItem.Category.earrings:
                        earringSlot.sprite = factory.GetItem(equipment.GetName(i)).icon;
                        break;
                    case IItem.Category.glasses:
                        glassesSlot.sprite = factory.GetItem(equipment.GetName(i)).icon;
                        break;
                    case IItem.Category.meleeWeapons1H:
                        meleeWeapon1HSlot.sprite = factory.GetItem(equipment.GetName(i)).icon;
                        break;
                    case IItem.Category.meleeWeapons2H:
                        meleeWeapon2HSlot.sprite = factory.GetItem(equipment.GetName(i)).icon;
                        break;
                    case IItem.Category.capes:
                        capeSlot.sprite = factory.GetItem(equipment.GetName(i)).icon;
                        break;
                    case IItem.Category.armor:
                        armorSlot.sprite = factory.GetItem(equipment.GetName(i)).icon;
                        break;
                    case IItem.Category.shields:
                        shieldSlot.sprite = factory.GetItem(equipment.GetName(i)).icon;
                        break;
                    case IItem.Category.bows:
                        bowsSlot.sprite = factory.GetItem(equipment.GetName(i)).icon;
                        break;
                }
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

            global.inventories[currentItem.category].Add(itemName);
            currentItem.Unequip(partyMember);
            global.getAudioSource.PlayOneShot(unequip);

            RefreshPartyMember();
            RefreshEquipmentPart(currentItem.category, global.inventories[currentItem.category]);
        }

        public void OnEquip(string itemName)
        {
            currentItem = factory.GetItem(itemName);

            string partyMemberItem = equipment.Find(i => factory.GetItem(i).category == currentItem.category);

            if (partyMemberItem == null)
            {
                global.inventories[currentPart].Remove(itemName);
            }
            else
            {
                global.inventories[currentPart].Add(partyMemberItem);
                global.inventories[currentPart].Remove(itemName);     
            }

            currentItem.Equip(partyMember);
            global.getAudioSource.PlayOneShot(equip);

            RefreshPartyMember();
            RefreshEquipmentPart(currentPart, globalInventoryUI.inventory);
        }

        public void OnPointerEnter(string itemName)
        {
            currentItem = factory.GetItem(itemName);

            string previousItemName = equipment.Find(i => factory.GetItem(i).category == currentItem.category);

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
                this.itemInfo.text += "\n" + reactor.getTrigger + "|" + reactor.getReaction;

            if (currentItem.getCounters.Count > 0)
                this.itemInfo.text += "\n\nInterrupts:";

            foreach (Reactor reactor in currentItem.getInterrupts)
                this.itemInfo.text += "\n" + reactor.getTrigger + "|" + reactor.getReaction;

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
                partyMemberIndex--;
                partyMemberIndex = Mathf.Clamp(partyMemberIndex, 0, global.getPartyMemberCount - 1);
                global.getAudioSource.PlayOneShot(cyclePartyMembers);
                RefreshPartyMember();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                partyMemberIndex++;
                partyMemberIndex = Mathf.Clamp(partyMemberIndex, 0, global.getPartyMemberCount - 1);
                global.getAudioSource.PlayOneShot(cyclePartyMembers);
                RefreshPartyMember();
            }

            //move Detailed Actor View Camera to the new character
            detailedActorViewCamera.transform.position = partyMember.getGameObject.transform.position + new Vector3(0f, 1f, -2.5f);
            //---------------------------------------------------
        }
    }
}