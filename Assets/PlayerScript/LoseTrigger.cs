using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class LoseTrigger : MonoBehaviour
{
    [SerializeField] float _loseTime = 5.0f;
    [ReadOnly, SerializeField] float _stayTime = 0.0f;

    [ReadOnly] public TextMeshProUGUI text;
    [ReadOnly] public Slider slider;

    PlayerController _playerController;
    bool _lose = false;

    [SerializeField] string _nextScene = "Result";
    ChangeScene _scene;

    bool _hit = false;

    private void Start()
    {
        if (!transform.root.TryGetComponent<PlayerController>(out _playerController))
        {
            Debug.Log("rootオブジェクトにplayerControllerがついていません");
        }

        _scene = GetComponent<ChangeScene>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _hit = true;
    }

    void OnTriggerStay(Collider _other)
    {
        if (_lose) return;

        if (_stayTime >= _loseTime)
        {
            _lose = true;

            GameVariables.SetLosePlayer(_playerController._playerNumber);
            _scene.Change(_nextScene);
        }

        _stayTime += Time.deltaTime;
    }

    void OnTriggerExit(Collider _other)
    {
        //_stayTime = 0.0f;
        _hit = false;
    }

    private void Update()
    {
        float life = _loseTime - _stayTime;
        slider.value = life / _loseTime;

        if (_hit || _stayTime == 0.0f) return;

        _stayTime = Mathf.Min(_stayTime - Time.deltaTime, 0.0f);

    }
}
