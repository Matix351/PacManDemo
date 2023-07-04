using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Ghost[] ghosts;

    public Pacman pacman;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text livesTex;
    [SerializeField]
    private GameObject playButtton;
    [SerializeField]
    private GameObject gameOver;

    public Transform pellets;

    public int ghostMultiplier { get; private set; } = 1;

    
    private int score = 0;

    public int lives { get; private set; }



    private void Start()
    {
        Pause();
        gameOver.SetActive(false);
        playButtton.SetActive(true);
        
    //NewGame();



}

    public void play()
    {
        playButtton.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = 1f;
        NewGame();

    }

    private void Pause()
    {
        Time.timeScale = 0f;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

        }
    }

    private void Update()
    {
        if (this.lives <=0 && Input.anyKeyDown)
        {
            NewGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();

    }

    private void NewRound()
    {
        foreach(Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();

    }


    //resetowanie duszkow i pacmana
    private void ResetState()
    {
       
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        pacman.ResetState();
        //pacman.gameObject.SetActive(true);
        //ResetGhostMultiplier();
    }

    private void GameOver()
    {

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
        

        this.gameOver.SetActive(true);
        this.playButtton.SetActive(true);

    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();

    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesTex.text = "x " + lives.ToString();
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier; 
        SetScore(this.score + ghost.points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        pacman.gameObject.SetActive(false);
        SetLives(this.lives -1);

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        } else
        {
            GameOver();
        }

    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);
        
        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < this.ghosts.Length; i++) {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
   
        //zmiana statusu duszkow

    }


    private bool HasRemainingPellets()
    {
        foreach(Transform pellet in this.pellets)
        {
            if(pellet.gameObject.activeSelf) return true;
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }



}
