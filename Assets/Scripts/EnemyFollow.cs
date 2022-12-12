using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private enum States { PATROL, CHASE }
    private States currenState;
    [SerializeField]
    private GameObject player;
    private NavMeshAgent agent;
    private int destPoint = 0;
    private void Awake()
    {
        currenState = States.PATROL;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
    }
    private void Update()
    {
        switch (currenState)
        {

            case States.PATROL:
                PatrolState();
                break;
            case States.CHASE:
                ChaseState();
                break;
        }
    }
    private void PatrolState()
    {
        Rotate(45f);
        ChaseTransition(player);
    }
    private void Rotate(float rotation)
    {
        transform.Rotate(Vector3.up, rotation * Time.deltaTime);
    }
    private void ChaseTransition(GameObject player)
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, player.transform.position, out hit)) { }
        if (hit.collider.gameObject.CompareTag("Player") &&
        Vector3.Distance(player.transform.position, transform.position) <= 5f) { }
        Vector3 vectorPlayerSelf = player.transform.position - transform.position;
        vectorPlayerSelf.Normalize();
        if (Vector3.Angle(transform.forward, vectorPlayerSelf) <= 45f) { }
        currenState = States.CHASE;
    }
    private void ChaseState()
    {
        Walk(player);
        PatrolTransition(player);
    }
    private void Walk(GameObject destination)
    {
        agent.SetDestination(destination.transform.position);
    }
    private void PatrolTransition(GameObject player)
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= 10f)
        {
            agent.SetDestination(transform.position);
            currenState = States.PATROL;
        }
    }
}