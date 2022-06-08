using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyExplodePrefab;
    [SerializeField] private int damage = 1;
    [SerializeField] public float scoreLaser = 10.0f;
    [SerializeField] public float scoreCollision = 5-0f;
    [SerializeField] private AudioClip clip;
    private UIManager uiManager;
    private Vector2 screenBounds;
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        this.screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        this.uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    void Update()
    {
        this.Movement();
        this.DestroyOnScreenBounds();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.GetComponent<Player>())
        {
            this.UpdateScore(this.scoreCollision);
            other.GetComponent<Player>().GetDamage(this.damage);
            this.DestroyEnemy();
        }
    }
    private void Movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * this.speed);
    }
    private void DestroyOnScreenBounds()
    {
        if (-this.screenBounds.y > this.transform.position.y)
        {
            //this.Spawn();
            Destroy(this.gameObject);
        }
    }
    private void InitAnimation()
    {
        GameObject animation = Instantiate(this.enemyExplodePrefab, this.transform.position, Quaternion.identity);
        StartCoroutine(this.AnimationRemoveRoutine(animation));
    }
    private IEnumerator AnimationRemoveRoutine(GameObject animation)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(animation);
    }
    public void Spawn()
    {
        this.transform.position = new Vector3(Random.Range(-this.screenBounds.x, this.screenBounds.x), 4, this.transform.position.z);
    }
    public void DestroyEnemy()
    {
        this.InitAnimation();
        AudioSource.PlayClipAtPoint(this.clip, Camera.main.transform.position);
        this.AnimationRemoveRoutine(this.enemyExplodePrefab);
        Destroy(this.gameObject);
    }
    private void UpdateScore(float score)
    {
        if (this.uiManager)
        {
            this.uiManager.UpdateScore(score);
        }
    }
}
