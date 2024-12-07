using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("daño");
            Player player = collision.GetComponent<Player>();
            player.animator.SetTrigger("IsHurt");
            GameController.Instance.LoseLives();
        }
    }
}
