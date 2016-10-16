﻿using UnityEngine;
using System.Collections;

// reference to the UI namespace
using UnityEngine.UI;

// reference to manage my scenes
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    // PRIVATE INSTANCE VARIABLES ++++++++++++++++++
    private int _livesValue;
    private int _scoreValue;
    //private AudioSource _endGameSound;


    [Header("UI Objects")]
    public Text LivesLabel;
    public Text ScoreLabel;
    //public Text GameOverLabel;
    //public Text FinalScoreLabel;
    //public Button RestartButton;

    // PUBLIC PROPERTIES +++++++++++++++++++++++++++
    public int LivesValue {
        get {
            return this._livesValue;
        }

        set {
            this._livesValue = value;
            if (this._livesValue <= 0) {
                //this._endGame();
            } else {
                this.LivesLabel.text = "Lives: " + this._livesValue;
            }
        }
    }

    public int ScoreValue {
        get {
            return this._scoreValue;
        }

        set {
            this._scoreValue = value;
            this.ScoreLabel.text = "Score: " + this._scoreValue;
        }
    }


    // Use this for initialization
    void Start() {
        this.LivesValue = 5;
        this.ScoreValue = 0;

        //this.GameOverLabel.gameObject.SetActive(false);
        //this.FinalScoreLabel.gameObject.SetActive(false);
        //this.RestartButton.gameObject.SetActive(false);
        //this._endGameSound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
    }

    private void _endGame() {
        this.ScoreLabel.gameObject.SetActive(false);
        this.LivesLabel.gameObject.SetActive(false);
        //this.GameOverLabel.gameObject.SetActive(true);
        //this.FinalScoreLabel.text = "Final Score: " + this.ScoreValue;
        //this.FinalScoreLabel.gameObject.SetActive(true);
        //this.RestartButton.gameObject.SetActive(true);
        //this._endGameSound.Play();
    }

    // PUBLIC METHODS ++++++++++++++++++++++++++++++
    public void RestartButton_Click() {
        SceneManager.LoadScene("Play");
    }
}