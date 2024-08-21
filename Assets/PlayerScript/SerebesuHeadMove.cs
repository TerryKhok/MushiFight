using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SerebesuHeadMove : MonoBehaviour,IHeadMove
{
    [Header("Transform")]
    [SerializeField] Transform _headTarget;
    [SerializeField] Transform _headPoint;
    [SerializeField] Transform _hipTarget;

    [Header("Value")]
    [SerializeField] float _pointDistance = 5.0f;
    [SerializeField] float _headMoveSpeed = 0.5f;
    [SerializeField] float _maxAngle = 60.0f;
    [SerializeField] float _minAngle = -30.0f;
    [ReadOnly,SerializeField] float headAngle = 0.0f;

    float _headTargetDistance = 0.0f;
    float _ratio = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        float cos = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180.0f);
        _pointDistance = (_pointDistance * cos);
        headAngle = -90.0f + 90.0f * cos;

        _minAngle -= transform.eulerAngles.y;
        _maxAngle -= transform.eulerAngles.y;

        _headTargetDistance = _headTarget.position.z - _hipTarget.position.z;
        _ratio = _headTargetDistance / _pointDistance;
    }


    public void Move(Vector2 _direction)
    {
        headAngle = Mathf.Clamp(headAngle + _direction.y * _headMoveSpeed * Time.deltaTime, _minAngle, _maxAngle);

        //if (_direction.y != 0.0f)
        {
            float rad = headAngle * Mathf.PI / 180.0f;
            _headPoint.localPosition = new Vector3(0.0f, Mathf.Sin(rad) * _pointDistance, Mathf.Cos(rad) * _pointDistance);

            _headTarget.position = Vector3.Lerp(_hipTarget.position, _headPoint.position, _ratio);
        }
    }

    public float GetHeadMaxAngle()
    {
        return _maxAngle;
    }

    public ref float GetHeadAngle()
    {
        return ref headAngle;
    }
}
