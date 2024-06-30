using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TCOY.UserActors
{
    public class StatusEffects : IStatusEffects
    {
        List<string> names = new List<string>();
        List<float> accumulators = new List<float>();

        public int getCount => names.Count;

        public bool Contains(string name)
        {
            return names.Contains(name);
        }

        public void Add(string name, float accumulator = 0f)
        {
            int index = names.IndexOf(name);

            if (index > -1)
            {
                accumulators[index] = accumulator;
            }
            else
            {
                names.Add(name);
                accumulators.Add(accumulator);
            }
        }

        public void AddRange(List<string> names)
        {
            foreach (string name in names)
                Add(name);
        }

        public void Remove(string name)
        {
            int index = names.IndexOf(name);

            if (index > -1)
            {
                names.RemoveAt(index);
                accumulators.RemoveAt(index);
            }
        }

        void RemoveAt(int index)
        {
            string name = names[index];
            names.RemoveAt(index);
            accumulators.RemoveAt(index);
        }

        public void RemoveRange(List<string> names)
        {
            foreach (string name in names)
                Remove(name);
        }

        public bool Elapse(string name, float duration)
        {
            int index = names.IndexOf(name);

            if (index == -1)
                return false;

            accumulators[index] += Time.deltaTime;

            if (accumulators[index] > duration)
                return false;

            return true;
        }

        public string[] GetNames()
        {
            return names.ToArray();
        }

        public float[] GetAccumulators()
        {
            return accumulators.ToArray();
        }

        public void SetNamesAndAccumulators(string[] names, float[] accumulators)
        {
            RemoveAll();

            for (int i = 0; i < names.Length; i++)
                Add(names[i], accumulators[i]);
        }

        public void RemoveAll()
        {
            for (int i = names.Count - 1; i > -1; i--)
                RemoveAt(i);         
        }

        public void RemoveWhere(Func<string, bool> predicate)
        {
            for (int i = names.Count - 1; i > -1; i--)
                if (predicate.Invoke(names[i]))
                    RemoveAt(i);
        }
    }
}