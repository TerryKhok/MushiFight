using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

interface IFixedSkill
{
    void Fixed(Rigidbody rb, float deltaTime);
}

class FixedAddForce : IFixedSkill
{
    Rigidbody _myRb;
    Transform _fixedTarget;
    float _fixedPower;

    public FixedAddForce(Rigidbody _rb, Transform target, float power)
    {
        _myRb = _rb;
        _fixedTarget = target;
        _fixedPower = power;
    }

    public void Fixed(Rigidbody rb, float deltaTime)
    {
        Vector3 dir = _fixedTarget.position - rb.position;
        dir.z += _myRb.velocity.z;
        rb.AddForce(dir * _fixedPower * deltaTime, ForceMode.Impulse);
    }
}

class FixedPosition : IFixedSkill
{
    Transform _fixedTarget;

    public FixedPosition(Transform target)
    {
        _fixedTarget = target;
    }

    public void Fixed(Rigidbody rb, float deltaTime)
    {
        rb.position = _fixedTarget.position;
    }
}


[RequireComponent(typeof(PlayerController))]
public class SandwichSkill : MonoBehaviour,IPlayerSkill
{

    [SerializeField] GameObject _pfJawCollider;

    [Header("Transform")]
    [SerializeField] Transform _leftBase;
    [SerializeField] Transform _rightBase;
    [SerializeField] Transform _leftTarget;
    [SerializeField] Transform _rightTarget;
    [SerializeField] Transform _fixedTarget;

    [Header("Limit")]
    [SerializeField] float _openAngle = 60.0f;
    [SerializeField] float _closeAngle = 10.0f;

    [Header("Collider")]
    [SerializeField] Vector3 _fixedColliderOffset = Vector3.zero;
    [SerializeField] Vector3 _fixedColliderSize = Vector3.zero;
    [SerializeField] LayerMask _fixedTargetLayer = new LayerMask();

    [Header("Skill")]
    [SerializeField] bool AddForce = true;
    [SerializeField] float _sandwichPower = 50.0f;
    [SerializeField] float _releasePower = 5.0f;
    [SerializeField] float _fixedPower = 3.0f;
    [SerializeField] float _fixedTime = 2.0f;
    [SerializeField] float _CDTime = 1.0f;
    Slider _skillSlider;

    [Header("SkillParticle")]
    [SerializeField] GameObject _SkillparticleObject;

    IFixedSkill _fixedSkill = null;

    PlayerController _playerController;

    Transform _leftJaw;
    Transform _rightJaw;
    Transform _leftSkillTf;
    Transform _rightSkillTf;

    HingeJoint _leftJoint;
    HingeJoint _rightJoint;

    Rigidbody _myRb;
    Rigidbody _leftRb;
    Rigidbody _rightRb;

    Vector3 colGizmoLeftPos = Vector3.zero;
    Quaternion colGizmoLeftRot = Quaternion.identity;
    Vector3 colGizmoRightPos = Vector3.zero;
    Quaternion colGizmoRightRot = Quaternion.identity;

