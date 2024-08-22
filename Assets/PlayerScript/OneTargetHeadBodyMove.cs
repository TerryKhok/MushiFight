using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OneTargetHeadBodyMove : MonoBehaviour, IHeadMove
{
    [Header("Head")]
    [SerializeField] Transform _headPoint;
    [SerializeField] float _pointDistance = 3.0f;
    [SerializeField] float _headMoveSpeed = 0.5f;
    [SerializeField] float _maxAngle = 60.0f;
    [SerializeField] float _minAngle = -30.0f;
    [ReadOnly, SerializeField] float _headAngle = 0.0f;

    [Header("Body")]
    [SerializeField] Transform _bodyPoint;
    [SerializeField] Transform _bodyTarget;
    [SerializeField, Range(0, 1)] float _bodyPointPower;
    [SerializeField] float _bodyTargetMoveWidth = 2.0f;

    Vector3 _startTargetPos;

    // Start is called before the first frame update
    void Start()
    {
        _startTargetPos = _bodyTarget.localPosition;
    }

    public void Move(Vector2 _direction)
    {
        _headAngle = Mathf.Clamp(_headAngle + _direction.y * _headMoveSpeed * Time.deltaTime, _minAngle, _maxAngle);

        //if (_direction.y != 0.0f)
        {
            float rad = _headAngle * Mathf.PI / 180.0f;
            Vector3 targetPos = new Vector3(0.0f, Mathf.Sin(rad) * _pointDistance, Mathf.Cos(rad) * _pointDistance);

            _headPoint.localPosition = targetPos;
            targetPos.z *= 0.5f;
            targetPos.y *= -_bodyPointPower;
            _bodyPoint.localPosition = targetPos;

            _bodyTarget.localPosition =
                _startTargetPos + new Vector3(0.0f, 0.0f, _bodyPoint.localPosition.y * _bodyTargetMoveWidth);
        }
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
