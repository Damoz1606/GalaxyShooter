using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    private UIManager uiManager;
    private Vector2 screenBounds;
    void Start()
    {
        this.screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        this.uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    void Update()
    {
        Movement();
        DestroyOnScreenBounds();
    }
    private void Movement()
    {
        this.transform.Translate(Vector3.up * this.speed * Time.deltaTime);
    }
    private void DestroyOnScreenBounds()
    {
        if (this.screenBounds.y < this.transform.position.y)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && other.GetComponent<Enemy>())
        {
            this.UpdateScore(other.GetComponent<Enemy>().scoreLaser);
            other.GetComponent<Enemy>().DestroyEnemy();
        } else if (other.tag == "Player" && other.GetComponent<Player>())
        {
            //other.GetComponent<Player>().GetDamage(this.damage);
        }
    }
    private void UpdateScore(float score)
    {
        if(this.uiManager)
        {
            this.uiManager.UpdateScore(score);
        }
    }
}
