using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterNameBehavior : MonoBehaviour
{
    [SerializeField] CharacterName characterName;
    [SerializeField] bool changeName = false;

    public void Update()
    {
        if (!changeName)
            return;

        changeName = false;
        RefreshCharacterName();
    }

    void RefreshCharacterName()
    {
        if (characterName == null)
        {
            characterName = (CharacterName)ScriptableObject.CreateInstance(typeof(CharacterName));
            AssetDatabase.CreateAsset(characterName, "Assets/_Scriptable/CharacterNames/" + name + ".asset");
        }

        if (name != characterName.name)
        {
            AssetDatabase.RenameAsset("Assets/_Scriptable/CharacterNames/" + characterName.name + ".asset", name);
            characterName.name = name;
            EditorUtility.SetDirty(characterName);
            AssetDatabase.SaveAssetIfDirty(characterName);        
        }
    }
}
