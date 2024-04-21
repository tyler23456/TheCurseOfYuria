using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Actors
{
    public class Equipment : IEquipment
    {
        public string helmet { get; set; }
        public string earring { get; set; }
        public string glasses { get; set; }
        public string meleeWeapon1H { get; set; }
        public string meleeWeapon2H { get; set; }
        public string cape { get; set; }
        public string armor { get; set; }
        public string shield { get; set; }
        public string bow { get; set; }
    }
}