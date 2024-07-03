using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    Animator anim;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtWeight(1f,.5f,1f,.5f);
        Ray lookAtRay = new Ray(transform.position, mainCamera.transform.forward);
        anim.SetLookAtPosition(lookAtRay.GetPoint(25));
    }
}
