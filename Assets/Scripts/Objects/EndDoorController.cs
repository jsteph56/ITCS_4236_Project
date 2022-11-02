using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoorController : MonoBehaviour
{
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entered Door collision");
        if (collision.gameObject != player) return;

        if (player.GetComponent<PlayerController>().CheckCode())
        {
            Debug.Log("Close Application");
            Time.timeScale = 0;
            Application.Quit();
        }
    }
}
