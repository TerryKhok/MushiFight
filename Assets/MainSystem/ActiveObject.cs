using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : MonoBehaviour
{
    [SerializeField] List<GameObject> _objects;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (var obj in _objects)
        {
            obj.SetActive(true);
        }
    }
}
