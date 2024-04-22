using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;

namespace TCOY.Actors
{
    [System.Serializable]
    public class Equipment : IEquipment
    {
        IFactory factory;

        [SerializeField] Character character;

        string[] parts;

        public void Initialize()
        {
            parts = new string[] { "None", "None", "None", "None", "None", "None", "None", "None", };
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();
        }

        public string GetPart(IEquipment.Part part)
        {
            return parts[(int)part];
        }

        public void Equip(IEquipment.Part part, string itemName = "None")
        {
            parts[(int)part] = itemName;
        }

        public string[] GetSerializedData()
        {
            return parts;
        }

        public void SetSerializedData(string[] array)
        {
            parts = array;
        }
    }
}