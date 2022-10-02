using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
public class Marble : MonoBehaviour, Playable
{
    const float SLOW = 1.5f;

    float accuTime = 0.0f;

    List<DangerHole> dangerHoles;
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

        dangerHoles = new List<DangerHole>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!running) return;


        // effect from hole force
        Debug.Log("force" + holeEffect());
        if (lastVelocity != new Vector3(0, 0, 0))
        {
            acceleration = (rigidbody.velocity - lastVelocity);

            rigidbody.velocity = rigidbody.velocity - acceleration / SLOW;

            rigidbody.velocity += holeEffect() * Time.deltaTime;

        }
        lastVelocity = rigidbody.velocity;
        accuTime += Time.deltaTime;

        rigidbody.AddForce(new Vector3(1, 0, 0) * 0.01f);





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

    public void storeHoles(List<DangerHole> holes)
    {
        this.dangerHoles = holes;
    }

    private Vector3 holeEffect()
    {
        // find acc vector from hole force
        Vector3 re = new Vector3();
        Vector3 force, dir;
        for (int i = 0; i < dangerHoles.Count; i++)
        {
            Vector3 tmp = dangerHoles[i].calculateForce(transform.position);
            Debug.DrawRay(transform.position, tmp, Color.green, 1.0f);
            re += tmp;
        }
        return re;

    }

}
