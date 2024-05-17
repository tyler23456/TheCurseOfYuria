using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TCOY.Actors
{
    public class ATBGuage : IATBGuage
    {
        float maximumValue = 5f;

        float accumulator = 0f;
        float speed = 1f;
        bool isFull = false;
        int activity = 0;

        public Action OnATBGuageFilled { get; set; } = () => { };
        public bool isActive => activity == 0;

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
            if (!isActive)
                return;

            accumulator += Time.deltaTime * speed;

            if (accumulator < maximumValue || isFull)
                return;

            isFull = true;
            OnATBGuageFilled.Invoke();
        }

        public void Activate()
        {
            activity++;

            if (activity > 0)
                activity = 0;
        }

        public void Deactivate()
        {
            activity--;
        }
    }
}