using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private SpriteSwitcher spriteSwitcher;
    public float healthPoints = 1000.0f;
    public float TOTAL_HEALTH_POINTS = 1000.0f;
    public float newHealth;
    public float switchDelay = 0.18f;
    public float attackDelay = 2.0f;
    private float startTimer;
    private float attackSwitchTimer;
    private float actionTimer;
    private bool isStopped;
    private string action = "";
    private string enemyAction = "walk";
    public Sprite[] walkingSprites;
    public Sprite[] standingSprites;
    public Sprite[] attackSprites;
    private Transform transform;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private float switchTimer;
    private float step = 0.16f;
    private float scrollSpeed;
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject madara = GameObject.Find("madara_0");
        spriteSwitcher = madara.GetComponent<SpriteSwitcher>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoints <= 0.0f)
        {
            Destroy(gameObject); // Destroy the current object
            return; // Exit the Update function since the object is destroyed
        }
        newHealth = (float)((0.02 * healthPoints) / TOTAL_HEALTH_POINTS);
        startTimer += Time.deltaTime;
        scrollSpeed = (Time.deltaTime * 0.9f) * 3.2f;
        Vector2 pos = transform.position;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        switchTimer += Time.deltaTime;

        if (enemyAction == "walk" && i <= 5 && switchTimer > switchDelay)
        {
            switchTimer = 0.0f;
            spriteRenderer.sprite = walkingSprites[i];
            fitCollider(walkingSprites);
            pos.x -= step;
            transform.position = pos;
            i += 1;
        }
        if (enemyAction == "walk" && i > 5)
        {
            i = 0;
            switchTimer = 0.0f;
        }

        if (enemyAction == "stand" && i <= 5 && switchTimer > switchDelay)
        {
            switchTimer = 0.0f;
            spriteRenderer.sprite = standingSprites[i];
            i += 1;
            fitCollider(standingSprites);
        }
        if (enemyAction == "stand" && i > 5)
        {
            i = 0;
            switchTimer = 0.0f;
            enemyAction = "attack";
        }

        if (enemyAction == "attack" && i <= 5 && switchTimer > switchDelay)
        {
            switchTimer = 0.0f;
            spriteRenderer.sprite = attackSprites[i];
            if (i == 3)
            {
                spriteSwitcher.healthPoints -= 50.0f;
            }
            //Debug.Log("haa " + i);
            i += 1;
            fitCollider(attackSprites);
        }
        if (enemyAction == "attack" && i > 5)
        {
            i = 0;
            switchTimer = 0.0f;
            enemyAction = "stand";
            attackSwitchTimer = 0.0f;
        }

        if (Input.GetKey(KeyCode.RightArrow) && startTimer > 1.2 && !isStopped)
        {
            pos.x -= scrollSpeed;
            transform.position = pos;
        }

        actionTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            actionTimer = 0;
            isStopped = true;
            action = "attack1";
        }

        if (Input.GetKey(KeyCode.Z))
        {
            actionTimer = 0;
            isStopped = true;
            action = "attack2";
        }

        if (Input.GetKey(KeyCode.E))
        {
            actionTimer = 0;
            isStopped = true;
            action = "attack3";
        }

        if (Input.GetKey(KeyCode.R))
        {
            actionTimer = 0;
            isStopped = true;
            action = "attack4";
        }

        switch (action)
        {
            case "attack1":
                if (actionTimer > 0.5f)
                {
                    isStopped = false;
                    actionTimer = 0;
                }
                break;
            case "attack2":
                if (actionTimer > 3.28f)
                {
                    isStopped = false;
                    actionTimer = 0;
                }
                break;
            case "attack3":
                if (actionTimer > 2.2f)
                {
                    isStopped = false;
                    actionTimer = 0;
                }
                break;
            case "attack4":
                if (actionTimer > 1.1f)
                {
                    isStopped = false;
                    actionTimer = 0;
                }
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player")) // Use collision.gameObject.CompareTag
        {
            enemyAction = "stand";
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
