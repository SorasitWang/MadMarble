using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRespawn : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject monsterRef;

    private float offsetY;
    void Start()
    {

        StartCoroutine(respawnMonster());
        offsetY = monsterRef.transform.localScale.y / 2.0f + 0.01f;
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
            yield return new WaitForSeconds(Random.Range(5, 10));
            GameObject spawnMonster = Instantiate(monsterRef);
            spawnMonster.transform.parent = Plate.Plate.Instance.transform;
            Vector3 newPos = Util.Util.randomPos(0.8f, offsetY);
            while (true)
            {
                if (!Marble.Instance.nearMarble(newPos, 0.6f))
                {
                    break;
                }
                newPos = Util.Util.randomPos(0.8f, offsetY);
            }
            spawnMonster.transform.localPosition = newPos;
            spawnMonster.transform.localRotation = Quaternion.identity;
            spawnMonster.GetComponent<Monster>().setMarbleTransform(Marble.Instance.transform);

        }
    }


}
