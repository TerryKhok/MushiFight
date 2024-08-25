using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController),typeof(ControllLegRig), typeof(Rigidbody))]
public class DashSkill : MonoBehaviour,IPlayerSkill
{
    [SerializeField] float _dashPower = 10.0f;
    [SerializeField] float _dashTime = 1.0f;
    //[SerializeField] float _dashChangeLegWidth = 0.7f;
    [SerializeField] float _CDTime = 1.0f;

    [Header("SkillParticle")]
    [SerializeField] GameObject _SkillparticleObject;

    PlayerController _playerController;
    ControllLegRig _controllLegRig;
    Rigidbody _rb;

    Vector3 _forceDir = new Vector3(0.0f, 0.0f, 1.0f);

    Slider _skillSlider;

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
        if (transform.eulerAngles.y == 180.0f)
        {
            _forceDir = new Vector3(0.0f, 0.0f, -1.0f);
        }
        else
        {
            _forceDir = new Vector3(0.0f, 0.0f, 1.0f);
        }
    }

    IEnumerator Dash()
    {
        _skillSlider.value = 0.0f;
        _skillSlider.gameObject.SetActive(true);

        //_rb.AddForce(transform.forward * _dashPower * _firsePower, ForceMode.Impulse);

        //_controllLegRig.PulusAllLegMoveDistance(-_dashChangeLegWidth);

        float t = 0.0f;
        while(t < _dashTime)
        {
            float dis = _dashTime - t;
            float deltaTime = Time.deltaTime;
            float hitLegNum = (float)_controllLegRig.hitLegNum;
            _rb.AddForce(_forceDir * _dashPower * hitLegNum * deltaTime * dis, ForceMode.Impulse);
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
