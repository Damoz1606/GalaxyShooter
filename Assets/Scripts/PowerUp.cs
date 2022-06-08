using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float powerUpId;
    [SerializeField] private AudioClip clip;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        this.screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        this.Movement();
        this.DestroyOnScreenBounds();
    }

    private void Movement()
    {
        transform.Translate(Vector3.down * this.speed * Time.deltaTime);
    }

    private void DestroyOnScreenBounds()
    {
        if (-this.screenBounds.y > this.transform.position.y)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.GetComponent<Player>())
        {
            AudioSource.PlayClipAtPoint(this.clip, Camera.main.transform.position);
            switch(this.powerUpId)
            {
                case 0:
                    other.GetComponent<Player>().TripleShootPowerUpOn();
                    break;
                case 1:
                    other.GetComponent<Player>().IncreaseSpeed();
                    break;
                case 2:
                    other.GetComponent<Player>().EnableShields();
                    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
