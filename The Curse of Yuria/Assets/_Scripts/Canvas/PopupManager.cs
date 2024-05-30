using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    [SerializeField] GameObject damagePopupPrefab;
    [SerializeField] GameObject recoveryPopupPrefab;

    public static PopupManager Instance { get; private set; }

    GameObject obj;

    void Awake()
    {
        Instance = this;
    }

    public void AddDamagePopup(int damageAmount, Vector2 position)
    {
        obj = Instantiate(damagePopupPrefab, position, Quaternion.identity, transform);
        obj.transform.GetChild(0).GetComponent<TMP_Text>().text = damageAmount.ToString();
    }

    public void AddRecoveryPopup(int recoveryAmount, Vector2 position)
    {
        obj = Instantiate(recoveryPopupPrefab, position, Quaternion.identity, transform);
        obj.transform.GetChild(0).GetComponent<TMP_Text>().text = recoveryAmount.ToString();
    }

    public void ClearAllPopups()
    {
        foreach (Transform t in transform)
            Destroy(t.gameObject);
    }
}
