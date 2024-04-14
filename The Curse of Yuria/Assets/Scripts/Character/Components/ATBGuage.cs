using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TCOY.Character
{
    [System.Serializable]
    public class ATBGuage
    {
        [SerializeField] float maximumValue;

        float accumulator = 0f;

        public Action OnATBGuageFilled { get; set; } = () => { };

        public void Initialize()
        {

        }

        public void Update()
        {
            accumulator += Time.deltaTime;

            if (accumulator < maximumValue)
                return;

            accumulator = 0f;
            OnATBGuageFilled.Invoke();
        }
    }
}