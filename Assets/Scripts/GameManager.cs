using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string BorderTag = "Border";
    private Vector2 borderSize;
    public Vector2 BorderSize => borderSize;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
        SetBorders();
    }

    void SetBorders()
    {
        var cam = FindObjectOfType<Camera>();
        borderSize.x = 2 * cam.aspect * cam.orthographicSize;
        borderSize.y = cam.orthographicSize;
        var upperBorder = new GameObject().AddComponent<BoxCollider2D>();
        upperBorder.transform.position = new Vector3(0, borderSize.y + .5f);
        upperBorder.size = new Vector2(borderSize.x, .24f);
        upperBorder.isTrigger = true;
        upperBorder.gameObject.tag = BorderTag;
        upperBorder.name = "UpperBorder";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
