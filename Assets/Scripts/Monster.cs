using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    Transform marbleT;
    float speed = 0.05f;

    Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        // random speed
    }
    public void setMarbleTransform(Transform t)
    {
        marbleT = t;
    }


    // Update is called once per frame
    void Update()
    {
        // move to marble
        Vector3 dir = Vector3.Normalize(marbleT.position - transform.position);
        dir.y = 0.0f;
        transform.localPosition += speed * dir * Time.deltaTime;
        //rigidbody.AddForce(speed * dir);

        // handle marble go down to plate

    }

    private void OnCollisionEnter(Collision other)
    {
    }
}
