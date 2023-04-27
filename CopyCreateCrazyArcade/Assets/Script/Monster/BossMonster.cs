using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossMonster : MonoBehaviour
{
    private int bossHP = 10;
    private Vector3 defaultPosition;
    private Vector3[,] moves = new Vector3[3, 4];
    private float moveDirection;

    private int movesRandomPositionX = 0;
    private int movesRandomPositionY = 0;
    private int savePositionX;
    private int savePositionY;
    private Vector3 saveTransformPosition;

    private float moveSpeed;
    private float speed = 2f;
    private int randomPattern;
    private Rigidbody2D _rigid;
    public WaterBalloon _Balloon;
    private float distance = 20f;
    private int attackCount = 3;
    private Animator _anim;
    RaycastHit2D hit;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();

        moves[0, 0] = new Vector3(-8, 6, 0);
        moves[0, 1] = new Vector3(-4, 6, 0);
        moves[0, 2] = new Vector3(0, 6, 0);
        moves[0, 3] = new Vector3(4, 6, 0);

        moves[1, 0] = new Vector3(-8, 2, 0);
        moves[1, 1] = new Vector3(-4, 2, 0);
        moves[1, 2] = new Vector3(0, 2, 0);
        moves[1, 3] = new Vector3(4, 2, 0);

        moves[2, 0] = new Vector3(-8, -2.5f, 0);
        moves[2, 1] = new Vector3(-4, -2.5f, 0);
        moves[2, 2] = new Vector3(0, -2.5f, 0);
        moves[2, 3] = new Vector3(4, -2.5f, 0);

        randomPattern = Random.Range(0, 3);
        if (randomPattern == 0)
        {
            movesRandomPositionX = Random.Range(0, 4);
        }
        else
        {
            movesRandomPositionY = Random.Range(0, 3);
        }

    }

    private void Update()
    {
        moveSpeed = speed * Time.deltaTime;

        if (transform.position == moves[movesRandomPositionY, movesRandomPositionX])
        {
            savePositionX = movesRandomPositionX;
            savePositionY = movesRandomPositionY;

            saveTransformPosition = transform.position;

            randomPattern = Random.Range(0, 3);
            Debug.Log(randomPattern);

            if (randomPattern == 0)
            {

                movesRandomPositionX = Random.Range(0, 4);
                if (savePositionX == movesRandomPositionX)
                {
                    movesRandomPositionX = Random.Range(0, 4);
                }
            }
            if (randomPattern == 1)
            {
                movesRandomPositionY = Random.Range(0, 3);

                if (savePositionY == movesRandomPositionY)
                {
                    movesRandomPositionY = Random.Range(0, 3);

                }
            }

            if (randomPattern == 2)
            {
                if (attackCount > 0)
                {
                    CanAttack();
                }
                attackCount = 3;
            }
            saveTransformPosition = moves[movesRandomPositionY, movesRandomPositionX] - saveTransformPosition;
            _anim.SetFloat("PositionX", saveTransformPosition.x);
            _anim.SetFloat("PositionY", saveTransformPosition.y);
        }

        transform.position = Vector3.MoveTowards(transform.position, moves[movesRandomPositionY, movesRandomPositionX], moveSpeed);

        if (Input.GetKeyDown(KeyCode.B))
            CanAttack();
    }

    private void CanAttack()
    {

        WaterBalloon balloon = Instantiate(_Balloon, transform.GetChild(0).transform.position, Quaternion.identity);
        balloon.currentPower = 3;
        balloon._collider.isTrigger = false;
        balloon._rigidbody.constraints = RigidbodyConstraints2D.None;
        Vector2 normal = saveTransformPosition;
        normal.Normalize();
        balloon._rigidbody.velocity = normal * 10;
        --attackCount;
    }
}
