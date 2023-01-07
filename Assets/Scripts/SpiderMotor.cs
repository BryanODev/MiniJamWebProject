using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class SpiderMotor : MonoBehaviour
{
    public float spiderMoveSpeed;
    public Camera playerCamera;
    PlayerInputs playerInputs;
    Rigidbody2D spiderRB;
    Animator spiderAnimator;

    Vector2 SpiderVelocity;
    [SerializeField] Vector2 movementInput;
    Vector2 cursorPosition;

    public GameObject SpiderWebProjectile;

    public bool isGrounded = false;
    public LayerMask groundable;

    private void Awake()
    {
        spiderRB = GetComponent<Rigidbody2D>();
        spiderAnimator = GetComponent<Animator>();

        playerInputs = new PlayerInputs();
        playerInputs.Enable();

        //We need to make sure the gamemode instance spawns first!
        GameMode.Instance.onStartGameDelegate += OnGameStart;
    }

    private void Start()
    {
        SetUpPlayerInputs();
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    void OnGameStart() 
    {
        Debug.Log("Spider is now playing");
    }

    void SetUpPlayerInputs()
    {
        playerInputs.Keyboard.Move.performed += ctx => AddMovementInput(ctx.ReadValue<float>());
        playerInputs.Keyboard.Fire.performed += ctx => FireWeb();

        //Debuging only
        playerInputs.Keyboard.ResetLevel.performed += ctx => ResetLevel();
    }

    private void Update()
    {
        spiderAnimator.SetFloat("YVelocity", spiderRB.velocity.y);
        spiderAnimator.SetBool("IsGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();

        SpiderVelocity.x = movementInput.x * spiderMoveSpeed * Time.fixedDeltaTime;
        SpiderVelocity.y = spiderRB.velocity.y;

        spiderRB.velocity = SpiderVelocity;
    }

    bool IsGrounded() 
    {
        if(spiderRB.velocity.y == 0) 
        {
            Debug.Log("grouded");
            return true;
        }

        if (Physics2D.Raycast(transform.position, Vector2.down, 1f, groundable)) 
        {
            return true;
        }

        return false;
    }

    void FireWeb() 
    {
        if (isGrounded) 
        {
            LaunchSpider(Vector2.up, 800);

            return;
        }

        //Debug.Log("Fire Web!");

        //cursorPosition = playerCamera.ScreenToWorldPoint(playerInputs.Keyboard.Mouse.ReadValue<Vector2>());

        //For now, instantiate a projectile
        SpiderWebProjectile spiderWebProjectileComponent = Instantiate(SpiderWebProjectile, transform.position, Quaternion.identity).GetComponent<SpiderWebProjectile>();

        if (spiderWebProjectileComponent)
        {
            //Vector2 direction = cursorPosition - (Vector2) spiderWebProjectileComponent.transform.position;
            Vector2 direction = Vector2.down;
            direction.Normalize();


            spiderWebProjectileComponent.Instigator = gameObject;
            spiderWebProjectileComponent.SetProjectileDirection(direction);
        }
    }

    public void AddMovementInput(float axis) 
    {
        movementInput.x = axis;
    }

    public void LaunchSpider(Vector3 Direction, float Force) 
    {
        spiderAnimator.SetTrigger("Jump");
        spiderRB.velocity = Vector2.zero;
        spiderRB.AddForce(Direction * Force);
    }

    private void OnDisable()
    {
        playerInputs.Disable();

        //If we become disabled, means we lost!
        GameMode.Instance.OnPlayerLost();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(cursorPosition, .25f);
    }

    void ResetLevel() 
    {
        SceneManager.LoadScene(0);
    }

}
