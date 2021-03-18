using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowMe : MonoBehaviour
{
    public Transform target;
    private Vector3 dest;
    private NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        dest = _agent.destination;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(dest, target.position) > 1.0f)
        {
            dest = target.position;
            _agent.destination = dest;
        }
    }
}
