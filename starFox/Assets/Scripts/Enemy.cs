using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LayerMask enemryMask;
    public float speed = 1f;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth;

    // Start is called before the first frame update
    void Start()
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(speed > 0)
        {
            Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
            Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
            bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemryMask);

            if (!isGrounded)
            {
                Vector3 currRot = myTrans.eulerAngles;
                currRot.y += 180;
                myTrans.eulerAngles = currRot;
            }

            Vector2 myVel = myBody.velocity;
            myVel.x = -myTrans.right.x * speed;
            myBody.velocity = myVel;
        }
        else
        {

        }
        
    }

    public void Hurt()
    {
        Destroy(this.gameObject);
    }

}
