//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SandBox.Staging.PlayerEnemy
{
    public class EnemyController : MonoBehaviour
    {
        //This component is attached to a mobile character 
        //in the game to allow it to navigate the Scene using the NavMesh
        public NavMeshAgent agent; 
        public Transform player;
        public GameObject playerObject;
        private PlayerController_V2 playerController;
        public LayerMask whatIsGround, whatIsPlayer;

        //health
        [SerializeField] int health;

        //animation
        private Rigidbody rb;
        private Animator anim;

        //patrol
        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;

        //chase
        Vector3 chasePoint;

        //attacking
        [SerializeField] int attackDamage;
        public float timeBetweenAttacks;
        bool alreadyAttacked = false;
        float timeCheck = 0;


        //states
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;

        private void Awake()
        {
            player = GameObject.Find("Player").transform;
            
            //playerController = player.GetComponent<PlayerController_V2>();
            playerController = playerObject.GetComponent<PlayerController_V2>();

            agent = GetComponent<NavMeshAgent>();
            rb = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (gameObject != null)
            {
                //check for sight and attack range
                playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
                playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

                //behaviour
                if (!playerInSightRange && !playerInAttackRange) Patrolling();
                else if (playerInSightRange && !playerInAttackRange) ChasePlayer();
                else if (playerInSightRange && playerInAttackRange) AttackPlayer();
            }

            if (health <= 0) Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.name == "PREFAB_Projectile(Clone)")
            {
                
                //push back enemy when hit by bullet
                GameObject bullet = collision.gameObject;
                Vector3 collisionDir = (bullet.transform.position - transform.position).normalized;
                transform.position -= collisionDir * 10;
                
                Debug.Log("bullet hit enemy");

                //reduce enemy health when hit by bullet
                health -= 10;
            }
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

                playerController.AttackedByEnemy(attackDamage);
                Debug.Log("player attacked");
            }
            else
            {
                timeCheck += Time.deltaTime;

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
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);

            if (!playerInSightRange && !playerInAttackRange)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, walkPoint);
            }
            else if (playerInSightRange && !playerInAttackRange)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, chasePoint);
            }
        }
    }
}
