using UnityEngine;
using UnityEngine.TextCore;

public class Health : MonoBehaviour
{
    private TMPro.TextMeshPro textMesh;
    private float stoneTouched;

    void Start()
    {
        textMesh = GetComponent<TMPro.TextMeshPro>();
    }

    void Update()
    {
        stoneTouched = GetComponentInParent<PlayerController>().stoneTouched;

        textMesh.text = "Stone Rot: " + stoneTouched;
    }
}