using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMOD.Studio;
using FMODUnity;

public class CanvasScore : MonoBehaviour {

    public static CanvasScore instance;

    public Dictionary<PlayerScore.PlayerNr, PlayerScore> buildings;
    public Text[] playerTextScores;
    public Dictionary<ScoreDestructableComponent.DestructableType, int> typeToPoints;

    //Audio
    [EventRef]
    public string audioStart;

    EventInstance eventStart;

    private void Awake()
    {
        instance = this;
        buildings = new Dictionary<PlayerScore.PlayerNr, PlayerScore>();
        for (int i = 0; i < playerTextScores.Length; i++)
        {
            buildings.Add((PlayerScore.PlayerNr)i, new PlayerScore());
            SetScore((PlayerScore.PlayerNr)i, 0);
        }

        typeToPoints = new Dictionary<ScoreDestructableComponent.DestructableType, int>();
        typeToPoints.Add(ScoreDestructableComponent.DestructableType.Roof, 10);
        typeToPoints.Add(ScoreDestructableComponent.DestructableType.Wall, 1);
    }

    private void Start()
    {
        eventStart = RuntimeManager.CreateInstance(audioStart);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    internal void Register(PlayerScore.PlayerNr owner, ScoreDestructableComponent.DestructableType type)
    {
        Debug.Log("Registering object " + type);
        buildings[owner].MaxScore += typeToPoints[type];
    }

    public void AddScore(PlayerScore.PlayerNr playerNr, ScoreDestructableComponent.DestructableType type)
    {
        SetScore(playerNr, buildings[playerNr].score + typeToPoints[type]);
        eventStart.start();

    }

    public void SetScore(PlayerScore.PlayerNr playerNr, int score)
    {
        buildings[playerNr].score = score;
        string percentage = buildings[playerNr].MaxScore > 0 ? (100 - ((float)score / buildings[playerNr].MaxScore) * 100f).ToString("0.0") : "100";
        playerTextScores[(int)playerNr].text = playerNr + " = " + percentage + "%";
    }
}
