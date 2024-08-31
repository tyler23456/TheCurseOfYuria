using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TCOY.Canvas
{
    public class NotificationManager : MonoBehaviour
    {
        public static NotificationManager Instance { get; private set; }

        [SerializeField] Camera mainCamera;
        [SerializeField] GameObject notificationPrefab;
        [SerializeField] Vector3 screenPosition = new Vector3(960f, 540f, 0f);

        GameObject obj;
        Vector3 halfSize;
        float accumulator = 0f;
        float duration = 2f;

        void Awake()
        {
            Instance = this;
        }

        public void Notify(string message, float duration = 2f)
        {
            accumulator = 0f;
            this.duration = duration;

            foreach (Transform child in transform)
                Destroy(child.gameObject);

            halfSize = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

            obj = Instantiate(notificationPrefab, mainCamera.ScreenToWorldPoint(screenPosition + halfSize), Quaternion.identity, transform);
            obj.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = message;

            MenuSFXManager.Instance.PlayNotification();
        }

        public void Update()
        {
            if (obj == null)
                return;

            accumulator += Time.unscaledDeltaTime;

            halfSize = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

            obj.transform.position = mainCamera.ScreenToWorldPoint(screenPosition + halfSize);
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 0f);

            if (accumulator < duration)
                return;

            GameObject.Destroy(obj);
        }
    }
}
