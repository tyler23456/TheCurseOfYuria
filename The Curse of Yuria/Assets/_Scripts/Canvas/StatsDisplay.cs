using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCOY.Canvas
{
    

    public class StatsDisplay : MonoBehaviour
    {
        [SerializeField] List<StatDisplay> statDisplays;

        void OnEnable()
        {   
            OnRefresh();
        }

        void OnDisable()
        {
            
        }

        public void OnRefresh()
        {
            for (int i = 0; i < statDisplays.Count; i++)
            {
                if (i < Global.instance.allies.count)
                {
                    int ii = i;
                    Global.instance.allies[i].getStats.onHPChanged = (value) => statDisplays[ii].getHP.value = value;
                    Global.instance.allies[i].getStats.onMPChanged = (value) => statDisplays[ii].getMP.value = value;
                    Global.instance.allies[i].getATBGuage.onATBChanged = (value) => statDisplays[ii].getAP.value = value;

                    statDisplays[i].getName.gameObject.SetActive(true);
                    statDisplays[i].getHP.gameObject.SetActive(true);
                    statDisplays[i].getMP.gameObject.SetActive(true);
                    statDisplays[i].getAP.gameObject.SetActive(true);

                    statDisplays[i].getName.text = Global.instance.allies[i].getGameObject.name;
                    statDisplays[i].getHP.maxValue = Global.instance.allies[i].getStats.GetAttribute(IStats.Attribute.MaxHP);
                    statDisplays[i].getMP.maxValue = Global.instance.allies[i].getStats.GetAttribute(IStats.Attribute.MaxMP);
                    statDisplays[i].getAP.maxValue = Global.instance.allies[i].getATBGuage.getMaximumValue;

                    statDisplays[i].getHP.value = Global.instance.allies[i].getStats.HP;
                    statDisplays[i].getMP.value = Global.instance.allies[i].getStats.HP;
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