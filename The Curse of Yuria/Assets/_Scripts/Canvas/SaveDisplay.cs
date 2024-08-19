using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using UnityEngine.UI;

namespace TCOY.Canvas
{
    public class SaveDisplay : MonoBehaviour
    {
        public enum State { NewSave, Overwrite, Load }

        [SerializeField] Button buttonPrefab;
        [SerializeField] GameObject leftPanel;
        [SerializeField] GameObject rightPanel;
        [SerializeField] RectTransform rightGrid;
        [SerializeField] Text heading;
        [SerializeField] Text description;

        [SerializeField] Button newSaveButton;
        [SerializeField] Button overwriteButton;
        [SerializeField] Button loadButton;


        Button button;
        State state = State.NewSave;

        void OnEnable()
        {
            leftPanel.SetActive(false);
            rightPanel.SetActive(true);

            newSaveButton.onClick.RemoveAllListeners();
            overwriteButton.onClick.RemoveAllListeners();
            loadButton.onClick.RemoveAllListeners();

            newSaveButton.onClick.AddListener(OnNewSave);
            overwriteButton.onClick.AddListener(OnOverwriteSettingSet);
            loadButton.onClick.AddListener(OnLoadSettingSet);

            newSaveButton.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;
            overwriteButton.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;
            loadButton.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;

            heading.text = "Save a new file";
            description.text = "";
            RefreshFiles();
        }

        void OnTabEnter()
        {
            MenuSFXManager.Instance.PlayHover();
        }

        void OnItemEnter()
        {
            MenuSFXManager.Instance.PlayHover();
        }

        void OnNewSave()
        {
            SaveManager.instance.OnNewSave();
            RefreshFiles();
            MenuSFXManager.Instance.PlayClick();
        }

        void RefreshFiles()
        {
            if (rightGrid == null)
                return;

            foreach (RectTransform child in rightGrid)
                GameObject.Destroy(child.gameObject);

            DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath + Path.AltDirectorySeparatorChar);
            FileInfo[] fileInfos = info.GetFiles();
            

            foreach (FileInfo fileInfo in fileInfos)
            {
                button = GameObject.Instantiate(buttonPrefab, rightGrid);
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    switch (state)
                    {
                        case State.Overwrite:
                            SaveManager.instance.OnOverwrite(fileInfo.Name);
                            RefreshFiles();
                            break;
                        case State.Load:
                            SaveManager.instance.OnLoad(fileInfo.Name);
                            break;
                    }
                    MenuSFXManager.Instance.PlayClick();
                });
                button.transform.GetChild(0).GetComponent<Text>().text = fileInfo.Name.Split('.')[0];

                button.GetComponent<PointerHover>().onPointerEnter = OnItemEnter;
            }
        }

        void OnOverwriteSettingSet()
        {
            state = State.Overwrite;
            heading.text = "Overwrite a save file";
            MenuSFXManager.Instance.PlayClick();
        }

        void OnLoadSettingSet()
        {
            state = State.Load;
            heading.text = "Load a save file";
            MenuSFXManager.Instance.PlayClick();
        }
    }
}