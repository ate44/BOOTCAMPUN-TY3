using System.Collections;
using UnityEngine;

public class FireElementalController : MonoBehaviour
{
    public Animator animator;
    public string isWalkingParameter = "walk";
    public float minWaitTime = 2.0f;
    public float maxWaitTime = 5.0f;
    public float speed = 3.0f;
    public string attackParameter = "attack";
    public Vector3 boundaryMin;
    public Vector3 boundaryMax;

    private bool isWalking = false;
    private bool isPlayerDetected = false;
    private Transform playerTransform;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

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
        Debug.Log("Trigger Enter: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected: " + other.name);
            isPlayerDetected = true;
            playerTransform = other.transform;
            StartCoroutine(AttackPlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player lost: " + other.name);
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
}
