using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float flightSpeed;

    private Rigidbody _rb;
    private Vector3 _velocity;

    public Vector3 Velocity => _velocity;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _velocity = Vector3.forward * flightSpeed * GameManager.Instance.ScoreManager.VelocityFractionRestricted * Time.deltaTime;

        _velocity += Vector3.right * InputHandler.Instance.GetHorizontalRaw() * movementSpeed * Time.deltaTime;
        _velocity += Vector3.up * InputHandler.Instance.GetVerticalRaw() * movementSpeed * Time.deltaTime;

        _rb.MovePosition(_rb.position + _velocity);
    }
}
