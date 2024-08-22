using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallLoseTrigger : MonoBehaviour
{
    [SerializeField] string _nextScene = "Result";
    ChangeScene _scene;

    private void Start()
    {
        _scene = GetComponent<ChangeScene>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController _controller = null;
        if(other.transform.root.TryGetComponent<PlayerController>(out _controller))
        {
            GameVariables.SetLosePlayer(_controller._playerNumber);
            _scene.Change(_nextScene);
        }
    }
}
