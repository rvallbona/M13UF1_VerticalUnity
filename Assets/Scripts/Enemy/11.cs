//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//public class EnemyBoss : MonoBehaviour
//{
//    private enum States { PATROL, CHASE }
//    private States currenState;
//    [SerializeField]
//    private GameObject player;
//    private NavMeshAgent agent;
//    [SerializeField] public PlayerGame playerGame;
//    public int dmg = 20;
//    [SerializeField] float range;
//    [SerializeField] Transform centrePoint;
//    private void Awake()
//    {
//        currenState = States.PATROL;
//        agent = GetComponent<NavMeshAgent>();
//    }
//    private void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        agent.autoBraking = false;
//    }
//    private void Update()
//    {
//        switch (currenState)
//        {

//            case States.PATROL:
//                PatrolState();
//                break;
//            case States.CHASE:
//                ChaseState();
//                break;
//        }
//    }
//    private void PatrolState()
//    {
//        Patrol();
//    }
//    private void ChaseState()
//    {
//        Debug.Log("Persiguiendo");
//        Walk(player);
//        PatrolTransition(player);
//    }
//    private void Walk(GameObject destination)
//    {
//        agent.SetDestination(destination.transform.position);
//    }
//    private void PatrolTransition(GameObject player)
//    {
//        if (Vector3.Distance(transform.position, player.transform.position) >= 10f)
//        {
//            agent.SetDestination(transform.position);
//            currenState = States.PATROL;
//        }
//    }
//    void Patrol()
//    {
//        Debug.Log("Patrullando");
//        if (agent.remainingDistance <= agent.stoppingDistance)
//        {
//            Vector3 point;
//            if (RandomPoint(centrePoint.position, range, out point))
//            {
//                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
//                agent.SetDestination(point);
//            }
//        }
//    }
//    bool RandomPoint(Vector3 center, float range, out Vector3 result)
//    {

//        Vector3 randomPoint = center + Random.insideUnitSphere * range;
//        NavMeshHit hit;
//        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
//        {
//            result = hit.position;
//            return true;
//        }

//        result = Vector3.zero;
//        return false;
//    }
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.tag == "Player")
//        {
//            Debug.Log("Damage");
//            playerGame.Damage(dmg);
//        }
//    }
//    private void OnTriggerStay(Collider other)
//    {
//        if (other.gameObject.tag == "Player")
//        {
//            Debug.Log("Damage");
//            playerGame.Damage(dmg);
//        }
//    }
//}