using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private Vector2 _velocity;
    private float _minGroundNormalY = .65f;
    private float _gravityModifier = 5f;
    private float _speedBooster = 10f;

    private const float _minMovementDistance = 0.001f;
    private const float _shellRadius = 0.01f;

    private Vector2 _targetVelocity;
    private Vector2 _groundNormal;
    private Rigidbody2D _rb2d;
    private ContactFilter2D _contactFilter2D;

    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    protected bool Grounded;
    protected PlayerMovement PlayerHorisontalImput;
    protected PlayerMovement PlayerVerticalImput;


    private void Awake()
    {
        PlayerHorisontalImput = new PlayerMovement();
        PlayerVerticalImput = new PlayerMovement();
        PlayerHorisontalImput.Enable();
    }

    private void OnEnable()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        PlayerVerticalImput.Enable();
    }

    private void OnDisable()
    {
        PlayerVerticalImput.Disable();
    }

    private void Start()
    {
        _contactFilter2D.useTriggers = false;
        _contactFilter2D.SetLayerMask(_layerMask);
        _contactFilter2D.useLayerMask = true;
    }

    private void Update()
    {
        _targetVelocity = PlayerHorisontalImput.Player.Move.ReadValue<Vector2>() * _speedBooster;

        if (PlayerVerticalImput.Player.Jump.triggered && Grounded)
            _velocity.y = 35;
    }

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;

        Grounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > _minMovementDistance)
        {
            int count = _rb2d.Cast(move, _contactFilter2D, _hitBuffer, distance + _shellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;
                if (currentNormal.y > _minGroundNormalY)
                {
                    Grounded = true;
                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - _shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rb2d.position = _rb2d.position + move.normalized * distance;
    }
}
