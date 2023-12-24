using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTwoFire : MonoBehaviour
{
    public Sprite[] attackTwoFireSprites;
    public float switchDelayFire = 0.1f;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;
    private Transform transform;
    private bool isSwitching = false;
    private float switchTimer = 0.0f;
    private float startTimer = 0.0f;
    private int i = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        transform.position = new Vector2(-6.8f, -1.44f); //this line here
        if (Input.GetKey(KeyCode.Z) && !isSwitching)
        {
            isSwitching = true;
            switchTimer = 0.0f; // Reset the timer when switching starts
        }

        if (isSwitching)
        {
            switchTimer += Time.deltaTime;
            startTimer += Time.deltaTime;

            if (startTimer > 2.9)
            {
                if (i < attackTwoFireSprites.Length && switchTimer > switchDelayFire)
                {
                    spriteRenderer.sprite = attackTwoFireSprites[i];
                    if (capsuleCollider != null && i < attackTwoFireSprites.Length - 1)
                    {
                        capsuleCollider.size = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
                    }
                    i += 1;
                    switchTimer = 0.0f;
                }

                if (i >= attackTwoFireSprites.Length)
                {
                    isSwitching = false;
                    capsuleCollider.size = new Vector2(0, 0);
                    startTimer = 0.0f;
                    i = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("enemy1"))
        {
            enemy enemyComponent = collider.GetComponent<enemy>(); // Get the enemy component

            if (enemyComponent != null)
            {
                enemyComponent.healthPoints -= 500.0f;

                if (enemyComponent.healthPoints <= 0)
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }
}
