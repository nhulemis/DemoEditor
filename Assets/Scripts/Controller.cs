using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Controller : MonoBehaviour
{
    [SerializeField]
    Transform m_target;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(m_target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
