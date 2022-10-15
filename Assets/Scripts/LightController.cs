using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightController : MonoBehaviour
{
    /*
        Control environment light and spotlight for Blackout Event
    */

    GameObject spotLight;
    void Start()
    {
        spotLight = GameObject.Find("SpotLight");
    }

    void Update()
    {
        Vector3 marblePos = Marble.Instance.transform.position;
        marblePos.y = 1.0f;
        spotLight.transform.position = marblePos;
    }

}
