using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managePlayerHealth : MonoBehaviour
{
    private SpriteSwitcher spriteSwitcher; // Reference to the script attached to Madara
    private Transform transform;

    private void Start()
    {
        // Get the GameObject "madara_0" using its name
        GameObject madara = GameObject.Find("madara_0");
        transform = GetComponent<Transform>();

        // Check if madara GameObject exists and has the SpriteSwitcher component
        if (madara != null)
        {
            spriteSwitcher = madara.GetComponent<SpriteSwitcher>();
        }
        else
        {
            Debug.LogWarning("GameObject 'madara_0' not found or does not have SpriteSwitcher component.");
        }
    }

    private void Update()
    {
        if (spriteSwitcher != null)
        {
            // Access the attribute from SpriteSwitcher
            float healthPoints = spriteSwitcher.healthPoints;
            float newHealth = spriteSwitcher.newHealth;

            transform.localScale = new Vector3(newHealth, transform.localScale.y, 1.0f);
        }



    }
}
