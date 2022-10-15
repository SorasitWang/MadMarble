using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;



public class Filter : MonoBehaviour
{
    // Start is called before the first frame update
    public static Filter Instance { get { return _instance; } }
    private static Filter _instance;

    Volume volume;

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
        volume = GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showPauseUI()
    {
        // show Text and filter
        //volume.weight = 1;
        ColorAdjustments tmp;
        if (volume.profile.TryGet<ColorAdjustments>(out tmp))
        {
            tmp.active = true;
        }
    }

    public void closePauseUI()
    {
        //volume.weight = 0;
        ColorAdjustments tmp;
        if (volume.profile.TryGet<ColorAdjustments>(out tmp))
        {
            tmp.active = false;
        }
    }

    public void lostUI()
    {

    }

}
