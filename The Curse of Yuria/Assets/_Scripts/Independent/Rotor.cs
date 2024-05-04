using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Independent
{
    public class Rotor : MonoBehaviour
    {
        [SerializeField] Transform transformToRotate;
        [SerializeField] float speed = 50f;

        void Update()
        {
            Vector3 eulerAngles = transformToRotate.eulerAngles;
            eulerAngles.z += speed * Time.deltaTime;
            transformToRotate.eulerAngles = eulerAngles;
        }
    }
}