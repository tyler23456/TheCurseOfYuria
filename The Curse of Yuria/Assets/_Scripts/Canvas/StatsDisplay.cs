using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatsDisplay : DisplayBase
{
    public static DisplayBase Instance { get; protected set; }

    [SerializeField] List<StatDisplay> statDisplays;

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

    public void OnRefresh()
    {
        for (int i = 0; i < statDisplays.Count; i++)
        {
            if (i < Global.Instance.allies.count)
            {
                int ii = i;
                Global.Instance.allies[i].getStats.onHPChanged = (value) => statDisplays[ii].getHP.value = value;
                Global.Instance.allies[i].getStats.onMPChanged = (value) => statDisplays[ii].getMP.value = value;
                Global.Instance.allies[i].getATBGuage.onATBChanged = (value) => statDisplays[ii].getAP.value = value;

                statDisplays[i].getName.gameObject.SetActive(true);
                statDisplays[i].getHP.gameObject.SetActive(true);
                statDisplays[i].getMP.gameObject.SetActive(true);
                statDisplays[i].getAP.gameObject.SetActive(true);

                statDisplays[i].getName.text = Global.Instance.allies[i].getGameObject.name;
                statDisplays[i].getHP.maxValue = Global.Instance.allies[i].getStats.GetAttribute(IStats.Attribute.MaxHP);
                statDisplays[i].getMP.maxValue = Global.Instance.allies[i].getStats.GetAttribute(IStats.Attribute.MaxMP);
                statDisplays[i].getAP.maxValue = Global.Instance.allies[i].getATBGuage.getMaximumValue;

                statDisplays[i].getHP.value = Global.Instance.allies[i].getStats.HP;
                statDisplays[i].getMP.value = Global.Instance.allies[i].getStats.HP;
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
