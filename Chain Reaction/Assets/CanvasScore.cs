﻿using System;
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
    public Image turnPanelImage;
    public int playerTurn;
    float turnAlpha;
    public Text wonText;
    public Image wonImage;

    private bool playAudio;
    public float audioCooldown;
    private float timer;

    //Audio
    [EventRef]
    public string audioStart;

    EventInstance eventStart;


    private void Awake()
    {
        turnAlpha = 2f;
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

        turnAlpha -= Time.deltaTime;
        if(turnAlpha > 0)
        {
            turnText.color = new Color(turnText.color.r, turnText.color.g, turnText.color.b, turnAlpha);
            turnPanelImage.color = new Color(turnPanelImage.color.r, turnPanelImage.color.g, turnPanelImage.color.b, turnAlpha);
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
        float multiplier = 2f;
        int visualizedScore = Mathf.RoundToInt(score * multiplier);
        if (buildings[playerNr].score > ((float)buildings[playerNr].MaxScore/ multiplier))
            PlayerWon(playerNr);
        string percentage = buildings[playerNr].MaxScore > 0 ? Mathf.Max(100 - ((float)visualizedScore / buildings[playerNr].MaxScore) * 100f, 0).ToString("0.0") : "100";
        playerTextScores[(int)playerNr].text = playerNr + " = " + percentage + "%";
    }

    private void PlayerWon(PlayerScore.PlayerNr playerNr)
    {
        wonImage.gameObject.SetActive(true);
        PlayerScore.PlayerNr otherPlayer = playerNr == PlayerScore.PlayerNr.P1 ? PlayerScore.PlayerNr.P2 : PlayerScore.PlayerNr.P1;
        wonText.text = "PLAYER " + ((int)otherPlayer + 1) + " WON!";
    }

    public void NextTurn()
    {
        playerTurn = (playerTurn + 1) % Enum.GetValues(typeof(PlayerScore.PlayerNr)).Length;
        ShowPlayerTurn();
    }

    private void ShowPlayerTurn()
    {
        turnAlpha = 2f;
        turnText.text = "Player " + (playerTurn + 1);
    }
}