    Vector3 _colOffset = Vector3.zero;

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0.0f, 0.5f, 1.0f);
        //Gizmos.DrawWireCube(colGizmoPos, _fixedColliderSize);
       
        var cache = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(colGizmoLeftPos, colGizmoLeftRot, _fixedColliderSize);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = cache;

        cache = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(colGizmoRightPos, colGizmoRightRot, _fixedColliderSize);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = cache;
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GetComponent<PlayerController>();

        Mirror();

        GameObject leftGameObject = Instantiate(_pfJawCollider, _leftBase.position, _leftBase.rotation);
        leftGameObject.name = gameObject.name + "LeftJawCollider";
        _leftJaw = leftGameObject.transform;

        _leftSkillTf = new GameObject("LeftSkillTF").transform;
        _leftSkillTf.parent = _leftJaw;
        _leftSkillTf.position = _leftJaw.position + _colOffset;
        _leftSkillTf.localRotation = Quaternion.identity;

        JointLimits limits;

        if(!_leftBase.TryGetComponent<HingeJoint>(out _leftJoint))
        {
            _leftJoint = _leftBase.AddComponent<HingeJoint>();
        }

        _leftJoint.connectedBody = _leftRb = _leftJaw.GetComponent<Rigidbody>();
        _leftRb.maxAngularVelocity = 50.0f;

        _leftJoint.axis = new Vector3(0.0f, 0.0f, 1.0f);

        limits = _leftJoint.limits;
        limits.min = -_openAngle;
        limits.max = _closeAngle;
        _leftJoint.limits = limits;
        _leftJoint.useLimits = true;

        _leftTarget.AddComponent<SetParent>().parent = _leftJaw;


        GameObject rightGameObject = Instantiate(_pfJawCollider, _rightBase.position, _rightBase.rotation);
        rightGameObject.name = gameObject.name + "RightJawCollider";
        _rightJaw = rightGameObject.transform;

        _colOffset.x *= -1.0f;

        _rightSkillTf = new GameObject("RightSkillTF").transform;
        _rightSkillTf.parent = _rightJaw;
        _rightSkillTf.position = _rightJaw.position + _colOffset;
        _rightSkillTf.localRotation = Quaternion.identity;

        if (!_rightBase.TryGetComponent<HingeJoint>(out _rightJoint))
        {
            _rightJoint = _rightBase.AddComponent<HingeJoint>();
        }

        _rightJoint.connectedBody = _rightRb = _rightJaw.GetComponent<Rigidbody>();
        _rightRb.maxAngularVelocity = 50.0f;

        _rightJoint.axis = new Vector3(0.0f, 0.0f, 1.0f);

        limits = _rightJoint.limits;
        limits.min = - _closeAngle;
        limits.max = + _openAngle;
        _rightJoint.limits = limits;
        _rightJoint.useLimits = true;

        _rightTarget.AddComponent<SetParent>().parent = _rightJaw;

        _myRb = transform.GetComponent<Rigidbody>();

        if(AddForce)
        {
            _fixedSkill = new FixedAddForce(_myRb, _fixedTarget, _fixedPower);
        }
        else
        {
            _fixedSkill = new FixedPosition(_fixedTarget);
        }
    }

    public void UsingSkill()
    {

        _leftRb.AddTorque(_leftJaw.forward * _sandwichPower, ForceMode.Impulse);
        _rightRb.AddTorque(_rightJaw.forward * -_sandwichPower, ForceMode.Impulse);

        StartCoroutine(SandWitch());
        PlaySkillEffect();
    }

    public void Mirror()
    {
        float mirror = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180.0f);
        _colOffset = _fixedColliderOffset * mirror;
    }

    private void Update()
    {
        //Gismos
        //==========================================
        colGizmoLeftPos = _leftSkillTf.position;
        colGizmoLeftRot = _leftSkillTf.rotation;
        colGizmoRightPos = _rightSkillTf.position;
        colGizmoRightRot = _rightSkillTf.rotation;
        //==========================================
    }

    IEnumerator SandWitch()
    {
        _skillSlider.value = 0.0f;
        _skillSlider.gameObject.SetActive(true);

        yield return new WaitForSeconds(.3f);

        _leftRb.AddTorque(_leftJaw.forward * -_sandwichPower, ForceMode.Impulse);
        _rightRb.AddTorque(_rightJaw.forward * _sandwichPower, ForceMode.Impulse);


        var leftColliders
          = Physics.OverlapBox(_leftSkillTf.position, _fixedColliderSize / 2, _leftSkillTf.rotation, _fixedTargetLayer);

        var rightColliders
            = Physics.OverlapBox(_rightSkillTf.position, _fixedColliderSize / 2, _rightSkillTf.rotation, _fixedTargetLayer);

        bool leftHit = leftColliders.Length != 0;
        if(leftHit) _leftRb.angularVelocity = Vector3.zero;

        bool rightHit = rightColliders.Length != 0;
        if (rightHit) _rightRb.angularVelocity = Vector3.zero;

        float t = 0.0f;
        while(t < 0.3f)
        {
            if(!leftHit)
            {
                leftColliders
                = Physics.OverlapBox(_leftSkillTf.position, _fixedColliderSize / 2, _leftSkillTf.rotation, _fixedTargetLayer);
                leftHit = leftColliders.Length != 0;
                if (leftHit) _leftRb.angularVelocity = Vector3.zero;
            }
           
            if(!rightHit)
            {
                rightColliders
                 = Physics.OverlapBox(_rightSkillTf.position, _fixedColliderSize / 2, _rightSkillTf.rotation, _fixedTargetLayer);
                rightHit = rightColliders.Length != 0;
                if (rightHit) _rightRb.angularVelocity = Vector3.zero;
            }

            t += Time.deltaTime;

            yield return null;
        }

        Rigidbody rb = null;
        foreach (var leftTarget in leftColliders)
        {
            foreach (var rightTarget in rightColliders)
            {
                if(leftTarget == rightTarget)
                {
                    if(rightTarget.transform.root.TryGetComponent<Rigidbody>(out rb) && rb == _myRb)
                    {
                        rb = null;
                    }   
                }
            }
        }

        if(rb != null)
        {
            Debug.Log(rb);

            t = 0.0f;
            while(t < _fixedTime)
            {
                float deltaTime = Time.deltaTime;

                _fixedSkill.Fixed(rb, deltaTime);

                t += deltaTime;

                yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(_fixedTime);
        }
        

        //_leftRb.freezeRotation = false;
        //_rightRb.freezeRotation = false;

        _leftRb.AddTorque(_leftJaw.forward * _releasePower, ForceMode.Impulse);
        _rightRb.AddTorque(_rightJaw.forward * -_releasePower, ForceMode.Impulse);

        yield return new WaitForSeconds(0.1f);

        _leftRb.angularVelocity = Vector3.zero;
        _rightRb.angularVelocity = Vector3.zero;

        t = 0.0f;
        while (t < _CDTime)
        {
            _skillSlider.value = t / _CDTime;

            t += Time.deltaTime;
            yield return null;
        }

        _skillSlider.gameObject.SetActive(false);


        //yield return new WaitForSeconds(_CDTime);

        _playerController.usingSkill = false;

    }

    Vector3 GetForce(Vector3 pos)
    {
        float dist = Vector3.Distance(_fixedTarget.position, pos);
        Vector3 dir = (_fixedTarget.position - pos).normalized;
        return dir / (dist * dist) * _fixedPower;
    }

    private void PlaySkillEffect()
    {
        GameObject effect = Instantiate(_SkillparticleObject, this.transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    public void SetSlider(Slider _slider)
    {
        _skillSlider = _slider;
    }
}
