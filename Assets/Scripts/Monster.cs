using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    Transform marbleT;
    float speed = 0.05f;
    private Collider collider;
    Rigidbody rigidbody;

    Animator m_Animator;
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        rigidbody = GetComponent<Rigidbody>();
        m_Animator = gameObject.GetComponent<Animator>();
        // random speed
    }
    public void setMarbleTransform(Transform t)
    {
        marbleT = t;
    }


    // Update is called once per frame
    void Update()
    {
        // move to marble
        Vector3 dir = Vector3.Normalize(marbleT.position - transform.position);
        dir.y = 0.0f;
        transform.localPosition += speed * dir * Time.deltaTime;

        // look at marble considered the case that if marble has falled


        //rigidbody.AddForce(speed * dir);

        // handle marble go down to plate

    }

    private void OnCollisionEnter(Collision other)
    {
        // if col marble, attack, set trigger (for not repeating col detecting) then fade out
        if (other.gameObject.name == "Marble")
        {
            m_Animator.SetTrigger("Attack");
            collider.isTrigger = true;

            StartCoroutine(Util.Util.fadeOut(5.0f, this.gameObject, true));

            //Debug.Log("attck");
        }
    }

}
