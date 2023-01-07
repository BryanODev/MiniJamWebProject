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

    private void Awake()
    {
        spiderRB = GetComponent<Rigidbody2D>();
        spiderAnimator = GetComponent<Animator>();

        playerInputs = new PlayerInputs();
        playerInputs.Enable();
    }

    private void Start()
    {
        SetUpPlayerInputs();
    }

    private void OnEnable()
    {
        playerInputs.Enable();
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
    }

    private void FixedUpdate()
    {
        SpiderVelocity.x = movementInput.x * spiderMoveSpeed * Time.fixedDeltaTime;
        SpiderVelocity.y = spiderRB.velocity.y;

        spiderRB.velocity = SpiderVelocity;
    }

    void FireWeb() 
    {
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
