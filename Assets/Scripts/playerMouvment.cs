using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite[] RunningSprites; // Array to hold your sprite images
    public Sprite[] jumpSprites;
    public Sprite[] attackOneSprites;
    public Sprite[] attackTwoSprites;
    public Sprite[] attackThreeSprites;
    public Sprite[] attackFourSprites;
    public Sprite[] restingSprites;
    public float switchDelay = 0.1f; // Delay between sprite switches
    public float switchDelayJump = 0.1f;
    public float switchDelayAttack1 = 0.1f;
    public float switchDelayAttack2 = 0.14f;
    public float switchDelayAttack3 = 0.1f;
    public float switchDelayAttack4 = 0.1f;
    public float switchDelayResting = 0.1f;
    public AudioSource runningAudio;
    public AudioSource attackOneAudio;
    public AudioSource handSignsAudio;
    public AudioSource fireAudio;
    private enemy enemyy;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private bool isRunning = false;
    private string action = "";
    private float switchTimer = 0.0f;
    private float startTimer = 0.0f;
    private Transform transform;
    private int i = 0;
    public float healthPoints = 1000.0f;
    private float TOTAL_HEALTH_POINTS = 1000.0f;
    public float newHealth;

    private void Start()
    {
        transform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject killer = GameObject.Find("enemy1");
        enemyy = killer.GetComponent<enemy>();
        boxCollider = GetComponent<BoxCollider2D>();
        runningAudio = transform.Find("Running").GetComponent<AudioSource>();
        attackOneAudio = transform.Find("Audio").GetComponent<AudioSource>();
        handSignsAudio = transform.Find("HandSigns").GetComponent<AudioSource>();
        fireAudio = transform.Find("FireAudio").GetComponent<AudioSource>();
    }

    private void Update()
    {
        newHealth = (float)((0.62 * healthPoints) / TOTAL_HEALTH_POINTS);
        if (action != "jump")
        {
            transform.position = new Vector2(-7.0f, -1.790011f);
        }
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        startTimer += Time.deltaTime;
        Vector2 pos = transform.position;

        if (startTimer > 1.2 && (action == "" || action == "resting"))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                isRunning = true;
                if (!runningAudio.isPlaying)
                {
                    runningAudio.Play(); // Play the audio if it's not already playing
                }
            }
            if (Input.GetKey(KeyCode.RightArrow) == false && action == "")
            {
                action = "resting";
                isRunning = false;
                switchTimer = 0.0f; // Reset the timer when key is released
                if (runningAudio.isPlaying)
                {
                    runningAudio.Stop(); // Stop the audio if the key is released
                }
            }

            if (isRunning)
            {
                if (action == "resting")
                {
                    action = "";
                }
                switchTimer += Time.deltaTime;

                if (i <= 5 && switchTimer >= switchDelay)
                {
                    SwitchToNextSprite(RunningSprites);
                    fitCollider(RunningSprites);
                }
                if (i > 5)
                {
                    reset();
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                action = "jump";
                i = 0;
            }

            if (Input.GetKey(KeyCode.A))
            {
                action = "attack1";
                attackOneAudio.Play();
                i = 0;
            }

            if (Input.GetKey(KeyCode.Z))
            {
                action = "attack2";
                handSignsAudio.Play();
                i = 0;
            }

            if (Input.GetKey(KeyCode.E))
            {
                action = "attack3";
                attackOneAudio.Play();
                i = 0;
            }

            if (Input.GetKey(KeyCode.R))
            {
                action = "attack4";
                attackOneAudio.Play();
                i = 0;
            }
        }

        switch (action)
        {
            case "jump":
                switchTimer += Time.deltaTime;
                if (i <= 5 && switchTimer > switchDelayJump)
                {
                    if (i == 1)
                    {
                        pos.y += 0.2f;
                        transform.position = pos;
                    }
                    if (i == 2)
                    {
                        pos.y += 0.4f;
                        transform.position = pos;
                    }
                    fitCollider(jumpSprites);
                    SwitchToNextSprite(jumpSprites);

                }
                if (i > 5)
                {
                    reset();
                }
                break;
            case "attack1":
                switchTimer += Time.deltaTime; // Accumulate time correctly

                if (i <= 4 && switchTimer > switchDelayAttack1)
                {
                    fitCollider(attackOneSprites);
                    SwitchToNextSprite(attackOneSprites);
                    
                }
                if (i > 4)
                {
                    reset();
                }
                CheckCollisionWithBoxCollider2D();
                break;
            case "attack2":
                switchTimer += Time.deltaTime; // Accumulate time correctly

                if (i == 12)
                {
                    if (!fireAudio.isPlaying)
                    {
                        fireAudio.Play(); // Play the audio only if it's not already playing
                    }
                }

                if (i <= 22 && switchTimer > switchDelayAttack2)
                {
                    fitCollider(attackTwoSprites);
                    SwitchToNextSprite(attackTwoSprites);
                }
                if (i > 22)
                {
                    reset();
                }
                break;
            case "attack3":
                switchTimer += Time.deltaTime; // Accumulate time correctly

                if (i <= 21 && switchTimer > switchDelayAttack3)
                {
                    fitCollider(attackThreeSprites);
                    SwitchToNextSprite(attackThreeSprites);
                }
                if (i > 21)
                {
                    reset();
                }
                CheckCollisionWithBoxCollider2D();
                break;
            case "attack4":
                switchTimer += Time.deltaTime; // Accumulate time correctly

                if (i <= 10 && switchTimer > switchDelayAttack4)
                {
                    if (i == 2)
                    {
                        pos.y += 0.2f;
                        transform.position = pos;
                        
                    }
                    if (i == 8)
                    {
                        pos.y += 0.4f;
                        transform.position = pos;
                    }
                    fitCollider(attackFourSprites);
                    SwitchToNextSprite(attackFourSprites);
                }
                if (i > 10)
                {
                    reset();
                }
                CheckCollisionWithBoxCollider2D();
                break;
            case "resting":
                switchTimer += Time.deltaTime; // Accumulate time correctly

                if (i <= 3 && switchTimer > switchDelayResting)
                {
                    //fitCollider(restingSprites);
                    SwitchToNextSprite(restingSprites);
                }
                if (i > 3)
                {
                    reset();
                }
                break;
            default:
                break;
        }
    }

    private void SwitchToNextSprite(Sprite[] sprites)
    {
        spriteRenderer.sprite = sprites[i];
        i += 1;
        switchTimer = 0.0f;
    }

    private void reset()
    {
        switchTimer = 0.0f;
        action = "resting";
        i = 0;
    }

    private void CheckCollisionWithBoxCollider2D()
    {
        Vector2 spriteSize = spriteRenderer.bounds.size;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, spriteSize, 0); // Adjust boxColliderSize and layerMask if needed

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("enemy1") && (action=="attack3" || action=="attack1" || action=="attack4")) // Replace "BoxTag" with the actual tag of your BoxCollider2D
            {
                enemy enemyComponent = collider.GetComponent<enemy>();
                enemyComponent.healthPoints -= 50.0f;
            }
        }
    }

    private void fitCollider(Sprite[] sprites)
    {
        if (boxCollider != null && i < sprites.Length - 1)
        {
            boxCollider.size = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
        }
    }

}