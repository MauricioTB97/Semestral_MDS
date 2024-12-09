using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherrys : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("vida");
            bool liverestore = GameController.Instance.AddLives();
            if (liverestore)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
