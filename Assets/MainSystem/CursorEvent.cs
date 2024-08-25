using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CursorEvent : MonoBehaviour, IMenuTrigger
{

    [SerializeField] UnityEvent _event = new UnityEvent();

    // Start is called before the first frame update
    public void Excute(PlayerNumber _number)
    {
        _event.Invoke();
    }
}
