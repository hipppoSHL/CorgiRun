using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    private CharacterController controller;
    private Vector3 moveVector;

    private float speed = 2.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;

    private float animationDuration = 3.0f;
    private float startTime;

    private bool isDead = false;

    public AudioSource CoinSound;
    public AudioSource DeathSound;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();   // charactercontroller으로 플레이어 조절
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (isDead)
        {
            // speed = 0;
            return;
        }


        else
        {
            if (Time.time - startTime < animationDuration)
            {
                controller.Move(Vector3.forward * speed * Time.deltaTime);  // 앞으로 가기
                return;
            }

            moveVector = Vector3.zero;

            if (controller.isGrounded)  // 떨어지기
            {
                verticalVelocity = -0.5f;
            }
            else
            {
                verticalVelocity -= gravity * Time.deltaTime;
            }

            // x - left and right
            moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
            // y - up and down
            moveVector.y = verticalVelocity;
            // z - forward and backward
            moveVector.z = speed;

            controller.Move(moveVector * Time.deltaTime);
        }
	}
    public void SetSpeed(float modifier)
    {
        speed = 2.0f + modifier;
    }

    void OnTriggerEnter(Collider col)   // 물체와 부딪혔을 때
    {
        if (col.gameObject.CompareTag("coin"))
        {
            Destroy(col.gameObject);
            CoinSound.Play();
            GetComponent<Score>().CoinScore();
        }

        if (col.gameObject.CompareTag("obstacle"))
        {
            DeathSound.Play();
            Death();
        }
    }

    private void Death()    // 죽었을 때
    { 
        // Destroy(gameObject);
        isDead = true;
        GetComponent<Score>().OnDeath();
        // GetComponent<TileManager>().OnDeath();
        // GetComponent<CameraMotor>().OnDeath();
    }
}