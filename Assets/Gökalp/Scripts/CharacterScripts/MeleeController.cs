using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MeleeController s�n�f�, yak�n d�v�� sald�r�lar�n� kontrol eder
public class MeleeController : MonoBehaviour
{
    // Animator bile�enine referans
    Animator anim;

    public WeaponHandler weaponHandler;
    private BoxCollider weaponCollider;

    public struct BufferObj
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 size;
    }

    private LinkedList<BufferObj> trailList = new LinkedList<BufferObj>();
    private LinkedList<BufferObj> trailFillerList = new LinkedList<BufferObj>();
    public bool debugTrail = false;
    private int maxFrameBuffer = 2;

    public LayerMask hitLayers;

    int attackId = 0;

    private HittableRigidHandler hittableRigidHandler;

    void Start()
    {
        // Animator bile�enini al
        anim = GetComponentInChildren<Animator>();
        weaponCollider = (BoxCollider)weaponHandler.weapon.GetComponent<Collider>();
        hittableRigidHandler = GetComponent<HittableRigidHandler>();
        hittableRigidHandler.InitializePool(8);
    }

    void Update()
    {
        // Sol fare tu�una bas�ld���nda sald�r� tipi 1 olarak ayarla
        if (Input.GetMouseButtonDown(0)) // left click
        {
            SetAttack(1);
        }
        // Sa� fare tu�una bas�ld���nda sald�r� tipi 2 olarak ayarla
        else if (Input.GetMouseButtonDown(1)) // right click
        {
            SetAttack(2);
        }
    }

    private void LateUpdate()
    {
        if (anim.GetBool("IsDamageOn"))
        {
            CheckTrail();
        }
    }

    // Sald�r� ayarlar�n� belirleyen metod
    private void SetAttack(int attackType)
    {
        // Animator'daki "CanAttack" bool parametresi true ise sald�r� ba�lat
        if (anim.GetBool("CanAttack"))
        {
            attackId++;

            // "Attack" tetikleyicisini ayarla
            anim.SetTrigger("Attack");
            // Sald�r� tipini belirle
            anim.SetInteger("AttackType", attackType);
            hittableRigidHandler.ClearCollisionList();
        }
    }

    private void CheckTrail()
    {
        BufferObj bo = new BufferObj();
        bo.size = weaponCollider.size;
        bo.rotation = weaponCollider.transform.rotation;
        bo.position = weaponCollider.transform.position + weaponCollider.transform.TransformDirection(weaponCollider.center);
        trailList.AddFirst(bo);

        if (trailList.Count > maxFrameBuffer)
        { //toplad���m�z frame say�s�n� s�n�rl�yoruz
            trailList.RemoveLast();
        }
        if (trailList.Count > 1)
        {
            trailFillerList = FillTrail(trailList.First.Value, trailList.Last.Value);
        }

        Collider[] hits = Physics.OverlapBox(bo.position, bo.size / 2, bo.rotation, hitLayers, QueryTriggerInteraction.Ignore);

        Dictionary<long, Collider> colliderList = new Dictionary<long, Collider>();
        CollectColliders(bo, hits, colliderList);

        foreach (BufferObj cbo in trailFillerList)
        {
            hits = Physics.OverlapBox(cbo.position, cbo.size / 2, cbo.rotation, hitLayers, QueryTriggerInteraction.Ignore);
            CollectColliders(cbo, hits, colliderList);
        }

        foreach (Collider collider in colliderList.Values)
        {
            HitData hd = new HitData();
            hd.id = attackId;
            Hittable hittable = collider.GetComponent<Hittable>();
            if (hittable)
            {
                hittable.Hit(hd);
            }
        }
    }

    private void CollectColliders(BufferObj source, Collider[] hits, Dictionary<long, Collider> colliderList)
    {
        if (hits.Length > 0)
        {
            hittableRigidHandler.ActivateHittableRigid(source.position, source.rotation);
        }

        for (int i = 0; i < hits.Length; i++)
        {
            if (!colliderList.ContainsKey(hits[i].GetInstanceID()))
            {
                colliderList.Add(hits[i].GetInstanceID(), hits[i]);
            }
        }
    }

    private LinkedList<BufferObj> FillTrail(BufferObj from, BufferObj to)
    {
        LinkedList<BufferObj> fillerList = new LinkedList<BufferObj>();

        float distance = Mathf.Abs((from.position - to.position).magnitude);

        if (distance > weaponCollider.size.z)
        {
            float steps = Mathf.Ceil(distance / weaponCollider.size.z);

            float stepsAmount = 1 / (steps + 1);
            float stepValue = 0;

            for (int i = 0; i < (int)steps; i++)
            {
                stepValue += stepsAmount;
                BufferObj tmpBo = new BufferObj();
                tmpBo.size = weaponCollider.size;
                tmpBo.position = Vector3.Lerp(from.position, to.position, stepValue);
                tmpBo.rotation = Quaternion.Lerp(from.rotation, to.rotation, stepValue);
                fillerList.AddFirst(tmpBo);
            }
        }

        return fillerList;
    }

    private void OnDrawGizmos()
    {
        if (debugTrail)
        {
            foreach (BufferObj obj in trailList)
            {
                Gizmos.color = Color.blue;
                Gizmos.matrix = Matrix4x4.TRS(obj.position, obj.rotation, Vector3.one);
                Gizmos.DrawWireCube(Vector3.zero, obj.size);
            }

            foreach (BufferObj obj in trailFillerList)
            {
                Gizmos.color = Color.yellow;
                Gizmos.matrix = Matrix4x4.TRS(obj.position, obj.rotation, Vector3.one);
                Gizmos.DrawWireCube(Vector3.zero, obj.size);
            }
        }
    }
}
