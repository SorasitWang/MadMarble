using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    float time = -1.0f;

    GameObject timerObj;
    TextMeshProUGUI timer;

    void Start()
    {
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {

        SetTimer();
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
        if (time == -1.0f) return;
        time += Time.deltaTime;
        timer.text = "Time : " + Mathf.RoundToInt(time * 5).ToString();
    }

    public void StopTimer()
    {
        time = -1.0f;
    }
}
