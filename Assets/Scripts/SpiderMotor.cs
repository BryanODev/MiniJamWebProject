using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class SpiderMotor : MonoBehaviour
{
    public float spiderMoveSpeed;
    public Camera playerCamera;

    InputManager inputManager;
    Rigidbody2D spiderRB;
    Animator spiderAnimator;
    BoxCollider2D spiderCollider;

    Vector2 SpiderVelocity;
    [SerializeField] Vector2 movementInput;
    Vector2 cursorPosition;

    public GameObject SpiderWebProjectile;

    public bool isGrounded = false;
    public LayerMask groundable;

    bool isAlive = true;

    AudioSource spiderAudioSource;
    public AudioClip[] JumpClips;
    public AudioClip[] ShootingClips;
    public AudioClip deathClip;

    Coroutine FireWebCoroutine;

    private void Awake()
    {
        spiderRB = GetComponent<Rigidbody2D>();
        spiderAnimator = GetComponent<Animator>();

        inputManager = InputManager.Instance;

        spiderAudioSource = GetComponent<AudioSource>();
        spiderCollider = GetComponent<BoxCollider2D>();

        //We need to make sure the gamemode instance spawns first!
        GameMode.Instance.onStartGameDelegate += OnGameStart;
    }

    private void Start()
    {
        SetUpPlayerInputs();
    }

    void OnGameStart() 
    {
        Debug.Log("Spider is now playing");
    }

    void SetUpPlayerInputs()
    {

#if UNITY_STANDALONE_WIN || UNITY_WEBGL || UNITY_EDITOR

#endif

#if UNITY_ANDROID || UNITY_IOS

#endif

        inputManager.GetPlayerInputsClass().Keyboard.Move.performed += ctx => AddMovementInput(ctx.ReadValue<float>());
        inputManager.GetPlayerInputsClass().Keyboard.Fire.performed += ctx => WantsToFireWeb();

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

    void WantsToFireWeb() 
    {
        if (FireWebCoroutine == null && gameObject.activeSelf) 
        {
            FireWebCoroutine = StartCoroutine(FireWeb());
        }
    }

    IEnumerator FireWeb() 
    {
        if (!isAlive)
        {
            FireWebCoroutine = null;
            yield return null;
        }

        spiderAudioSource.PlayOneShot(GetRandomShootingAudioClip());

        if (isGrounded) 
        {
            LaunchSpider(Vector2.up, 800);
            FireWebCoroutine = null;
            yield return null;
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

        yield return new WaitForSeconds(0.3f);

        FireWebCoroutine = null;

        yield return null;
    }

    public void AddMovementInput(float axis) 
    {
        if (!isAlive) 
        {
            movementInput.x = 0;
            return;
        }

        movementInput.x = axis;
    }

    public void LaunchSpider(Vector3 Direction, float Force) 
    {
        if (!isAlive)
        {
            return;
        }

        spiderAudioSource.clip = GetRandomJumpAudioClip();

        if (spiderAudioSource.clip != null)
        {
            spiderAudioSource.Play();
        }

        spiderAnimator.SetTrigger("Jump");
        spiderRB.velocity = Vector2.zero;
        spiderRB.AddForce(Direction * Force);
    }

    private void OnDisable()
    {
        if (AudioManager.Instance) 
        {
            //Stop the game music
            AudioManager.Instance.StopAudio();

            //Play on Death
            AudioManager.Instance.PlayAudioOneShot(deathClip);
        }

        //If we become disabled, means we lost!
        GameMode.Instance.OnPlayerLost();
    }

    AudioClip GetRandomJumpAudioClip()
    {
        int randomJump = Random.Range(0, JumpClips.Length);

        if (randomJump < JumpClips.Length)
        {
            if (JumpClips[randomJump] != null)
            {
                return JumpClips[randomJump];
            }
        }

        return null;
    }

    AudioClip GetRandomShootingAudioClip()
    {
        int randomJump = Random.Range(0, ShootingClips.Length);

        if (randomJump < ShootingClips.Length)
        {
            if (ShootingClips[randomJump] != null)
            {
                return ShootingClips[randomJump];
            }
        }

        return null;
    }

    public void Kill(Vector3 KillDirection) 
    {
        if (spiderAnimator != null) 
        {
            if (AudioManager.Instance)
            {
                AudioManager.Instance.StopAudio();
                AudioManager.Instance.PlayAudioOneShot(deathClip);
            }

            spiderAnimator.SetBool("IsDead", true);
            isAlive = false;
            spiderCollider.isTrigger = true;

            movementInput.x = 0;
            spiderRB.velocity = Vector2.zero;

            spiderRB.constraints = RigidbodyConstraints2D.None;
            KillDirection.Normalize();
            LaunchSpider(KillDirection, 200);
        }
    }

}
