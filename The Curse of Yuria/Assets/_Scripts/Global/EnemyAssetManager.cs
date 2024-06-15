using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AddressableAssets;

[ExecuteInEditMode]
public class EnemyAssetManager : MonoBehaviour
{
    [SerializeField] PhysicsMaterial2D noFrictionMaterial;
    [SerializeField] AssetLabelReference enemyPrefab;
    [SerializeField] bool refresh = false;

    
    
    void Update()
    {
        if (!refresh)
            return;

        refresh = false;

        List<GameObject> prefabs = new List<GameObject>();

        Addressables.LoadAssetsAsync<GameObject>(enemyPrefab, (i) =>
        {
            prefabs.Add(i);
        }).WaitForCompletion();

        foreach (GameObject prefab in prefabs)
            RefreshEnemyPrefabs(prefab);
    }

    void RefreshEnemyPrefabs(GameObject prefab)
    {
        TCOY.UserActors.Enemy enemy = prefab.GetComponent<TCOY.UserActors.Enemy>();
        TCOY.AStar.GroundUnit unit = prefab.GetComponent<TCOY.AStar.GroundUnit>();
        TCOY.DontDestroyOnLoad.RandomDrop drop = prefab.GetComponent<TCOY.DontDestroyOnLoad.RandomDrop>();
        TCOY.Independent.MaterialConverter converter = prefab.GetComponent<TCOY.Independent.MaterialConverter>();

        Rigidbody2D body = prefab.GetComponent<Rigidbody2D>();
        BoxCollider2D collider = prefab.GetComponent<BoxCollider2D>();

        if (enemy == null)
            enemy = prefab.AddComponent<TCOY.UserActors.Enemy>();

        if (unit == null)
            unit = prefab.AddComponent<TCOY.AStar.GroundUnit>();

        if (drop == null)
            drop = prefab.AddComponent<TCOY.DontDestroyOnLoad.RandomDrop>();

        if (converter == null)
            converter = prefab.AddComponent<TCOY.Independent.MaterialConverter>();


        if (body == null)
            body = prefab.GetComponent<Rigidbody2D>();

        if (collider == null)
            collider = prefab.GetComponent<BoxCollider2D>();

        drop.enabled = false;

        body.sharedMaterial = noFrictionMaterial;
        body.gravityScale = 8f;
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
        body.freezeRotation = true;

        collider.sharedMaterial = noFrictionMaterial;
    }
}
