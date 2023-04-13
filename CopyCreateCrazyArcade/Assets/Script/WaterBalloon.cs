using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro.EditorUtilities;
using UnityEngine;
namespace Assets.Script
{
    public class WaterBalloon : MonoBehaviour
    {
        private float elapsedTime;
        Collider2D _collider;

        public Explosion explosionPrefeb;
        public int currentPower;

        public LayerMask explosionLayer;
        public LayerMask explosionLayer2;

        private const float clearTime = 0.36f;

        Collider2D[] resut = new Collider2D[2];

        private void Awake()
        {
            currentPower = 4;
            _collider = GetComponent<Collider2D>();
        }

        void Update()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 3)
            {
                BoomBalloon();
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Explosion"))
            {
                // 시간 전에 물줄기에 풍선이 맞았을때
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

        void BoomBalloon()
        {
            // 센터 이미지
            Explosion explod = Instantiate(explosionPrefeb, transform.position, transform.rotation);
            Animator _anim = explod.GetComponent<Animator>();
            _anim.SetBool("CenterExplosion", true);

            Destroy(gameObject);
            explod.DestroyAfter(clearTime);

            CrossExplodCreate();

            elapsedTime = 0;
        }

        // 진행할 방향
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

            //포지션으로부터의 방향 측정.
            position += direction;

            // 부술 수 없는 오브젝트 충돌시 중단
            if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayer))
            {
                return;
            }

            // 오버랩 박스로 블록인경우 해당 블록삭제 추후 아이템도 삭제
            if (resut[0] = Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayer2))
            {
                // 풍선이라면 지나쳐서 랜더링.
                if (resut[0].gameObject.name == "Balloon(Clone)")
                {
                }
                else
                {
                    Animator _anim = resut[0].GetComponent<Animator>();

                    _anim.SetBool("MovingDestroy", true);
                    Destroy(resut[0].gameObject, clearTime);
                    return;
                }
            }

            //물줄기 추가생성
            Explosion explosion = Instantiate(explosionPrefeb, position, transform.rotation);
            explosion.SetDirection(direction);

            explosion.DestroyAfter(clearTime);
            if (length == 1)
            {
                Animator _anim = explosion.GetComponent<Animator>();
                _anim.SetBool("MiddleExplosion", true);

            }
            // 최대 길이에서 재귀적으로 최대길이를 줄여나가는것.
            Explode(position, direction, length - 1);
        }


    }
}
