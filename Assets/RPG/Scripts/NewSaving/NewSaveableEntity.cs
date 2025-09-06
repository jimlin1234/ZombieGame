using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.NewSaving 
{
    public class NewSaveableEntity : MonoBehaviour
    {
        public string GetUniqueIdentifier()
        {
            return "";
        }

        public object CaptureState()
        {
            print("Capturing state for " + GetUniqueIdentifier());
            return null;
        }

        public void RestoreState(object state)
        {
            print("Restoring state for " + GetUniqueIdentifier());
        }
    }
}
