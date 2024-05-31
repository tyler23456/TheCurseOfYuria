using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public interface IAction
{
    IEnumerator Activate(List<IActor> actors, Image image, TMP_Text text);
}
