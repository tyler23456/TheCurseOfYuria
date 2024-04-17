using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.ObjectModel;

public interface ICutscene
{
    public bool waitingForInput { get; set; }
    public Action onStart { get; set; }
    public Action onStop { get; set; }

    public void Play(ReadOnlyCollection<string> actionList);
}
