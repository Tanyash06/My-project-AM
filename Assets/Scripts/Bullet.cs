using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    private float speed = 10.0F;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }//нам нужно считать значение направления как у игрока
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void UpDate()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed*Time.deltaTime);
    }
}
