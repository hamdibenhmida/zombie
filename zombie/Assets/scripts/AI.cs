using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    public enum WanderType {  Random, Waypoint};

    
    float viewdistance = 10f;
    bool isAware;
    public CharacterController ch;
    public WanderType wanderType = WanderType.Random;
    public float wanderSpeed = 7f;
    public float chaseSpeed = 7f;
    public int health = 100;
    public float fov = 120f;
    public GameObject Target;
    public float speed = 1.4f;
    public float wanderRaduis = 7f;
    public Transform[] waypoints;
    public int waypointIndex = 0 ;
    public Animator animator;
    public bool dead = false; 


    NavMeshAgent agent;


    private Vector3 wadnderPoint;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wadnderPoint = RandomWanderPoint();
        animator = GetComponent<Animator>();   
    }
    void Update()
    {
       if (health <= 0)
       {
            dead = true;
            Destroy(gameObject);
        }
        else
        {

        if (isAware)
        {
            agent.SetDestination(ch.transform.position);
            animator.SetBool("Aware", true);
            agent.speed = chaseSpeed; 
        }
        else
        {
            searchForPlayer();
            wander();
            animator.SetBool("Aware", false);
            agent.speed = wanderSpeed;

        }
           
        }
    }
    void searchForPlayer ()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(ch.transform.position)) < fov/2)
        {
            if (Vector3.Distance(ch.transform.position, transform.position)<viewdistance)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position,ch.transform.position, out hit, -1))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        onAware();
                    }
                }
                    
            }
        }
    }
    void onAware()
    {
        isAware = true;
    }
    public void wander()
    {
        if (wanderType == WanderType.Random)
        {
            if (Vector3.Distance(transform.position, wadnderPoint) < 2f)
            {
                wadnderPoint = RandomWanderPoint();
            }
            else
            {
                agent.SetDestination(wadnderPoint);
            }
        }
        else
        {
          if( waypoints.Length >= 2)
            {

            
            if (Vector3.Distance(waypoints[waypointIndex].position, transform.position) <2f )
            {
                if(waypointIndex == waypoints.Length -1)
                {
                    waypointIndex = 0;
                }
                else
                {
                    waypointIndex++; 
                }
                
            }else
            {
                agent.SetDestination(waypoints[waypointIndex].position);
            }

          }
            else
            {
                Debug.LogWarning("7ot akther men 1  waypoint lel AI zombie  "); 
            }

        }

    }

    public void Onhit(int damage)
    {
        health -= damage;
       

    }


    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRaduis) + transform.position ;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRaduis, -1 );
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }
}
