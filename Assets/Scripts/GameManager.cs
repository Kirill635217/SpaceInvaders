using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string BorderTag = "Border";

    [Range(0, 100)]
    [SerializeField] private int rowCount;
    [Range(0, 100)]
    [SerializeField] private int enemiesPerRow;
    [SerializeField] private int startYPosition;
    [Range(0, 100)]
    [SerializeField] private int spaceBetweenRows;

    [SerializeField] private Invader invaderPrefab;
    
    private Vector2 borderSize;
    public Vector2 BorderSize => borderSize;
    
    private List<Invader[]> invaders = new ();
    private List<InvadersRow> rows = new();
    
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
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < rowCount; i++)
        {
            invaders.Add(new Invader[enemiesPerRow]);
            var rowGO = new GameObject().transform;
            rowGO.name = $"Row{i}";
            for (int j = 0; j < enemiesPerRow; j++)
            {
                invaders[i][j] = Instantiate(invaderPrefab, Vector3.zero, Quaternion.identity, rowGO);
            }
        }
        foreach (var invader in invaders)
        {
            rows.Add(new InvadersRow(invader, borderSize, startYPosition - spaceBetweenRows * rows.Count));
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}