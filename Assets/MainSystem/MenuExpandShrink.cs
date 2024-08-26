using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuExpandShrink : MonoBehaviour
{
    [SerializeField] GameObject _uiObject;
    [SerializeField] float _scaleRate;
    float _scaleNumber = 0;
    bool _reverse = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_reverse)
        {
            _scaleNumber = _scaleNumber + _scaleRate;
            _uiObject.transform.localScale = new Vector3(3 + _scaleNumber, 3 + _scaleNumber, 3 + _scaleNumber);
            if (_scaleNumber >= .20f)
            {
                _reverse = !_reverse;
            }
        }
        else
        {
            _scaleNumber = _scaleNumber - _scaleRate;
            _uiObject.transform.localScale = new Vector3(3 + _scaleNumber, 3 + _scaleNumber, 3 + _scaleNumber);
            if (_scaleNumber <= 0f)
            {
                _reverse = !_reverse;
            }
        }
    }
}
