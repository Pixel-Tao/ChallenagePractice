using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerWeek4 : MonoBehaviour
{
    public static GameManagerWeek4 Instance;

    [SerializeField] private TextMeshProUGUI scoreText;

    public Vector2 minVector;
    public Vector2 maxVector;
    public PanelPositionType[] panelPositionTypes;
    public InputController inputController;
    private Camera cam;
    private List<MovePanel> panels;

    private int bestScore;
    private int score;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
        Init();
    }

    private void Init()
    {
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));
        minVector = -topRight;
        maxVector = topRight;

        CreatePanels();
        CreateBall();

        UpdatePointText(score, bestScore);
    }

    private void CreatePanels()
    {
        panels = new List<MovePanel>();

        foreach (PanelPositionType panelPositionType in panelPositionTypes)
        {
            if (panelPositionType == PanelPositionType.None)
                continue;

            GameObject go = new GameObject($"PlayerPanel_{panelPositionType}");
            go.tag = "Player";
            MovementController movement = go.AddComponent<MovementController>();
            MovePanel playerPanel = go.AddComponent<MovePanel>();
            playerPanel.SetData(panelPositionType);
            inputController.AddMoveTarget(movement);
            panels.Add(playerPanel);
        }
    }

    public MovePanel GetRandomPanel(MovePanel without)
    {
        int index = Random.Range(0, panels.Count - 1);
        for (int i = 0; i < panels.Count; i++)
        {
            if (panels[index] == without)
            {
                index++;
                if (index >= panels.Count)
                {
                    index = 0;
                }
            }
            else
            {
                return panels[index];
            }
        }

        return panels[index];
    }

    private void CreateBall()
    {
        GameObject go = new GameObject("Ball");
        go.tag = "Ball";
        go.transform.position = Vector2.zero;
        go.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        Ball ball = go.AddComponent<Ball>();
        MovePanel panel = panels[Random.Range(0, panels.Count)];
        ball.SetStartTarget(panel);
        ball.Shoot();
    }

    public void IncreaseScore()
    {
        score += 1;
        if (score > bestScore)
            bestScore = score;

        UpdatePointText(score, bestScore);
    }

    public void ResetScore()
    {
        score = 0;
        UpdatePointText(score, bestScore);
    }

    private void UpdatePointText(int value, int maxValue)
    {
        if (scoreText == null) return;

        scoreText.text = $"Score\n{value}\nBest\n{maxValue}";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(maxVector.x - minVector.x, maxVector.y - minVector.y, 1));
    }

}
