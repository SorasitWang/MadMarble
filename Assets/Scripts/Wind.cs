using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wind : MonoBehaviour
{
    // Start is called before the first frame update
    public static Wind Instance { get { return _instance; } }
    private static Wind _instance;
    const float minVelo = 0.2f, maxVelo = 1.0f;

    const float minLength = 0.75f, maxLength = 1.5f;
    [SerializeField]
    public Vector2 direction;
    [SerializeField]
    float force = 0.0f, velocity;

    const float ROTATE_STEP = 0.1f;
    float desireTorque = 0.0f;
    Quaternion sliceResult, destinationRotation;
    Transform windCompassT;

    bool first = true;
    const float rotationSpeed = 30.0f;
    const float angleThreshold = 0.1f;

    public float newDirectionDelay = 10.0f;
    private Quaternion previousTargetRotation = Quaternion.identity;
    private float timeSinceDirectionChange = 0.0f;
    private Vector2 currentDirection = Vector2.zero;
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
        transform.rotation = Random.rotation;
        windCompassT = GameObject.Find("WindCompass").transform;
        velocity = minVelo;
        direction = new Vector2(0.0f, 0.0f);
        // TODO : init wind label
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        updateWindProp();
    }

    void updateWindProp()
    {
        // Check if it's time to change the direction
        timeSinceDirectionChange += Time.deltaTime;
        float angleDifference = Quaternion.Angle(windCompassT.rotation, previousTargetRotation);
        if (timeSinceDirectionChange >= newDirectionDelay && (Mathf.Abs(angleDifference) < angleThreshold || first))
        {
            // Generate a new random direction
            currentDirection = Random.insideUnitCircle.normalized;
            first = false;
            // Reset the timer
            timeSinceDirectionChange = 0.0f;
            // adapt to match the orientation of model
            direction = new Vector2(currentDirection.y, -currentDirection.x);
            newDirectionDelay = Random.Range(7.5f, 10.0f);
            velocity = Random.Range(minVelo, maxVelo);
        }

        // Rotate towards the current direction
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(currentDirection.x, 0.0f, currentDirection.y));
        previousTargetRotation = targetRotation;
        //Debug.Log(angleDifference + " currentDirection " + currentDirection);
        windCompassT.rotation = Quaternion.RotateTowards(windCompassT.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    float interpolate(float minOld, float maxOld, float minNew, float maxNew, float value)
    {
        return ((value - minOld) / (maxOld - minOld)) * (maxNew - minNew) + minNew;
    }


}
