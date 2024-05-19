using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    public class Position : IPosition
    {
        [SerializeField] Rigidbody2D rigidbody2D;

        int activity = 0;

        public bool isActive => activity == 0;

        public Position(GameObject obj)
        {
            this.rigidbody2D = obj.GetComponent<Rigidbody2D>();
        }

        public void Set(Vector2 position)
        {
            rigidbody2D.Sleep();
            rigidbody2D.transform.position = position;
            rigidbody2D.WakeUp();
        }

        public void Add(Vector2 offset, ForceMode2D forceMode2D)
        {
            rigidbody2D.AddForce(offset, forceMode2D);
        }

        public void FixedUpdate()
        {
            if (!isActive)
                return;


        }

        public void Update()
        {
            if (!isActive)
                return;


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