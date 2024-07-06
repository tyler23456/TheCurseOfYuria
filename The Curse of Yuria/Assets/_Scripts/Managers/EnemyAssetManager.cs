using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AddressableAssets;
using UnityEditor.Animations;
using System.Linq;
using TCOY.UserActors;

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

    IPrefabComponentsBuilder[] prefabComponentsBuilders;
    
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

        prefabComponentsBuilders = GetComponents<IPrefabComponentsBuilder>();

        foreach (GameObject prefab in prefabs)
            RefreshEnemyPrefabs(prefab);

        foreach (AnimatorController controller in controllers)
            RefreshAnimatorControllers(controller);

        AssetDatabase.SaveAssets();
    }

    void RefreshEnemyPrefabs(GameObject prefab)
    {
        //--------------------------------
        //prefab.GetComponent<RandomDrop>().Reset();
        //prefab.GetComponent<Actor>().Reset();
        //--------------------------------

        foreach (IPrefabComponentsBuilder prefabComponentsBuilder in prefabComponentsBuilders)
            prefabComponentsBuilder.AddComponentsWithAppropriateValuesTo(prefab);

        CapsuleCollider2D collider = prefab.GetComponent<CapsuleCollider2D>();
        Rigidbody2D body = prefab.GetComponent<Rigidbody2D>();
        BoxCollider2D previousCollider = prefab.GetComponent<BoxCollider2D>();
        Animator animator = prefab.GetComponent<Animator>();

        if (animator == null)
            animator = prefab.AddComponent<Animator>();

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
