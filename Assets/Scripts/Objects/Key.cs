using UnityEngine;

public class Key : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != player) return;

        player.GetComponent<PlayerController>().hasKey = true;
        // GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.gameObject);
    }

}