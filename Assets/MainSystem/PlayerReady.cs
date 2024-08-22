using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ChangeScene))]
public class PlayerReady : MonoBehaviour,IMenuTrigger
{
    [SerializeField] string _nextSceneName;

    [SerializeField] TextMeshProUGUI _p1Text;
    [SerializeField] TextMeshProUGUI _p2Text;

    [ReadOnly, SerializeField] bool _p1Ready;
    [ReadOnly, SerializeField] bool _p2Ready;

    ChangeScene _scene;

    void Start()
    {
        _scene = GetComponent<ChangeScene>();
    }

    public void Excute(PlayerNumber _number)
    {
        switch (_number)
        {
            case PlayerNumber.player_01:
                _p1Ready = true;
                _p1Text.color = Color.red;
                break;
            case PlayerNumber.player_02:
                _p2Ready = true;
                _p2Text.color = Color.red;
                break;
        }

        if(_p1Ready && _p2Ready)
        {
            _scene.Change(_nextSceneName);
        }

    }

}
