using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.NewSaving
{
    [System.Serializable]
    public class NewSerializableVector3
    {
        float x;
        float y;
        float z;

        public NewSerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }
    }
}
