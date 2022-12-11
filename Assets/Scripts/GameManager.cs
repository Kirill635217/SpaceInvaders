using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        InProgress,
        Ended
    }

    private const string BorderTag = "Border";

    private int score;
    private float speedMultiplier = 1;
    public float SpeedMultiplier => speedMultiplier;

    #region ForUnityEditor

    [Range(0, 10)] [SerializeField] private float SpeedMultiplierPerKill;
    [Range(0, 100)] [SerializeField] private int rowCount;
    [Range(0, 100)] [SerializeField] private int enemiesPerRow;
    [SerializeField] private int startYPosition;
    [Range(0, 100)] [SerializeField] private int spaceBetweenRows;

    [SerializeField] private Invader invaderPrefab;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI scoreText;

    #endregion

    private GameState gameState = GameState.InProgress;
    private Vector2 borderSize;
    public Vector2 BorderSize => borderSize;

    private List<Invader[]> invaders = new();
    private List<InvadersRow> rows = new();

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
        if (gameOverMenu != null)
            gameOverMenu.SetActive(false);
        if (restartButton != null)
            restartButton.onClick.AddListener(Restart);
        UpdateUI();
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
            var row = new InvadersRow(invader, borderSize, startYPosition - spaceBetweenRows * rows.Count);
            row.OnLeaderMoveDown += CheckRows;
            row.OnInvaderLost += () => speedMultiplier -= SpeedMultiplierPerKill;
            rows.Add(row);
        }
    }

    void CheckRows(InvadersRow owner, bool isRightLeader)
    {
        foreach (var row in rows)
        {
            row.MoveRowDown();
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = $"{score}";
    }

    public void AddScore()
    {
        score += 25;
        UpdateUI();
    }

    public void ReachedTheBottom()
    {
        if (gameState == GameState.Ended)
            return;
        gameOverMenu.SetActive(true);
        gameState = GameState.Ended;
        Time.timeScale = 0;
    }

    void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }
}