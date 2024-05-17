using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager
{
    enum State { NewSave, Overwrite, Load }
    void OnNewGame();
    void OnNewSave();
    void OnOverwrite(string fileName);
    void OnLoad(string fileName);
}
