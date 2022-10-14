using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Marble : MonoBehaviour
{
    const float SLOW = 1.5f, CHARGE_TIME = 5.0f;

    bool isJumping = false;

    float accuTime = 0.0f, accuCharge = 0.0f;
    Wind wind;
    List<DangerHole> dangerHoles;
    Rigidbody rigidbody;

    Collider collider;

    Vector3 acceleration, lastVelocity = new Vector3(0, 0, 0);
    Renderer ren;
    ManaBar mana;

    bool running;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        collider = GetComponent<Collider>();

        running = true;

        dangerHoles = new List<DangerHole>();

        wind = (Wind)GameObject.Find("Wind").GetComponent(typeof(Wind));

        mana = (ManaBar)GameObject.Find("ManaBar").GetComponent<ManaBar>();

        ren = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            jump();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (!running) return;


        // effect from hole force
        Debug.Log("force" + holeEffect());
        if (lastVelocity != new Vector3(0, 0, 0))
        {
            acceleration = (rigidbody.velocity - lastVelocity);

            //rigidbody.velocity = rigidbody.velocity - acceleration / SLOW;

            //rigidbody.velocity += holeEffect() * Time.deltaTime;

        }
        lastVelocity = rigidbody.velocity;
        accuTime += Time.deltaTime;

        // Wind force
        rigidbody.AddForce(new Vector3(wind.direction.normalized.x, 0.0f, wind.direction.normalized.y) * 0.01f);





    }

    private void jump()
    {
        if (mana.isReady())
        {
            isJumping = true;
            // Actually, normal vector of plate
            rigidbody.AddForce(Vector3.up * 2.0f, ForceMode.VelocityChange);
            mana.use();
        }

    }

    private void bounce(Vector3 colNormal)
    {
        //Vector3 bounceForce = Vector3.ClampMagnitude(rigidbody.velocity * -45, 35.0f);


        var speed = rigidbody.velocity.magnitude;
        var direction = Vector2.Reflect(rigidbody.velocity.normalized,
                                        colNormal);

        rigidbody.velocity = direction * Mathf.Max(speed, 2f);
        Debug.Log("Bounce " + direction * Mathf.Min(3.0f, Mathf.Max(speed, 2f)));
        rigidbody.AddForce(direction * Mathf.Min(3.0f, Mathf.Max(speed, 2f)));

    }

    // public void resume()
    // {
    //     running = true;
    //     rigidbody.constraints = RigidbodyConstraints.None;
    // }

    // public void stop()
    // {
    //     running = false;
    //     rigidbody.constraints = RigidbodyConstraints.FreezePosition;
    // }

    public void storeHoles(List<DangerHole> holes)
    {
        this.dangerHoles = holes;
    }

    private Vector3 holeEffect()
    {
        // find acc vector from hole force
        Vector3 re = new Vector3();
        Vector3 force, dir;
        for (int i = 0; i < dangerHoles.Count; i++)
        {
            Vector3 tmp = dangerHoles[i].calculateForce(transform.position);
            Debug.DrawRay(transform.position, tmp, Color.green, 1.0f);
            re += tmp;
        }
        return re;

    }

    private void hurtEffect()
    {
        ren.material.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("col" + other.gameObject.name);
        if (other.gameObject.tag == "Star")
        {
            InGameManager m = (InGameManager)GameObject.Find("InGameManager").GetComponent(typeof(InGameManager));
            m.collectStar();
        }
        else if (other.gameObject.tag == "Monster")
        {
            InGameManager m = (InGameManager)GameObject.Find("InGameManager").GetComponent(typeof(InGameManager));
            m.colMonster();
            Destroy(other.gameObject);
            // attacked affect
            hurtEffect();
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Tornado")
        {
            // bumping to negative current direction
            bounce(other.contacts[0].normal);

        }
    }

    public bool nearMarble(Vector3 pos, float threshold)
    {
        if (Vector3.Distance(transform.position, pos) < threshold)
            return true;
        return false;
    }

}
