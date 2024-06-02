using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    [SerializeField] GameObject HPDamagePopupPrefab;
    [SerializeField] GameObject HPRecoveryPopupPrefab;
    [SerializeField] GameObject MPDamagePopupPrefab;
    [SerializeField] GameObject MPRecoveryPopupPrefab;

    public static PopupManager Instance { get; private set; }

    GameObject obj;

    void Awake()
    {
        Instance = this;
    }

    public void AddHPDamagePopup(int damageAmount, Vector2 position)
    {
        AddPopup(HPDamagePopupPrefab, damageAmount, position);
    }

    public void AddHPRecoveryPopup(int recoveryAmount, Vector2 position)
    {
        AddPopup(HPRecoveryPopupPrefab, recoveryAmount, position);
    }

    public void AddMPDamagePopup(int damageAmount, Vector2 position)
    {
        AddPopup(MPDamagePopupPrefab, damageAmount, position);
    }

    public void AddMPRecoveryPopup(int recoveryAmount, Vector2 position)
    {
        AddPopup(MPRecoveryPopupPrefab, recoveryAmount, position);
    }

    void AddPopup(GameObject prefab, int amount, Vector2 position)
    {
        obj = Instantiate(prefab, position, Quaternion.identity, transform);
        obj.transform.GetChild(0).GetComponent<TMP_Text>().text = amount.ToString();
    }

    public void ClearAllPopups()
    {
        foreach (Transform t in transform)
            Destroy(t.gameObject);
    }
}
