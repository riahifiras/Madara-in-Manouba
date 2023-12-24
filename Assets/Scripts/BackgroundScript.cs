using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float scrollSpeed = 0.5f;
    private float startTimer;
    private float actionTimer;
    private bool isStopped;
    private string action = "";
    private float offset;
    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        startTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow) && startTimer > 1.2 && !isStopped)
        {
            offset += (Time.deltaTime * scrollSpeed) / 6f;
            mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }

        actionTimer += Time.deltaTime;

        if(Input.GetKey(KeyCode.A)){
            actionTimer = 0;
            isStopped = true;
            action = "attack1";
        }

        if(Input.GetKey(KeyCode.Z)){
            actionTimer = 0;
            isStopped = true;
            action = "attack2";
        }

        if(Input.GetKey(KeyCode.E)){
            actionTimer = 0;
            isStopped = true;
            action = "attack3";
        }

        if(Input.GetKey(KeyCode.R)){
            actionTimer = 0;
            isStopped = true;
            action = "attack4";
        }

        switch (action)
        {
            case "attack1":
            if(actionTimer > 0.5f){
                isStopped = false;
                actionTimer = 0;
            }
            break;
            case "attack2":
            if(actionTimer > 3.28f){
                isStopped = false;
                actionTimer = 0;
            }
            break;
            case "attack3":
            if(actionTimer > 2.2f){
                isStopped = false;
                actionTimer = 0;
            }
            break;
            case "attack4":
            if(actionTimer > 1.1f){
                isStopped = false;
                actionTimer = 0;
            }
            break;
            default:
            break;
        }
    }
}
