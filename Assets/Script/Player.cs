using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public int MaxJumps = 1;
    public int gems, JumpsRemaining;
    public float jumpForce = 8f;
    public Vector2 direction;
    public GameController gameController;

    public Collider2D col;
    Rigidbody2D rb;
    public Animator animator;
    CapsuleCollider2D boxCollider;
    public LayerMask groundLayer;
    public GameController gc;
    public GameObject flyenemy, proyectil;
    // Tiempo entre disparos
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<CapsuleCollider2D>();
        JumpsRemaining = MaxJumps;
    }

    void Update()
    {
        GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
        if (gameController.Lives > 0)
        {
            Move();
            Jump();
            if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFireTime)
            {
                Disparar();
            }

        }
        else
        {
            animator.SetBool("IsHurt", true);
            rb.velocity = new Vector2(0, 0);
            animator.SetBool("IsRunning", false);
            StartCoroutine(Moricion());
            col.enabled = false;
            Destroy(gameObject, 2);
        }
    }


    void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            animator.SetBool("IsRunning", true);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            animator.SetBool("IsRunning", true);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("IsRunning", false);
        }
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);

    }
    void Disparar()
    {
        nextFireTime = Time.time + fireRate;
        Vector3 direccion = transform.localScale.x > 0 ? Vector3.right : Vector3.left;

        GameObject bala = Instantiate(proyectil, transform.position, Quaternion.identity);
        Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();
        rbBala.velocity = direccion * 10f; 
        if (direccion == Vector3.left)
        {
            Vector3 escala = bala.transform.localScale;
            escala.x *= -1;
            bala.transform.localScale = escala;
        }


    }
    void Jump()
    {
        if (IsGrounded())
        {
            JumpsRemaining = MaxJumps;
        }
        if (Input.GetKeyDown(KeyCode.Space) && JumpsRemaining > 0)
        {
            animator.SetTrigger("IsJumping");
            JumpsRemaining--;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x - 0.9f, boxCollider.bounds.size.y), 0f, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Perk-Jump"))
        {
            MaxJumps++;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "key")
        {
            Debug.Log("Pegatis");
            gc.Ganaste();
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Activador1")
        {
            Instantiate(flyenemy, new Vector3(218, -39, 0), Quaternion.identity);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Activador2")
        {
            Instantiate(flyenemy, new Vector3(254, -7, 0), Quaternion.identity);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Activador3")
        {
            Instantiate(flyenemy, new Vector3(314, 22, 0), Quaternion.identity);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Activador4")
        {
            Instantiate(flyenemy, new Vector3(375, 28, 0), Quaternion.identity);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Activador5")
        {
            Instantiate(flyenemy, new Vector3(442, 48, 0), Quaternion.identity);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Activador6")
        {
            Instantiate(flyenemy, new Vector3(475, 93, 0), Quaternion.identity);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Activador7")
        {
            Instantiate(flyenemy, new Vector3(528, 95, 0), Quaternion.identity);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Activador8")
        {
            Instantiate(flyenemy, new Vector3(665, -93, 0), Quaternion.identity);

        }
    }

    IEnumerator Moricion()
    {
        rb.velocity = new Vector2(0, 10);
        yield return new WaitForSeconds(0.3f);

        rb.velocity = new Vector2(0, -10);
    }

}
