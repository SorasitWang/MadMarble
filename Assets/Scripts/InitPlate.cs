using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Parabox.CSG;
// using UnityEngine.ProBuilder;
using Plate;
enum Difficulty
{
    Easy, Medium, Hard
}

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
        this.rad = GameObject.Find("Marble").transform.localScale.x + 0.2f;
        this.force = 1;
    }
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
public class InitPlate : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    float mouseSensitivity = 1.0f;

    Difficulty level = Difficulty.Easy;
    GameObject holeObj;
    List<Vector3> holes;

    Vector3 targetHole;

    List<DangerHole> dangerHoles = new List<DangerHole>();

    Vector3 startRotation;
    Vector3 endRotation;
    Vector3 rotateStep;

    int dangerHoleNum = 1;

    float marbleRad;

    GameObject result;

    void Start()
    {

        marbleRad = GameObject.Find("Marble").transform.localScale.x;
        CreatePlate();


    }

    // Update is called once per frame
    void Update()
    {

    }
    void CreatePlate()
    {
        /*
            Init hole and wall depend on level
        */
        result = this.gameObject;
        holeObj = GameObject.Find("Hole");

        // random position of target gole
        float offset = 0.2f;
        float outRad = 1.0f - marbleRad - offset;
        float posRad = Random.Range(0, outRad);
        float angle = Random.Range(0, 360);
        targetHole = new Vector3(Mathf.Cos(angle * Mathf.Rad2Deg) * outRad, 0.0f, Mathf.Sin(angle * Mathf.Rad2Deg) * outRad);
        targetHole += this.gameObject.transform.position;
        Debug.Log("hole" + angle + new Vector3(Mathf.Cos(angle * Mathf.Rad2Deg) * outRad, 0.0f, Mathf.Sin(angle * Mathf.Rad2Deg) * outRad));
        csgHole(targetHole);


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
                if (true) // not too near with other hole
                    break;
            }
            if (tryNum > 10)
                dangerHoleNum -= 1;
            else
            {
                tmpPos += this.gameObject.transform.position;
                Debug.Log("danger" + angle + new Vector3(Mathf.Cos(angle * Mathf.Rad2Deg) * outRad, 0.0f, Mathf.Sin(angle * Mathf.Rad2Deg) * outRad));
                this.dangerHoles.Add(new DangerHole(tmpPos));
                csgHole(tmpPos);
            }
        }




        result.AddComponent(typeof(Plate.Plate));
        Destroy(holeObj.gameObject);
        //Destroy(this.gameObject);
    }

    private bool nearOtherHoles(Vector3 pos)
    {
        if (Vector3.Distance(pos, targetHole) <= 2 * marbleRad + 0.2f)
            return true;
        for (int i = 0; i < dangerHoleNum; i++)
        {
            if (Vector3.Distance(pos, dangerHoles[i].pos) <= 2 * marbleRad + 0.2f)
                return true;
        }
        return false;

    }
    private void csgHole(Vector3 pos)
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
