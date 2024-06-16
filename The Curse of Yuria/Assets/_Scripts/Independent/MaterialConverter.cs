using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Independent
{
    [ExecuteInEditMode]
    public class MaterialConverter : MonoBehaviour
    {
        [SerializeField] Material materialToConvertTo;
        [SerializeField] public bool convertToMaterial = false;

        void Update()
        {
            if (!convertToMaterial)
                return;

            convertToMaterial = false;

            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer renderer in spriteRenderers)
                renderer.material = materialToConvertTo;
        }

        public void SetMaterial(Material material)
        {
            materialToConvertTo = material;
        }
    }
}