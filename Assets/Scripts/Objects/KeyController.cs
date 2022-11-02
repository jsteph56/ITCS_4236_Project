using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] int KeyTag;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject != player) return;

        player.GetComponent<PlayerController>().code[1, KeyTag] = 1;
        Debug.Log("Retrieved key labeled: " + this.name);
        Debug.Log("CheckCode Statis: " + player.GetComponent<PlayerController>().CheckCode());

        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this);
    }
}
