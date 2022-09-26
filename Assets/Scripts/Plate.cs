using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plate
{
    // using UnityEngine.ProBuilder;
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
    public class Plate : MonoBehaviour
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
            this.gameObject.name = "Plate";
            this.gameObject.transform.position.Set(3, 0, 0);
            //igidbody rigidBody = this.gameObject.AddComponent<Rigidbody>();
            //rigidBody.constraints = RigidbodyConstraints.FreezePosition;

        }

        // Update is called once per frame
        void Update()
        {
            Vector2 rotateRat = mouse_handle();
            plate_rotate(rotateRat);
            Quaternion rotation = this.transform.rotation;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // reset
                round = 0;
                rotateState = 0;
            }
            //transform.Rotate(new Vector3(0, 0, 0.1f));
            //Debug.Log(this.transform.rotation);
        }

        Vector2 mouse_handle()
        {
            /* TODO : 
                1. Handle mouse sensitibity
                2. If user want to click UI component? does the position is within playing area?
            */

            Vector2 mousePosRat = 2 * new Vector3(Input.mousePosition.y / Screen.height - 0.5f, Input.mousePosition.x / Screen.width - 0.5f);
            mousePosRat.x = Mathf.Min(1.0f, Mathf.Max(-1.0f, mousePosRat.x));
            mousePosRat.y = -1 * Mathf.Min(1.0f, Mathf.Max(-1.0f, mousePosRat.y));
            mousePosRat *= CLAMP_MOUSE;
            Debug.Log(mousePosRat);
            return mousePosRat;
        }

        // void StartRotating(float zPosition)
        // {

        //     // Here we cache the starting and target rotations
        //     startRotation = transform.rotation;
        //     endRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, zPosition);

        //     // This starts the rotation, but you can use a boolean flag if it's clearer for you
        //     rotationProgress = 0;
        // }

        Vector3 adapt(Vector3 v)
        {
            return v;
            Vector3 re = new Vector3();
            for (int i = 0; i < 3; i++)
            {
                re[i] = v[i];

                if (v[i] < 0) re[i] += 360;
            }
            return re;
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
            Vector3 _rotate = new Vector3(rotateRat.x * 90 - euler.x, 0.0f, rotateRat.y * 90 - euler.z);

            transform.Rotate(_rotate.x, 0.0f, _rotate.z);
            //return;
            if (rotateState == 0)
            {

                startRotation = this.transform.rotation.eulerAngles;
                endRotation = adapt(startRotation + _rotate);
                rotateStep = _rotate / 50.0f;
                Debug.Log("start" + startRotation + " " + adapt(endRotation) + " " + rotateStep);
                rotateState = 1;
            }
            if (rotateState == 1)
            {
                //Debug.Log("start" + startRotation + " " + endRotation + " " + rotateStep + " : " + this.transform.rotation.eulerAngles);
                if (Vector3.Magnitude(this.transform.rotation.eulerAngles - adapt(endRotation)) < 10.0f)
                {
                    rotateState = 0;
                }
                round += 1;
                if (round >= 50)
                    rotateState = -1;
                Debug.Log(this.transform.rotation.eulerAngles + " " + adapt(endRotation) + " " + rotateStep);
                //_rotate = rotateStep;
                transform.Rotate(rotateStep.x, 0.0f, rotateStep.z);

            }

            //Debug.Log("mag " + _rotate + " " + _rotate.magnitude);
            //if (_rotate.magnitude > 1) _rotate /= 5.0f;
            // Vector2 diffAngle;

            // if (lastRotate != new Vector2(0, 0))
            // {
            //     diffAngle = _rotate - lastRotate;
            //     //if (diffAngle.magnitude > 1)
            //     //{
            //     Debug.Log("diff" + _rotate + " / " + lastRotate + " / " + diffAngle + " = " + (lastRotate + diffAngle / 2.0f));
            //     //diffAngle.Normalize();
            //     //_rotate -= diffAngle;
            //     _rotate = (lastRotate + diffAngle / 2.0f);

            //     //}
            // }

            // lastRotate = _rotate;

            //_rotate = _rotate.normalized * ROTATE_STEP;
            //Debug.Log("ro" + _rotate);
            //transform.Rotate(_rotate.x, 0.0f, _rotate.y);
            ///_rotate = new Vector2(0, 90) * Time.deltaTime;
            //transform.Rotate(_rotate.x, 0.0f, _rotate.z);
            //transform.rotation = Quaternion.LookRotation(newDirection);
        }



    }

}