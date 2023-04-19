using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Script
{
    public class PlayerStatus : MonoBehaviour
    {
        private Animator _anim;
        private TakeItem item;
        public PlayerInput _input { get; set; }
        public PlayerStatus _status;

        public float currentSpeed { get; set; } = 5;
        public int currentExplosionPower { get; set; } = 1;
        public int currentBalloonCount { get; set; } = 1;
        public bool kickBalloon { get; set; } = false;
        public bool dieWaitState { get; set; } = false;
        public int needleCount { get; set; }
        public int needleMaxCount { get; private set; } = 1;

        public float storageSpeed { get; set; }
        public int storageAttackCount { get; set; }
        public int MAX_SPEED { get; private set; } = 9;
        public int MAX_BALLOON_COUNT { get; private set; } = 6;
        public int MAX_EXPLOSION_POWER { get; private set; } = 7;

        private WaitForSeconds moveOnTim = new WaitForSeconds(1);

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _input = GetComponent<PlayerInput>();

        }
        IEnumerator StartMoving()
        {

            yield return moveOnTim;
            currentBalloonCount = storageAttackCount;
            currentSpeed = storageSpeed;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && dieWaitState == true)
            {
                DieConfirmation();
            }

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Explosion"))
            {
                _anim.SetBool($"{gameObject.name}DieWait", true);

            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("Item") && dieWaitState == false)
            {
                item = collision.GetComponent<TakeItem>();

                item.PlayerTakeItem(gameObject);
                Destroy(collision.gameObject);
                collision.gameObject.SetActive(false);
            }
        }

        public void UseNeedle()
        {
            Physics2D.IgnoreLayerCollision(3, 3, true);
            _anim.SetBool($"{name}DieWait", false);
            dieWaitState = false;
            _anim.SetTrigger($"{name}Live");

            StartCoroutine(StartMoving());

            _input.FirstNeedleDown();


        }

        public void SecondUseNeedle()
        {
            Physics2D.IgnoreLayerCollision(3, 3, true);
            _anim.SetBool($"{name}DieWait", false);

            dieWaitState = false;
            _anim.SetTrigger($"{name}Live");

            StartCoroutine(StartMoving());
            _input.SecondNeedleDown();


        }

        public void WaitDie()
        {
            storageAttackCount = currentBalloonCount;
            storageSpeed = currentSpeed;
            Physics2D.IgnoreLayerCollision(3, 3, false);
            dieWaitState = true;

            currentSpeed = 0.5f;
            currentBalloonCount = 0;
        }
        public void DieConfirmation()
        {
            _input.GameClear();
            currentSpeed = 0f;
            _anim.SetBool($"{name}DieWait", false);
            _anim.SetBool($"{name}DieConfirmation", true);
            Physics2D.IgnoreLayerCollision(3, 3, true);

            _status._anim.SetTrigger("Win");


        }
        public void WinTrigger()
        {
            _anim.SetTrigger("Win");

        }
        public void GameEnd()
        {
            gameObject.SetActive(false);
        }

    }
}
