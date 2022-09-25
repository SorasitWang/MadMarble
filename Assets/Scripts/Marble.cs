using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
    const float SLOW = 1.5f;

    float accuTime = 0.0f;
    Rigidbody rigidbody;

    Collider collider;

    Vector3 acceleration;
    Vector3 lastVelocity = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastVelocity != new Vector3(0, 0, 0))
        {
            acceleration = (rigidbody.velocity - lastVelocity);

            rigidbody.velocity = rigidbody.velocity - acceleration / SLOW;
        }
        lastVelocity = rigidbody.velocity;
        Debug.Log("acc " + acceleration);

        accuTime += Time.deltaTime;
        if (accuTime > 3.0f)
        {
            // collider.isTrigger = true;
        }
    }
}
