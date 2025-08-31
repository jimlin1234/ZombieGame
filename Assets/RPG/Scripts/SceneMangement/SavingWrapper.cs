using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.SceneMangement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "Save";
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        private void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        private void Load()
        {
            //TODO: call to saving system load
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }
}
