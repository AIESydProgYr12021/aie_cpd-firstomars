//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SandBox.Staging.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        //This component is attached to a mobile character 
        //in the game to allow it to navigate the Scene using the NavMesh
        public NavMeshAgent agent; 

        public Transform player;

        public LayerMask whatIsGround, whatIsPlayer;

        public float attackBump = 10f;

        private Rigidbody rb;
        private Animator anim;

        //patrol
        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;

        //attacking
        public float timeBetweenAttacks;
        bool alreadyAttacked;

        //states
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;

        private void Awake()
        {
            player = GameObject.Find("PlayerTemp").transform;
            agent = GetComponent<NavMeshAgent>();
            rb = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            //check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            else if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            else if (playerInAttackRange) AttackPlayer();
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isChasing", false);
            }
        }

        private void Patrolling()
        {
            anim.SetBool("isWalking", true);
            
            if (!walkPointSet)  
                SearchWalkPoint();

            if (walkPointSet)   
                agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //walkpoint reached?
            if (distanceToWalkPoint.magnitude < 1f) 
                walkPointSet = false;
        }

        private void SearchWalkPoint()
        {
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            {
                walkPointSet = true;
            }
        }

        private void ChasePlayer()
        {
            anim.SetBool("isChasing", true);
            anim.SetBool("isWalking", false);

            agent.SetDestination(player.position);
        }

        private void AttackPlayer()
        {
            anim.SetBool("isChasing", false);

            //stop enemy moving before attacking
            agent.SetDestination(transform.position);

            Vector3 startingAttackPos = transform.position;

            transform.LookAt(player);
        
            if (!alreadyAttacked)
            {
                //attack code here

                //rb.AddForce(transform.forward * attackBump);
                //transform.Translate(Vector3.forward * 10f * Time.deltaTime);

                if (Vector3.Distance(transform.position, player.position) < 0.5f)
                {
                    alreadyAttacked = true;

                    agent.SetDestination(startingAttackPos); //move to reset attack
                    Invoke(nameof(ResetAttack), timeBetweenAttacks); //resets attack in X secs
                }
            }
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

        //health / damage
        //destroy gameobject if health < 0

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, walkPoint);
        }
    }
}
