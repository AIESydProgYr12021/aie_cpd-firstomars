//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//namespace SandBox.Staging.EnemyWaves
public class EnemyController : MonoBehaviour
{
    //[SerializeField] GameObject gameManagerObj;

    //This component is attached to a mobile character 
    //in the game to allow it to navigate the Scene using the NavMesh
    [Header("NavMesh")]
    public NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;
    private Transform playerTransform;

    //take damage
    [Header("Health")]
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] GameObject healthBarUI;
    [SerializeField] Slider healthSlider;
    [SerializeField] float maxVelocityWhenShot;
    private ThirdPersonScript playerController; // to access ammo

    //animation
    [Header("Animation")]
    private Rigidbody rb;
    private Animator anim;

    //patrol
    [Header("Walk")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    [SerializeField] private float walkSpeed;

    //chase
    [Header("Chase")]
    [SerializeField] Transform rayStart;
    private Transform rayEnd;
    Vector3 chasePoint;
    [SerializeField] private float chaseSpeed;

    //attacking
    [Header("Attack")]
    public int attackDamage;
    public float timeBetweenAttacks;
    bool alreadyAttacked = false;
    float timeCheck = 0;
    public float attackRange;
    public bool playerInAttackRange;

    private void Awake()
    {
        GameObject player = PlayerManager.instance.player;

        if (player != null)
        {
            playerController = player.GetComponent<ThirdPersonScript>();
            playerTransform = player.GetComponent<Transform>();
            rayEnd = player.transform.Find("RayEnd").GetComponent<Transform>();
        }

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
        if (gameObject != null)
        {
            //check for attack range
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (CanSeePlayer() && !playerInAttackRange) ChasePlayer();
            else if (CanSeePlayer() && playerInAttackRange) AttackPlayer();
            else Patrolling();
        }

        CheckHealth();
    }
    private bool CanSeePlayer()
    {
        if (rayEnd != null)
        {
            Vector3 dir = (rayEnd.position - rayStart.position).normalized;
            var hit = Physics.Raycast(rayStart.position, dir, out RaycastHit hitInfo, 50);

            if (hit)
            {
                return hitInfo.collider.gameObject.CompareTag("Player");
            }
        }

        return false;
    }

    private void CheckHealth()
    {
        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);
            healthBarUI.transform.LookAt(playerTransform);
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        healthSlider.value = CalculateHealth();

        if (health <= 0)
        {
            Vector3 spawnAmmoPos = new Vector3(transform.position.x, 1.2f, transform.position.z);
            playerController.SpawnAmmo(spawnAmmoPos);
            Destroy(gameObject);
        }
    }

    private float CalculateHealth()
    {
        return health / maxHealth;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PREFAB_Projectile(Clone)")
        {
            //reduce enemy health when hit by bullet
            health -= 10;

            Invoke("SetVelocityToZero", 0.5f);
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

        agent.speed = walkSpeed;

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

        chasePoint = playerTransform.position;
        agent.speed = chaseSpeed;
        agent.SetDestination(chasePoint);
    }

    private void AttackPlayer()
    {
        //no longer chasing
        anim.SetBool("isChasing", false);

        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform);

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

    private void ResetAttack()
    {
        alreadyAttacked = false;
        timeCheck = 0;
        anim.SetBool("isWaitingForAttack", false);
    }

    private void OnDrawGizmosSelected()
    {
        //attack radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        //is player in sight
        if  (rayEnd != null)
        {
            if (CanSeePlayer()) Gizmos.color = Color.red;
            else Gizmos.color = Color.black;
            Gizmos.DrawLine(rayStart.position, rayEnd.position);
        }
        
        //walkpoint
        if (!CanSeePlayer() && !playerInAttackRange)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, walkPoint);
        }
    }
}