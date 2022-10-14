using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManaBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slider;

    int value = 0;
    float accuTime = 0.0f;

    bool reset = false;

    const int step = 2, MAX = 6 * step;


    // charge time = 6 second


    void Start()
    {
        slider.maxValue = MAX;
    }

    // Update is called once per frame
    void Update()
    {
        if (value != MAX)
        {
            accuTime += Time.deltaTime;
            if (accuTime > 1.0f / step)
            {
                value += 1;
                accuTime = 0.0f;
                setValue();
            }
        }

    }

    void setValue()
    {
        slider.value = value;
    }

    public bool isReady()
    {
        return value == MAX;
    }

    public void use()
    {
        value = 0;
        setValue();
    }
}
