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

        private AudioSource[] _audio = new AudioSource[4];


        public AudioClip _itemClip;
        public AudioClip _liveClip;
        public AudioClip _dieConfirmationClip;
        public AudioClip _dieWaitClip;


        public PlayerInput _input { get; set; }
        private PlayerStatus _status;


        public float currentSpeed { get; set; } = 5;
        public int currentExplosionPower { get; set; } = 1;
        public int currentBalloonCount { get; set; } = 1;
        public bool kickBalloon { get; set; } = false;
        public bool dieWaitState { get; set; } = false;
        public int needleCount { get; set; }
        public int needleMaxCount { get; private set; } = 1;

        public float storageSpeed { get; set; } = 5;
        public int storageAttackCount { get; set; } = 1;
        public int MAX_SPEED { get; private set; } = 9;
        public int MAX_BALLOON_COUNT { get; private set; } = 6;
        public int MAX_EXPLOSION_POWER { get; private set; } = 7;

        public bool firstDie;
        public bool secondDie;

        private WaitForSeconds moveOnTime = new WaitForSeconds(1);
        private GameData _gamedata;


        private void Awake()
        {
            _gamedata = FindAnyObjectByType<GameData>();
            _anim = GetComponent<Animator>();
            _input = GetComponent<PlayerInput>();

            _audio[0] = GetComponent<AudioSource>();
            _audio[1] = GetComponent<AudioSource>();
            _audio[2] = GetComponent<AudioSource>();
            _audio[3] = GetComponent<AudioSource>();
        }

        IEnumerator StartMoving()
        {

            yield return moveOnTime;
            currentBalloonCount = storageAttackCount;
            currentSpeed = storageSpeed;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && dieWaitState == true)
            {
                if (_gamedata.defaultMode)
                {
                    DieConfirmation();
                }
                if (_gamedata.monsterMode)
                {
                    if (gameObject.name == "1PCharacter(Clone)")
                    {
                        SecondUseNeedle();
                    }
                    else
                    {
                        UseNeedle();
                    }
                }
            }

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Explosion"))
            {
                Debug.Log("플레이어 맞음");

                _anim.SetBool($"{gameObject.name}DieWait", true);

            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("Item") && dieWaitState == false)
            {
                item = collision.GetComponent<TakeItem>();
                _audio[0].clip = _itemClip;

                _audio[0].Play();
                item.PlayerTakeItem(gameObject);
                collision.gameObject.SetActive(false);
            }
        }

        public void UseNeedle()
        {
            Physics2D.IgnoreLayerCollision(3, 3, true);

            _audio[1].clip = _liveClip;
            _audio[1].Play();

            _anim.SetBool($"{name}DieWait", false);
            dieWaitState = false;
            _anim.SetTrigger($"{name}Live");

            StartCoroutine(StartMoving());



        }

        public void SecondUseNeedle()
        {
            Physics2D.IgnoreLayerCollision(3, 3, true);

            _audio[1].clip = _liveClip;
            _audio[1].Play();

            _anim.SetBool($"{name}DieWait", false);
            dieWaitState = false;
            _anim.SetTrigger($"{name}Live");

            StartCoroutine(StartMoving());

        }

        public void WaitDie()
        {
            _audio[3].clip = _dieWaitClip;
            _audio[3].Play();

            storageSpeed = currentSpeed;
            Physics2D.IgnoreLayerCollision(3, 3, false);
            dieWaitState = true;

            currentSpeed = 0.5f;
            currentBalloonCount = 0;
        }
        public void DieConfirmation()
        {
            if (_gamedata.defaultMode)
            {
                _audio[2].clip = _dieConfirmationClip;
                _audio[2].Play();

                _input.GameClear();
                _input.IsPlayerWin();
                currentSpeed = 0f;

                _anim.SetBool($"{name}DieWait", false);
                _anim.SetBool($"{name}DieConfirmation", true);

                Physics2D.IgnoreLayerCollision(3, 3, true);

                _input.playerLive = false;


            }
            if (_gamedata.monsterMode)
            {

                _audio[2].clip = _dieConfirmationClip;
                _audio[2].Play();

                _input.PlayerCountSub();
                currentSpeed = 0f;

                _anim.SetBool($"{name}DieWait", false);
                _anim.SetBool($"{name}DieConfirmation", true);

                Physics2D.IgnoreLayerCollision(3, 3, true);


            }


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
