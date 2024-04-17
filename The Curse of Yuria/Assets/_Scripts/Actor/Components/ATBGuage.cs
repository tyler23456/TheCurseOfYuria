using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TCOY.Actors
{
    [System.Serializable]
    public class ATBGuage
    {
        [SerializeField] float maximumValue = 100f;

        float accumulator = 0f;
        float speed = 0.01f;

        public Action OnATBGuageFilled { get; set; } = () => { };

        public void Initialize(Dictionary<string, int> statsDictionary)
        {
            speed = statsDictionary["Speed"];
        }

        public void OnStatsChanged(Dictionary<string, int> statsDictionary)
        {
            speed = statsDictionary["Speed"];
        }

        public void Update()
        {
            accumulator += Time.deltaTime * speed;

            if (accumulator < maximumValue)
                return;

            accumulator = 0f;
            OnATBGuageFilled.Invoke();
        }
    }
}