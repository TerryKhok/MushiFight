using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class SandwichSkill : MonoBehaviour,IPlayerSkill
{
    [SerializeField] Rigidbody _left;
    [SerializeField] Rigidbody _right;

    PlayerController _playerController;
    Vector3 _torqueZ = new Vector3(0.0f, 0.0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    public void UsingSkill()
    {
        //float rad = _playerController._headAngle * Mathf.PI / 180.0f;
        //_left.Rotate(-Mathf.Sin(rad) * 30.0f, 0.0f, Mathf.Cos(rad) * 30.0f);
        _left.AddTorque(_torqueZ * -10.0f, ForceMode.Impulse);
        _right.AddTorque(_torqueZ * 10.0f, ForceMode.Impulse);

        StartCoroutine(SandWitch());
    }

    IEnumerator SandWitch()
    {
       

        yield return null;
    }
}
