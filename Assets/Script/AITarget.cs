using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class AITarget : MonoBehaviour
{
    public Transform Target;

    private NavMeshAgent m_Agent;
    private float m_Distance;


    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        m_Distance = Vector3.Distance(m_Agent.transform.position, Target.position);
        if(m_Distance > 20) {
            m_Agent.isStopped = true;
        }
        else
        {
            m_Agent.destination = Target.position;
            m_Agent.isStopped = false;
        }
            
        

    }
}
