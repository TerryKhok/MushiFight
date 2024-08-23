using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController),typeof(IHeadMove))]
public class PushUpSkill : MonoBehaviour, IPlayerSkill
{
    [Header("Skill")]
    [SerializeField] Transform _headPoint;
    [SerializeField] float _pushUpForce = 1.0f;
    [SerializeField] float _CDtime = 1.0f;
    [SerializeField] float _pushUpSpeed = 120.0f;

    [Header("Collider")]
    [SerializeField] Transform _colliderParent;
    [SerializeField] Vector3 _skillColliderOffset = Vector3.zero;
    [SerializeField] LayerMask _skillLayerMask = new LayerMask();

    [Header("SkillParticle")]
    [SerializeField] GameObject _SkillparticleObject;

    PlayerController _playerController;
    Rigidbody _myRb;
    IHeadMove _headMove;

    Vector3 colPos = Vector3.zero;
    Quaternion colRot = Quaternion.identity;
    Vector3 colSize = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = transform.GetComponent<PlayerController>();
        _myRb = GetComponent<Rigidbody>();
        _headMove = transform.GetComponent<IHeadMove>();

        BoxCollider collider = null;
        if (_colliderParent.TryGetComponent<BoxCollider>(out collider))
        {
            colSize = collider.size;
        }
        else
        {
            colSize = _colliderParent.lossyScale;
        }
    }    

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0.0f, 0.5f, 1.0f);
        //Gizmos.DrawWireCube(colGizmoPos, _fixedColliderSize);

        var cache = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(colPos, colRot, colSize);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = cache;
    }

    public void UsingSkill()
    {
        StartCoroutine(PushUp());
        PlaySkillEffect();
    }

    //Gismo
    //======================================================
    void Update()
    {
        colPos = _colliderParent.position + _skillColliderOffset;
        colRot = _colliderParent.rotation;

        Debug.DrawRay(colPos, -_colliderParent.forward, Color.green);
    }
    //======================================================


    IEnumerator PushUp()
    {
        float maxAngle = _headMove.GetHeadMaxAngle();

        colPos = _colliderParent.position + _skillColliderOffset;
        colRot = _colliderParent.rotation;

        var leftColliders
          = Physics.OverlapBox(colPos, colSize, colRot, _skillLayerMask);

        Rigidbody targetRb = null;
        foreach (var collider in leftColliders)
        {
            if (collider.transform == _colliderParent) continue;

            if (collider.transform.root.TryGetComponent<Rigidbody>(out targetRb) && targetRb != _myRb)
            {
                Debug.Log(collider);
                targetRb.AddForceAtPosition(-_colliderParent.forward * _pushUpForce, colPos, ForceMode.Impulse);
            }
        }

        while (_headMove.GetHeadAngle() < maxAngle)
        {
            _headMove.GetHeadAngle() += Time.deltaTime * _pushUpSpeed;

            yield return null;
        }

        //float startHeight = _headPoint.position.y;
        //float t = 0.0f;
        ////float dis = _maxHeight - _headPoint.localPosition.y;
        //while (_headPoint.localPosition.y < _maxHeight)
        //{
        //    Vector3 pos = _headPoint.localPosition;
        //    pos.y = Mathf.Lerp(startHeight, _maxHeight, t);
        //    _headPoint.localPosition = pos;
        //    t += Time.deltaTime * _pushUpSpeed;

            //    yield return null;
            //}


        yield return new WaitForSeconds(_CDtime);

        _playerController.usingSkill = false;
    }

    private void PlaySkillEffect()
    {
        GameObject effect = Instantiate(_SkillparticleObject, this.transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }
}
