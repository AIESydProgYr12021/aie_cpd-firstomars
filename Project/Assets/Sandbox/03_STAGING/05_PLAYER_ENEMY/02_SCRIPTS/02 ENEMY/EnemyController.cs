//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace SandBox.Staging.PlayerEnemy
{
    public class EnemyController : MonoBehaviour
    {
        //This component is attached to a mobile character 
        //in the game to allow it to navigate the Scene using the NavMesh
        public NavMeshAgent agent; 
        public Transform player;
        public LayerMask whatIsGround, whatIsPlayer;

        //take damage
        [SerializeField] float maxHealth;
        [SerializeField] float health;
        [SerializeField] GameObject healthBarUI;
        [SerializeField] Slider healthSlider;
        [SerializeField] float maxVelocityWhenShot;

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
        public int attackDamage;
        public float timeBetweenAttacks;
        bool alreadyAttacked = false;
        float timeCheck = 0;
        

        //states
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;
        Vector3 sightPoint;

        private void Awake()
        {
            player = GameObject.Find("Player").transform; // necessary
            agent = GetComponent<NavMeshAgent>();
            rb = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();
            
            //health
            health = maxHealth;
            healthSlider.value = CalculateHealth();
            healthBarUI.SetActive(false);
        }


        private void Update()
        {

            //sightPoint = new Vector3(transform.position.x, transform.position.y, transform.forward.z + sightRange);

            sightPoint = transform.forward * sightRange;

            if (gameObject != null)
            {
                //check for sight and attack range
                playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
                playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

                //bool canSeePlayer = Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, sightRange);
                //if (canSeePlayer) canSeePlayer = hitInfo.collider.gameObject.CompareTag("Player");

                //behaviour
                if (!playerInSightRange && !playerInAttackRange) Patrolling();
                else if (playerInSightRange && !playerInAttackRange) ChasePlayer(); //&& canSeePlayer
                else if (playerInSightRange && playerInAttackRange) AttackPlayer();
            }

            CheckHealth();
        }

        private void CheckHealth()
        {
            if (health < maxHealth)
            {
                healthBarUI.SetActive(true);
                healthBarUI.transform.LookAt(player);
            }

            if (health > maxHealth)
            {
                health = maxHealth;
            }

            healthSlider.value = CalculateHealth();

            if (health <= 0) Destroy(gameObject);
        }

        private float CalculateHealth()
        {
            return health / maxHealth;
        }


        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.name == "PREFAB_Projectile(Clone)")
            {
                //if(rb.velocity.magnitude > maxVelocityWhenShot)
                //{
                //    rb.velocity = rb.velocity.normalized * maxVelocityWhenShot;
                //}
                
                
                //push back enemy when hit by bullet
                //GameObject bullet = collision.gameObject;
                //Vector3 collisionDir = (bullet.transform.position - transform.position).normalized;
                //transform.position -= collisionDir * 10;
                
                Debug.Log("bullet hit enemy");

                //reduce enemy health when hit by bullet
                health -= 10;

                Invoke("SetVelocityToZero", 1);
            }
        }

        private void SetVelocityToZero()
        {
            rb.velocity = Vector3.zero;
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

        //public void DamagePlayer()
        //{
        //    playerController.AttackedByEnemy(attackDamage);
        //    Debug.Log("player attacked");
        //}

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

            //bool canSeePlayer = Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, sightRange);

            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, sightPoint);

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
