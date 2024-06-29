using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AddressableAssets;
using UnityEditor.Animations;

[ExecuteInEditMode]
public class EnemyAssetManager : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] PhysicsMaterial2D noFrictionMaterial;
    [SerializeField] AssetLabelReference enemyPrefabReference;
    [SerializeField] AssetLabelReference animatorControllerReference;
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

        drop.enabled = false;

        converter.SetMaterial(material);
        converter.convertToMaterial = true;

        body.sharedMaterial = noFrictionMaterial;
        body.gravityScale = 8f;
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
        body.freezeRotation = true;

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

    }
}
