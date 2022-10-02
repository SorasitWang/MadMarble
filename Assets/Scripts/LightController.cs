using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightController : MonoBehaviour
{
    /*
        Control environment light and spotlight for Blackout Event
    */


    void Start()
    {

    }

    void Update()
    {
        Vector3 marblePos = GameObject.Find("Marble").transform.position;
        marblePos.y = 1.0f;
        GameObject.Find("SpotLight").transform.position = marblePos;
    }

}
