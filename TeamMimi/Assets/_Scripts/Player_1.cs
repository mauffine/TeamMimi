using UnityEngine;
using System.Collections;

public class Player_1 : MonoBehaviour {
    float speed;
    float jump;
    Vector2 move;
    public bool canJump;
    public int Health = 100;
    public float chargeLimit=25;
    float chargeTime = 5;
    float chargeSpeed;
    float charge;
    bool isCharging;
    bool isFacingRight;
    bool isMoving;
    [SerializeField]
    int Playerno;
    [SerializeField]
    private GameObject dText;
    bool cooldown = false;
    [SerializeField]
    private GameObject Tomb;
	
    // Use this for initialization
    void Start()
    {
        canJump = true;
        jump = 14;
        speed = 3.5f;
        isFacingRight = true;
        isCharging = false;
        chargeSpeed = 7;
        charge = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        float moveHorizontal = 0;


        if (Input.GetAxis("P" + Playerno.ToString() + "xAxis") > 0.5f && !cooldown && !isCharging)
        {
            if (isFacingRight == false)
                Flip();
            moveHorizontal = moveHorizontal + speed;
            if (!isMoving && canJump)
            {
                gameObject.GetComponent<Animator>().Play("Run");
                isMoving = true;
            }
        }
        else if (Input.GetAxis("P" + Playerno.ToString() + "xAxis") < -0.5f && !cooldown && !isCharging)
        {
            if (isFacingRight == true)
                Flip();
            moveHorizontal = moveHorizontal + speed;
            if (!isMoving && canJump)
            {
                gameObject.GetComponent<Animator>().Play("Run");
                isMoving = true;
            }
        }
        else if (isMoving)
        {
            gameObject.GetComponent<Animator>().Play("Idle");
            isMoving = false;
        }

        if (Input.GetButton("P" + Playerno.ToString() + "aButton") && IsGrounded())
        {
            rigid.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("P" + Playerno.ToString() + "xButton") && IsGrounded() && isCharging == false)
        {
            if (charge < chargeLimit)
            {
                charge += 0.5f * Time.deltaTime;
            }
            if (!cooldown)
                isCharging = true;
            
        }
        if (isCharging)
        {
            if (charge < .5f)
            {
                moveHorizontal = chargeSpeed;
                charge += Time.deltaTime;
                gameObject.GetComponent<Animator>().Play("Run");
            }
            else
            {
                isCharging = false;
                cooldown = true;
                charge = 0;
                moveHorizontal = speed;

                gameObject.GetComponent<Animator>().Play("Idle");
            }
            
        }
        if (cooldown && charge < .5f)
        {
            charge += Time.deltaTime;
        }
        else if (!isCharging)
        {
            cooldown = false;
            charge = 0;
        }
        move.x = moveHorizontal;
        transform.Translate(move * speed * Time.deltaTime);

        if (Health <= 0)
            Death(rigid);
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
        int damage;
        if (isCharging)
            damage = 10;
        else
            damage = 5;

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

    void Death(Rigidbody2D rigid)
    {
        GameObject.Instantiate(Tomb,rigid.position, Quaternion.Euler(Vector3.zero));
        GameObject.Destroy(this.gameObject);
    }

}
