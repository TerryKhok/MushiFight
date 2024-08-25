using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroPower : MonoBehaviour
{
    [SerializeField] LayerMask _targetLayer;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == _targetLayer)
        {
            if(other.transform.root.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.velocity = new Vector3(0.0f, rb.velocity.y, 0.0f);
            }
        }
    }
}
