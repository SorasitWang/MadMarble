using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
using Util;

public class InGameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static InGameManager Instance { get { return _instance; } }
    private static InGameManager _instance;
    float time = -1.0f;

    int score = 0;

    [SerializeField]
    private GameObject[] starRef;
    GameObject timerObj, starObj;

    TextMeshProUGUI timer, scoreUI, velocity;


    List<Star> stars;

    bool running = false, end = false;
    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Destroy()
    {
        GameObject.Destroy(_instance.gameObject);
        _instance = null;
    }
    void Start()
    {

        InitPlate.createPlate();

        running = true;
        startScore();
        startTimer();
        createStar();



        // marbleObj = GameObject.Find("Marble");



        velocity = (TextMeshProUGUI)GameObject.Find("Velocity").GetComponent(typeof(TextMeshProUGUI));
        //createStar();
    }

    // Update is called once per frame
    void Update()
    {

        setTimer();
        setVelocity();
        handleInput();
        // wait for x seconds then create


    }

    private void setVelocity()
    {
        velocity.text = "Velocity : " + Mathf.Round(Marble.Instance.GetComponent<Rigidbody>().velocity.magnitude * 10f) / 10f;
    }


    private void startScore()
    {
        timerObj = GameObject.Find("Score");
        scoreUI = (TextMeshProUGUI)timerObj.GetComponent(typeof(TextMeshProUGUI));
        // timer.transform.parent = GameObject.Find("Canvas").transform;
        // //timer.autoSizeTextContainer = true;
        scoreUI.fontSize = 24;
        //timer.transform.position = new Vector3(375, 160, 0);
        //scoreUI.alignment = TextAlignmentOptions.Center;
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
        //timer.alignment = TextAlignmentOptions.Center;

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
                Time.timeScale = 0;
                // Suggest : create list of object? if we have more playable objects.
                //marble.stop();
                //plate.stop();
                Filter.Instance.showPauseUI();
            }
            else
            {
                Time.timeScale = 1;
                //marble.resume();
                //plate.resume();
                Filter.Instance.closePauseUI();
            }
            running = !running;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            // restart
            restart();
        }
    }

    public void collectStar()
    {
        // increase score
        updateScore(1);
        // create new start
        createStar();

        // remove old start
    }

    private void restart()
    {
        //clear timer, score, mana bar

        // destroy monster, tornado, star

        // reset marble position
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
                    if (!Marble.Instance.nearMarble(newPosition, 0.5f))
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
        starObj.transform.parent = Plate.Plate.Instance.transform;
        starObj.transform.localPosition = newPosition;
        starObj.transform.localRotation = new Quaternion();

        Debug.Log("starPos" + starObj.transform.position + starObj.transform.localPosition);
        //newPosition =

    }

    public void colMonster()
    {
        updateScore(-1);
    }

    private void updateScore(int change)
    {
        score += change;
        score = (int)Mathf.Max(0, score);
        scoreUI.text = "Score : " + score.ToString();
    }

}
