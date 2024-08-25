using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

class MirrorPos
{
    Transform _target;

    public MirrorPos(Transform _tf)
    {
        _target = _tf;
    }

    public float GetZ()
    {
        return _target.position.z;
    }
}

public class GameManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool _trigger = false;
    [SerializeField] MushiType _p1MushiType;
    [SerializeField] MushiType _p2MushiType;

    [Header("Cinemachine")]
    [SerializeField] CinemachineTargetGroup _targetGroup;

    [Header("Prefab")]
    [SerializeField] GameObject _pfKokusan;
    [SerializeField] GameObject _pfSerebesu;
    [SerializeField] GameObject _pfKanabun;

    [Header("p1Input")]
    [SerializeField] InputActionAsset _p1Asset;
    [SerializeField] InputActionReference _p1movement;
    [SerializeField] InputActionReference _p1headMovement;
    [SerializeField] InputActionReference _p1skill;

    [Header("p2Input")]
    [SerializeField] InputActionAsset _p2Asset;
    [SerializeField] InputActionReference _p2movement;
    [SerializeField] InputActionReference _p2headMovement;
    [SerializeField] InputActionReference _p2skill;

    [SerializeField] Vector3 _p1StartPos;
    [SerializeField] Vector3 _p2StartPos;

    [Header("UI")]
    [SerializeField] Slider _p1HpSlider;
    [SerializeField] Slider _p2HpSlider;
    [SerializeField] Slider _p1SkillSlider;
    [SerializeField] Slider _p2SkillSlider;

    [Header("Mirror")]
    [SerializeField] float _mirrorDis = 2.0f;
    [ReadOnly,SerializeField] Transform _left;
    [ReadOnly, SerializeField] Transform _right;

    Transform _p1Mushi;
    Transform _p2Mushi;

    PlayerController _p1Controll;
    PlayerController _p2Controll;

    PlayerController _leftControll = null;
    PlayerController _rightControll = null;

    // Start is called before the first frame update
    void Awake()
    {
        if(_trigger)
        {
            GameVariables.ChangeMushiType(_p1MushiType, PlayerNumber.player_01);
            GameVariables.ChangeMushiType(_p2MushiType, PlayerNumber.player_02);
        }

        switch (GameVariables.GetPlayerMushiType(PlayerNumber.player_01))
        {
            case MushiType.Kokusan:
                _p1Mushi = Instantiate(_pfKokusan).transform;
                break;
            case MushiType.Serebesu:
                _p1Mushi = Instantiate(_pfSerebesu).transform;
                break;
            case MushiType.Kanabun:
                _p1Mushi = Instantiate(_pfKanabun).transform;
                break;
        }

        _p1Mushi.name = "Player01";

        _p1Mushi.position = _p1StartPos;
        _p1Mushi.GetComponent<PlayerInput>().actions = _p1Asset;


        _p1Controll = _p1Mushi.GetComponent<PlayerController>();

        _p1Controll._playerNumber = PlayerNumber.player_01;

        _p1Controll.movement = _p1movement;
        _p1Controll.headMovement = _p1headMovement;
        _p1Controll.skill = _p1skill;

        switch (GameVariables.GetPlayerMushiType(PlayerNumber.player_02))
        {
            case MushiType.Kokusan:
                _p2Mushi = Instantiate(_pfKokusan).transform;
                break;
            case MushiType.Serebesu:
                _p2Mushi = Instantiate(_pfSerebesu).transform;
                break;
            case MushiType.Kanabun:
                _p2Mushi = Instantiate(_pfKanabun).transform;
                break;
        }

        _p2Mushi.name = "Player02";

        _p2Mushi.position = _p2StartPos;
        _p2Mushi.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        _p2Mushi.GetComponent<PlayerInput>().actions = _p2Asset;

        _p2Controll = _p2Mushi.GetComponent<PlayerController>();

        _p2Controll._playerNumber = PlayerNumber.player_02;

        _p2Controll.movement = _p2movement;
        _p2Controll.headMovement = _p2headMovement;
        _p2Controll.skill = _p2skill;

        _targetGroup.AddMember(_p1Mushi, 1, 3);
        _targetGroup.AddMember(_p2Mushi, 1, 3);

        _left = _p1Mushi;
        _right = _p2Mushi;

        _leftControll = _p1Controll;
        _rightControll = _p2Controll;

        var trigger = _p1Mushi.GetComponentInChildren<LoseTrigger>();
        trigger.slider = _p1HpSlider;

        trigger = _p2Mushi.GetComponentInChildren<LoseTrigger>();
        trigger.slider = _p2HpSlider;

        _p1Controll.SetSkillSlider(_p1SkillSlider);
        _p2Controll.SetSkillSlider(_p2SkillSlider);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0, 0.5f);
        Gizmos.DrawSphere(_p1StartPos, 0.25f);
        Gizmos.DrawSphere(_p2StartPos, 0.25f);
    }

    private void Update()
    {
        float dis = _left.position.z - _right.position.z;
        if(dis > _mirrorDis)
        {
            Transform swap = _left;
            _left = _right;
            _right = swap;

            //Vector3 angles = _right.eulerAngles;
            //angles.y = -180.0f;
            //_right.eulerAngles = angles;

            _p1Controll.Mirror();
            _p2Controll.Mirror();
        }
    }
}
