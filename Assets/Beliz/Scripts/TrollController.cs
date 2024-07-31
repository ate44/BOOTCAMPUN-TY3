using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class TrollController : MonoBehaviour
{
    public Animator animator;
    public string isWalkingParameter = "walk";
    public float minWaitTime = 2.0f;
    public float maxWaitTime = 5.0f;
    public float speed = 3.0f;
    public string attackParameter = "attack";
    public Vector3 boundaryMin;
    public Vector3 boundaryMax;
    public int maxHealth = 2;

    private bool isWalking = false;
    private bool isPlayerDetected = false;
    private Transform playerTransform;
    private int currentHealth;
    private NavMeshAgent navMeshAgent;

    private AudioManagerSc audioManager;

    

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManagerSc>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene!");
        }
    }

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;

        currentHealth = maxHealth;
        StartCoroutine(PlayRandomAnimation());

 
    }

    void Update()
    {
        if (isWalking && !isPlayerDetected)
        {
            Patrol();
        }
        else if (isPlayerDetected)
        {
            FollowPlayer();
        }
    }

    IEnumerator PlayRandomAnimation()
    {
        while (true)
        {
            if (!isPlayerDetected)
            {
                isWalking = Random.value > 0.5f;
                animator.SetBool(isWalkingParameter, isWalking);

                if (isWalking)
                {
                    SetRandomDestination();
                }

                float waitTime = Random.Range(minWaitTime, maxWaitTime);
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = true;
            playerTransform = other.transform;
            StartCoroutine(AttackPlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = false;
            animator.SetBool(attackParameter, false);
            animator.SetBool(isWalkingParameter, false);
            StopCoroutine(AttackPlayer());
        }
    }

    IEnumerator AttackPlayer()
    {
        while (isPlayerDetected)
        {
            animator.SetBool(attackParameter, true);

            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            animator.SetBool(attackParameter, false);
            yield return new WaitForEndOfFrame();
        }
    }

    private void Patrol()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            SetRandomDestination();
        }
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = new Vector3(
            Random.Range(boundaryMin.x, boundaryMax.x),
            transform.position.y,
            Random.Range(boundaryMin.z, boundaryMax.z)
        );

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 1.0f, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
    }

    private void FollowPlayer()
    {
        if (playerTransform != null && navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.SetDestination(playerTransform.position);
        }
    }

    public void TakeDamage()
    {
        audioManager.PlaySFX(audioManager.wounded);
        audioManager.PlaySFX(audioManager.swordHitting);
        currentHealth--;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Dead");
        audioManager.PlaySFX(audioManager.monsterScream);

        StartCoroutine(DestroyingObjects());

    }

    IEnumerator DestroyingObjects()
    {
        animator.SetBool("dead", true);
        navMeshAgent.isStopped = true;

        yield return new WaitForSeconds(2.0f);

        Destroy(gameObject);

        SceneManager.LoadScene(2);
    }
}

