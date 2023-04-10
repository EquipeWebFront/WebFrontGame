using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class player : MonoBehaviour
{

    public float Speed;
    public float jumpForce;

    public bool isGirl;
    public bool isFire;
    public bool isJumping;
    public bool doubleJump;

    private Rigidbody2D rig;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        charSelector();
    }

    void charSelector(){
        if (isFire == false && isGirl == false) {
            anim.SetBool("girl", false);
            anim.SetBool("fire", false);
        }
        if (isFire == true && isGirl == false) {
            anim.SetBool("girl", false);
            anim.SetBool("fire", true);
        }
        if (isFire == false && isGirl == true) {
            anim.SetBool("girl", true);
            anim.SetBool("fire", false);
        }
        if (isFire == true && isGirl == true) {
            anim.SetBool("girl", true);
            anim.SetBool("fire", true);
        }
    }

    void Move() 
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }

        if(Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }
        if(Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("walk", false);
        }

    }

    void Jump() 
    {
        if(Input.GetButtonDown("Jump"))
            if(!isJumping)
            {
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("jump", true);
            }
            else
            {
                if(doubleJump)
                {
                    rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }

        if (collision.gameObject.tag == "Spike")
        {
            Debug.Log("Tocou o espinho!");
            Destroy(gameObject);
            /* Ap�s o teste:
            player.instance.RestartGame();
            */
            SceneManager.LoadScene("lvl_1_fr");
        }

    }

    /* Ap�s o teste:
    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }
    */

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isJumping = true;
        }
    }
}
