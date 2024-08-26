using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCOY.Canvas
{
    public class StatsDisplay : DisplayBase
    {
        public static DisplayBase Instance { get; protected set; }

        [SerializeField] Image border;
        [SerializeField] Color borderColorWhenInBattle;
        [SerializeField] List<StatDisplay> statDisplays;


        bool isInBattle = false;
        Color defaultColor;

        void Awake()
        {
            defaultColor = border.color;
        }

        public override void Initialize()
        {
            base.Initialize();
            Instance = this;
        }

        protected override void OnEnable()
        {
            OnRefresh();
        }

        protected override void OnDisable()
        {
        }

        protected void Update()
        {
            if (IBattleData.isInBattle && !this.isInBattle)
            {
                this.isInBattle = true;
                border.color = borderColorWhenInBattle;
            }
            else if (!IBattleData.isInBattle && this.isInBattle)
            {
                this.isInBattle = false;
                border.color = defaultColor;
            }
        }

        public void OnTransformChildrenChanged()
        {
            OnRefresh();
        }

        public void OnRefresh()
        {
            for (int i = 0; i < statDisplays.Count; i++)
            {
                if (i < transform.childCount)
                {
                    int ii = i;
                    IActor allie = transform.GetChild(i).GetComponent<IActor>();

                    allie.getStats.onHPChanged = (value) => statDisplays[ii].getHP.value = value;
                    allie.getStats.onMPChanged = (value) => statDisplays[ii].getMP.value = value;
                    allie.getATBGuage.onATBChanged = (value) => statDisplays[ii].getAP.value = value;

                    statDisplays[i].getName.gameObject.SetActive(true);
                    statDisplays[i].getHP.gameObject.SetActive(true);
                    statDisplays[i].getMP.gameObject.SetActive(true);
                    statDisplays[i].getAP.gameObject.SetActive(true);

                    statDisplays[i].getName.text = allie.obj.name;
                    statDisplays[i].getHP.maxValue = allie.getStats.GetAttribute(IStats.Attribute.MaxHP);
                    statDisplays[i].getMP.maxValue = allie.getStats.GetAttribute(IStats.Attribute.MaxMP);
                    statDisplays[i].getAP.maxValue = allie.getATBGuage.getMaximumValue;

                    statDisplays[i].getHP.value = allie.getStats.HP;
                    statDisplays[i].getMP.value = allie.getStats.MP;
                }
                else
                {
                    statDisplays[i].getName.gameObject.SetActive(false);
                    statDisplays[i].getHP.gameObject.SetActive(false);
                    statDisplays[i].getMP.gameObject.SetActive(false);
                    statDisplays[i].getAP.gameObject.SetActive(false);
                }
            }
        }

        [System.Serializable]
        class StatDisplay
        {
            [SerializeField] Text name;
            [SerializeField] Slider hp;
            [SerializeField] Slider mp;
            [SerializeField] Slider ap;

            public Text getName => name;
            public Slider getHP => hp;
            public Slider getMP => mp;
            public Slider getAP => ap;
        }
    }
}
