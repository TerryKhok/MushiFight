using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLegRig : MonoBehaviour
{
    [Header("Ray")]
    [SerializeField] LayerMask rayLayer = new LayerMask();
    public Vector3 offset;
    public float rayLenght = 10.0f;
    [ReadOnly] public bool isHit = false;

    [Header("Move")]
    [SerializeField] float moveTime = 1.0f;
    [SerializeField] float moveDistance = 1.0f;
    [SerializeField] float moveHeight = 0.2f;
    [ReadOnly] public float mirror = 1.0f;

    public IReturnRotation targetRot;

    Transform _rootObject;
    Vector3 _startPos;
    Vector3 _endPos;
    Vector3 _targetPos;
    float _deltaTime = 0.0f;
    float _goalTime = 0.0f;
    bool _isMove = false;


    private void Awake()
    {
        _rootObject = transform.root.transform;

        float radY = -_rootObject.eulerAngles.y * Mathf.PI / 180.0f;
        offset = new Vector3(
                Mathf.Cos(radY) * offset.x - Mathf.Sin(radY) * offset.z,
                offset.y,
                Mathf.Sin(radY) * offset.x + Mathf.Cos(radY) * offset.z
                );
    }

    // Start is called before the first frame update
    void Start()
    {
        _deltaTime = moveTime + 1.0f;
        _endPos = _rootObject.position + offset;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0, 0.5f);
        Gizmos.DrawSphere(_targetPos, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        float radX = -_rootObject.eulerAngles.x * Mathf.PI / 180.0f * mirror;
        Vector3 LegPos = new Vector3(
            offset.x,
            Mathf.Sin(radX) * offset.z + Mathf.Cos(radX) * offset.y,
            Mathf.Cos(radX) * offset.z - Mathf.Sin(radX) * offset.y
            );

        float rot = targetRot.GetRotation();
        if (Mathf.Abs(rot) > 0.7f)
        {
            LegPos.z *= -1.0f;
        }

        Vector3 rayPos = _rootObject.position + LegPos;

        Ray ray = new Ray(rayPos, Quaternion.Euler(_rootObject.eulerAngles) * Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, rayLenght, rayLayer))
        {
            _targetPos = hit.point;
            isHit = true;
        }
        else
        {
            _targetPos = rayPos;
            _endPos = _targetPos;
            isHit = false;
            //_goalTime = 1.0f;
        }

        float distance = Vector3.Distance(transform.position, _targetPos);
        if (!_isMove && distance > moveDistance)
        {
            _isMove = true;
            _deltaTime = 0.0f;
            _startPos = transform.position;
            _endPos = _targetPos;
            _goalTime = moveTime * moveDistance / distance;
        }

        if(_deltaTime <= _goalTime)
        {
            float t = _deltaTime / _goalTime;
            transform.position = Vector3.Lerp(_startPos, _endPos, t) 
                + new Vector3(0.0f,Mathf.Sin(t * Mathf.PI) * moveHeight * hit.distance, 0.0f);
            
            _deltaTime += Time.deltaTime;
        }
        else
        {
            _isMove = false;
            transform.position = _endPos;
        }


        Debug.DrawRay(rayPos, Quaternion.Euler(_rootObject.eulerAngles) * Vector3.down, Color.green);
    }
}
