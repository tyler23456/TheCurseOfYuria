using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Inventory : IInventory
    {
        public void Add(string name, int count)
        {
            throw new System.NotImplementedException();
        }

        public string[] getAllItems()
        {
            throw new System.NotImplementedException();
        }

        public string[] getUnmarkedItems()
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string name, int count = 1)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index, int count = 1)
        {
            throw new System.NotImplementedException();
        }

        bool IInventory.Contains(string name, int count)
        {
            throw new System.NotImplementedException();
        }
    }
}