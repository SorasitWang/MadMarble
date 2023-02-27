using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{

    public class DangerHole
    {

        public Vector3 pos;

        public float rad;

        public float force;

        public DangerHole(Vector3 pos, float rad, float force)
        {
            this.pos = pos;
            this.rad = rad;
            this.force = force;
        }
        public DangerHole(Vector3 pos)
        {
            this.pos = pos;
            this.rad = Marble.Instance.transform.localScale.x + 0.2f;
            this.force = 1;
        }

        public Vector3 calculateForce(Vector3 pos)
        {
            float distanceEffect(float dist)
            {
                float maxW = 1, minW = 0.0f;
                float maxDir = 2 * this.rad;
                if (dist > maxDir) return 0;
                return Mathf.Lerp(maxW, minW, dist / (2 * this.rad));
            }
            Vector3 re;
            Vector3 dir = (this.pos - pos).normalized;
            Vector3 force = dir * this.force;
            // distance ; if dist = 2*rad => 1, dist = 0 => 0.25
            float dist = Vector3.Distance(this.pos, pos);
            Debug.Log("hole effect " + force + " * " + distanceEffect(dist));
            return force * distanceEffect(dist);

        }
    }

    public static class Util
    {
        //static GameObject plate = GameObject.Find("Plate");
        public static Vector3 randomPos(float outRad, float hight = 0.0f)
        {
            // not cosider whether is near some object or not
            float posRad = Random.Range(0, outRad);
            float angle = Random.Range(0, 360);
            return new Vector3(Mathf.Cos(angle * Mathf.Rad2Deg) * posRad, hight, Mathf.Sin(angle * Mathf.Rad2Deg) * posRad);


        }

        public static IEnumerator fadeOut(float waitTime, GameObject gameObject, bool destroy = false)
        {
            // Decrease 1 at a time, 
            // with a delay equal to the time, 
            // until the Animation finished / 100.
            float delay = waitTime / 100;
            foreach (var rend in gameObject.GetComponentsInChildren<Renderer>(true))
            {
                Color color = rend.material.color;
                while (color.a > 0)
                {
                    rend.material.color = new Color(color.r - 0.1f, 0, 0, 0);
                    Debug.Log("color.a" + color.a);
                    yield return new WaitForSeconds(delay);
                }
            }
            // Color color = gameObject.GetComponent<Renderer>().material.color;
            // while (color.a > 0)
            // {
            //     gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, color.a--);
            //     yield return new WaitForSeconds(delay);
            // }

            // if (destroy)
            //     Object.Destroy(gameObject);
        }






    }
}