using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    
    public float rotationSpeed = 50.0f;
    public float scaleSpeed = 0.1f;

    // Update is called once per frame
    void Update() {
        // Rotate the object slightly
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Scale the object up and down slightly
        float scaleFactor = Mathf.PingPong(Time.time * scaleSpeed, 0.2f) + 0.8f;
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}

