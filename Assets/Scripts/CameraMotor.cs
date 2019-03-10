using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {

    private Transform lookAt;
    private Vector3 startOffset;
    private Vector3 moveVector;

    private float transition = 0.0f;
    private float animationDuration = 3.0f;
    private Vector3 animationOffset = new Vector3(0, 3, 3);
    private bool isDead = false;


	// Use this for initialization
	void Start () {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (isDead)
            return;

        moveVector = lookAt.position + startOffset;

        // x
        moveVector.x = 0;
        // y
        moveVector.y = Mathf.Clamp(moveVector.y, 0, 1);
        // z
        // camera transform
        if (transition > 1.0f)
        {
            transform.position = moveVector;
        }
        else
        {
            // animation at the start of the game
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(lookAt.position + Vector3.up);
        }
	}
    public void OnDeath()
    {
        isDead = true;
    }
}
