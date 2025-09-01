using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.NewSaving
{
    public class NewSavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                GetComponent<NewSavingSystem>().Save(defaultSaveFile);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                GetComponent<NewSavingSystem>().Load(defaultSaveFile);
            }
        }
    }
}
