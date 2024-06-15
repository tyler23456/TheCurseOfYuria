using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Independent
{
    [ExecuteInEditMode]
    public class MaterialConverter : MonoBehaviour
    {
        [SerializeField] Material materialToConvertTo;
        [SerializeField] bool convertToMaterial = false;

        void Update()
        {
            if (!convertToMaterial)
                return;

            convertToMaterial = false;

            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer renderer in spriteRenderers)
                renderer.material = materialToConvertTo;


        }
    }
}