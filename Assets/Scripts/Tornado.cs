using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    // Start is called before the first frame update


    Vector3 destination;

    const float speed = 0.03f;
    void Start()
    {
        StartCoroutine(tornadoMovement());


    }

    IEnumerator tornadoMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(7, 10));
            destination = Util.Util.randomPos(0.9f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        // move to destination
        if (destination != null)
        {
            if ((this.transform.localPosition - destination).magnitude < 0.0001f)
                return;

            Vector3 dir = (destination - transform.localPosition).normalized;
            transform.localPosition += speed * dir * Time.deltaTime;
        }
    }


}
