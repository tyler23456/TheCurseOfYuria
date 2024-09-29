using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TCOY.UserActors
{
    [System.Serializable]
    public class ATBGuage : IATBGuage
    {
        [SerializeField] float maximumValue = 5f;
        [SerializeField] float speed = 1f;

        float accumulator = 0f;
        bool isFull = false;
        int priority = int.MaxValue;

        bool isInStasis = false;

        public Action OnATBGuageFilled { get; set; } = () => { };
        public Action<float> onATBChanged { get; set; } = (value) => {};
        public float getMaximumValue => maximumValue;
        public bool isActive => priority == int.MaxValue;
        public float getValue => accumulator;

        public void Initialize(Dictionary<string, int> statsDictionary)
        {
            speed = statsDictionary["Speed"];
        }

        public void OnStatsChanged(Dictionary<string, int> statsDictionary)
        {
            speed = statsDictionary["Speed"];
        }

        public void Reset()
        {
            accumulator = 0;
            isFull = false;
        }

        public void Update()
        {
            if (priority != int.MaxValue)
                return;

            accumulator += Time.deltaTime * speed;
            onATBChanged.Invoke(accumulator);

            if (accumulator < maximumValue || isFull)
                return;

            if (isInStasis)
                return;

            isFull = true;
            OnATBGuageFilled.Invoke();
        }

        public void RaisePriority()
        {
            if (priority == int.MaxValue)
                return;

            priority++;
        }

        public void LowerPriority()
        {
            if (priority == 0)
                return;

            priority--;
        }

        public void EnterStasis()
        {
            isInStasis = true;
        }

        public void ExitStasis()
        {
            isInStasis = false;
        }
    }
}