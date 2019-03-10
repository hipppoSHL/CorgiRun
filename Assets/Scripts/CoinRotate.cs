using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour {

    public float speed = 5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // transform.rotation = Quaternion.Euler(0, 0, 90);
        transform.Rotate(0, 0, speed);
    }
}
