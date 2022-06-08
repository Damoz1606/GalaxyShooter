using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject[] engineFailures;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float coolDown = 5.0f;
    [SerializeField] private int life = 3;
    [SerializeField] private bool isPlayerOne = true;
    [SerializeField] private bool isPlayerTwo = false;

    private int engineFailed = 0;
    private float canFire = 0.0f;
    private float increaseSpeed = 2f;
    private AudioSource audioSource;

    private bool shieldsOn = false;
    private bool canTripleShoot = false;

    public float speed = 0.0f;

    private UIManager uiManager;
    private GameManager gameManager;
    private SpawnManager spawnManager;
    void Start()
    {
        this.speed = 5f;
        this.InitPlayer();
        this.LivesUIManager();
        this.InitSpawnRoutine();
    }
    private void InitPlayer()
    {
        this.spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        this.uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        this.gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        this.audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        Movement();
        Attack();
    }
    public void TripleShootPowerUpOn()
    {
        this.canTripleShoot = true;
        StartCoroutine(this.TripleShootPowerDownRoutine());
    }
    public void IncreaseSpeed()
    {
        float auxiliarSpeed = this.speed;
        this.speed *= this.increaseSpeed;
        StartCoroutine(this.IncreaseSpeedDownRoutine(auxiliarSpeed));

    }
    public void GetDamage(int damage)
    {
        if (this.shieldsOn)
        {
            this.DisableShields();
            return;
        }
        this.life -= damage;
        this.LivesUIManager();
        if (this.life <= 0)
        {
            this.Explode();
            this.GameOverManager();
        } else
        {
            this.EngineHitFailureAnimation();
        }
    }
    public void EnableShields()
    {
        this.shieldsOn = true;
        this.shield.gameObject.SetActive(true);
    }
    public void DisableShields()
    {
        this.shieldsOn = false;
        this.shield.gameObject.SetActive(false);
    }
    private IEnumerator TripleShootPowerDownRoutine()
    {
        yield return new WaitForSeconds(this.coolDown);
        this.canTripleShoot = false;
    }
    private IEnumerator IncreaseSpeedDownRoutine(float speed)
    {
        yield return new WaitForSeconds(this.coolDown);
        this.speed = speed;
    }
    private IEnumerator AnimationRemoveRoutine(GameObject animation)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(animation);
    }
    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (this.isPlayerOne && (
            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            transform.Translate(Vector3.right * Time.deltaTime * this.speed * horizontal);
            transform.Translate(Vector3.up * Time.deltaTime * this.speed * vertical);
        } else if (this.isPlayerTwo && (
            Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))
        {
            transform.Translate(Vector3.right * Time.deltaTime * this.speed * horizontal);
            transform.Translate(Vector3.up * Time.deltaTime * this.speed * vertical);
        }
    }
    private void Attack()
    {
        if (this.isPlayerOne && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            this.Shoot();
        } else if (this.isPlayerTwo && (Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(0)))
        {
            this.Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > canFire)
        {
            this.audioSource.Play();
            canFire = Time.time + fireRate;
            if (this.canTripleShoot)
            {
                this.TripleShoot();
            }
            else
            {
                this.SingleShoot();
            }
        }
    }

    private void SingleShoot()
    {
        Instantiate(this.laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
    }
    private void TripleShoot()
    {
        Instantiate(this.laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
        Instantiate(this.laserPrefab, transform.position + new Vector3(-0.35f, 0.1f, 0), Quaternion.identity);
        Instantiate(this.laserPrefab, transform.position + new Vector3(0.35f, 0.1f, 0), Quaternion.identity);
    }
    private void Explode()
    {
        GameObject animation = Instantiate(this.explosionPrefab, this.transform.position, Quaternion.identity);
        StartCoroutine(this.AnimationRemoveRoutine(animation));
    }

    private void LivesUIManager()
    {
        if(this.uiManager)
        {
            this.uiManager.UpdateLives(this.life);
        }
    }
    private void InitSpawnRoutine()
    {
        if(this.spawnManager)
        {
            this.spawnManager.StartGameRoutines();
        }
    }
    private void GameOverManager()
    {
        if (this.gameManager && this.uiManager)
        {
            this.gameManager.UpdateGameOver();
            this.uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
    }

    private void EngineHitFailureAnimation()
    {
        this.engineFailed = (this.engineFailed == 0) ? Random.Range(1, 3) : this.engineFailed == 1 ? 2 : 1;
        this.engineFailures[this.engineFailed - 1].SetActive(true);
    }
}
