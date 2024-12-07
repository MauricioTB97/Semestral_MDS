using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float velocidad = 5f;
    public float tiempocambiodireccion = 2f;
    private float tiempopasado = 0f;
    private Transform jugador;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;

    }
    void Update()

    {
        seguirjugador();
        tiempopasado += Time.deltaTime;
        if (tiempopasado >= tiempocambiodireccion)
        {

            cambiardireccionaleatoria();
            tiempopasado = 0f;
        }
    }

    void seguirjugador()
    {
        if (jugador != null)
        {
            Vector2 direccionaljugador = (jugador.position - transform.position).normalized;
            Vector3 movimiento = new Vector3(direccionaljugador.x, direccionaljugador.y, 0) * velocidad * Time.deltaTime;
            transform.Translate(movimiento);
        }

    }

    void cambiardireccionaleatoria()
    {

        float randomx = Random.RandomRange(-1f, 1f);
        float randomy = Random.RandomRange(-1f, 1f);
        Vector2 direccionaleatoria = new Vector2(randomx, randomy).normalized;

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("daño");
            Player player = collision.GetComponent<Player>();
            player.animator.SetTrigger("IsHurt");
            GameController.Instance.LoseLives();
            Destroy(gameObject);
        }
        if (collision.CompareTag("pew"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
