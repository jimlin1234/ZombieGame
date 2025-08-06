using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    public class PatrolPath : MonoBehaviour
    {
        private int wayPoints;
        private void Start()
        {
            wayPoints = transform.childCount;
        }
        private void OnDrawGizmos()
        {
            for (int i = 0; i < wayPoints; i++)
            {
                print(i);
            }
        }
    }
}
