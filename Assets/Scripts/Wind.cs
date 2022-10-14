using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wind : MonoBehaviour
{
    // Start is called before the first frame update
    const float minVelo = 0.2f, maxVelo = 1.0f;

    const float minLength = 0.75f, maxLength = 1.5f;
    [SerializeField]
    public Vector2 direction;
    [SerializeField]
    float force = 0.0f, velocity;

    const float ROTATE_STEP = 0.1f;
    Transform windCompassT;


    void Start()
    {
        transform.rotation = Random.rotation;
        windCompassT = GameObject.Find("WindCompass").transform;
        StartCoroutine(windDir());
        velocity = minVelo;

        // TODO : init wind label
    }

    // Update is called once per frame
    void Update()
    {
        // TODO : update wind direction label 
        windCompassT.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan(direction.x / direction.y), Vector3.up);
        windCompassT.localScale = new Vector3(interpolate(minVelo, maxVelo, minLength, maxLength, velocity), windCompassT.localScale.y, windCompassT.localScale.z);
        //Debug.Log("compass" + windCompassT.rotation);
    }

    float interpolate(float minOld, float maxOld, float minNew, float maxNew, float value)
    {
        return ((value - minOld) / (maxOld - minOld)) * (maxNew - minNew) + minNew;
    }
    IEnumerator windDir()
    {
        while (true)
        {
            // every [ , ] random direction
            // make sure changeWindDir will be done before change
            yield return new WaitForSeconds(Random.Range(10, 15));

            // random 
            Vector2 destDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            float destVelo = Random.Range(minVelo, maxVelo);


            Vector2 changeDir = (destDir - direction).normalized * Time.deltaTime;
            float changeVelo = (destVelo - velocity) * Time.deltaTime;

            //Debug.Log("Change to" + dest);

            // Can we also use destDir-signDir for destVelo-signVelo to determine breakpoint? 
            StartCoroutine(changeWind(changeDir, destDir,
                new Vector2(Mathf.Sign(destDir.x - direction.x), Mathf.Sign(destDir.y - direction.y))
                , changeVelo));

        }
    }

    IEnumerator changeWind(Vector2 changeDir, Vector2 destDir, Vector2 signDir
    , float changeVelo)
    {
        while (true)
        {
            yield return new WaitForSeconds(3 * Time.deltaTime);
            // rotate current to dest direction gradually
            direction += changeDir;
            velocity += changeVelo;
            //Debug.Log(Vector2.Distance(direction, dest) + " / " + change.magnitude);
            if (new Vector2(Mathf.Sign(direction.x - destDir.x), Mathf.Sign(direction.y - destDir.y)) == signDir)
            {
                //Debug.Log("reach");
                yield break;
            }
        }
    }

}
