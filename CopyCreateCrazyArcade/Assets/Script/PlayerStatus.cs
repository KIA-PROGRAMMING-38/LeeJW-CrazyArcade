using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Script
{
    public class PlayerStatus : MonoBehaviour
    {
        Animator _anim;
        TakeItem item;
        PlayerStatus _status;
        public float currentSpeed { get; set; } = 5;
        public int currentExplosionPower { get; set; } = 1;
        public int currentBalloonCount { get; set; } = 5;
        public bool kickBalloon { get; set; } = false;
        public bool dieWaitState { get; set; } = false;
        public int needleCount;
        
        public float storageSpeed;
        public int storageAttackCount;

        public int MAX_SPEED { get; private set; } = 9;
        public int MAX_BALLOON_COUNT { get; private set; } = 6;
        public int MAX_EXPLOSION_POWER { get; private set; } = 7;

        private WaitForSeconds moveOnTim = new WaitForSeconds(1);
        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }
        private void Update()
        {
            Debug.Log($"{gameObject.name} {needleCount}");
        }
        IEnumerator StartMoving()
        {

            yield return moveOnTim;
            currentBalloonCount = storageAttackCount;
            currentSpeed = storageSpeed;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && dieWaitState == false)
            {
                _status = collision.gameObject.GetComponent<PlayerStatus>();
                _status.DieConfirmation();
            }

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Explosion"))
            {
                _anim.SetBool($"{gameObject.name}DieWait", true);
                Debug.Log($"{gameObject.name}DieWait");

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
            Debug.Log(gameObject.name);
            _anim.SetBool($"{gameObject.name}DieWait", false);
            dieWaitState = false;
            _anim.SetTrigger($"{gameObject.name}Live");

            StartCoroutine(StartMoving());

        }

        public void SecondUseNeedle()
        {
            Physics2D.IgnoreLayerCollision(3, 3, true);
            Debug.Log(gameObject.name);
            _anim.SetBool($"{gameObject.name}DieWait", false);
            dieWaitState = false;
            _anim.SetTrigger($"{gameObject.name}Live");
            
            StartCoroutine(StartMoving());


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
            currentSpeed = 0f;
            _anim.SetBool($"{gameObject.name}DieWait", false);
            _anim.SetBool($"{gameObject.name}DieConfirmation", true);
            Physics2D.IgnoreLayerCollision(3, 3, true);

        }
        public void GameEnd()
        {
            gameObject.SetActive(false);
        }

    }
}
