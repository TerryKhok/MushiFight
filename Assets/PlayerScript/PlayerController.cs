using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public interface IPlayerSkill
{
    void UsingSkill();
}

public interface IHeadMove
{
    void Move(Vector2 _direction);
    float GetHeadMaxAngle();
    ref float GetHeadAngle();
}

public interface IReturnRotation
{
    float GetRotation();
}

public class ReturnRotationX : IReturnRotation
{
    Transform _transform;

    public ReturnRotationX(Transform _tf)
    {
        _transform = _tf;
    }

    public float GetRotation()
    {
        return _transform.rotation.x;
    }
}

public class ReturnRotationZ : IReturnRotation
{
    Transform _transform;

    public ReturnRotationZ(Transform _tf)
    {
        _transform = _tf;
    }

    public float GetRotation()
    {
        return _transform.rotation.z;
    }
}

public class PlayerController : MonoBehaviour
{
    public PlayerNumber _playerNumber;

    [Header("Input")]
    public InputActionReference movement;
    public InputActionReference headMovement;
    public InputActionReference skill;

    [Header("Movement")]
    public float moveSpeed;
    float _startAngleY = 0.0f;
    IReturnRotation _targetRot;

    Rigidbody _rb;
    private Vector2 _moveDirection;
   
    [Header("HeadMove")]
    [SerializeField, ComponentRestriction(typeof(IHeadMove))] Component _headMoveComponent;

    private Vector2 _headMoveDirection;

    IHeadMove headlogger = null;
    IHeadMove headLogger
    {
        get
        {
            if (headlogger == null)
                headlogger = _skillComponent.GetComponent<IHeadMove>();
            return headlogger;
        }
    }


    [Header("Skill")]
    [SerializeField, ComponentRestriction(typeof(IPlayerSkill))] Component _skillComponent;
    [ReadOnly] public bool usingSkill = false;

    IPlayerSkill skilllogger = null;
    IPlayerSkill skillLogger
    {
        get
        {
            if (skilllogger == null)
                skilllogger = _skillComponent.GetComponent<IPlayerSkill>();
            return skilllogger;
        }
    }

    private void OnDisable()
    {
        skill.action.started -= UseSkill;
    }

    Vector3 _centerMass = Vector3.zero;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.5f, 0.5f, 1.0f);
        Gizmos.DrawSphere(_centerMass, 0.25f);
    }

    // Start is called before the first frame update
    void Start()
    {
        skill.action.started += UseSkill;

        _rb = transform.GetComponent<Rigidbody>();

        float cos = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180.0f);
        if (cos == -1.0f)
        {
            _targetRot = new ReturnRotationZ(transform);
        }
        else
        {
            _targetRot = new ReturnRotationX(transform);
        }

        _startAngleY = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        _rb.centerOfMass = Vector3.zero;
        _centerMass = _rb.centerOfMass + transform.position;

        //Quaternion rotation = transform.rotation;
        ////rotation.y = _startAngleY;
        ////rotation.z = 0.;
        //transform.rotation = rotation;

        _moveDirection = movement.action.ReadValue<Vector2>();
        _headMoveDirection = headMovement.action.ReadValue<Vector2>();

        headLogger.Move(_headMoveDirection);

        float rot = _targetRot.GetRotation();
        float angle = transform.eulerAngles.x;
        if (Mathf.Abs(rot) > 0.7f)
            angle = 180.0f - angle;

        transform.eulerAngles = new Vector3(angle, _startAngleY, 0.0f);
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(0.0f, _rb.velocity.y/*_moveDirection.y * moveSpeed * 0.01f*/, _moveDirection.x * moveSpeed);
    }

    private void UseSkill(InputAction.CallbackContext obj)
    {
        if (usingSkill) return;

        skillLogger.UsingSkill();

        usingSkill = true;
    }
}
