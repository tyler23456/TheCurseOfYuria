using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        IGlobal global;

        void Start()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
        }

        public void Load(int level, Vector2 newPosition)
        {
            global.StartCoroutine(CoroutineLoad(level, newPosition));
        }

        IEnumerator CoroutineLoad(int level, Vector2 newPosition)
        {


            yield return null;
        }

        public void MoveParty(Vector2 newPosition)
        {

        }
    }
}