using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageEnemyHealth : MonoBehaviour
{
    private enemy enemyy; // Reference to the script attached to Madara
    private Transform transform;

    private void Start()
    {
        
        // Get the GameObject "madara_0" using its name
        GameObject madara = Resources.Load<GameObject>("enemy1");
        transform = GetComponent<Transform>();

        // Check if madara GameObject exists and has the Enemyy component
        if (madara != null)
        {
            enemyy = madara.GetComponent<enemy>();
        }
        else
        {
            Debug.LogWarning("GameObject 'madara_0' not found or does not have Enemyy component.");
        }
    }

    private void Update()
    {
        if (enemyy != null)
        {
            // Access the attribute from Enemyy
            float healthPoints = enemyy.healthPoints;
            float newHealth = enemyy.newHealth;
            Debug.Log(newHealth);

            transform.localScale = new Vector3(newHealth, transform.localScale.y, 1.0f);
        }

    }
}