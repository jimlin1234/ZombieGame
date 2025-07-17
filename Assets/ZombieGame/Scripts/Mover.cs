using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;
    //Ray lastRay;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCorsor();
        }
        
    }
        
    private void MoveToCorsor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        bool hasHit = Physics.Raycast(ray,out hit);
        if(hasHit == true)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
       // lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //GetComponent<NavMeshAgent>().destination = target.position;
    }
    
}
