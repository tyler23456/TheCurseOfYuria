using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterName", menuName = "CharacterName/CharacterName")]
public class CharacterName : ScriptableObject
{
    public GameObject gameObject { get; set; }
}
