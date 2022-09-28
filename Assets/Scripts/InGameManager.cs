using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
using Filter;
public class InGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    float time = -1.0f;

    GameObject timerObj;
    TextMeshProUGUI timer;
    Plate.Plate plate;


    Filter.Filter filter;
    Marble marble;
    bool running = false, end = false;

    void Start()
    {

        filter = (Filter.Filter)GameObject.Find("Filter").GetComponent(typeof(Filter.Filter));
        plate = null;
        running = true;
        StartTimer();
        setPlate();

        marble = (Marble)GameObject.Find("Marble").GetComponent(typeof(Marble));
    }

    // Update is called once per frame
    void Update()
    {
        setPlate();
        SetTimer();
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

    private void setPlate()
    {
        // TODO : how to initial plate after Plate is created. not recurrent checking
        if (plate == null)
        {

            if (GameObject.Find("Plate") == null) return;
            plate = (Plate.Plate)GameObject.Find("Plate").GetComponent(typeof(Plate.Plate));
        }
    }

    private void StartTimer()
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

    private void SetTimer()
    {
        if (!running) return;
        time += Time.deltaTime;
        timer.text = "Time : " + Mathf.RoundToInt(time * 5).ToString();
    }


}
