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

    public GameObject ChangeMesh(MushiType _type)
    {
        _kokusanMesh.SetActive(false);
        _serebesuMesh.SetActive(false);
        _kanabunMesh.SetActive(false);

        GameObject targetObj = null;

        switch (_type)
        {
            case MushiType.Kokusan:
                targetObj = _kokusanMesh;
                break;
            case MushiType.Serebesu:
                targetObj = _serebesuMesh;
                break;
            case MushiType.Kanabun:
                targetObj = _kanabunMesh;
                break;
        }

        targetObj.SetActive(true);

        return targetObj;
    }
}
