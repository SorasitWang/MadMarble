using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRespawn : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject monsterRef;
    void Start()
    {

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
            spawnMonster.transform.parent = Plate.Plate.Instance.transform;
            Vector3 newPos = Util.Util.randomPos(0.8f, 0.1f);
            while (true)
            {
                if (!Marble.Instance.nearMarble(newPos, 0.6f))
                {
                    break;
                }
                newPos = Util.Util.randomPos(0.8f, 0.1f);
            }
            spawnMonster.transform.localPosition = newPos;
            spawnMonster.GetComponent<Monster>().setMarbleTransform(Marble.Instance.transform);

        }
    }
}
