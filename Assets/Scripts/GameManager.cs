using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    [Header("Scripts/Class")]
    public Ghost[] ghosts;
    public Pacman pacman;

    [Header("Pellets")]
    public Transform pellets;

    [Header("Audio")]
    public AudioClip audioClipEatPowerPellet;
    public AudioClip newRoundSound;
    private AudioSource audioSource;
    public Music backgroundMusic;
    public AudioClip pacmanDies;
    public AudioClip pacmanEat;
    public AudioClip pacmanEatten;

    [Header("Game Objects / Fruits")]
    public GameObject[] fruits;

    public int score { get; private set;}//private
    public int lives { get; private set; }
    public int rounds { get; private set; }
    public int GhostMultiplier { get; private set; } = 1;
    public int recordScore { get; set; }
   
    
    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
        DisabledFruits();
        StartNewGame();
    }
    private void StartNewGame()
    { 
        backgroundMusic.Background(0.4f);
        SetScore(0);
        SetLives(3);
        SetRounds(0);
        LoadFromFile();
        NewRound();
    }
    private void NewRound()
    {
        //reset all
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        if (this.rounds % 5 == 0 && this.lives > 0 && rounds != 0)
        {
            int choice = Random.Range(0, fruits.Length - 1);
            fruits[choice].gameObject.SetActive(true);
        }
        this.rounds++;
        if (rounds>1)
        {
            AudioSoundOn(newRoundSound,0.2f);
        }       
        ResetState();      
    }
   
    private void ResetState() //если пакман умрет
    {
        ResetGhostMultipler();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }
        pacman.ResetState();
    }
    private void GameOver()
    {
        backgroundMusic.StopMusic();
        AudioSoundOn(pacmanDies);
       for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }
        pacman.gameObject.SetActive(false);
        this.rounds = 1;
        SaveInFile();
        
    }
    private void SaveInFile()
    {
        if (recordScore < this.score)
        {
            PlayerPrefs.SetInt("SavedInteger", this.score);
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
        }
    }
    private void LoadFromFile()
    {
        try
        {           
                if (PlayerPrefs.HasKey("SavedInteger"))
                {
                    this.recordScore = PlayerPrefs.GetInt("SavedInteger");
                    Debug.Log("Game data loaded!");
                }
                else
                    Debug.LogError("There is no save data!");         
        }
        catch (System.Exception e)
        {
            e.Message.ToString();
            throw;
        }
       
    }
    private void SetScore(int score)
    {
        this.score = score;
    }
    private void SetLives(int lives)
    {
        this.lives = lives;
    }
    private void SetRounds(int rounds)
    {
        this.rounds= rounds;
    }
    public void GhostEaten(Ghost ghost)
    {
        AudioSoundOn(pacmanEat);
        int points = ghost.points * this.GhostMultiplier;
        SetScore(this.score + points);
        this.GhostMultiplier++;
    }
    public void PacmanEaten()
    {
        AudioSoundOn(pacmanEatten);
        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives - 1);
        if (this.lives > 0)
            Invoke(nameof(ResetState), 3f); //ждем 3 секунды, вызываем метод(т.е.пакмана с призраками)
        else
            GameOver();
    }
    private void Update()
    {
        if (this.lives<=0 && Input.anyKeyDown)
        {
            StartNewGame();
        }
    }
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score+pellet.points);

        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
        
    }
    private void AudioSoundOn(AudioClip audioClip,float volume=1.0f)
    {
        audioSource.enabled = true;
        audioSource.clip = audioClip;
        audioSource.volume = volume;   
        audioSource.Play();

    }
    public void PowerPelletEaten(PowerPellet powerPellet)
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frighned.Enable(powerPellet.direction);
        } 
        PelletEaten(powerPellet);
        AudioSoundOn(audioClipEatPowerPellet);
        CancelInvoke();
        Invoke(nameof(GhostMultiplier), powerPellet.direction);
       
    }
    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
          
        }
        return false;
    }
    private void ResetGhostMultipler()
    {
        this.GhostMultiplier = 1;
    }
    #region фрукты/fruits
    public void CherryEaten(Cherry cherry)
    {
        cherry.gameObject.SetActive(false);
        SetScore(this.score + cherry.points);
    }
    public void StrawberryEaten(Strawberry strawberry)
    {
        strawberry.gameObject.SetActive(false);
        SetScore(this.score + strawberry.points);
    }
    public void OrangeEaten(Orange orange)
    {
        orange.gameObject.SetActive(false);
        SetScore(this.score + orange.points);
    }
    public void MelonEaten(Melon melon)
    {
        melon.gameObject.SetActive(false);
        SetScore(this.score + melon.points);
    }
    public void KeyEaten(Key key)
    {
        key.gameObject.SetActive(false);
        SetScore(this.score + key.points);
    }
    public void GalaxianEaten(Galaxian galaxian)
    {
        galaxian.gameObject.SetActive(false);
        SetScore(this.score + galaxian.points);
    }
    public void BellEaten(Bell bell)
    {
        bell.gameObject.SetActive(false);
        SetScore(this.score + bell.points);
    }
    public void AppleEaten(Apple apple)
    {
        apple.gameObject.SetActive(false);
        SetScore(this.score + apple.points);
    }
    #endregion
    private void DisabledFruits()
    {
        foreach (GameObject fruits in this.fruits)
        {
            fruits.gameObject.SetActive(false);
        }      
    }
}

