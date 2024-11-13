using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{   
    public static Player Instance;
    private Transform model;
    private Rigidbody2D rb;
    public Transform groundCheck;
    public Vector2 boxSize;
    public LayerMask groundLayer;
    private AudioManager audioManager;
    
    private GameObject lastHoveredObject = null;
    [Header("effect")]
    [SerializeField] private float bounceScale = 1.1f; 
    [SerializeField] private float bounceDuration = 0.45f;

    public FixedJoystick joystick;
    // player movement
    [SerializeField] private PlayerInput controls;
    private Vector2 moveInput;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [SerializeField] 
    private TrailRenderer trail;

    //effect when die
    [SerializeField] private GameObject explosionEffect;
    private void Awake()
    {
        Instance = this;
        controls = new PlayerInput();
        controls.PlayerControls.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.PlayerControls.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.PlayerControls.Jump.performed += ctx => Jump();
    }
    private void Start()
    {
        //InputEvent();
        audioManager = AudioManager.instance;
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<TrailRenderer>();
        trail.emitting = false;
        model = transform.Find("Model");
    }
    private void Update()
    {
        //.Translate(moveInput * moveSpeed * Time.deltaTime);
        Move();

        Debug.DrawRay(groundCheck.position + new Vector3(boxSize.x / 2, 0), Vector2.down * 0.1f, Color.red);
        Debug.DrawRay(groundCheck.position - new Vector3(boxSize.x / 2, 0), Vector2.down * 0.1f, Color.red);
        Debug.DrawRay(groundCheck.position + new Vector3(0, boxSize.y / 2), Vector2.down * 0.1f, Color.red);
        Debug.DrawRay(groundCheck.position - new Vector3(0, boxSize.y / 2), Vector2.down * 0.1f, Color.red);

        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);


        if (hit.collider != null)
        {

            if (hit.collider.gameObject.tag == "HasColorMixer")
            {
                ColorMixer colorMixer = hit.collider.gameObject.GetComponentInChildren<ColorMixer>();
                colorMixer.OnPointerEnter();
                lastHoveredObject = colorMixer.transform.gameObject;
            }
        }
        else
        {
            if (lastHoveredObject != null)
            {
                lastHoveredObject.GetComponentInChildren<ColorMixer>().OnPointerExit();
                lastHoveredObject = null;
            }
        }
    }

    private void Move()
    {
        rb.velocity = new Vector3(moveInput.x*moveSpeed, rb.velocity.y, 0);
        /*if (moveInput.x > 0)
        {
            model.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            model.localScale = new Vector3(-1, 1, 1);
        }*/
        MoveEffect();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    public void Jump() 
    {
        if (!IsGrounded())
        {
            return;
        }
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        audioManager.PlayJumpSound();
        Bounce();
    }
    private bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
    }
    #region effect
    private void MoveEffect()
    {   
        float velocityX = rb.velocity.x;
        if (velocityX != 0)
        {
            trail.emitting = true;
        }
        else
        {
            trail.emitting = false;
        }
        if (velocityX < 0)
        {
            model.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (velocityX > 0)
        {
            model.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void Bounce()
    {
        model.DOScale(new Vector3(bounceScale, 1.2f, 1f), bounceDuration / 2)
            .OnComplete(() =>model.DOScale(Vector3.one, bounceDuration / 2));
    }
    #endregion
    public void OnDead()
    {
        GameObject obj= Instantiate(explosionEffect, transform.position, Quaternion.identity);
        obj.SetActive(true);
        Destroy(gameObject);
    }
}
