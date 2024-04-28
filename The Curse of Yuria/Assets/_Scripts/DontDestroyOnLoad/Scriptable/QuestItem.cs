using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class QuestItem : ItemBase
    {
        [SerializeField] GameObject bookPrefab;
        [SerializeField] [TextArea(3, 10)] protected List<string> pagesText;
        
        //maybe put in some book functionality....
    }
}