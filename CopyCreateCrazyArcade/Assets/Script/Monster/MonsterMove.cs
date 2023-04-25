using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    private Vector3 moveDirection = new Vector3(1, 0, 0);
    private Vector3 saveDirection = new Vector3(1, 0, 0);
    private Vector3 shearchRandge = new Vector3(0, 1, 0);
    private float speed = 1.5f;
    private float moveSpeed;
    private Collider2D[] target = new Collider2D[2];
    private int random;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();

        random = Random.Range(0, 3);
        if (random == 0)
            moveDirection = -moveDirection;
        if (random == 2)
            speed = 2f;
    }

    private void FixedUpdate()
    {
        moveSpeed = Time.deltaTime * speed;
        transform.Translate(moveDirection * moveSpeed);

        target[0] = Physics2D.OverlapBox(transform.position + moveDirection / 2, Vector2.one / 5f, 0f);

        if (target[0] != null && !target[0].CompareTag("Player"))
        {
            RandomSpeed();
            target[0] = Physics2D.OverlapBox(transform.position + shearchRandge / 2, Vector2.one / 5f, 0f);
            target[0] = Physics2D.OverlapBox(transform.position - shearchRandge / 2, Vector2.one / 5f, 0f);
            moveDirection = -moveDirection;

            if (target[0] == null)
            {
                moveDirection = shearchRandge;
            }
            if (target[0] != null)
            {
                moveDirection = saveDirection;
            }

        }
        _anim.SetFloat("PositionX", moveDirection.x);
        _anim.SetFloat("PositionY", moveDirection.y);
    }

    private void RandomSpeed()
    {
        random = Random.Range(0, 3);
        if (random == 0)
            speed = 1.5f;
        if (random == 1)
            speed = 2f;
        if (random == 2)
            speed = 2.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStatus _status = collision.gameObject.GetComponent<PlayerStatus>();
            _status.DieConfirmation();
        }

    }
}
