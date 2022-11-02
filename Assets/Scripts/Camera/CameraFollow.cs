using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;
    [SerializeField] float followSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, followSpeed);
    }

    
    void OnGUI()
    {
        GUI.Label(new Rect(player.transform.position.x, player.transform.position.y - 1, 10, 20), player.GetComponent<PlayerController>().fear.ToString());
    }
}
