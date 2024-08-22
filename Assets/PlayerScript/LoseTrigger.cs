using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class LoseTrigger : MonoBehaviour
{
    [SerializeField] float _loseTime = 5.0f;
    [ReadOnly, SerializeField] float _stayTime = 0.0f;
    PlayerController _playerController;
    bool _lose = false;

    [SerializeField] string _nextScene = "Result";
    ChangeScene _scene;

    private void Start()
    {
        if (!transform.root.TryGetComponent<PlayerController>(out _playerController))
        {
            Debug.Log("rootオブジェクトにplayerControllerがついていません");
        }

        _scene = GetComponent<ChangeScene>();
    }

    void OnTriggerStay(Collider _other)
    {
        if (_lose) return;

        _stayTime += Time.deltaTime;

        if (_stayTime >= _loseTime)
        {
            _lose = true;

            GameVariables.SetLosePlayer(_playerController._playerNumber);
            _scene.Change(_nextScene);
        }

    }

    void OnTriggerExit(Collider _other)
    {
        _stayTime = 0.0f;
    }
}
