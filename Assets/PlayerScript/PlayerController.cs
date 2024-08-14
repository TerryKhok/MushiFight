using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerSkill
{
    void UsingSkill();
}

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference movement;
    public InputActionReference headMovement;
    public InputActionReference skill;

    [Header("Movement")]
    public float moveSpeed;

    Rigidbody _rb;
    private Vector2 _moveDirection;
    private Vector2 _headMoveDirection;

    [Header("HeadMove")]
    [SerializeField] Transform _headTarget;
    [SerializeField] Transform _headPoint;
    [SerializeField] Transform _hipTarget;
    [SerializeField] float _pointDistance = 5.0f;
    [SerializeField] float _headMoveSpeed = 0.5f;
    [ReadOnly] public float _headAngle = 0.0f;

    [Header("Skill")]
    [SerializeField, ComponentRestriction(typeof(IPlayerSkill))] Component _skillComponent;

    IPlayerSkill logger = null;
    IPlayerSkill Logger
    {
        get
        {
            if (logger == null)
                logger = _skillComponent.GetComponent<IPlayerSkill>();
            return logger;
        }
    }

    float _headTargetDistance = 0.0f;
    float _ratio = 0.0f;

    private void OnEnable()
    {
        skill.action.started += UseSkill;
    }

    private void OnDisable()
    {
        skill.action.started -= UseSkill;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (transform.eulerAngles.y == 180.0f)
        {
            _pointDistance *= -1.0f;
            _headAngle = 180.0f;
        }

        _rb = GetComponent<Rigidbody>();
        _headTargetDistance = _headTarget.position.z - _hipTarget.position.z;
        _ratio = _headTargetDistance / _pointDistance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0.0f, 0.0f);


        _moveDirection = movement.action.ReadValue<Vector2>();
        _headMoveDirection = headMovement.action.ReadValue<Vector2>();

        if(_headMoveDirection.y != 0.0f)
        {
            float rad = _headAngle * Mathf.PI / 180.0f;
            _headPoint.localPosition = new Vector3(0.0f, Mathf.Sin(rad) * _pointDistance, Mathf.Cos(rad) * _pointDistance);

            _headTarget.position = Vector3.Lerp(_hipTarget.position, _headPoint.position, _ratio);
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(0.0f, _moveDirection.y * moveSpeed * 0.01f, _moveDirection.x * moveSpeed);
        _headAngle += _headMoveDirection.y * _headMoveSpeed;
    }

    private void UseSkill(InputAction.CallbackContext obj)
    {
        Logger.UsingSkill();
    }
}
