using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TCOY.Actors
{
    [System.Serializable]
    public class ATBGuage : IATBGuage
    {
        [SerializeField] float maximumValue = 100f;

        float accumulator = 0f;
        float speed = 0.01f;
        bool isFull = false;

        public Action OnATBGuageFilled { get; set; } = () => { };

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
            accumulator += Time.deltaTime * speed;

            if (accumulator < maximumValue || isFull)
                return;

            isFull = true;
            OnATBGuageFilled.Invoke();
        }
    }
}