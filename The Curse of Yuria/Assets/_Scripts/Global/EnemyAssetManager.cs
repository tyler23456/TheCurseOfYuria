using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AddressableAssets;

[ExecuteInEditMode]
public class EnemyAssetManager : MonoBehaviour
{
    [SerializeField] Material material;
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

        CapsuleCollider2D collider = prefab.GetComponent<CapsuleCollider2D>();
        Rigidbody2D body = prefab.GetComponent<Rigidbody2D>();
        BoxCollider2D previousCollider = prefab.GetComponent<BoxCollider2D>();

        //SpriteRenderer[] spriteRenderers = prefab.GetComponentsInChildren<SpriteRenderer>();

        if (enemy == null)
            enemy = prefab.AddComponent<TCOY.UserActors.Enemy>();

        if (unit == null)
            unit = prefab.AddComponent<TCOY.AStar.GroundUnit>();

        if (drop == null)
            drop = prefab.AddComponent<TCOY.DontDestroyOnLoad.RandomDrop>();

        if (converter == null)
            converter = prefab.AddComponent<TCOY.Independent.MaterialConverter>();


        drop.enabled = false;

        converter.SetMaterial(material);
        converter.convertToMaterial = true;

        body.sharedMaterial = noFrictionMaterial;
        body.gravityScale = 8f;
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
        body.freezeRotation = true;

        collider.sharedMaterial = noFrictionMaterial;

        //foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        //{
        //    spriteRenderer.sortingOrder = 200;
        //}

        //----------------------------make sure BoxCollider2D gets deleted.
        //----------------------------change layer in here too.
        //----------------------------give troll his capsule collider back (use a capsule collider from one of the previous trolls)

        if (previousCollider != null)
            DestroyImmediate(previousCollider);

        PrefabUtility.SavePrefabAsset(prefab);
    }
}
