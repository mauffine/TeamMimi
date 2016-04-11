using UnityEngine;
using System.Collections;

public class Player_1 : MonoBehaviour {
    float speed;
    float jump;
    Vector2 move;
    public bool canJump;
    public int Health = 100;
    bool isFacingRight;

    // Use this for initialization
    void Start()
    {
        canJump = true;
        jump = 8;
        speed = 3.5f;
        isFacingRight = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        float moveHorizontal = Input.GetAxis("Horizontal");
       
        if (Input.GetAxis("P1xAxis") > 0.5f)
        {
            if (isFacingRight==false)
                Flip();
            moveHorizontal = moveHorizontal + speed;
        }

        if (Input.GetAxis("P1xAxis") < -0.5f)
        {
            if (isFacingRight==true)
                Flip();
            moveHorizontal = moveHorizontal + speed;
        }

        if (Input.GetButton("P1aButton") && IsGrounded())
        {
            rigid.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }

        move.x = moveHorizontal;
        rigid.transform.Translate(move * speed * Time.deltaTime);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.LogFormat("Enter: " + col.gameObject.tag);
        if (col.gameObject.CompareTag("Platform"))
        {
            canJump = true;
        }

        if(col.gameObject.CompareTag("player"))
        {
            col.gameObject.SendMessage("ApplyDamage");
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (isFacingRight)
                rb.AddForce(Vector2.left * 2, ForceMode2D.Impulse);
            if (!isFacingRight)
                rb.AddForce(Vector2.right * 2, ForceMode2D.Impulse);
        }

    }
    void OnCollisionExit2D(Collision2D col)
    {
        Debug.LogFormat("Exit: " + col.gameObject.tag);

        canJump = false;
    }

    bool IsGrounded()
    {
        return canJump;
    }

    void Flip()
    {
        if(isFacingRight==true)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        else
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        isFacingRight = !isFacingRight;

    }

}
