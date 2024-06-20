using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public interface ICutsceneAction
{
    IEnumerator Activate();
}
