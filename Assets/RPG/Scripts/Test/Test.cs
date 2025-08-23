using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] GameObject cube;
    void Start()
    {
        StartCoroutine(CreatCube());
    }
    IEnumerator CreatCube()
    {
        //GameObject cube = new GameObject();
        yield return new WaitForSeconds(3);
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(47, 5.7f, -6);
    }
}
