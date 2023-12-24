using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerNewController : MonoBehaviour
{
    #region Moving

    public float speed;
    public float runningSpeed;
    private float _horizontalInput;

    #endregion

    #region Jumping

    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool _isGrounded;
    private bool _groundHit;

    #endregion

    #region Fliping

    private bool _facingRight = false;

    #endregion

    #region LedgeClimbing

    [SerializeField] private LedgeDetection ledgeDetection;
    [SerializeField] private Vector2 offset0;
    [SerializeField] private Vector2 offset1;
    private Vector2 _beforeClimbPosition;
    private Vector2 _afterClimbPosition;
    private bool _canGrabLedge = true;
    private bool _canClimb;
    private bool _canGoOn;

    #endregion
    
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    
    private bool _automatGround;
    public LayerMask layer;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (!_canClimb)
        {
            Move();
            FlipSprite();
        }
        Jump();
        LedgeClimb();
        
        _animator.SetFloat("YVelocity", _rigidbody2D.velocity.y);
        _animator.SetBool("canLedgeClimb", _canClimb);

        Debug.Log(_horizontalInput);
    }

    private void FixedUpdate()
    {
        if (_isGrounded && !_groundHit && Mathf.Abs(_rigidbody2D.velocity.y) < 0.05f)
        {
            _animator.SetBool("Jumping", false);
            _animator.SetBool("Landing", true);
        }

        if (_automatGround && !_groundHit && Mathf.Abs(_rigidbody2D.velocity.y) < 0.05f)
        {
            _animator.SetBool("Jumping", false);
            _animator.SetBool("Landing", true);
        }
    }

    private void Move()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
    
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runningSpeed : speed;
    
        _rigidbody2D.velocity = new Vector2(_horizontalInput * moveSpeed, _rigidbody2D.velocity.y);
    }
    
    private void Jump()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.01f, groundLayer);
        _automatGround = Physics2D.OverlapCircle(groundCheck.position, 0.01f, layer);
        
        if ((_isGrounded || _automatGround) && Input.GetButtonDown("Jump"))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
            _animator.SetBool("Jumping", true);
            _animator.SetBool("Landing", false);
            _groundHit = false;
        }
    }
    
    private void FlipSprite()
    {
        _animator.SetBool("Sprint", _horizontalInput is > 0 or < 0);
            
        if (_horizontalInput < 0f && !_facingRight)
        {
            Flip();
            offset0 = new Vector2(0.2f, -2);
            offset1 = new Vector2(-0.4f, -0.071f);
        }
        if (_horizontalInput > 0f && _facingRight)
        {
            Flip();
            offset0 = new Vector2(-0.2f, -2);
            offset1 = new Vector2(0.4f, 0.071f);
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }
    
    private void LedgeClimb()
    {
        if (ledgeDetection.ledgeDetect && _canGrabLedge)
        {
            _canGrabLedge = false;

            Vector2 ledgePosition = ledgeDetection.transform.position;

            _beforeClimbPosition = ledgePosition + offset0;
            _afterClimbPosition = ledgePosition + offset1;

            _canClimb = true;
        }

        if (_canClimb)
            transform.position = _beforeClimbPosition;
    }

    private void LedgeClimbOver()
    {
        _canClimb = false;
        CanGrabLedgeTimer();
        transform.position = _afterClimbPosition;
    }
    
    private async void CanGrabLedgeTimer()
    {
        await Task.Delay(100);
        _canGrabLedge = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Automat")
        {
            jumpForce = 8;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Automat")
        {
            jumpForce = 7;
        }
    }
}
