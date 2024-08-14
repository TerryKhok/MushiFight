using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJawRig : MonoBehaviour
{
    [SerializeField] Vector3 _massCenter = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] Transform _parent;
    [SerializeField] float _leftRotateZMax =  -30.0f;
    [SerializeField] float _rightRotateZMax = 30.0f;

    float _rotateMin = 0.0f;
    float _rotateMax = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = _massCenter;

        _rotateMin = transform.eulerAngles.z + _leftRotateZMax;
        _rotateMax = transform.eulerAngles.z + _rightRotateZMax;
    }

    private void Update()
    {
        //transform.position = _parent.position;

        //Quaternion rotation = transform.rotation;
        //rotation.x = _parent.rotation.x;

        //transform.rotation = rotation;

        //transform.eulerAngles = new Vector3(
        //    _parent.eulerAngles.x,
        //    _parent.eulerAngles.y,
        //    Mathf.Clamp(transform.eulerAngles.z, _rotateMin, _rotateMax)
        //    );
    }
}
