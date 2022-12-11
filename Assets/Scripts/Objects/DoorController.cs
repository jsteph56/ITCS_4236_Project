using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] int moveScene;
    
    private GameObject player;
    private bool activeTrigger;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        activeTrigger = false;
    }

    void Update()
    {
        if (activeTrigger && player.GetComponent<PlayerController>().hasKey && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(moveScene);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player") return;

        GetComponentInChildren<MeshRenderer>().enabled = true;
        activeTrigger = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
        activeTrigger = false;
    }
}