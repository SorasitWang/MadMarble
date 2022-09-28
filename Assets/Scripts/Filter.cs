using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;


namespace Filter
{
    public class Filter : MonoBehaviour
    {
        // Start is called before the first frame update


        Volume volume;
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
}