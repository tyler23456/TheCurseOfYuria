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
        IInventoryUI inventoryUI;
        IGlobal global;
        IFactory factory;

        [SerializeField] RectTransform grid;
        [SerializeField] RectTransform root;

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
        [SerializeField] Text itemInfo;

        [SerializeField] Image partyMemberSprite;
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

        IItem.Category currentPart = IItem.Category.helmets;
        int partyMemberIndex = 0;

        Modifier modifier;

        void OnEnable()
        {
            inventoryUI = GameObject.Find("/DontDestroyOnLoad").GetComponent<IInventoryUI>();
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();

            helmetsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IItem.Category.helmets, global.inventories[IItem.Category.helmets]); });
            earringsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IItem.Category.earrings, global.inventories[IItem.Category.earrings]); });
            glassesTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IItem.Category.glasses, global.inventories[IItem.Category.glasses]); });
            meleeWeapons1HTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IItem.Category.meleeWeapons1H, global.inventories[IItem.Category.meleeWeapons1H]); });
            meleeWeapons2HTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IItem.Category.meleeWeapons2H, global.inventories[IItem.Category.meleeWeapons2H]); });
            capesTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IItem.Category.capes, global.inventories[IItem.Category.capes]); });
            armorTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IItem.Category.armor, global.inventories[IItem.Category.armor]); });
            shieldsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IItem.Category.shields, global.inventories[IItem.Category.shields]); });
            bowsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(IItem.Category.bows, global.inventories[IItem.Category.bows]); });

            partyMemberIndex = 0;
            OnClickEquipmentPartTab(IItem.Category.helmets, global.inventories[(int)IItem.Category.helmets]);
            RefreshPartyMember();
        }
        
        public void OnClickEquipmentPartTab(IItem.Category part, IInventory inventory)
        {
            currentPart = part;
            inventoryUI.grid = grid;
            inventoryUI.inventory = inventory;         
            inventoryUI.OnClick = (itemName) =>
            {
                OnEquip(itemName);
            };
            inventoryUI.onPointerEnter = (itemName) => OnPointerEnter(itemName);
            inventoryUI.onPointerExit = (itemName) => OnPointerExit(itemName);
            inventoryUI.Show();
        }

        public void OnEquip(string itemName)
        {
            IEquipment equipment = global.getParty[partyMemberIndex].getEquipment;
            IItem item = factory.GetItem(itemName);
            IStats stats = global.getParty[partyMemberIndex].getStats;

            if (equipment.GetPart(currentPart) == "")
            {   
                global.inventories[currentPart].Remove(itemName);
                equipment.Equip(currentPart, itemName);

                List<Modifier> modifiers = item.getModifiers;
                foreach (Modifier modifier in modifiers)
                    stats.OffsetAttribute(modifier.getAttribute, modifier.getOffset);
            }
            else if (equipment.GetPart(currentPart) == itemName)
            {               
                global.inventories[currentPart].Add(itemName);
                equipment.Unequip(currentPart);

                List<Modifier> modifiers = item.getModifiers;
                foreach (Modifier modifier in modifiers)
                    stats.OffsetAttribute(modifier.getAttribute, -modifier.getOffset);
            }
            else
            {
                List<Modifier> previousModifiers = factory.GetItem(equipment.GetPart(currentPart)).getModifiers;
                foreach (Modifier modifier in previousModifiers)
                    stats.OffsetAttribute(modifier.getAttribute, -modifier.getOffset);

                global.inventories[currentPart].Add(equipment.GetPart(currentPart));
                global.inventories[currentPart].Remove(itemName);
                equipment.Equip(currentPart, itemName);

                List<Modifier> modifiers = item.getModifiers;
                foreach (Modifier modifier in modifiers)
                    stats.OffsetAttribute(modifier.getAttribute, modifier.getOffset);
            }

            RefreshPartyMember();
            OnClickEquipmentPartTab(currentPart, inventoryUI.inventory);
        }

        public void OnPointerEnter(string itemName)
        {
            IItem item = factory.GetItem(itemName);

            this.itemName.text = itemName;
            this.itemInfo.text = item.getInfo;
            partyMemberIncreases.text = "";

            int length = global.getParty[partyMemberIndex].getStats.GetAttributes().Length;

            List<Modifier> modifiers = item.getModifiers;

            if (modifiers.Count == 0)
                return;

            for (int i = 0; i < length; i++)
            {
                modifier = modifiers.FirstOrDefault(e => e.getAttribute == (IStats.Attributes)i);

                if (modifier == null)
                {
                    partyMemberIncreases.text += "<color=#555555ff>" + "0\n" + "</color>";
                }             
                else if (modifier.getOffset >= 0)
                {
                    partyMemberIncreases.text += "<color=#00ff00ff>" + "+ " + modifier.getOffset.ToString() + "\n" + "</color>";
                }
                else
                {
                    partyMemberIncreases.text += "<color=#ff0000ff>" + "" + modifier.getOffset.ToString() + "\n" + "</color>";
                }
            }
        }

        public void OnPointerExit(string itemName)
        {
            IItem item = factory.GetItem(itemName);

            this.itemName.text = "";
            itemInfo.text = "";
            partyMemberIncreases.text = "";
        }

        public void RefreshPartyMember()
        {
            IEquipment equipment = global.getParty[partyMemberIndex].getEquipment;

            if (equipment.GetPart(IItem.Category.helmets) != "")
                helmetSlot.sprite = factory.GetItem(equipment.GetPart(IItem.Category.helmets)).icon;
            else
                helmetSlot.sprite = helmetSprite;

            if (equipment.GetPart(IItem.Category.earrings) != "")
                earringSlot.sprite = factory.GetItem(equipment.GetPart(IItem.Category.earrings)).icon;
            else
                earringSlot.sprite = earringSprite;

            if (equipment.GetPart(IItem.Category.glasses) != "")
                glassesSlot.sprite = factory.GetItem(equipment.GetPart(IItem.Category.glasses)).icon;
            else
                glassesSlot.sprite = glassesSprite;

            if (equipment.GetPart(IItem.Category.meleeWeapons1H) != "")
                meleeWeapon1HSlot.sprite = factory.GetItem(equipment.GetPart(IItem.Category.meleeWeapons1H)).icon;
            else
                meleeWeapon1HSlot.sprite = meleeWeapon1HSprite;

            if (equipment.GetPart(IItem.Category.meleeWeapons2H) != "")
                meleeWeapon2HSlot.sprite = factory.GetItem(equipment.GetPart(IItem.Category.meleeWeapons2H)).icon;
            else
                meleeWeapon2HSlot.sprite = meleeWeapon2HSprite;

            if (equipment.GetPart(IItem.Category.capes) != "")
                capeSlot.sprite = factory.GetItem(equipment.GetPart(IItem.Category.capes)).icon;
            else
                capeSlot.sprite = capeSprite;

            if (equipment.GetPart(IItem.Category.armor) != "")
                armorSlot.sprite = factory.GetItem(equipment.GetPart(IItem.Category.armor)).icon;
            else
                armorSlot.sprite = armorSprite;

            if (equipment.GetPart(IItem.Category.shields) != "")
                shieldSlot.sprite = factory.GetItem(equipment.GetPart(IItem.Category.shields)).icon;
            else
                shieldSlot.sprite = shieldSprite;

            if (equipment.GetPart(IItem.Category.bows) != "")
                bowsSlot.sprite = factory.GetItem(equipment.GetPart(IItem.Category.bows)).icon;
            else
                bowsSlot.sprite = bowsSprite;

            partyMemberName.text = global.getParty[partyMemberIndex].getGameObject.name;
            partyMemberStats.text = "";
            partyMemberValues.text = "";

            int[] stats = global.getParty[partyMemberIndex].getStats.GetAttributes();
            for (int i = 0; i < stats.Length; i++)
            {
                partyMemberStats.text += ((IStats.Attributes)i).ToString() + "\n";
                partyMemberValues.text += stats[i].ToString() + "\n";
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                partyMemberIndex--;
                partyMemberIndex = Mathf.Clamp(partyMemberIndex, 0, global.getParty.Count - 1);
                RefreshPartyMember();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                partyMemberIndex++;
                partyMemberIndex = Mathf.Clamp(partyMemberIndex, 0, global.getParty.Count - 1);
                RefreshPartyMember();
            }
        }
    }
}