using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public AudioClip audioClipEatPowerPellet;
    public AudioClip newRoundSound;
    private AudioSource audioSource;
    public Music backgroundMusic;
    

    public int score { get; private set;}
    public int lives { get; private set; }
    public int rounds { get; private set; }
    public int GhostMultiplier { get; private set; } = 1;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        
        StartNewGame();
    }
    private void StartNewGame()
    {
        backgroundMusic.Background(0.4f);
        SetScore(0);
        SetLives(3);
        SetRounds(0);
        NewRound();
    }
    private void NewRound()
    {
        //reset all
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
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
       for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }
        pacman.gameObject.SetActive(false);
        this.rounds = 1;
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
        int points = ghost.points * this.GhostMultiplier;
        SetScore(this.score + points);
        this.GhostMultiplier++;
    }
    public void PacmanEaten()
    {
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

}
