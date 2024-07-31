using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarriorController : MonoBehaviour
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

        currentHealth = maxHealth;
        StartCoroutine(PlayRandomAnimation());
    }

    void Update()
    {
        if (isWalking && !isPlayerDetected)
        {
            MoveAndCheckBoundaries();
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

    private void MoveAndCheckBoundaries()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Vector3 position = transform.position;

        if (position.x < boundaryMin.x || position.x > boundaryMax.x || position.z < boundaryMin.z || position.z > boundaryMax.z)
        {
            Vector3 directionToCenter = (new Vector3((boundaryMin.x + boundaryMax.x) / 2, position.y, (boundaryMin.z + boundaryMax.z) / 2) - position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToCenter);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    private void FollowPlayer()
    {
        if (playerTransform != null)
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
        Debug.Log("�LD�");

        audioManager.PlaySFX(audioManager.deathScream);
        //enemyCounter.counter++;

        //Debug.Log(enemyCounter.counter);

                

        StartCoroutine(DestroyingObjects());
        
    }

    IEnumerator DestroyingObjects()
    {
        animator.SetBool("dead", true);

        yield return new WaitForSeconds(3.9f);

        Destroy(gameObject);

    }

}