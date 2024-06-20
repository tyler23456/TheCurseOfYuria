using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class StateDatabase : MonoBehaviour
{
    public static StateDatabase Instance { get; set; }

    [SerializeField] AssetLabelReference actionStateReference;
    [SerializeField] AssetLabelReference goalStateReference;

    Dictionary<string, IAction> actionDatabase = new Dictionary<string, IAction>();
    Dictionary<string, IGoal> goalDatabase = new Dictionary<string, IGoal>();

    void Awake()
    {
        Instance = this;

        Addressables.LoadAssetsAsync<IAction>(actionStateReference, (i) =>
        {
            actionDatabase.Add(i.getName, i);
        }).WaitForCompletion();

        Addressables.LoadAssetsAsync<IGoal>(goalStateReference, (i) =>
        {
            goalDatabase.Add(i.getName, i);
        }).WaitForCompletion();
    }

    public IAction GetAction(string stateName)
    {
        return actionDatabase[stateName];
    }

    public IGoal GetGoal(string stateName)
    {
        return goalDatabase[stateName];
    }
}
