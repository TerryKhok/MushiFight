using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController),typeof(ControllLegRig), typeof(Rigidbody))]
public class DashSkill : MonoBehaviour,IPlayerSkill
{
    [Header("Skill")]
    [SerializeField] float _dashPower = 10.0f;
    [SerializeField] float _AddPower = 1000.0f;
    [SerializeField] float _dashTime = 1.0f;
    //[SerializeField] float _dashChangeLegWidth = 0.7f;
    [SerializeField] float _CDTime = 1.0f;

    [Header("Collider")]
    [SerializeField] Vector3 _colOffset = Vector3.zero;
    [SerializeField] Vector3 _colSize = Vector3.zero;
    [SerializeField] LayerMask _skillLayerMask = new LayerMask();

    [Header("SkillParticle")]
    [SerializeField] GameObject _SkillparticleObject;
    [SerializeField] GameObject _hitParticle;

    PlayerController _playerController;
    ControllLegRig _controllLegRig;
    Rigidbody _rb;

    Vector3 _forceDir = new Vector3(0.0f, 0.0f, 1.0f);

    Slider _skillSlider;

    Vector3 _offset;
    Vector3 _colPos;
    

    void OnDrawGizmos()
    {
        Gizmos.color = new UnityEngine.Color(0.5f, 0.0f, 0.5f, 1.0f);
        Gizmos.DrawWireCube(_colPos, _colSize);
    }

        // Start is called before the first frame update
        void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _controllLegRig = GetComponent<ControllLegRig>();
        _rb = GetComponent<Rigidbody>();

        Mirror();
    }

    public void UsingSkill()
    {
        StartCoroutine(Dash());
        PlaySkillEffect();
    }

    public void Mirror()
    {
        _offset = _colOffset;

        if (transform.eulerAngles.y == 180.0f)
        {
            _forceDir = new Vector3(0.0f, 0.0f, -1.0f);
            _offset.z *= -1.0f;
        }
        else
        {
            _forceDir = new Vector3(0.0f, 0.0f, 1.0f);
        }
    }

    void Update()
    {
        _colPos = transform.position + _offset;
    }

    IEnumerator Dash()
    {
        _skillSlider.value = 0.0f;
        _skillSlider.gameObject.SetActive(true);

        //_rb.AddForce(transform.forward * _dashPower * _firsePower, ForceMode.Impulse);

        //_controllLegRig.PulusAllLegMoveDistance(-_dashChangeLegWidth);

        Collider[] Colliders;
        Rigidbody targetRb = null;
        //bool push = false;
        //Colliders
        //= Physics.OverlapBox(_colPos, _colSize, Quaternion.identity, _skillLayerMask);

        

        float t = 0.0f;
        while(t < _dashTime)
        {
            float dis = _dashTime - t;
            float deltaTime = Time.deltaTime;
            float hitLegNum = (float)_controllLegRig.hitLegNum;
            _rb.AddForce(_forceDir * _dashPower * hitLegNum * deltaTime * dis, ForceMode.Impulse);

            //if (!push)
            {
                Colliders
                = Physics.OverlapBox(_colPos, _colSize, Quaternion.identity, _skillLayerMask);

                targetRb = null;
                foreach (var collider in Colliders)
                {
                    if (collider.transform.root.TryGetComponent<Rigidbody>(out targetRb) && targetRb != _rb)
                    {
                        //targetRb.AddForceAtPosition(_forceDir * _dashPower, _colPos, ForceMode.Impulse);
                        targetRb.AddForce(_forceDir * _AddPower/* * _rb.velocity.z*/, ForceMode.Force);
                        //push = true;
                    }
                }
            }

            if(t < 0.5f)
            {
                GameObject effect = Instantiate(_hitParticle, transform.position/*collider.ClosestPointOnBounds(this.transform.position)*/, Quaternion.identity);
                Destroy(effect, 1.5f);
            }

            t += deltaTime;

            yield return null;
        }

        //_controllLegRig.PulusAllLegMoveDistance(_dashChangeLegWidth);

        t = 0.0f;
        while (t < _CDTime)
        {
            _skillSlider.value = t / _CDTime;

            t += Time.deltaTime;
            yield return null;
        }

        _skillSlider.gameObject.SetActive(false);


        // yield return new WaitForSeconds(_CDTime);

        _playerController.usingSkill = false;
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
