using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainScript : MonoBehaviour
{
    private string nextLevelPath;
    private string nextLevelName;
    private string currentLevelName;
    private int currentLevelIndex;
    public AudioSource coin_sound;
    public AudioSource hit_sound;
    public AudioSource powerup_sound;
    private int life = 5;
    Rigidbody2D rb2d;
    private GameObject[] coin;
    private Vector3 lineOffset = new Vector3(-0.18f, +0.42f, 0);
    public Text coin_counter;
    public Text life_counter;
    public Text level_counter;
    private int point = 0;
    //touch variables
    public bool touchEnabled = true;
    public float hitSpeed = 10f;
    private Vector3 hitForce;
    private Vector3 touch_pos;
    private Vector3 fingerDown;
    private Vector3 fingerUp;
    public LineRenderer line;
    public Transform startPoint;
    void Start()
    {
        startPoint = GameObject.Find("startPoint").GetComponent<Transform>();
        transform.position = startPoint.position;
        rb2d = GetComponent<Rigidbody2D>();
        coin_sound = GameObject.Find("c_sound").GetComponent<AudioSource>();
        hit_sound = GameObject.Find("h_sound").GetComponent<AudioSource>();
        powerup_sound = GameObject.Find("powerup").GetComponent<AudioSource>();
        line = GameObject.Find("Line").GetComponent<LineRenderer>();
        coin_counter = GameObject.Find("coin_counter").GetComponent<Text>();
        life_counter = GameObject.Find("life_counter").GetComponent<Text>();
        level_counter = GameObject.Find("level_counter").GetComponent<Text>();
        nextLevelPath = SceneUtility.GetScenePathByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);
        nextLevelName = System.IO.Path.GetFileNameWithoutExtension(nextLevelPath);
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        currentLevelName = SceneManager.GetActiveScene().name;
    }

private void FixedUpdate()
    {
        rb2d.velocity = Vector2.Lerp(rb2d.velocity, Vector2.zero, 0.04f);
    }

    void Update()
    {
        DontDestroyOnLoad(coin_sound);
        DontDestroyOnLoad(hit_sound);

        if (Input.touchCount>0 && touchEnabled == true) {
            Touch touch = Input.GetTouch(0);
            touch_pos = Camera.main.ScreenToWorldPoint(touch.position);
            line.SetPosition(0, transform.position + lineOffset);
            line.SetPosition(1, transform.position + hitForce + lineOffset);

            //stop the ball if velocity is near 0
            if (Mathf.Round(rb2d.velocity.x) == 0 && Mathf.Round(rb2d.velocity.y) == 0)
            {
                rb2d.velocity = Vector2.zero;
            }

            if (touch.phase == TouchPhase.Began)
            {
                fingerDown = touch_pos;
                line.enabled = true;
                line.SetColors(Color.green, Color.red);
            }
            hitForce = (touch_pos - fingerDown);
            //limit hitForce
            hitForce.x = Mathf.Clamp(hitForce.x, -5f, 5f);
            hitForce.y = Mathf.Clamp(hitForce.y, -5f, 5f);

            //minimum force threshold
            if (Mathf.Abs(hitForce.x) < 1 && Mathf.Abs(hitForce.y) < 1)
            {
                hitForce = Vector3.zero;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                line.enabled = false;  
                if (rb2d.velocity == Vector2.zero)
                {
                    if (Mathf.Abs(hitForce.x) > 1 || Mathf.Abs(hitForce.y) > 1)
                    {
                        life -= 1;
                    }
                    //apply force
                    rb2d.AddForce(-hitForce * hitSpeed, ForceMode2D.Impulse);
                    hitForce = Vector3.zero;
                }
            }

        }
        if (touchEnabled==false)
        {
            line.enabled = false;
        }
        coin_counter.text = point.ToString();
        life_counter.text = life.ToString();
        level_counter.text = "Level " + currentLevelName.Trim(new char[] {'l', 'e', 'v'});
        coin = GameObject.FindGameObjectsWithTag("coin");
        //next level
        if (coin.Length == 0)
        {
            //load next level
            SceneManager.LoadScene(currentLevelIndex + 1);
            //load main components if next level is not win screen
            if (nextLevelName != "win")
            {
                SceneManager.LoadScene("mainLevel", LoadSceneMode.Additive);
            }
        }
        //die
        if (life <= 0 && rb2d.velocity == Vector2.zero)
        {
            SceneManager.LoadScene("game_over");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            //play coin sound
            if (Data.SoundEnabled == true)
            {
                coin_sound.Play();
            }
            point += 1;
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.tag == "heart")
        {
            //play life bonus sound
            if (Data.SoundEnabled == true)
            {
                powerup_sound.Play();
            }
            life += 1;
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //play wall hit sound
        if (Data.SoundEnabled==true)
        {
            hit_sound.Play();
        }
    }

}
