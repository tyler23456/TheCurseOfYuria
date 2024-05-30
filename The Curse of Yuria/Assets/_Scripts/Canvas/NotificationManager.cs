using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    [SerializeField] GameObject notificationPrefab;
    [SerializeField] Vector2 screenPosition = new Vector2(960f, 540f);

    GameObject obj;

    void Awake()
    {
        Instance = this;
    }

    public void Notify(string message, float duration = 2f)
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        obj = Instantiate(notificationPrefab, screenPosition, Quaternion.identity, transform);
        obj.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = message;
        Destroy(obj, duration);
    }
}
