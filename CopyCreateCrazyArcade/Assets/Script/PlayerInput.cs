using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 10f)]
    private float _speed = 1f;

    public float horizontal { get; private set; }
    public float vertical { get; private set; }

    void Start()
    {

    }

    private Vector2 direction;
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        direction = new Vector2(horizontal, vertical);
        transform.Translate(direction * (_speed * Time.deltaTime));
    }
}
