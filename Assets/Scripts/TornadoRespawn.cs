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
        spawnTornado.transform.parent = Plate.Plate.Instance.transform;
        Vector3 newPos = Util.Util.randomPos(0.8f, 0.1f);
        // not near marble

        spawnTornado.transform.localPosition = newPos;

    }
}
