using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Canvas
{
    public class MenuBase : MonoBehaviour
    {
        protected void OnEnable()
        {
            Global.instance.gameState = Global.GameState.Paused;
            SelectionMarkers.instance.DestroyAllMarkers();
            //Global.instance.getAudioSource.PlayOneShot(open);
        }

        protected void OnDisable()
        {
            Global.instance.gameState = Global.GameState.Playing;
            SelectionMarkers.instance.DestroyAllMarkers();
            //Global.instance.getAudioSource.PlayOneShot(open);
        }
    }
}