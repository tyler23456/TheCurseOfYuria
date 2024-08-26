using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class StateDatabase : MonoBehaviour
{
    public static StateDatabase Instance { get; set; }

    [SerializeField] AssetLabelReference actionStateReference;
    [SerializeField] AssetLabelReference goalStateReference;

    Dictionary<string, ActionState> actionDatabase = new Dictionary<string, ActionState>();
    Dictionary<string, GoalState> goalDatabase = new Dictionary<string, GoalState>();

    void Awake()
    {
        Instance = this;

        Addressables.LoadAssetsAsync<ActionState>(actionStateReference, (i) =>
        {
            actionDatabase.Add(i.name, i);
        }).WaitForCompletion();

        Addressables.LoadAssetsAsync<GoalState>(goalStateReference, (i) =>
        {
            goalDatabase.Add(i.name, i);
        }).WaitForCompletion();
    }

    public ActionState GetAction(string stateName)
    {
        return actionDatabase[stateName];
    }

    public GoalState GetGoal(string stateName)
    {
        return goalDatabase[stateName];
    }
}
