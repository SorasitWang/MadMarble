using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Star : MonoBehaviour
{

    void Start()
    {
        //transform.localPosition = Util.Util.randomPos(0.8f);
        // WARNING! Plate must be initiated

        //this.transform.parent = GameObject.Find("Plate").transform;

    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Marble")
        {
            InGameManager m = (InGameManager)GameObject.Find("InGameManager").GetComponent(typeof(InGameManager));
            m.collectStar();
        }
    }

}


