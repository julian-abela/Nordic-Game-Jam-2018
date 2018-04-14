using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScore : MonoBehaviour {

    public static CanvasScore instance;

    public Dictionary<PlayerScore.PlayerNr, PlayerScore> scores;
    public Text[] playerTextScores;
    public Dictionary<ScoreDestructableComponent.DestructableType, int> typeToPoints;

    private void Awake()
    {
        instance = this;
        scores = new Dictionary<PlayerScore.PlayerNr, PlayerScore>();
    }

    void Start () {
        typeToPoints = new Dictionary<ScoreDestructableComponent.DestructableType, int>();
        typeToPoints.Add(ScoreDestructableComponent.DestructableType.Roof, 10);
        typeToPoints.Add(ScoreDestructableComponent.DestructableType.Wall, 1);

        for (int i = 0; i < playerTextScores.Length; i++)
        {
            scores.Add((PlayerScore.PlayerNr)i, new PlayerScore());
            SetScore((PlayerScore.PlayerNr)i, 0);
        }
	}

    public void AddScore(PlayerScore.PlayerNr playerNr, ScoreDestructableComponent.DestructableType type)
    {
        SetScore(playerNr, scores[playerNr].score + typeToPoints[type]);
    }

    public void SetScore(PlayerScore.PlayerNr playerNr, int score)
    {
        scores[playerNr].score = score;
        playerTextScores[(int)playerNr].text = playerNr + " = " + score.ToString();
    }
}
