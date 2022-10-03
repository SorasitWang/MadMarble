using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    const float period = 5.0f;
    const float rad = 5.0f;

    Vector3 center = new Vector3();

    Rigidbody rigidbody;

    float accTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        accTime += Time.deltaTime / 15.0f;
        if (accTime > period)
        {
            accTime = 0.0f;
        }
        // [0,360]
        float angle = (accTime / period) * 360;
        this.transform.position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f) * rad;
        //rigidbody.AddForce(new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f));
        //Debug.Log("force" + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f));
    }
}
