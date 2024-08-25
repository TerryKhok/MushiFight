using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TwoTargetHeadMove : MonoBehaviour,IHeadMove
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
    [ReadOnly,SerializeField] float _headAngle = 0.0f;

    float _headTargetDistance = 0.0f;
    float _ratio = 0.0f;

    float _max = 0.0f;
    float _min = 0.0f;

    float _pDis = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        float cos = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180.0f);
        _pDis = _pointDistance * cos;

        _headAngle = -90.0f + 90.0f * cos;

        _min = _minAngle - transform.eulerAngles.y;
        _max = _maxAngle - transform.eulerAngles.y;

        _headTargetDistance = _headTarget.position.z - _hipTarget.position.z;
        _ratio = _headTargetDistance / _pDis;
    }


    public void Move(Vector2 _direction)
    {
        _headAngle = Mathf.Clamp(_headAngle + _direction.y * _headMoveSpeed * Time.deltaTime, _min, _max);

        //if (_direction.y != 0.0f)
        {
            float rad = _headAngle * Mathf.PI / 180.0f;
            _headPoint.localPosition = new Vector3(0.0f, Mathf.Sin(rad) * _pDis, Mathf.Cos(rad) * _pDis);

            _headTarget.position = Vector3.Lerp(_hipTarget.position, _headPoint.position, _ratio);
        }
    }

    public void Mirror()
    {
        float cos = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180.0f);
        _pDis = _pointDistance * cos;

        //headAngle = -90.0f + 90.0f * cos;

        _headAngle += 180.0f * cos;

        _min = _minAngle - transform.eulerAngles.y;
        _max = _maxAngle - transform.eulerAngles.y;
    }

    public float GetHeadMaxAngle()
    {
        return _maxAngle;
    }

    public ref float GetHeadAngle()
    {
        return ref _headAngle;
    }
}
