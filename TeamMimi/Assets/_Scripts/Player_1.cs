﻿using UnityEngine;
using System.Collections;

public class Player_1 : MonoBehaviour {
    float speed;
    float jump;
    Vector2 move;
    public bool canJump;
    public int Health = 100;
    bool isFacingRight;
    bool isMoving;
    [SerializeField]
    int Playerno;
    [SerializeField]
    private GameObject dText;

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
        float moveHorizontal = 0;


        if (Input.GetAxis("P" + Playerno.ToString() + "xAxis") > 0.5f)
        {
            //Debug.LogFormat("Player no: {0}", Playerno);
            if (isFacingRight == false)
                Flip();
            moveHorizontal = moveHorizontal + speed;
            if (!isMoving && canJump)
            {
                gameObject.GetComponent<CharacterAnimator>().Animate(1);
                isMoving = true;
            }
        }
        else if (Input.GetAxis("P" + Playerno.ToString() + "xAxis") < -0.5f)
        {
            if (isFacingRight == true)
                Flip();
            moveHorizontal = moveHorizontal + speed;
            if (!isMoving && canJump)
            {
                gameObject.GetComponent<CharacterAnimator>().Animate(1);
                isMoving = true;
            }
        }
        else if (isMoving)
        {
            gameObject.GetComponent<CharacterAnimator>().Animate(0);
            isMoving = false;
        }

        if (Input.GetButton("P" + Playerno.ToString() + "aButton") && IsGrounded())
        {
            rigid.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }
        move.x = moveHorizontal;
        transform.Translate(move * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.LogFormat("Enter: " + col.gameObject.tag);
        if (col.gameObject.CompareTag("Platform"))
        {
            canJump = true;
        }

       

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Weapon"))
        {
            Debug.LogFormat("Hit Player", col.gameObject.tag);
            ApplyDamage();
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (isFacingRight)
                rb.AddForce(Vector2.left * 0.5f, ForceMode2D.Impulse);
            if (!isFacingRight)
                rb.AddForce(Vector2.right * 0.5f, ForceMode2D.Impulse);
        }
    }


    void OnCollisionExit2D(Collision2D col)
    {
        Debug.LogFormat("Exit: " + col.gameObject.tag);
        if(col.gameObject.CompareTag("Platform"))
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

    void ApplyDamage()
    {
        int damage = 10;
        Health -= damage;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 tPos;
        if (isFacingRight)
            tPos = new Vector2(rb.transform.position.x - 1.3f, rb.transform.position.y + 0.5f);
        else
            tPos = new Vector2(rb.transform.position.x - .7f, rb.transform.position.y + 0.5f);
        dText.GetComponent<DamageText>().CreateText(tPos, damage.ToString());
        Debug.Log(tPos);
    }

}
