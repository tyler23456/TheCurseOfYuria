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

        [SerializeField] AudioClip open;
        [SerializeField] AudioClip close;
        [SerializeField] AudioClip equip;
        [SerializeField] AudioClip unequip;
        [SerializeField] AudioClip cyclePartyMembers;
        [SerializeField] AudioClip cycleEquipmentParts;

        [SerializeField] Camera detailedActorViewCamera;

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

        IActor actor;

        IEquipment equipment; 
        IStats stats;

        IItem previousItem;
        IItem newItem;

        List<Modifier> oldModifiers;
        List<Modifier> newModifiers;

        Modifier oldModifier;
        Modifier newModifier;
        int oldModifierValue = 0;
        int newModifierValue = 0;
        int modifierValue = 0;
        
        void OnEnable()
        {
            inventoryUI = GameObject.Find("/DontDestroyOnLoad").GetComponent<IInventoryUI>();
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();

            helmetsTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.helmets, global.inventories[IItem.Category.helmets]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            earringsTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.earrings, global.inventories[IItem.Category.earrings]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            glassesTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.glasses, global.inventories[IItem.Category.glasses]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            meleeWeapons1HTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.meleeWeapons1H, global.inventories[IItem.Category.meleeWeapons1H]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            meleeWeapons2HTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.meleeWeapons2H, global.inventories[IItem.Category.meleeWeapons2H]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            capesTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.capes, global.inventories[IItem.Category.capes]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            armorTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.armor, global.inventories[IItem.Category.armor]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            shieldsTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.shields, global.inventories[IItem.Category.shields]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });
            bowsTab.onClick.AddListener(() => { RefreshEquipmentPart(IItem.Category.bows, global.inventories[IItem.Category.bows]); global.getAudioSource.PlayOneShot(cycleEquipmentParts); });

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
            inventoryUI.grid = grid;
            inventoryUI.inventory = inventory;         
            inventoryUI.OnClick = (itemName) => OnEquip(itemName);
            inventoryUI.onPointerEnter = (itemName) => OnPointerEnter(itemName);
            inventoryUI.onPointerExit = (itemName) => OnPointerExit(itemName);
            inventoryUI.Show();
        }
        public void RefreshPartyMember()
        {
            actor = global.getParty[partyMemberIndex];

            //move Detailed Actor View Camera to the new character
            detailedActorViewCamera.cullingMask = LayerMask.GetMask("Actor" + (partyMemberIndex + 1).ToString());
            //---------------------------------------------------

            equipment = actor.getEquipment;
            stats = actor.getStats;

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

            int[] statValues = stats.GetAttributes();
            for (int i = 0; i < statValues.Length; i++)
            {
                partyMemberStats.text += ((IStats.Attributes)i).ToString() + "\n";
                partyMemberValues.text += statValues[i].ToString() + "\n";
            }
        }
        public void OnEquip(string itemName)
        {
            newItem = factory.GetItem(itemName);

            if (equipment.GetPart(currentPart) == "")
            {   
                global.inventories[currentPart].Remove(itemName);
                equipment.Equip(currentPart, itemName);
                AddModifiers();
                global.getAudioSource.PlayOneShot(equip);
            }
            else if (equipment.GetPart(currentPart) == itemName)
            {
                RemoveModifiers();
                global.inventories[currentPart].Add(itemName);
                equipment.Unequip(currentPart);
                global.getAudioSource.PlayOneShot(unequip);
            }
            else
            {
                RemoveModifiers();
                global.inventories[currentPart].Add(equipment.GetPart(currentPart));
                global.inventories[currentPart].Remove(itemName);
                equipment.Equip(currentPart, itemName);
                AddModifiers();
                global.getAudioSource.PlayOneShot(equip);
            }

            RefreshPartyMember();
            RefreshEquipmentPart(currentPart, inventoryUI.inventory);
        }

        public void RemoveModifiers()
        {
            List<Modifier> previousModifiers = factory.GetItem(equipment.GetPart(currentPart)).getModifiers;
            foreach (Modifier modifier in previousModifiers)
                stats.OffsetAttribute(modifier.getAttribute, -modifier.getOffset);
        }

        public void AddModifiers()
        {
            List<Modifier> modifiers = factory.GetItem(equipment.GetPart(currentPart)).getModifiers;
            foreach (Modifier modifier in modifiers)
                stats.OffsetAttribute(modifier.getAttribute, modifier.getOffset);
        }

        public void OnPointerEnter(string itemName)
        {
            if (equipment.GetPart(currentPart) == "")
                previousItem = factory.GetItem("Empty");
            else
                previousItem = factory.GetItem(equipment.GetPart(currentPart));
            newItem = factory.GetItem(itemName);

            this.itemName.text = itemName;
            this.itemInfo.text = newItem.getInfo;
            partyMemberIncreases.text = "";
            this.itemSprite.sprite = newItem.icon;

            oldModifiers = previousItem.getModifiers;
            newModifiers = newItem.getModifiers;

            int length = global.getParty[partyMemberIndex].getStats.GetAttributes().Length;

            for (int i = 0; i < length; i++)
            {
                oldModifier = oldModifiers.FirstOrDefault(e => e.getAttribute == (IStats.Attributes)i);
                newModifier = newModifiers.FirstOrDefault(e => e.getAttribute == (IStats.Attributes)i);

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
                partyMemberIndex = Mathf.Clamp(partyMemberIndex, 0, global.getParty.Count - 1);
                global.getAudioSource.PlayOneShot(cyclePartyMembers);
                RefreshPartyMember();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                partyMemberIndex++;
                partyMemberIndex = Mathf.Clamp(partyMemberIndex, 0, global.getParty.Count - 1);
                global.getAudioSource.PlayOneShot(cyclePartyMembers);
                RefreshPartyMember();
            }

            //move Detailed Actor View Camera to the new character
            detailedActorViewCamera.transform.position = actor.getGameObject.transform.position + new Vector3(0f, 1f, -2.5f);
            //---------------------------------------------------
        }
    }
}