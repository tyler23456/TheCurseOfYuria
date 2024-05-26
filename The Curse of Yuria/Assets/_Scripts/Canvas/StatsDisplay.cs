using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCOY.Canvas
{
    

    public class StatsDisplay : MonoBehaviour
    {
        [SerializeField] List<StatDisplay> statDisplays;

        void Start()
        {   
            OnRefresh();
        }

        public void OnRefresh()
        {
            IGlobal global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();

            for (int i = 0; i < statDisplays.Count; i++)
            {
                if (i < global.allies.count)
                {
                    int ii = i;
                    global.allies[i].getStats.onHPChanged = (value) => statDisplays[ii].getHP.value = value;
                    global.allies[i].getStats.onMPChanged = (value) => statDisplays[ii].getMP.value = value;
                    global.allies[i].getATBGuage.onATBChanged = (value) => statDisplays[ii].getAP.value = value;

                    statDisplays[i].getName.gameObject.SetActive(true);
                    statDisplays[i].getHP.gameObject.SetActive(true);
                    statDisplays[i].getMP.gameObject.SetActive(true);
                    statDisplays[i].getAP.gameObject.SetActive(true);

                    statDisplays[i].getName.text = global.allies[i].getGameObject.name;
                    statDisplays[i].getHP.maxValue = global.allies[i].getStats.GetAttribute(IStats.Attribute.MaxHP);
                    statDisplays[i].getMP.maxValue = global.allies[i].getStats.GetAttribute(IStats.Attribute.MaxMP);
                    statDisplays[i].getAP.maxValue = global.allies[i].getATBGuage.getMaximumValue;

                    statDisplays[i].getHP.value = global.allies[i].getStats.HP;
                    statDisplays[i].getMP.value = global.allies[i].getStats.HP;
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