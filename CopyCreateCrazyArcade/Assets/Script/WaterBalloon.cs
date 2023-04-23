using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Script
{
    public class WaterBalloon : MonoBehaviour
    {
        private float elapsedTime;
        private Collider2D _collider;
        private Rigidbody2D _rigidbody;
        private const float BALLOON_CLEAR_TIME = 3f;
        public int currentPower = 1;

        public Explosion explosionPrefeb;
        public LayerMask NonDestroyLayer;
        public LayerMask destoryLayer;

        private const float clearTime = 0.36f;
        Collider2D[] target = new Collider2D[2];




        private IEnumerator myBoomCoroutine;
        private WaitForSeconds boomWaitTime = new WaitForSeconds(3);

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();

        }


        void Update()
        {
            myBoomCoroutine = BoomCoroutine();
            StartCoroutine(myBoomCoroutine);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Explosion"))
            {
                BoomBalloon();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _collider.isTrigger = false;
            }
        }

        private IEnumerator BoomCoroutine()
        {
            yield return boomWaitTime;

            BoomBalloon();
        }

        void BoomBalloon()
        {

            Explosion explod = Instantiate(explosionPrefeb, transform.position, transform.rotation);
            Animator anim = explod.GetComponent<Animator>();
            explod.ExplosionSound();

            anim.SetBool("CenterExplosion", true);
            gameObject.SetActive(false);
            CrossExplodCreate();
        }

        void CrossExplodCreate()
        {
            Explode(transform.position, Vector2.up, currentPower);
            Explode(transform.position, Vector2.down, currentPower);
            Explode(transform.position, Vector2.left, currentPower);
            Explode(transform.position, Vector2.right, currentPower);
        }

        private void Explode(Vector2 position, Vector2 direction, int length)
        {
            if (length <= 0)
            {
                return;
            }

            //���������κ����� ���� ����.
            position += direction;

            // �μ� �� ���� ������Ʈ �浹�� �ߴ�
            if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, NonDestroyLayer))
            {
                return;
            }



            // �μ� �� �ִ� ������Ʈ.
            if (target[0] = Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, destoryLayer))
            {
                // ������ �ڽ��� ����ΰ�� �ش� ��ϻ���
                if (target[0].gameObject.layer == LayerMask.NameToLayer("Block"))
                {
                    Animator _anim = target[0].GetComponent<Animator>();

                    _anim.SetBool("BlockDestroy", true);

                    return;
                }
                // ����� �ƴϸ� �����ļ� ���ٱ� ���� �� ����
                if (target[0].gameObject.layer == LayerMask.NameToLayer("Item"))
                {
                    target[0].gameObject.SetActive(false);
                }

            }

            //���ٱ� �߰�����
            Explosion explosion = Instantiate(explosionPrefeb, position, transform.rotation);
            explosion.SetDirection(direction);

            if (length == 1)
            {
                Animator _anim = explosion.GetComponent<Animator>();
                _anim.SetBool("MiddleExplosion", true);

            }
            // �ִ� ���̿��� ��������� �ִ���̸� �ٿ������°�.
            Explode(position, direction, length - 1);
        }


    }
}
