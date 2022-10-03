using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
using Filter;
using Util;

public class InGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    float time = -1.0f;

    int score = 0;

    [SerializeField]
    private GameObject[] starRef;
    GameObject timerObj, starObj;

    TextMeshProUGUI timer, scoreUI;
    Plate.Plate plate;

    List<Star> stars;
    Filter.Filter filter;
    Marble marble;
    bool running = false, end = false;

    void Start()
    {

        filter = (Filter.Filter)GameObject.Find("Filter").GetComponent(typeof(Filter.Filter));
        InitPlate.createPlate();
        plate = null;
        running = true;
        startScore();
        startTimer();
        setPlate();

        marble = (Marble)GameObject.Find("Marble").GetComponent(typeof(Marble));

        //createStar();
    }

    // Update is called once per frame
    void Update()
    {
        setPlate();
        setTimer();
        handleInput();
        // wait for x seconds then create


    }

    private void setPlate()
    {
        // TODO : how to initial plate after Plate is created. not recurrent checking
        if (plate == null)
        {

            if (GameObject.Find("Plate") == null) return;
            plate = (Plate.Plate)GameObject.Find("Plate").GetComponent(typeof(Plate.Plate));
            createStar();
        }
    }

    private void startScore()
    {
        timerObj = GameObject.Find("Score");
        scoreUI = (TextMeshProUGUI)timerObj.GetComponent(typeof(TextMeshProUGUI));
        // timer.transform.parent = GameObject.Find("Canvas").transform;
        // //timer.autoSizeTextContainer = true;
        scoreUI.fontSize = 24;
        //timer.transform.position = new Vector3(375, 160, 0);
        scoreUI.alignment = TextAlignmentOptions.Center;
        scoreUI.extraPadding = true;
        scoreUI.enableWordWrapping = false;
    }

    private void startTimer()
    {
        timerObj = GameObject.Find("Timer");
        timer = (TextMeshProUGUI)timerObj.GetComponent(typeof(TextMeshProUGUI));


        // timer.transform.parent = GameObject.Find("Canvas").transform;
        // //timer.autoSizeTextContainer = true;

        timer.fontSize = 24;
        //timer.transform.position = new Vector3(375, 160, 0);
        timer.alignment = TextAlignmentOptions.Center;

        timer.extraPadding = true;

        timer.enableWordWrapping = false;
        time = 0;
    }

    private void setTimer()
    {
        if (!running) return;
        time += Time.deltaTime;
        timer.text = "Time : " + Mathf.RoundToInt(time * 5).ToString();
    }

    private void handleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TODO : Handle mouse position? save position before pause and set back when resume? or label position.
            if (running)
            {
                // Suggest : create list of object? if we have more playable objects.
                marble.stop();
                plate.stop();
                filter.showPauseUI();
            }
            else
            {
                marble.resume();
                plate.resume();
                filter.closePauseUI();
            }
            running = !running;
        }
    }

    public void collectStar()
    {
        // increase score
        score += 1;
        scoreUI.text = "Score : " + score.ToString();
        // create new start
        createStar();

        // remove old start
    }

    private void createStar()
    {
        // also cosider the position of previous start
        int randomIdx = Random.Range(0, starRef.Length);

        Vector3 newPosition = Util.Util.randomPos(0.8f, 0.1f);

        while (true)
        {
            // if too near hole or oldPosition, re-random
            if (!InitPlate.nearOtherHoles(newPosition, 0.5f, false))
            {
                if (starObj != null)
                {
                    Vector3 oldPosition = starObj.transform.position;
                    if (Vector3.Distance(newPosition, oldPosition) > 0.2f)
                    {
                        Destroy(starObj);
                        break;
                    }
                }
                else
                    break;
                //Debug.Log("starPos" + newPosition);
            }
            newPosition = Util.Util.randomPos(0.8f, 0.1f);
            // destroy old star
        }

        // some start may have different properties
        ///starObj.transform.position = newPosition;
        starObj = Instantiate(starRef[randomIdx]);
        starObj.transform.parent = GameObject.Find("Plate").transform;
        starObj.transform.localPosition = newPosition;
        starObj.transform.localRotation = new Quaternion();

        Debug.Log("starPos" + starObj.transform.position + starObj.transform.localPosition);
        //newPosition =

        // rotate according to plate's rotation
        //starObj.transform.rotation = GameObject.Find("Marble").transform.rotation;
    }
}
