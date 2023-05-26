using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private NavMeshAgent agent;
    [SerializeField] public PlayerGame playerGame;
    public int dmg = 10;
    [SerializeField] float distanceAtack = 10f;


    private enum States { PATROL, CHASE }
    private States currenState;

    [SerializeField] float range;
    [SerializeField] Transform centrePoint;
    Animator animator;
    void Start()
    {
        currenState = States.PATROL;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        switch (currenState)
        {
            case States.PATROL:
                PatrolState();
                ChaseTransition(player);
                break;
            case States.CHASE:
                ChaseState();
                PatrolTransition(player);
                break;
        }
    }
    void PatrolState()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);
                animator.SetFloat("Speed", 1);
            }
        }
    }
    private void PatrolTransition(GameObject player)
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= distanceAtack)
        {
            agent.SetDestination(transform.position);
            currenState = States.PATROL;
        }
    }
    void ChaseTransition(GameObject player)
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, player.transform.position, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Player") && Vector3.Distance(player.transform.position, transform.position) <= 5f)
            {
                Vector3 vectorPlayerSelf = player.transform.position - transform.position;
                vectorPlayerSelf.Normalize();
                if (Vector3.Angle(transform.forward, vectorPlayerSelf) <= 45f)
                {
                    currenState = States.CHASE;
                }
            }
        }
    }
    void ChaseState()
    {
        agent.SetDestination(player.transform.position);

        if (player.transform.position.x <= this.gameObject.transform.position.x && player.transform.position.y <= this.gameObject.transform.position.y && player.transform.position.z <= this.gameObject.transform.position.z)
        {
            animator.SetFloat("Speed", 0);
        }
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerGame.Damage(dmg);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Damage");
            playerGame.Damage(dmg);
        }
    }
}