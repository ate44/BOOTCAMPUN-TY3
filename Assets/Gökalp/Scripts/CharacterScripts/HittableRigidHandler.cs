using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableRigidHandler : MonoBehaviour
{
    private List<Vector3> collisionPoints = new List<Vector3>();

    public HittableRigid hrModel;
    public ParticleSystem bloodEffect; // Particle system reference

    private HittableRigid[] hittableRigidsPool;

    private void Start()
    {
        hrModel.handler = this;
    }

    public void CollectCollisionPoint(Vector3 cPoint)
    {
        collisionPoints.Add(cPoint);
    }

    public void ClearCollisionList()
    {
        collisionPoints.Clear();
    }

    public void InitializePool(int poolSize)
    {
        hittableRigidsPool = new HittableRigid[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            hittableRigidsPool[i] = Instantiate(hrModel);
            hittableRigidsPool[i].handler = this;
            hittableRigidsPool[i].gameObject.SetActive(false); //ihtiya� oldu�unda aktive olacak
        }
    }

    public void ActivateHittableRigid(Vector3 position, Quaternion rotation)
    {
        foreach (HittableRigid hittableRigid in hittableRigidsPool)
        {
            if (!hittableRigid.isActiveAndEnabled)
            {
                hittableRigid.gameObject.SetActive(true);
                hittableRigid.transform.position = position;
                hittableRigid.transform.rotation = rotation;

                // Activate the blood effect at the collision point
                bloodEffect.transform.position = position;
                bloodEffect.transform.rotation = rotation;
                bloodEffect.Play();
                
                FireElementalController enemy = GetComponent<FireElementalController>();
                
                if (enemy != null)
                {
                    Debug.Log("Enemy detected");
                    enemy.TakeDamage();
                }
                
                hittableRigid.gameObject.SetActive(false);

                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Vector3 point in collisionPoints)
        {
            Gizmos.DrawWireSphere(point, .05f);
        }
    }
}
