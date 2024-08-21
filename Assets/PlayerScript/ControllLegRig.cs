using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class ControllLegRig : MonoBehaviour
{
    [Header("Ray")]
    [SerializeField] LayerMask rayLayer = new LayerMask();
    [SerializeField] bool setLegRayLenght = true;
    [SerializeField] float rayLenght = 2.0f;

    [Header("Offset")]
    [SerializeField] float backMoveOffsetZ = 1.0f;
    [SerializeField] float airOffsetZ = 1.0f;

    [Header("Leg")]
    [SerializeField] List<MoveLegRig> moveLegList = new List<MoveLegRig>();
    [SerializeField] float _maxGripPower = 10.0f;
    [ReadOnly] public int hitLegNum = 0;

    List<float> legZOffsets = new List<float>();

    Rigidbody _rb;
    float _oldPosZ = 0.0f;
    float _oldPosY = 0.0f;
    bool _isBack = false;
    float _mirror = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _oldPosZ = transform.position.z;

        float cos = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180.0f);
        _mirror = cos;
        backMoveOffsetZ *= _mirror;
        airOffsetZ *= _mirror;

        //if (backMoveOffsetZ < 0.0f)
        //{
        //    _mirror = -1.0f;
        //    airOffsetZ *= _mirror;
        //}

        IReturnRotation returnRot = null;
        if (_mirror == -1.0f)
            returnRot = new ReturnRotationZ(transform);
        else
            returnRot = new ReturnRotationX(transform);

        foreach (var leg in moveLegList)
        {
            legZOffsets.Add(leg.offset.z);
            leg.mirror = _mirror;
            leg.targetRot = returnRot;
        }

        if(setLegRayLenght)
        {
            foreach (var leg in moveLegList)
            {
                leg.rayLenght = rayLenght;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isMoveY = Mathf.Abs(_oldPosY - transform.position.y) > 0.1f;
        //if(isMoveY)
        {
            hitLegNum = 0;
            foreach (var leg in moveLegList)
            {
                if(leg.isHit)
                {
                    hitLegNum++;
                }
            }

            float gripPower = Mathf.Max(_maxGripPower * hitLegNum / moveLegList.Count, 0.1f);
            _rb.angularDrag = gripPower;
        }
        

        float moveOffset = backMoveOffsetZ;

        Ray ray = new Ray(transform.position, Quaternion.Euler(transform.eulerAngles) * Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, rayLenght, rayLayer))
        {
            moveOffset -= hit.distance * _mirror;
        }
        else
        {
            for (int i = 0; i < moveLegList.Count; i++)
            {
                moveLegList[i].offset.z = legZOffsets[i] - airOffsetZ;
            }
            return;
        }

        float disZ = (transform.position.z - _oldPosZ) * _mirror;
        if (disZ < -0.01f)
        {
            if (!_isBack || isMoveY)
            {
                for(int i = 0;i < moveLegList.Count;i++)
                {
                    moveLegList[i].offset.z = legZOffsets[i] - moveOffset;
                }
                //foreach (var leg in moveLegList)
                //{
                //    leg.offset.z -= moveOffset;
                //}
            }
            
            _isBack = true;
        }
        else if(_isBack && disZ > 0.01f)
        {
            _isBack = false;
            for (int i = 0; i < moveLegList.Count; i++)
            {
                moveLegList[i].offset.z = legZOffsets[i];
            }
        }

        _oldPosZ = transform.position.z;
        _oldPosY = transform.position.y;
    }
}
