using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh_point_click_movement : MonoBehaviour
{
    public Camera cam;

    public NavMeshAgent agent;


    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 random_position;
            random_position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Debug.Log(random_position);
            agent.SetDestination(random_position);

        }

    }
}
