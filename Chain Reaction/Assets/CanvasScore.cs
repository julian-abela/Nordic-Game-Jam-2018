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
    public Text turnText;
    public int playerTurn;

    private bool playAudio;
    public float audioCooldown;
    private float timer;

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
        playAudio = true;
        timer = audioCooldown;
    }

    internal void Initialize()
    {
        ShowPlayerTurn();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (playAudio == false)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = audioCooldown;
                playAudio = true;
            }
        }
    }

    internal void Register(PlayerScore.PlayerNr owner, ScoreDestructableComponent.DestructableType type)
    {
        Debug.Log("Registering object " + type);
        buildings[owner].MaxScore += typeToPoints[type];
    }

    public void AddScore(PlayerScore.PlayerNr playerNr, ScoreDestructableComponent.DestructableType type)
    {
        SetScore(playerNr, buildings[playerNr].score + typeToPoints[type]);

        if (playAudio)
        {
            eventStart.start();

            playAudio = false;
        }
    }

    public void SetScore(PlayerScore.PlayerNr playerNr, int score)
    {
        buildings[playerNr].score = score;
        string percentage = buildings[playerNr].MaxScore > 0 ? (100 - ((float)score / buildings[playerNr].MaxScore) * 100f).ToString("0.0") : "100";
        playerTextScores[(int)playerNr].text = playerNr + " = " + percentage + "%";
    }
    
    public void NextTurn()
    {
        playerTurn = (playerTurn + 1) % Enum.GetValues(typeof(PlayerScore.PlayerNr)).Length;
        ShowPlayerTurn();
    }

    private void ShowPlayerTurn()
    {
        turnText.text = "Player " + (playerTurn + 1);
    }
}
