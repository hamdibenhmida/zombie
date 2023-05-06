using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    float viewdistance = 10f;
    bool isAware;
    public CharacterController ch;
    public float fov = 120f;
    public GameObject Target;
    public float speed = 1.4f;

    NavMeshAgent mohsen_hedi;
    // Start is called before the first frame update
    void Start()
    {
        mohsen_hedi = GetComponent<NavMeshAgent>();

    }
    void Update()
        {
        if (isAware)
        {
            mohsen_hedi.SetDestination(ch.transform.position);
            Debug.Log("...");
           //transform.LookAt(Target.gameObject.transform);
           //transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else
        {
            searchForPlayer();
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
}
