using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    public int valor = 10;
    public GameController gc;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //animator.SetTrigger("Fx");
            Debug.Log("puntos a�adidos");
            animator = collision.GetComponent<Animator>();
            gc.AddPoints(valor);
            Destroy(this.gameObject);
        }

    }
}
