using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreatureController : MonoBehaviour
{
    public Animator animator; 
    public float speed = 3.0f;
    public float turnSpeed = 200.0f; 
    public Vector3 boundaryMin; 
    public Vector3 boundaryMax; 

    private bool isWalking = false;
    private Vector3 targetDirection;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        targetDirection = transform.forward;
        
        SetRandomStartingPosition();
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

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

        Vector3 position = transform.position;
        if (position.x < boundaryMin.x || position.x > boundaryMax.x || position.z < boundaryMin.z || position.z > boundaryMax.z)
        {
            Vector3 directionToCenter = (new Vector3((boundaryMin.x + boundaryMax.x) / 2, position.y, (boundaryMin.z + boundaryMax.z) / 2) - position).normalized;
            targetDirection = directionToCenter;
        }
    }

    public void OnWalkAnimationFinished()
    {
        Debug.Log("Walk animation finished, changing direction.");
        // Rotate randomly when walk animation finishes
        targetDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.Play("Creep|Punch_Action ");
            animator.SetBool("isDetected", true);
        }
    }
}
