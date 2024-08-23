using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [Header("HitParticle")]
    [SerializeField] GameObject _StrongHitparticleObject;
    [SerializeField] GameObject _WeakHitparticleObject;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    //接触時のエフェクト
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 hitspeed = rb.velocity;
            foreach (ContactPoint contactPoint in other.contacts)
            {
                if (hitspeed.z > 1.0f)
                {   
                    GameObject effect = (GameObject)Instantiate(_StrongHitparticleObject, (Vector3)contactPoint.point, Quaternion.identity);
                    Debug.Log("Hello, World!");
                    print((Vector3)contactPoint.point);
                    
                    Destroy(effect, 1.5f);
                }
                else
                {
                    GameObject effect = (GameObject)Instantiate(_WeakHitparticleObject, (Vector3)contactPoint.point, Quaternion.identity);

                    print((Vector3)contactPoint.point);

                    Destroy(effect, 1.5f);
                }
            }
        }
    }
}
