using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableRigid : MonoBehaviour
{
    //burada contact pointler toplanacak

    public HittableRigidHandler handler; //link to handler
    
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = new ContactPoint[collision.contactCount];
        collision.GetContacts(contactPoints);
        foreach (ContactPoint contactPoint in contactPoints)
        {
            handler.CollectCollisionPoint(contactPoint.point); //pointleri topluyoruz.
        }

        gameObject.SetActive(false);
    }
}
