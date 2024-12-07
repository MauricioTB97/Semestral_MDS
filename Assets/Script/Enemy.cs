using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    private float moveSpeed = 2f;

    public int waypointIndex = 0;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        Move();
    }

    //private void Move()
    //{
    //    if (waypointIndex <= waypoints.Length - 1)
    //    {

    //        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

    //        if (transform.position == waypoints[waypointIndex].transform.position)
    //        {
    //            waypointIndex += 1;
    //        }
    //    }
    //    if (waypointIndex == waypoints.Length)
    //    {
    //        if (transform.position == waypoints[waypointIndex].transform.position)
    //        {
    //            waypointIndex = -1;
    //        }
    //    }
    //}
    private bool movingForward = true;

    private void Move()
    {
        if (waypointIndex >= 0 && waypointIndex < waypoints.Length)
        {
            Vector2 targetPosition = waypoints[waypointIndex].position;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Comparaci�n de posiciones utilizando una peque�a tolerancia para manejar las diferencias m�nimas
            if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
            {
                if (movingForward)
                {
                    waypointIndex++;
                    if (waypointIndex >= waypoints.Length)
                    {
                        movingForward = false;
                        waypointIndex = waypoints.Length - 2;
                        // Aqu� podr�as querer cambiar la velocidad de movimiento si necesitas un ajuste de comportamiento m�s suave
                        // moveSpeed *= -1; // Invertir la direcci�n del movimiento (si es necesario)
                        FlipSprite();
                    }
                }
                else
                {
                    waypointIndex--;
                    if (waypointIndex < 0)
                    {
                        movingForward = true;
                        waypointIndex = 1;
                        // Aqu� tambi�n podr�as querer cambiar la velocidad de movimiento si necesitas un ajuste m�s suave
                        // moveSpeed *= -1; // Invertir la direcci�n del movimiento (si es necesario)
                        FlipSprite();
                    }
                }
            }
        }
    }
    void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("da�o");
            Player player = collision.GetComponent<Player>();
            player.animator.SetTrigger("IsHurt");
            GameController.Instance.LoseLives();
        }
        if (collision.CompareTag("pew"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

    }
}
