using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreatureController : MonoBehaviour
{
    public Animator animator; 
    public float speed = 3.0f;
    //public float turnSpeed = 200.0f; 
    public Vector3 boundaryMin; 
    public Vector3 boundaryMax; 

    private bool isWalking = false;
    private Vector3 targetDirection;

    private readonly string[] animationParameters = { "walk", "idle2", "sniff", "roar", "crouch" };

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        targetDirection = transform.forward;

        SetRandomStartingPosition();
        StartCoroutine(PlayRandomAnimation());
    }

    void Update()
    {
        isWalking = animator.GetCurrentAnimatorStateInfo(0).IsName("Creep|Walk1_Action") || 
                    animator.GetCurrentAnimatorStateInfo(0).IsName("Creep|Crouch_Action");

        if (isWalking)
        {
            MoveCreature();
        }
    }
    
    void SetRandomStartingPosition()
    {
        float randomX = Random.Range(boundaryMin.x, boundaryMax.x);
        float randomZ = Random.Range(boundaryMin.z, boundaryMax.z);
        transform.position = new Vector3(randomX, transform.position.y, randomZ);
    }

    void MoveCreature()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        

        Vector3 position = transform.position;
        if (position.x < boundaryMin.x || position.x > boundaryMax.x || position.z < boundaryMin.z || position.z > boundaryMax.z)
        {
            Vector3 directionToCenter = (new Vector3((boundaryMin.x + boundaryMax.x) / 2, position.y, (boundaryMin.z + boundaryMax.z) / 2) - position).normalized;
            targetDirection = directionToCenter;
        }
    }

    IEnumerator PlayRandomAnimation()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, animationParameters.Length);
            string chosenAnimation = animationParameters[randomIndex];

            foreach (string parameter in animationParameters)
            {
                animator.SetBool(parameter, false);
            }

            animator.SetBool(chosenAnimation, true);
            Debug.Log($"Playing animation: {chosenAnimation}");

            float waitTime = Random.Range(2.0f, 5.0f);
            yield return new WaitForSeconds(waitTime);

            if (chosenAnimation == "Creep|Walk1_Action" || chosenAnimation == "Creep|Crouch_Action")
            {
                targetDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name);  
        if (other.CompareTag("Player"))
        {
            animator.SetBool("attack", true);
        }
    }
}


