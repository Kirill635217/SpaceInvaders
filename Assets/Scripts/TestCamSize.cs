using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var cam = FindObjectOfType<Camera>();
        transform.localScale = new Vector3(2 * cam.aspect * cam.orthographicSize, 2 * cam.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
