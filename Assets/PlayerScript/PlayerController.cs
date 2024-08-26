using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
#if UNITY_EDITOR
using static UnityEditor.PlayerSettings;
#endif

public interface IPlayerSkill
{
    void UsingSkill();
    void Mirror();
    void SetSlider(Slider _slider);
}

public interface IHeadMove
{
    void Move(Vector2 _direction);
    void Mirror();

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

[RequireComponent(typeof(ControllLegRig))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] bool _debugMirror = false;

    public PlayerNumber _playerNumber;

    [Header("Input")]
    public InputActionReference movement;
    public InputActionReference headMovement;
    public InputActionReference skill;

    [Header("Movement")]
    public float moveSpeed;
    [SerializeField] LayerMask _groundLayer = new LayerMask();
    [SerializeField] Vector3 _rayOffset = Vector3.zero;
    float _startAngleY = 0.0f;
    IReturnRotation _targetRot;
    Vector3 _rayDir = Vector3.forward;
    

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

    [Header("Material")]
    [SerializeField] SkinnedMeshRenderer _renderer;
    [SerializeField] Material _p1Material;
    [SerializeField] Material _p2Material;

    ControllLegRig _controllLeg;


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

        _controllLeg = GetComponent<ControllLegRig>();

        if (_playerNumber == PlayerNumber.player_01)
        {
            _renderer.material = _p1Material;
        }
        else if (_playerNumber == PlayerNumber.player_02)
        {
            _renderer.material = _p2Material;
        }

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

        _rayDir *= cos;
    }

    public void Mirror()
    {
        Debug.Log(_playerNumber);

        _startAngleY = (transform.eulerAngles.y + 180.0f) % 360.0f;

        Vector3 angles = transform.eulerAngles;
        angles.y = (angles.y + 180.0f) % 360.0f;
        transform.eulerAngles = angles;

        float cos = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180.0f);
        if (cos == -1.0f)
        {
            _targetRot = new ReturnRotationZ(transform);
        }
        else
        {
            _targetRot = new ReturnRotationX(transform);
        }

        //_startAngleY = transform.eulerAngles.y;

        _rayDir *= cos;

        _controllLeg.Mirror();

        skillLogger.Mirror();
        headLogger.Mirror();
    }

    // Update is called once per frame
    void Update()
    {
        if(_debugMirror)
        {
            _debugMirror = false;

            
            Mirror();
        }

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
        Debug.DrawRay(transform.position + _rayOffset, _rayDir);

        Ray ray = new Ray(transform.position + _rayOffset, _rayDir);
        if (Physics.Raycast(ray, out RaycastHit hit, 3.0f, _groundLayer))
        {
            return;
        }

        _rb.velocity = new Vector3(0.0f, _rb.velocity.y/*_moveDirection.y * moveSpeed * 0.01f*/, _moveDirection.x * moveSpeed);
    }

    private void UseSkill(InputAction.CallbackContext obj)
    {
        if (usingSkill) return;

        skillLogger.UsingSkill();

        usingSkill = true;
    }

    public void SetSkillSlider(Slider _slider)
    {
        skillLogger.SetSlider(_slider);
    }
}
