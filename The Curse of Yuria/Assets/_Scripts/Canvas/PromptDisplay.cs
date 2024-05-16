using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TCOY.Canvas
{
    public class PromptDisplay : MonoBehaviour
    {
        [SerializeField] Image promptDisplayImage;
        [SerializeField] TMP_Text promptDisplayText;


        //uses prompt queue to display all strings in the queue
        void OnEnable()
        {
            
        }

        void Update()
        {

        }

        void OnDisable()
        {
            
        }
    }
}