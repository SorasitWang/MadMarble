using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoRespawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject tornadoRef;

    Vector3 destination;

    const float speed = 1.0f;
    void Start()
    {
        StartCoroutine(init());


    }

    IEnumerator init()
    {
        yield return new WaitForSeconds(Random.Range(2, 7));
        GameObject spawnTornado = Instantiate(tornadoRef);
        spawnTornado.transform.parent = GameObject.Find("Plate").transform;
        Vector3 newPos = Util.Util.randomPos(0.8f, 0.1f);

        spawnTornado.transform.localPosition = newPos;
        StartCoroutine(tornadoMovement());
    }

    IEnumerator tornadoMovement()
    {
        while (true)
        {
            // if game is paused, stop respawn
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
            Vector3 dir = (destination - transform.localPosition).normalized;
            transform.localPosition += speed * dir * Time.deltaTime;
        }
    }


}
