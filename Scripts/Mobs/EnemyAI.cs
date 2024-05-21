using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public UnityEvent Chase_swap, Patrol_swap, Search_Swap;

    public enum State { Chase, Search, Patrol}
    public State state;

    public NavMeshAgent agent;

    [SerializeField]
    private GameObject start_waypoint, waypoint, player;

    private Vector3 target_pos;

    [SerializeField]
    private LayerMask player_detection_layer;

    IEnumerator Chase_state()
    {
        Debug.Log("chase: Enter");
        while (state == State.Chase)
        {
            while (Check_for_player() == true)
            {
                target_pos = player.transform.position;
                agent.SetDestination(target_pos);

                yield return null;
            }

            yield return new WaitForSeconds(1);

            target_pos = player.transform.position;
            agent.SetDestination(target_pos);
            state = State.Search;
            
        }
        Debug.Log("patrol: Exit");
        NextState();
    }

    IEnumerator Search_state()
    {
        Debug.Log("search: Enter");
        var last_known_pos = player.transform.position;
        while (state == State.Search)
        {

            if(Check_for_player() == true)
            {
                state = State.Chase;
            }
            else
            {
                target_pos = last_known_pos;
                yield return new WaitForSeconds(0.5f);
                target_pos = this.transform.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
                yield return new WaitForSeconds(0.5f);
                target_pos = this.transform.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
                yield return new WaitForSeconds(0.5f);
                target_pos = this.transform.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
                yield return new WaitForSeconds(0.5f);
                state = State.Patrol;

                yield return null;
            }
            
        }
        Debug.Log("patrol: Exit");
        NextState();
    }

    IEnumerator Patrol_state()
    {
        Debug.Log("patrol: Enter");
        target_pos = waypoint.transform.position;
        while (state == State.Patrol)
        {
            agent.SetDestination(target_pos);
            yield return null;
        }
        Debug.Log("patrol: Exit");
        NextState();
    }


    private void Update()
    {

    }


    private void Start()
    {
        state = State.Patrol;
        player = GameObject.FindGameObjectWithTag("Player");
        NextState();
    }

    private void FixedUpdate()
    {
        Check_for_player();
    }

    void NextState()
    {
        string methodName = state.ToString() + "_state";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

    //private void Add_waypoints_to_queue()
    //{
    //    waypoints.Enqueue(start_waypoint.transform.position);
    //    waypoints.Enqueue(waypoint.transform.position);
    //}

    private void OnTriggerEnter(Collider other)
    { 
        //once enemy collides with target waypoint, it spaws to the other waypoint
        if (other.tag == "Waypoint")
        {
            target_pos = start_waypoint.transform.position;
        }
        else if (other.tag == "Start_Waypoint")
        {
            target_pos = waypoint.transform.position;
        }
    }

    private bool Check_for_player()
    {
        RaycastHit hit;
        // forward ray
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, 3, player_detection_layer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            state = State.Chase;
            return true;
        }
        else
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 3, Color.white);

        //wide left ray
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(new Vector3(-0.4f, 0, 0)) + transform.TransformDirection(new Vector3(0,0,0.9f)), out hit, 3, player_detection_layer))
        {
            Debug.DrawRay(transform.position, (transform.TransformDirection(new Vector3(-0.4f, 0, 0)) + transform.TransformDirection(new Vector3(0, 0, 0.9f))) * hit.distance, Color.yellow);
            state = State.Chase;
            return true;
        }
        else
            Debug.DrawRay(transform.position, (transform.TransformDirection(new Vector3(-0.4f, 0, 0)) + transform.TransformDirection(Vector3.forward)) * 3, Color.white);

        //narrow left ray
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(new Vector3(-0.2f, 0, 0)) + transform.TransformDirection(new Vector3(0, 0, 0.9f)), out hit, 3, player_detection_layer))
        {
            Debug.DrawRay(transform.position, (transform.TransformDirection(new Vector3(-0.2f, 0, 0)) + transform.TransformDirection(new Vector3(0, 0, 0.9f))) * hit.distance, Color.yellow);
            state = State.Chase;
            return true;
        }
        else
            Debug.DrawRay(transform.position, (transform.TransformDirection(new Vector3(-0.2f, 0, 0)) + transform.TransformDirection(Vector3.forward)) * 3, Color.white);

        //wide right ray
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(new Vector3(0.4f, 0, 0)) + transform.TransformDirection(Vector3.forward), out hit, 3, player_detection_layer))
        {
            Debug.DrawRay(transform.position, (transform.TransformDirection(new Vector3(0.4f, 0, 0)) + transform.TransformDirection(Vector3.forward)) * hit.distance, Color.yellow);
            state = State.Chase;
            return true;
        }
        else
            Debug.DrawRay(transform.position, (transform.TransformDirection(new Vector3(0.4f,0,0)) + transform.TransformDirection(Vector3.forward)) * 3, Color.white);

        // narrow right ray
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(new Vector3(0.2f, 0, 0)) + transform.TransformDirection(Vector3.forward), out hit, 3, player_detection_layer))
        {
            Debug.DrawRay(transform.position, (transform.TransformDirection(new Vector3(0.2f, 0, 0)) + transform.TransformDirection(Vector3.forward)) * hit.distance, Color.yellow);
            state = State.Chase;
            return true;
        }
        else
            Debug.DrawRay(transform.position, (transform.TransformDirection(new Vector3(0.2f, 0, 0)) + transform.TransformDirection(Vector3.forward)) * 3, Color.white);
        return false;
    }

}
