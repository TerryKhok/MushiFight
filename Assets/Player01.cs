using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player01 : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed;
    public InputActionReference movement;
    public InputActionReference headMovement;
    public InputActionReference skill;
    public HingeJoint headJoint;
    public Transform head;
    public Transform lookAt;

    private Vector2 _moveDirection;
    private Vector2 _headDirection;

    void Start()
    {

    }

    void Update()
    {
        _moveDirection = movement.action.ReadValue<Vector2>();
        _headDirection = headMovement.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed * 0.01f);
        //  lookAt.position = new Vector3(lookAt.position.x,head.position.y + _headDirection.y,lookAt.position.z);
        //  head.LookAt(lookAt);
    }

    private void OnEnable()
    {
        skill.action.started += UseSkill;
    }

    private void OnDisable()
    {
        skill.action.started -= UseSkill;
    }

    private void UseSkill(InputAction.CallbackContext obj)
    {
        StartCoroutine(UsingSkill());
    }

    IEnumerator UsingSkill()
    {
        var motor = headJoint.motor;
        motor.force = 500;
        motor.targetVelocity = -500;
        motor.freeSpin = false;
        headJoint.motor = motor;
        headJoint.useMotor = true;
        yield return new WaitForSeconds(.3f);
        motor.force = 0;
        motor.targetVelocity = 0;
        motor.freeSpin = false;
        headJoint.motor = motor;
        headJoint.useMotor = false;
    }
}
