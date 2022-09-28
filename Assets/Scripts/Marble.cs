using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour, Playable
{
    const float SLOW = 1.5f;

    float accuTime = 0.0f;
    Rigidbody rigidbody;

    Collider collider;

    Vector3 acceleration;
    Vector3 lastVelocity = new Vector3(0, 0, 0);

    bool running;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        collider = GetComponent<Collider>();

        running = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!running) return;
        if (lastVelocity != new Vector3(0, 0, 0))
        {
            acceleration = (rigidbody.velocity - lastVelocity);

            rigidbody.velocity = rigidbody.velocity - acceleration / SLOW;
        }
        lastVelocity = rigidbody.velocity;
        //Debug.Log("acc " + acceleration);

        accuTime += Time.deltaTime;
        if (accuTime > 3.0f)
        {
            // collider.isTrigger = true;
        }
    }

    public void resume()
    {
        running = true;
        rigidbody.constraints = RigidbodyConstraints.None;
    }

    public void stop()
    {
        running = false;
        rigidbody.constraints = RigidbodyConstraints.FreezePosition;
    }

}
