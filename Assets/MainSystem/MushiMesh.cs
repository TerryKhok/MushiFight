using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushiMesh : MonoBehaviour
{
    [SerializeField] GameObject _kokusanMesh;
    [SerializeField] GameObject _serebesuMesh;
    [SerializeField] GameObject _kanabunMesh;

    private void Update()
    {
        Vector3 angles = transform.eulerAngles;
        angles.y = (angles.y + Time.deltaTime * 36.0f) % 360.0f;
        transform.eulerAngles = angles;
    }

    public void ChangeMesh(MushiType _type)
    {
        _kokusanMesh.SetActive(false);
        _serebesuMesh.SetActive(false);
        _kanabunMesh.SetActive(false);

        switch (_type)
        {
            case MushiType.Kokusan:
                _kokusanMesh.SetActive(true);
                break;
            case MushiType.Serebesu:
                _serebesuMesh.SetActive(true);
                break;
            case MushiType.Kanabun:
                _kanabunMesh.SetActive(true);
                break;
        }
    }
}
