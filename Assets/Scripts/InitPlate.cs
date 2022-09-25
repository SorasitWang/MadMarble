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

    List<Vector3> holes;

    const float HOLE_RAD = 0.5f;

    const float CLAMP_MOUSE = 0.4f;

    const float ROTATE_STEP = 1.0f;

    Vector3 startRotation;
    Vector3 endRotation;

    Vector3 rotateStep;

    float rotationProgress = -1;

    int rotateState = 0, round = 0;

    Vector2 lastRotate = new Vector2(0, 0);

    void Start()
    {

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
        GameObject holeObj = GameObject.Find("Hole");
        Model result = CSG.Subtract(this.gameObject, holeObj);

        // Create a gameObject to render the result
        var composite = new GameObject();
        composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
        composite.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
        MeshCollider meshc = composite.AddComponent(typeof(MeshCollider)) as MeshCollider;


        meshc.sharedMesh = result.mesh; ; // Give it your mesh here.
        composite.AddComponent(typeof(Plate.Plate));


        Destroy(holeObj.gameObject);
        Destroy(this.gameObject);
    }
}
