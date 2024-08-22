using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
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

    Transform _p1Mushi;
    Transform _p2Mushi;

    // Start is called before the first frame update
    void Awake()
    {
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


        PlayerController _playerController = null;
        _playerController = _p1Mushi.GetComponent<PlayerController>();

        _playerController._playerNumber = PlayerNumber.player_01;

        _playerController.movement = _p1movement;
        _playerController.headMovement = _p1headMovement;
        _playerController.skill = _p1skill;

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

        _playerController = null;
        _playerController = _p2Mushi.GetComponent<PlayerController>();

        _playerController._playerNumber = PlayerNumber.player_02;

        _playerController.movement = _p2movement;
        _playerController.headMovement = _p2headMovement;
        _playerController.skill = _p2skill;

    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0, 0.5f);
        Gizmos.DrawSphere(_p1StartPos, 0.25f);
        Gizmos.DrawSphere(_p2StartPos, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
