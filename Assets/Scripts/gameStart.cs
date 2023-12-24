using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack2 : MonoBehaviour
{
    public Sprite[] summoned;
    public float switchDelay = 0.07f;
    public AudioSource doorBreak;
    private SpriteRenderer spriteRenderer;
    private float switchTimer = 0.0f;
    private int i = 0;
    private bool isSwitching = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isSwitching = true; // Start sprite switching
        doorBreak = transform.Find("DestroyDoor").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSwitching)
        {
            // Increment switchTimer with Time.deltaTime
            switchTimer += Time.deltaTime;
            

            // Check if switchTimer has exceeded the switchDelay
            if (switchTimer > switchDelay)
            {
                if(i==2){
                    doorBreak.Play();
                }
                // Update sprite and reset timer
                spriteRenderer.sprite = summoned[i];
                i = (i + 1) % summoned.Length; // Use modulo to cycle through sprites
                switchTimer = 0.0f;

                if (i == 0)
                {
                    isSwitching = false; // Stop sprite switching after all sprites are shown
                }
            }
        }
    }
}
