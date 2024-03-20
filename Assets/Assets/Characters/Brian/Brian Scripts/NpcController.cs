using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public GameObject PATH;
   [SerializeField] private Transform[] PathPoints;

    public float minDistance = 10f;
    public int index = 0;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();   

        PathPoints = new Transform[PATH.transform.childCount];

        for(int i=0; i<PathPoints.Length; i++)
        {
            PathPoints[i] = PATH.transform.GetChild(i);
        }
    }

    void Update()
    {
        roam();
        
    }
    void roam()
    { 
        if(Vector3.Distance(transform.position, PathPoints[index].position) < minDistance)
            {
            if(index +1 != PathPoints.Length)
                {
                index += 1;
            }
            else { index = 0; }
            }
        agent.SetDestination(PathPoints[index].position);
        animator.SetFloat("vertical", !agent.isStopped ? 1 : 0);
    }
}
