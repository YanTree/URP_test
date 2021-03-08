using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour {
    public float fallMultiplayer = 2.5f;
    Rigidbody rb;
	void Awake () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * -9.8f * (fallMultiplayer) * Time.deltaTime;
        }
	}
}
