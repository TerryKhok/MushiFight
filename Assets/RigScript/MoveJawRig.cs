using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJawRig : MonoBehaviour
{
    [SerializeField] Vector3 _massCenter = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] Transform _parent;
    [SerializeField] PlayerController _playerController;
    [SerializeField] float _leftRotateZMax =  -30.0f;
    [SerializeField] float _rightRotateZMax = 30.0f;

    float _rotateMin = 0.0f;
    float _rotateMax = 0.0f;
    Vector3 _startAngleX;

    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = _massCenter;

        _rotateMin = transform.eulerAngles.z + _leftRotateZMax;
        _rotateMax = transform.eulerAngles.z + _rightRotateZMax;

        _startAngleX = transform.localEulerAngles;
    }

    private void Update()
    {
        //transform.eulerAngles = new Vector3(90.0f, 0.0f, transform.eulerAngles.z);

        Debug.DrawRay(transform.position, transform.up,Color.green);

        //transform.position = _parent.position;

        //Vector3 rotation = _startAngleX;
        //rotation.z = transform.localEulerAngles.z;
        //transform.localEulerAngles = rotation;

        //Vector3 rotation = transform.localEulerAngles;
        //rotation.x = _startAngleX - _playerController.headAngle;
        //transform.localEulerAngles = rotation;

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
