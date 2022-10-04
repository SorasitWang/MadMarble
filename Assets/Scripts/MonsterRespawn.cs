using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRespawn : MonoBehaviour
{
    // Start is called before the first frame update
    Transform marbleT;

    Marble marble;
    public GameObject monsterRef;
    void Start()
    {
        marble = (Marble)GameObject.Find("Marble").GetComponent(typeof(Marble));
        marbleT = GameObject.Find("Marble").transform;
        StartCoroutine(respawnMonster());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator respawnMonster()
    {
        while (true)
        {
            // if game is paused, stop respawn
            yield return new WaitForSeconds(Random.Range(2, 7));
            GameObject spawnMonster = Instantiate(monsterRef);
            spawnMonster.transform.parent = GameObject.Find("Plate").transform;
            Vector3 newPos = Util.Util.randomPos(0.8f, 0.1f);
            while (true)
            {
                if (!marble.nearMarble(newPos, 0.6f))
                {
                    break;
                }
                newPos = Util.Util.randomPos(0.8f, 0.1f);
            }
            spawnMonster.transform.localPosition = newPos;
            spawnMonster.GetComponent<Monster>().setMarbleTransform(marbleT);

        }
    }
}
