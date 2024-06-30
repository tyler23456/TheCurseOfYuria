using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AddressableAssets;
using UnityEditor.Animations;
using System.Linq;

[ExecuteInEditMode]
public class EnemyAssetManager : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] PhysicsMaterial2D noFrictionMaterial;
    [SerializeField] AssetLabelReference enemyPrefabReference;
    [SerializeField] AssetLabelReference animatorControllerReference;
    [SerializeField] TCOY.ControllerStates.GoalBase initialGoalState;
    [SerializeField] TCOY.ControllerStates.ActionBase initialActionState;
    [SerializeField] bool refresh = false;
    
    void Update()
    {
        if (!refresh)
            return;
        
        refresh = false;

        List<GameObject> prefabs = new List<GameObject>();
        List<AnimatorController> controllers = new List<AnimatorController>();

        Addressables.LoadAssetsAsync<GameObject>(enemyPrefabReference, (i) =>
        {
            prefabs.Add(i);
        }).WaitForCompletion();

        Addressables.LoadAssetsAsync<AnimatorController>(animatorControllerReference, (i) =>
        {
            controllers.Add(i);
        }).WaitForCompletion();

        foreach (GameObject prefab in prefabs)
            RefreshEnemyPrefabs(prefab);

        foreach (AnimatorController controller in controllers)
            RefreshAnimatorControllers(controller);

        AssetDatabase.SaveAssets();
    }

    void RefreshEnemyPrefabs(GameObject prefab)
    {
        TCOY.UserActors.Enemy enemy = prefab.GetComponent<TCOY.UserActors.Enemy>();
        TCOY.AStar.ControllerUnit unit = prefab.GetComponent<TCOY.AStar.ControllerUnit>();
        TCOY.DontDestroyOnLoad.RandomDrop drop = prefab.GetComponent<TCOY.DontDestroyOnLoad.RandomDrop>();
        TCOY.Independent.MaterialConverter converter = prefab.GetComponent<TCOY.Independent.MaterialConverter>();

        CapsuleCollider2D collider = prefab.GetComponent<CapsuleCollider2D>();
        Rigidbody2D body = prefab.GetComponent<Rigidbody2D>();
        BoxCollider2D previousCollider = prefab.GetComponent<BoxCollider2D>();
        Animator animator = prefab.GetComponent<Animator>();

        if (enemy == null)
            enemy = prefab.AddComponent<TCOY.UserActors.Enemy>();

        if (unit == null)
            unit = prefab.AddComponent<TCOY.AStar.ControllerUnit>();

        if (drop == null)
            drop = prefab.AddComponent<TCOY.DontDestroyOnLoad.RandomDrop>();

        if (converter == null)
            converter = prefab.AddComponent<TCOY.Independent.MaterialConverter>();

        if (animator == null)
            animator = prefab.AddComponent<Animator>();

        Color color = enemy.trajectoryPathColor;

        if (color.r < 0.2f && color.g < 0.2f && color.b < 0.2f)
        {
            float valueA = Random.Range(0.65f, 1f);
            float valueB = Random.Range(0.3f, 1f);
            float valueC = Random.Range(0f, 0.5f);
            List<float> randomValues = new List<float> { valueA, valueB, valueC };
            randomValues = randomValues.OrderBy(i => Random.Range(0, 1)).ToList();
            enemy.trajectoryPathColor = new Color(randomValues[0], randomValues[1], randomValues[2], 0.9f);
        }
        
        unit.SetInitialStates(initialGoalState, initialActionState);

        drop.enabled = false;

        converter.SetMaterial(material);
        converter.convertToMaterial = true;

        body.sharedMaterial = noFrictionMaterial;
        body.gravityScale = 8f;
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
        body.freezeRotation = true;
        body.drag = 16;

        collider.sharedMaterial = noFrictionMaterial;

        prefab.layer = LayerMask.NameToLayer("Enemy");

        if (previousCollider != null)
            DestroyImmediate(previousCollider, true);
        
        PrefabUtility.SavePrefabAsset(prefab);
    }

    void RefreshAnimatorControllers(AnimatorController controller)
    {
        List<AnimatorControllerParameter> parameters = new List<AnimatorControllerParameter> (controller.parameters);

        AnimatorControllerParameter movePriority = parameters.Find(i => i.name == "MovePriority");

        if (movePriority == null)
        {
            AnimatorControllerParameter parameter = new AnimatorControllerParameter();
            parameters.Add(parameter);
            movePriority = parameter;
        }

        movePriority.name = "MovePriority";
        movePriority.type = AnimatorControllerParameterType.Int;
        movePriority.defaultInt = int.MaxValue;

        AnimatorControllerParameter isGrounded = parameters.Find(i => i.name == "IsGrounded");

        if (isGrounded == null)
        {
            AnimatorControllerParameter parameter = new AnimatorControllerParameter();
            parameters.Add(parameter);
            isGrounded = parameter;
        }

        isGrounded.name = "IsGrounded";
        isGrounded.type = AnimatorControllerParameterType.Bool;
        isGrounded.defaultBool = true;

        controller.parameters = parameters.ToArray();

        EditorUtility.SetDirty(controller);
    }
}
