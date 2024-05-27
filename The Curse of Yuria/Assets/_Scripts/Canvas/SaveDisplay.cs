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

        [SerializeField] GameObject partyMemberPrefab;

        [SerializeField] Button buttonPrefab; 
        [SerializeField] RectTransform grid;
        [SerializeField] Text heading;
        [SerializeField] Text description;

        [SerializeField] Button newSaveButton;
        [SerializeField] Button overwriteButton;
        [SerializeField] Button loadButton;


        Button button;
        State state = State.NewSave;

        void OnEnable()
        {
            newSaveButton.onClick.RemoveAllListeners();
            overwriteButton.onClick.RemoveAllListeners();
            loadButton.onClick.RemoveAllListeners();

            newSaveButton.onClick.AddListener(() => { SaveManager.instance.OnNewSave(); RefreshFiles(); });
            overwriteButton.onClick.AddListener(OnOverwriteSettingSet);
            loadButton.onClick.AddListener(OnLoadSettingSet);

            heading.text = "Save a new file";
            description.text = "";
            RefreshFiles();
        }

        void RefreshFiles()
        {
            if (grid == null)
                return;

            foreach (RectTransform child in grid)
                GameObject.Destroy(child.gameObject);

            DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath + Path.AltDirectorySeparatorChar);
            FileInfo[] fileInfos = info.GetFiles();
            

            foreach (FileInfo fileInfo in fileInfos)
            {
                button = GameObject.Instantiate(buttonPrefab, grid);
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
                });
                button.transform.GetChild(0).GetComponent<Text>().text = fileInfo.Name.Split('.')[0];
            }
        }

        void OnOverwriteSettingSet()
        {
            state = State.Overwrite;
            heading.text = "Overwrite a save file";
        }

        void OnLoadSettingSet()
        {
            state = State.Load;
            heading.text = "Load a save file";
        }
    }
}