using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int[,] code;
    public float fear;

    // Start is called before the first frame update
    void Start()
    {
        code = new int[2, 4] {{5, 7, 4, 1}, {0, 0, 0, 0}};
        fear = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckCode()
    {
        for (int i = 0; i < code.GetLength(1); i++)
        {
            if (code[1, i] == 0) return false;
        }

        return true;
    }
}
