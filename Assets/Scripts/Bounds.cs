using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    private Vector2 minScreenBounds;
    private Vector2 maxScreenBounds;
    private float objectWidth;
    private float objectHeight;
    // Start is called before the first frame update
    void Start() {
        minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0,0,0));
        maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent <SpriteRenderer>().bounds.extents.y;
    }
    void LateUpdate()
    {
        Vector3 viewPosition = transform.position;
        viewPosition.x = Mathf.Clamp(viewPosition.x, minScreenBounds.x + objectWidth, maxScreenBounds.x - objectWidth);
        viewPosition.y = Mathf.Clamp(viewPosition.y, minScreenBounds.y + objectHeight, maxScreenBounds.y - objectHeight);
        transform.position = viewPosition;
    }
}
