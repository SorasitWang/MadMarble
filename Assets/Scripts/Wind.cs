using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wind : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public Vector2 direction;
    [SerializeField]
    float force = 0.0f;

    Transform windCompassT;


    void Start()
    {
        transform.rotation = Random.rotation;
        windCompassT = GameObject.Find("WindCompass").transform;

        // TODO : init wind label
    }

    // Update is called once per frame
    void Update()
    {
        // TODO : update wind direction label 
        windCompassT.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan(direction.x / direction.y), Vector3.up);
        Debug.Log("compass" + windCompassT.rotation);
    }

}
