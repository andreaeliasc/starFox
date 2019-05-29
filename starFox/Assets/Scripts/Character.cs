using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{


    Rigidbody2D rb2d;
    SpriteRenderer sr;
    Animator anim;
    private float speed = 5f;
    private float jumpForce = 350;
    private bool facingRight = true;
    public GameObject feet;
    public GameObject fox;
    public LayerMask layerMask;
    public Joystick Horizontaljoystick;
    public Joystick Verticaljoystick;
    public GameObject DiedText, HomeButton, Pausemenu, PauseButton, RetryButton;
    public GameObject Heart1, Heart2, Heart3;
    public GameObject NoHeart1, NoHeart2, NoHeart3;
    public int Health = 3;
    public GameObject WinText;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if(rb2d.velocity.y == 0)
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", false);
        }

        if (rb2d.velocity.y > 0)
        {
            anim.SetBool("Jump", true);
        }

        if (rb2d.velocity.y < 0)
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", true);
        }

        float move = Horizontaljoystick.Horizontal;
        if (Horizontaljoystick.Horizontal >= .5f)
        {
           
            rb2d.transform.Translate(new Vector3(1, 0, 0) * move * speed * Time.deltaTime);
            
            facingRight = move > 0;
        } else if (Horizontaljoystick.Horizontal <= -.5f)
        {
           
            rb2d.transform.Translate(new Vector3(1, 0, 0) * move * speed * Time.deltaTime);
          
            facingRight = move > 0;
        }
        else
        {
            move = 0f;
        }

        float VerticalMove = Verticaljoystick.Vertical;
        if (Verticaljoystick.Vertical >= .5f)
        {
            RaycastHit2D raycast = Physics2D.Raycast(feet.transform.position, Vector2.down, 0.1f, layerMask);
            if (raycast.collider != null)
                rb2d.AddForce(Vector2.up * jumpForce);
        }
        else if (Verticaljoystick.Vertical <= -.3f)
        {
            anim.SetBool("Crouch", true);
        }
        else
        {
            anim.SetBool("Crouch", false);
        }

        anim.SetFloat("Speed", Mathf.Abs(move));

        sr.flipX = !facingRight;

        if (transform.position.y < -20)
        {
            Destroy(fox);
            Pausemenu.SetActive(false);
            PauseButton.SetActive(false);
            HomeButton.SetActive(false);
            DiedText.SetActive(true);
            RetryButton.SetActive(true);
            Heart1.SetActive(false);
            Heart2.SetActive(false);
            Heart3.SetActive(false);
            NoHeart1.SetActive(true);
            NoHeart2.SetActive(true);
            NoHeart3.SetActive(true);
        }

        else if (transform.position.y > 65)
        {
            WinText.SetActive(true);
            Pausemenu.SetActive(false);
            PauseButton.SetActive(false);
            HomeButton.SetActive(false);
            DiedText.SetActive(false);
            RetryButton.SetActive(true);
            Heart1.SetActive(false);
            Heart2.SetActive(false);
            Heart3.SetActive(false);
            NoHeart1.SetActive(false);
            NoHeart2.SetActive(false);
            NoHeart3.SetActive(false);
        }
    }

    void Hurt()
    {
        Health--;
        if (Health <= 0)
        {
            Destroy(fox);
            Pausemenu.SetActive(false);
            PauseButton.SetActive(false);
            HomeButton.SetActive(false);
            DiedText.SetActive(true);
            RetryButton.SetActive(true);
            Heart1.SetActive(false);
            Heart2.SetActive(false);
            Heart3.SetActive(false);
            NoHeart1.SetActive(true);
            NoHeart2.SetActive(true);
            NoHeart3.SetActive(true);
        }
        else if (Health == 3)
        {
            Heart1.SetActive(true);
            Heart2.SetActive(true);
            Heart3.SetActive(true);
            NoHeart1.SetActive(false);
            NoHeart2.SetActive(false);
            NoHeart3.SetActive(false);
        }
        else if (Health == 2)
        {
            RaycastHit2D raycast = Physics2D.Raycast(feet.transform.position, Vector2.down, 0.1f, layerMask);
            if (raycast.collider != null)
                rb2d.AddForce(Vector2.up * jumpForce);
            Heart1.SetActive(true);
            Heart2.SetActive(true);
            Heart3.SetActive(false);
            NoHeart1.SetActive(false);
            NoHeart2.SetActive(false);
            NoHeart3.SetActive(true);
        }
        else if (Health == 1)
        {
            RaycastHit2D raycast = Physics2D.Raycast(feet.transform.position, Vector2.down, 0.1f, layerMask);
            if (raycast.collider != null)
                rb2d.AddForce(Vector2.up * jumpForce);
            Heart1.SetActive(true);
            Heart2.SetActive(false);
            Heart3.SetActive(false);
            NoHeart1.SetActive(false);
            NoHeart2.SetActive(true);
            NoHeart3.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if(enemy != null)
        {
            foreach(ContactPoint2D point in collision.contacts)
            {
                Debug.Log(point.normal);
                Debug.DrawLine(point.point, point.point + point.normal, Color.red, 10);
                if(point.normal.y >= 0.5f)
                {
                    rb2d.AddForce(Vector2.up * jumpForce);
                    enemy.Hurt();
                }
                else
                {
                    Hurt();
                }
            }
            
        }
    }

}
