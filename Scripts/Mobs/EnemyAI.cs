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

    private bool at_waypoint = false;

    public Vector3 target;
    private Queue<Vector3> waypoints = new Queue<Vector3>();

    [SerializeField]
    private LayerMask player_detection_layer;

    IEnumerator Chase_state()
    {
        Debug.Log("chase: Enter");
        while (state == State.Chase)
        {
            target = player.transform.position;
            agent.SetDestination(target);
            if (Vector3.Distance(this.transform.position, player.transform.position) > 15)
                state = State.Search;

            yield return null;
        }
        Debug.Log("patrol: Exit");
        NextState();
    }

    IEnumerator Search_state()
    {
        Debug.Log("search: Enter");
        while (state == State.Search)
        {
            target = this.transform.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            if (Vector3.Distance(this.transform.position, player.transform.position) <= 10)
                state = State.Chase;
            yield return new WaitForSeconds(1);
            target = this.transform.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            if (Vector3.Distance(this.transform.position, player.transform.position) <= 10)
                state = State.Chase;
            yield return new WaitForSeconds(1);
            target = this.transform.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            if (Vector3.Distance(this.transform.position, player.transform.position) <= 10)
                state = State.Chase;
            yield return new WaitForSeconds(1);
            state = State.Patrol;

            yield return null;
        }
        Debug.Log("patrol: Exit");
        NextState();
    }

    IEnumerator Patrol_state()
    {
        Debug.Log("patrol: Enter");
        target = waypoint.transform.position;
        while (state == State.Patrol)
        {
            agent.SetDestination(target);
            if (Vector3.Distance(this.transform.position, player.transform.position) <= 15)
                state = State.Chase;

            yield return null;
        }
        Debug.Log("patrol: Exit");
        NextState();
    }


    private void Update()
    {
        //if (state == State.chase)
        //{
        //    StopAllCoroutines();
        //    StartCoroutine(Chase_state());
        //}

        //else if (state == State.patrol)
        //{
        //    StopAllCoroutines();
        //    StartCoroutine(Patrol_state());
        //}

        //else if (state == State.search)
        //{
        //    StopAllCoroutines();
        //    StartCoroutine(Search_state());
        //}
    }


    private void Start()
    {
        

        state = State.Patrol;
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

    public void Set_patrol_state()
    {
        StopAllCoroutines();
        StartCoroutine(Patrol_state());
    }
    public void Set_search_state()
    {
        StopAllCoroutines();
        StartCoroutine(Search_state());
    }
    public void Set_chase_state()
    {
        StopAllCoroutines();
        StartCoroutine(Chase_state());
    }

    private void Add_waypoints_to_queue()
    {
        waypoints.Enqueue(start_waypoint.transform.position);
        waypoints.Enqueue(waypoint.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.tag == "Waypoint")
        {
            target = start_waypoint.transform.position;
        }
        else if (other.tag == "Start_Waypoint")
        {
            target = waypoint.transform.position;

        }
    }

    private void Check_for_player()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f, player_detection_layer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            state = State.Chase;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }

        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.left), out hit, 10f, player_detection_layer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.yellow);
            state = State.Chase;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 1000, Color.white);
        }

        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.right), out hit, 10f, player_detection_layer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
            state = State.Chase;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1000, Color.white);
        }

        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.back), out hit, 10f, player_detection_layer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);
            state = State.Chase;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 1000, Color.white);
        }
    }

}
