using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState{
    PLAYING,
    PAUSE,
    DIE,    
    END
}

public class GameManager : MonoBehaviour
{
    /*==================================
            GAMEPLAY ELEMENTS
    ====================================*/
    
    private GameState state;
    private int Score = 0;

    [Header("Gameplays")]
    public int maxLife = 3;
    private int currentLife;
    [SerializeField] float respawnTime = 1f;
    [SerializeField] float invinciblityTime = 0.5f;
    

    /*====================================
            HUD ELEMENTS
    ======================================*/
    [Header("HUD")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI extraLifeText;

    /*====================================
            GAME OBJECTS REFERENCES
    ======================================*/
    private GameObject player;
    private Collider2D collider2d;
    private GameObject spawner;

    /*====================================
            MENU SYSTEMS
    ======================================*/
    [Header("Menu")]
    public GameObject PauseMenu;
    public GameObject EndMenu;

    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button saveBtn;
    [SerializeField] private TextMeshProUGUI finalScore;
    
    /*====================================
            Sound FXs
    ======================================*/
    [Header("Sound Effects")]
    [SerializeField] AudioSource DieSFX;
    [SerializeField] AudioSource GameOverSFX;

    void Start()
    {
        player = GameObject.Find("Player");
        collider2d = player.GetComponent<Collider2D>();
        
        currentLife = maxLife +1;
        UpdateLife();

        Score = -1;
        UpdateScore();

        spawner = GameObject.Find("AsteroidsSpawner");

        GameStateChange(GameState.PLAYING);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(state == GameState.PLAYING)
                GameStateChange(GameState.PAUSE);
            else if(state == GameState.PAUSE)
                GameStateChange(GameState.PLAYING);
        }
    }

    public void GameStateChange(GameState newState)
    {
        this.state = newState;
        Debug.Log("Current State : "  + this.state.ToString());
        switch (state)
        {
            case GameState.PLAYING:
                PlayState();
                return;
            
            case GameState.PAUSE:
                PauseState();
                return;

            case GameState.DIE:
                Die();
                return;
            
            case GameState.END:
                EndGame();
                return;
        }
    }

    private void PlayState()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        EndMenu.SetActive(false);
    }

    private void PauseState()
    {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
    }
    
    private void Die()
    {
        //Die
        collider2d.enabled = false;
        player.SetActive(false);   

        DieSFX.Play();
         
        //Update life left
        UpdateLife();    

        if(Score > 0)
        {
            Score -= 2;
            UpdateScore();
        }

        if(currentLife <= 0)
        {
            GameStateChange(GameState.END);
            return;
        }
        else
        {
            //wait to respawn
            StartCoroutine(nameof(RespawnAfterTimer));  
        }
        
        //State change back to PLAYING
        GameStateChange(GameState.PLAYING);
    }

    private void EndGame()
    {
        GameOverSFX.Play();

        spawner.SetActive(false);
        EndMenu.SetActive(true);

        finalScore.SetText("Your Score: " + Score);
    }

    public void UpdateScore()
    {
        Score++;
        scoreText.text = ""+Score;
        //Debug.Log("SCORE = "  + Score);
    }

    public void UpdateLife()
    {
        currentLife -= 1;
        extraLifeText.text = "x "+currentLife;
    }

    private IEnumerator RespawnAfterTimer()
    {
        yield return new WaitForSeconds(respawnTime);

        //Respawn with invincibility
        player.transform.position = Vector3.zero;
        player.transform.eulerAngles = Vector3.zero;
        player.SetActive(true);

        yield return new WaitForSeconds(invinciblityTime);
        
        //Can take damage
        collider2d.enabled = true;   
    }

    //--------------------------------------------
    //          PAUSE MENU FUNCTIONS
    //--------------------------------------------
    public void OnResumeBtnClicked()
    {
        GameStateChange(GameState.PLAYING);
    }

    public void OnMainTitleBtnClicked()
    {
        //Load the main title
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    //-------------------------------------------
    //          END MENU FUCTIONS
    //-------------------------------------------
    public void OnRetryBtnClicked()
    {
        //Reload the main scene
        SceneManager.LoadScene( SceneManager.GetActiveScene().name , LoadSceneMode.Single);
    }

    public void OnSaveBtnClicked()
    {
        //Save the player name with high score
        string newPlayerName = nameInput.text ;
        
        //Debug.Log("Name: " + newPlayerName + " || Score : " + Score );
        SaveSystem.SaveHighScore(newPlayerName , Score);

        saveBtn.interactable = false;
        
    }

}
