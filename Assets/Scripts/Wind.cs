using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wind : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    Vector3 direction;
    [SerializeField]
    float force = 0.0f;


    void Start()
    {
        transform.rotation = Random.rotation;
        GameObject.Find("WindCompass").transform.LookAt(GameObject.Find("MainCamera").transform);

        // TODO : init wind label
    }

    // Update is called once per frame
    void Update()
    {
        // TODO : update wind direction label 
    }

}
