using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    public float speed;
    public GameObject restartBtn;

    private float amountToMove;
    private bool isDead;

    private Vector3 direction;

    private int score; //Realtime score
    private int pickUpCount; //How many items collected
    private static bool isFirstRun = true;
    public Text scoreText;
    public Text highScoreText;
    public Text scoreOverText;

    public Animator gameOverAnim;
    public GameObject player;
    public GameObject partical;
    public GameObject firstRunObject;

    public AudioClip[] a_Clip;

    void Start ()
    {
        isDead = false;
        direction = Vector3.zero; //Set player to stay constant at start   
        score = 0;     

        scoreText.gameObject.SetActive(false); //u

        if (isFirstRun == true)
        {
            //do stuff
            
            firstRunObject.SetActive(true);
        }
        

    }
	
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space") || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3") || Input.GetButtonDown("Jump"))
        {
            score++;
            scoreText.text = score.ToString();
            speed = speed + 0.12f;
            print(" Player Speed Updated: " + speed);
            if (direction == Vector3.forward)
            {
                direction = Vector3.left;
            }
            else
            {
                direction = Vector3.forward;
            }
            isFirstRun = false;
            firstRunObject.SetActive(false);
        }

        amountToMove = (speed * Time.deltaTime);
        transform.Translate(amountToMove * direction); // movement calculation
        PlayerFallDownCheck();

        if (isDead == true && Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene(0);
        }
    }

    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter(Collider other) //Collider enter
    {
        if (other.tag == "PickUp")
        {
            PlaySound(0);
            pickUpCount++;
            other.gameObject.SetActive(false);
            Instantiate(partical, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            score += 3;
            scoreText.text = score.ToString();
            
            print("Total Pick Up: " + pickUpCount);
            
        }
        if (other.tag == "Enemy")
        {
            GameOver();
            isDead = true;
        }

        if (isDead != true)
        {
            scoreText.gameObject.SetActive(true);
        }
    }


    private void GameOver()
    {
        //int randomSound = Random.Range(1, 2);
        //if (randomSound == 1)
        //{
        //    PlaySound(1);
        //}
        //else if (randomSound == 2)
        //{
        //    PlaySound(2);
        //}
        
        isDead = true;
        if (transform.childCount > 0)
        {
            transform.GetChild(0).transform.parent = null;
        }
        
        restartBtn.SetActive(true);
        scoreText.gameObject.SetActive(false);
        gameOverAnim.SetTrigger("GameOver");
        scoreOverText.text = score.ToString();
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        
        //StartCoroutine(SaveHighScores());
    }

    private void PlayerFallDownCheck()
    {
        if (transform.position.y < 2.5f)
        {
            GameOver();
            
        }
    }

    public void LoadLeaderBoard()
    {
        SceneManager.LoadScene(1);
    }

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = a_Clip[clip];
        GetComponent<AudioSource>().Play();
    }

    //out of use Azure mobile services required
    //IEnumerator SaveHighScores()
    //{
    //    HighScoreData playerInfo = GetPlayerInfo();

    //    //get high scores
    //    yield return StartCoroutine(HighScoreService.SetScore(playerInfo));
    //}
    HighScoreData GetPlayerInfo()
    {
        return new HighScoreData()
        {
            id = new System.Guid("1eab3cb4-15ea-46e3-9303-e00a90aa8d16"), //grab from player prefs...
            playername = "Chad", //grab from player prefs...\
            score = score
        };
    }
    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
