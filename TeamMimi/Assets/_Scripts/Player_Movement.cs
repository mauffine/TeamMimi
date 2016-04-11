using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour
{
    float speed;
    float jump; 
    Vector2 move;
   public bool canJump;

	// Use this for initialization
	void Start ()
    {
        canJump = true;
        jump = 300;
        speed = 0.00000003f;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        float moveHorizontal = Input.GetAxis("Horizontal");
        move.x = moveHorizontal;
        rigid.transform.Translate(move);
       
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //moveHorizontal -= speed;
            rigid.velocity = 
            rigid.AddForce(Vector2.left * speed, ForceMode2D.Force);
        }
       
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //moveHorizontal += speed;
            rigid.AddForce(Vector2.right * speed, ForceMode2D.Force);
        }
       
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            rigid.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }
    }

    void OnCollisonEnter(Collision2D col)
    {
        if(col.gameObject.tag == "Platform")
        {
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
            canJump = true;
        }
    }
    void OnCollisonExit(Collision2D col)
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.constraints = RigidbodyConstraints2D.None;
        canJump = false;
    }

}
