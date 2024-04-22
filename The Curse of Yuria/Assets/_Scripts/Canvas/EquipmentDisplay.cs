using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TCOY.Canvas
{
    public class EquipmentDisplay : MonoBehaviour
    {
        [SerializeField] Button helmetsTab;
        [SerializeField] Button earringsTab;
        [SerializeField] Button glassesTab;
        [SerializeField] Button meleeWeapon1HTab;
        [SerializeField] Button meleeWeapon2HTab;
        [SerializeField] Button capesTab;
        [SerializeField] Button armorTab;
        [SerializeField] Button shieldTab;

        [SerializeField] Button riverTab;
        [SerializeField] Button sarahTab;
        [SerializeField] Button nathanTab;
        [SerializeField] Button ashlynTab;
        [SerializeField] Button juelTab;
        [SerializeField] Button ninaTab;
        [SerializeField] Button onyxTab;

        [SerializeField] Image partyMemberSprite;
        [SerializeField] Text itemInfo;
        [SerializeField] Text partyMemberInfo;

        [SerializeField] Text partyMemberName;
        [SerializeField] Image helmetSlot;
        [SerializeField] Image earringSlot;
        [SerializeField] Image glassesSlot;
        [SerializeField] Image swordSlot;
        [SerializeField] Image capeSlot;
        [SerializeField] Image armorSlot;
        [SerializeField] Image shieldSlot;

        [SerializeField] RectTransform root;

        IInventoryUI inventoryUI;
        IGlobal global;

        void Start()
        {
            inventoryUI = GameObject.Find("/DontDestroyOnLoad").GetComponent<IInventoryUI>();

            helmetsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(global.getHelmets); });
            earringsTab.onClick.AddListener(() => { OnClickEquipmentPartTab(global.getEarrings); });
            glassesTab.onClick.AddListener(() => { OnClickEquipmentPartTab(global.getGlasses); });
            meleeWeapon1HTab.onClick.AddListener(() => { OnClickEquipmentPartTab(global.getMeleeWeapons1H); });
            meleeWeapon2HTab.onClick.AddListener(() => { OnClickEquipmentPartTab(global.getMeleeWeapons2H); });
            capesTab.onClick.AddListener(() => { OnClickEquipmentPartTab(global.getCapes); });
            armorTab.onClick.AddListener(() => { OnClickEquipmentPartTab(global.getArmor); });
            shieldTab.onClick.AddListener(() => { OnClickEquipmentPartTab(global.getShields); });
        }
        
        public void OnClickEquipmentPartTab(IInventory inventory)
        {
            inventoryUI.isVertical = false;
            inventoryUI.OnClick = (itemName) => {  };
            inventoryUI.inventory = inventory;
            inventoryUI.buttonParent = root;
            inventoryUI.Show();
        }

        public void OnClickPartyMemberTab(string partyMemberName)
        {
            /*helmetSlot.sprite = ;
            earringSlot;
            glassesSlot;
            swordSlot;
            capeSlot;
            armorSlot;
            shieldSlot;*/
        }

        public void Show()
        {
            root.gameObject.SetActive(true);
            OnClickEquipmentPartTab(global.getHelmets);
        }

        public void Hide()
        {
            root.gameObject.SetActive(false);
        }
    }
}