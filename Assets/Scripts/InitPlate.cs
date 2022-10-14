using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Parabox.CSG;
// using UnityEngine.ProBuilder;
using Plate;
using Util;


public enum Difficulty
{
    Easy, Medium, Hard
}
public class Wall
{
    float width;
    float length;

    Vector3 dir;

    GameObject obj;

    Wall()
    {
        width = 0.2f;
        length = 0.5f;
        dir = new Vector3(1, 0, 0);
        initObj();
    }

    Wall(float w, float l, Vector3 _dir)
    {
        width = w;
        length = l;
        dir = _dir;
        initObj();
    }

    private void initObj()
    {

    }

}


public static class InitPlate
{


    // Start is called before the first frame update



    public static float mouseSensitivity = 1.0f;

    public static Difficulty level = Difficulty.Easy;
    public static GameObject holeObj;
    public static List<Vector3> holes;

    public static Vector3 targetHole;

    public static List<DangerHole> dangerHoles = new List<DangerHole>();

    // public static Vector3 startRotation;
    // public static Vector3 endRotation;
    // public static Vector3 rotateStep;

    public static int dangerHoleNum = 1;

    public static float marbleRad;

    public static GameObject result;

    public static void init()
    {
        GameObject marbleObj = GameObject.Find("Marble");
        Marble marble = (Marble)marbleObj.GetComponent(typeof(Marble));
        marbleRad = marbleObj.transform.localScale.x;

        createPlate();
        marble.storeHoles(dangerHoles);



    }

    public static void createPlate()
    {
        /*
            Init hole and wall depend on level
        */
        result = GameObject.Find("Template");
        holeObj = GameObject.Find("Hole");

        // random position of target gole
        float offset = 0.2f;
        float outRad = 1.0f - marbleRad - offset;
        float posRad = Random.Range(0, outRad);
        float angle = Random.Range(0, 360);
        targetHole = new Vector3(Mathf.Cos(angle * Mathf.Rad2Deg) * outRad, 0.0f, Mathf.Sin(angle * Mathf.Rad2Deg) * outRad);
        targetHole += result.transform.position;
        Debug.Log("hole" + angle + new Vector3(Mathf.Cos(angle * Mathf.Rad2Deg) * outRad, 0.0f, Mathf.Sin(angle * Mathf.Rad2Deg) * outRad));
        //csgHole(targetHole);


        // create danger hole
        Vector3 tmpPos;
        int _dangerHoleNum = dangerHoleNum;
        for (int i = 0; i < _dangerHoleNum; i++)
        {
            outRad = 1.0f - marbleRad - offset;

            // prevent infinite loop
            int tryNum = 0;
            tmpPos = new Vector3();

            while (true)
            {
                if (tryNum > 10) break;
                tryNum += 1;
                posRad = Random.Range(0, outRad);
                angle = Random.Range(0, 360);
                tmpPos = new Vector3(Mathf.Cos(angle * Mathf.Rad2Deg) * outRad, 0.0f, Mathf.Sin(angle * Mathf.Rad2Deg) * outRad);
                if (!nearOtherHoles(tmpPos)) // not too near with other hole
                    break;
            }
            if (tryNum > 10)
                dangerHoleNum -= 1;
            else
            {
                tmpPos += result.transform.position;
                Debug.Log("danger" + angle + new Vector3(Mathf.Cos(angle * Mathf.Rad2Deg) * outRad, 0.0f, Mathf.Sin(angle * Mathf.Rad2Deg) * outRad));
                dangerHoles.Add(new DangerHole(tmpPos));
                csgHole(tmpPos);
                GameObject particle = (GameObject)Object.Instantiate((UnityEngine.Object)Resources.Load("BlackholeParticle"));
                particle.transform.parent = GameObject.Find("Template").transform;
                particle.transform.localPosition = tmpPos + new Vector3(0, 0.03f, 0);

            }
        }




        result.AddComponent(typeof(Plate.Plate));
        UnityEngine.Object.Destroy(holeObj.gameObject);
        //Destroy(this.gameObject);
    }

    public static bool nearOtherHoles(Vector3 pos, float offset = 0.2f, bool forHole = true)
    {

        if (Vector3.Distance(pos, targetHole) <= 2 * marbleRad + offset)
            return true;
        // not include itself yet
        for (int i = 0; i < dangerHoleNum - (forHole ? 1 : 0); i++)
        {
            Debug.Log("Near" + Vector3.Distance(pos, dangerHoles[i].pos));
            if (Vector3.Distance(pos, dangerHoles[i].pos) <= 2 * marbleRad + offset)
                return true;
        }
        return false;

    }
    private static void csgHole(Vector3 pos)
    {
        holeObj.transform.position = new Vector3(pos.x, pos.y, pos.z);
        Model re = CSG.Subtract(result, holeObj);

        // Create a gameObject to render the result

        result.GetComponent<MeshFilter>().sharedMesh = re.mesh;
        result.GetComponent<MeshRenderer>().sharedMaterials = re.materials.ToArray();
        MeshCollider meshc = result.GetComponent(typeof(MeshCollider)) as MeshCollider;

        meshc.sharedMesh = re.mesh; ; // Give it your mesh here.


    }

}