using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI Gems;
    public GameObject[] lives;

    void Start()
    {
        
    }

    void Update()
    {
        Gems.text = GameController.Instance.TotalPoint.ToString();
    }

    public void UpdatePoints(int puntostotales)
    {
        Gems.text = puntostotales.ToString();
    }
    public void LivesOff(int index)
    {
        lives[index].SetActive(false);
    }
    public void Liveson(int index)
    {
        lives[index].SetActive(true);
    }

}
