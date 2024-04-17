using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TCOY.Level
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] float parallaxEffect = 2f;
        [SerializeField] SpriteRenderer sprite;

        new Transform camera;
        float startPosition = 0f;
        float length = 0f;
        float offsetY;

        // Start is called before the first frame update
        void Start()
        {
            camera = GameObject.Find("/DontDestroyOnLoad/Main Camera").transform;
            startPosition = transform.position.x;
            length = sprite.bounds.size.x;
            offsetY = transform.position.y;
        }

        // Update is called once per frame
        void Update()
        {

            float offset = (camera.transform.position.x * (1 - parallaxEffect));
            float distance = (camera.transform.position.x * parallaxEffect);

            transform.position = new Vector3(startPosition + distance, offsetY + camera.transform.position.y, transform.position.z);

            if (offset > startPosition + length)
            {
                startPosition += length;
            }
            else if (offset < startPosition - length)
            {
                startPosition -= length;
            }
        }
    }
}