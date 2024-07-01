using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollController : MonoBehaviour
{
    public Animator animator;
    public string isWalkingParameter = "walk"; 
    public float minWaitTime = 2.0f; 
    public float maxWaitTime = 5.0f; 
    public float speed = 3.0f; 

    private bool isWalking = false;

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
        if (isWalking)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    IEnumerator PlayRandomAnimation()
    {
        while (true)
        {
            isWalking = Random.value > 0.5f;
            animator.SetBool(isWalkingParameter, isWalking);

            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}