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

        //chase
        Vector3 chasePoint;


        //attacking
        public float timeBetweenAttacks;
        bool alreadyAttacked = false;
        Vector3 startingAttackPos;
        float timeCheck = 0;

        //states
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;

        private void Awake()
        {
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            rb = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();

            startingAttackPos = new Vector3(0, 0, 0);
        }

        private void Update()
        {
            //check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            else if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            else if (playerInSightRange && playerInAttackRange) AttackPlayer();
            //else if (!playerInSightRange && !playerInAttackRange)
            //{
            //    anim.SetBool("isWalking", false);
            //    anim.SetBool("isChasing", false);
            //}
        }

        private void Patrolling()
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isChasing", false);
            anim.SetBool("isAttacking", false);

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
            //rb.velocity = Vector3.zero;

            //reset walkpoint
            walkPoint = Vector3.zero;

            anim.SetBool("isChasing", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);

            chasePoint = player.position;
            agent.SetDestination(chasePoint);
        }

        private void AttackPlayer()
        {
            //no longer chasing
            anim.SetBool("isChasing", false);

            agent.SetDestination(transform.position);
            transform.LookAt(player);

            if (!alreadyAttacked)
            {
                anim.SetBool("isAttacking", true);
                alreadyAttacked = true;


                //anim.SetBool("isWaitingForAttack", true);
                //anim.SetBool("isAttacking", false);

                //Invoke(nameof(ResetAttack), timeBetweenAttacks); //resets attack in X secs
            }
            else
            {
                timeCheck += Time.deltaTime;

                //if (timeCheck >= 0.2f) anim.SetBool("isAttacking", false);
                if (timeCheck > 1.0f && timeCheck < 2.0f)
                {
                    anim.SetBool("isWaitingForAttack", true);
                    anim.SetBool("isAttacking", false);
                }
                else if (timeCheck >= 2.0f) ResetAttack();
            }
        }
        private void ResetAttack()
        {
            alreadyAttacked = false;
            timeCheck = 0;
            anim.SetBool("isWaitingForAttack", false);
            //anim.SetBool("isAttacking", true);
        }

        //TO DELETE
        private void AttackPlayerV3()
        {
            //no longer chasing
            anim.SetBool("isChasing", false);
            anim.SetBool("isAttacking", true);

            Vector3 vectorCheck = new Vector3(0, 0, 0);

            //only get starting attack pos once
            if (startingAttackPos == vectorCheck) startingAttackPos = transform.position;

            //stop enemy moving before attacking
            agent.SetDestination(startingAttackPos);
            transform.LookAt(player);

            if (!alreadyAttacked)
            {
                rb.AddForce(transform.forward * 10f, ForceMode.Impulse);

                alreadyAttacked = true;

                //agent.SetDestination(startingAttackPos); //move to reset attack
                Invoke(nameof(ResetAttack), timeBetweenAttacks); //resets attack in X secs
            }
        }
        //TO DELETE
        private void AttackPlayerV2()
        {
            //no longer chasing
            anim.SetBool("isChasing", false);


            Vector3 vectorCheck = new Vector3(0, 0, 0);

            //only get starting attack pos once
            if (startingAttackPos == vectorCheck) startingAttackPos = transform.position;

            //stop enemy moving before attacking
            agent.SetDestination(startingAttackPos);
            transform.LookAt(player);

            if(!alreadyAttacked)
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWaitingForAttack", false);

                alreadyAttacked = true;

                anim.SetBool("isWaitingForAttack", true);

                agent.SetDestination(startingAttackPos); //move to reset attack
                Invoke(nameof(ResetAttack), timeBetweenAttacks); //resets attack in X secs
            }
           
            //if (Vector3.Distance(transform.position, player.position) < 1.5f)
            //{
            //    alreadyAttacked = true;

            //    anim.SetBool("isWaitingForAttack", true);

            //    agent.SetDestination(startingAttackPos); //move to reset attack
            //    Invoke(nameof(ResetAttack), timeBetweenAttacks); //resets attack in X secs
            //}


            //if (Vector3.Distance(transform.position, player.position) < 1.5f)
            //{
            //    alreadyAttacked = true;

            //    anim.SetBool("isWaitingForAttack", true);

            //    agent.SetDestination(startingAttackPos); //move to reset attack
            //    Invoke(nameof(ResetAttack), timeBetweenAttacks); //resets attack in X secs
            //}


        }
        //TO DELETE
        private void AttackPlayerV1()
        {
            Vector3 vectorCheck = new Vector3(0, 0, 0);
            startingAttackPos = new Vector3( 0, 0, 0 );

            //only get starting attack pos once
            if(startingAttackPos == vectorCheck) startingAttackPos = transform.position;
            
            //stop enemy moving before attacking
            //agent.SetDestination(transform.position);
            agent.SetDestination(startingAttackPos);
            transform.LookAt(player);

            if (!alreadyAttacked)
            {
                //attack code here
                anim.SetBool("isAttacking", true);
                anim.SetBool("isChasing", false);

                //float distDebug = 0;
                float distDebug = Vector3.Distance(transform.position, player.position); 

                if (Vector3.Distance(transform.position, player.position) < 2f)
                {
                    alreadyAttacked = true;

                    anim.SetBool("isWaitingForAttack", true);

                    agent.SetDestination(startingAttackPos); //move to reset attack
                    Invoke(nameof(ResetAttack), timeBetweenAttacks); //resets attack in X secs
                }
            }
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
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, chasePoint);
        }
    }
}
