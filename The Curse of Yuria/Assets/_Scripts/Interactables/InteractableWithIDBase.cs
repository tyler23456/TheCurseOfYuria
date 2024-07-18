using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TCOY.Interactables
{
    public class InteractableWithIDBase : InteractableBase
    {
        [SerializeField] protected UniqueIdentifier uniqueIdentifier;

        protected string getID => uniqueIdentifier.getID;


        protected void OnValidate()
        {
            string path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject);
            GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (obj == null)
                return;

            uniqueIdentifier.Initialize(obj.GetComponent<InteractableWithIDBase>().getID);
        }
    }
}