using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    
    private Vector3 moveDirection = new Vector3(1, 0, 0);
    private Vector3[] directions = new Vector3[4];
    private float speed = 1.5f;
    private float moveSpeed;
    private int random;
    public int count = 6;

    private Animator _anim;
    private float distance = 0.5f;
    private int currentDirectionIndex = 0;
    private bool setOff = true;
    private GameManager _manager;
    private void Awake()
    {
        directions[0] = new Vector3(1, 0, 0);
        directions[1] = new Vector3(-1, 0, 0);
        directions[2] = new Vector3(0, 1, 0);
        directions[3] = new Vector3(0, -1, 0);

        _manager = FindAnyObjectByType<GameManager>();

        _anim = GetComponent<Animator>();

        random = Random.Range(0, 3);
        if (random == 0)
            moveDirection = -moveDirection;
        if (random == 2)
            speed = 2f;
    }
    private void Update()
    {
        _anim.SetFloat("PositionX", moveDirection.x);
        _anim.SetFloat("PositionY", moveDirection.y);
    }
    float elap;
    private void FixedUpdate()
    {
        if (setOff)
        {
            moveSpeed = Time.deltaTime * speed;
            transform.Translate(moveDirection * moveSpeed);

        }

        elap += Time.deltaTime;

        if (elap >= 0.25f)
        {
            CanMove();
            MoveForward();
            elap = 0;
        }

    }

    bool CanMove()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (moveDirection/2), moveDirection, distance);
        if (hit.collider != null && hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void SetDirection()
    {
        if (moveDirection == directions[currentDirectionIndex])
            currentDirectionIndex = Random.Range(0, 4);
    }

    void MoveForward()
    {
        if (false == CanMove())
        {
            RandomSpeed();
            SetDirection();
            moveDirection = directions[currentDirectionIndex];
            return;
        }


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
            setOff = false;
            transform.position = collision.transform.position;
            _anim.SetTrigger("MonsterDie");
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStatus _status = collision.gameObject.GetComponent<PlayerStatus>();
            collision.collider.isTrigger = true;
            _status.DieConfirmation();
        }

    }

    public void SetFalse()
    {
        gameObject.SetActive(false);
        _manager.monsterCount -= 1;

    }


}
