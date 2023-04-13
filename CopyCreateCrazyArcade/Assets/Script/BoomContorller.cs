using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Script
{

    public class BoomContorller : MonoBehaviour
    {
        public GameObject bombPrefab;
        public KeyCode Inputkey = KeyCode.Space;
        public float bombFuseTime = 2.8f;
        private int bombAmount = 10;
        private int bombsRemaining;
        public Explosion explosionprefeb;
        PlayerInput _input;
        private float explosionDuration = 0.36f;
        private int explosionRadius = 2;

        WaitForSeconds waitbomb = new WaitForSeconds(3);
        public LayerMask explosionLayer;
        public LayerMask explosionLayer2;
        public LayerMask explosionLayer3;


        Collider2D[] resut = new Collider2D[2];

        public BlockManager _blockprefeb;

        private void OnEnable()
        {
            bombsRemaining = bombAmount;
        }
        private void Awake()
        {
            _input= GetComponent<PlayerInput>();
        }
        private void Update()
        {
            //if (bombsRemaining > 0 && _input.Attack())
            //{
                StartCoroutine(PlaceBomb());
            //}

        }

        Vector2 spawnPosition = Vector2.zero;
        private IEnumerator PlaceBomb()
        {


            spawnPosition = MapManager.Instance.LocalToCellPosition(transform);
            GameObject balloon = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
            bombsRemaining--;

            // 풍선 대기 시간 진행후 익스플로전 생성 및 풍선 제거


            yield return waitbomb;

            spawnPosition = MapManager.Instance.LocalToCellPosition(balloon.transform);

            Destroy(balloon);

            // 중앙 물줄기
            Explosion explosion = Instantiate(explosionprefeb, spawnPosition, Quaternion.identity);
            Destroy(explosion.gameObject, explosionDuration);



            // 상, 하 , 좌 , 우 각각 생성하기
            ExplodeHelper();


            bombsRemaining++;
        }
        void ExplodeHelper()
        {
            Explode2(spawnPosition, Vector2.up, explosionRadius);
            Explode2(spawnPosition, Vector2.down, explosionRadius);
            Explode2(spawnPosition, Vector2.left, explosionRadius);
            Explode2(spawnPosition, Vector2.right, explosionRadius);
        }
        private void Explode(Vector2 position, Vector2 direction, int length)
        {
            if (length <= 0)
            {
                return;
            }

            //방향정보 더해준 만큼 갖고있는 길이 까지 출력.
            position += direction;

            // 부술 수 없는 오브젝트 충돌시 중단
            if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayer))
            {
                Debug.Log("부술수업성");
                return;
            }
            // 오버랩 박스로 블록인경우 해당 블록삭제 추후 아이템도.
            if (resut[0] = Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayer2))
            {
                Destroy(resut[0].gameObject);
                Debug.Log("block맞음");
                return;
            }


            //물줄기 추가생성
            Explosion explosion = Instantiate(explosionprefeb, position, transform.rotation);
            explosion.SetDirection(direction);
            Destroy(explosion.gameObject, explosionDuration);

            // 최대 길이에서 재귀적으로 최대길이를 줄여나가는것.
            Explode(position, direction, length - 1);
        }

        void Explode2(Vector2 position, Vector2 direction, int length)
        {
            while (length > 0)
            {
                //방향정보 더해준 만큼 갖고있는 길이 까지 출력.
                position += direction;

                // 부술 수 없는 오브젝트 충돌시 중단
                if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayer))
                {
                    return;
                }
                // 오버랩 박스로 블록인경우 해당 블록삭제 추후 아이템도.
                if (resut[0] = Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayer2))
                {
                    Destroy(resut[0].gameObject);
                    return;
                }

                //물줄기 추가생성
                Explosion explosion = Instantiate(explosionprefeb, position, transform.rotation);
                explosion.SetDirection(direction);
                Destroy(explosion.gameObject, explosionDuration);

                // 최대 길이에서 재귀적으로 최대길이를 줄여나가는것.
                --length;
            }
        }






    }
}
