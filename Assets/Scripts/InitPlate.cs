using System.Collections;
using System.Collections.Generic;

using UnityEngine;

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

    void Start()
    {
        modifyPlate();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rotateRat = mouse_handle();
        plate_rotate(rotateRat);
        Quaternion rotation = this.transform.rotation;
        //transform.Rotate(new Vector3(0, 0, 0.1f));
        //Debug.Log(this.transform.rotation);
    }

    Vector2 mouse_handle()
    {
        /* TODO : 
            1. Handle mouse sensitibity
            2. If user want to click UI component? does the position is within playing area?
        */

        Vector2 mousePosRat = 2 * new Vector3(Input.mousePosition.y / Screen.width - 0.5f, Input.mousePosition.x / Screen.height - 0.5f);
        mousePosRat.x = Mathf.Min(1.0f, Mathf.Max(-1.0f, mousePosRat.x));
        mousePosRat.y = Mathf.Min(1.0f, Mathf.Max(-1.0f, mousePosRat.y));
        mousePosRat /= 2.5f;
        Debug.Log(mousePosRat);
        return mousePosRat;
    }

    void plate_rotate(Vector2 rotateRat)
    {
        /*
            X : rotate along X-axis
            Y : rotate along Z-axis
        */
        //Debug.Log(rotateRat.x + " " + rotateRat.y);
        float degreesPerSecond = 90 * Time.deltaTime;
        /* create mouse position x = 1 : right, -1 : left 
                                y = 1 : back, -1 : front 
        */
        /*rotateRat.x = 0.0f;
        rotateRat.y = 0.1f;
        Vector3 lookatPos = new Vector3(transform.position.x + rotateRat.x, transform.position.y, transform.position.z + rotateRat.y);


        Vector3 direction = lookatPos - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, degreesPerSecond);*/
        Vector3 euler = this.transform.rotation.eulerAngles;
        // Debug.Log("euler" + euler + " // " + rotateRat.x + " " + rotateRat.y);
        transform.Rotate(rotateRat.x * 90 - euler.x, 0.0f, rotateRat.y * 90 - euler.z, Space.Self);

    }


    void modifyPlate()
    {
        /*
            Init hole and wall depend on level
        */
    }
}
