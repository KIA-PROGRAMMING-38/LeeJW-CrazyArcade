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

            // ǳ�� ��� �ð� ������ �ͽ��÷��� ���� �� ǳ�� ����


            yield return waitbomb;

            spawnPosition = MapManager.Instance.LocalToCellPosition(balloon.transform);

            Destroy(balloon);

            // �߾� ���ٱ�
            Explosion explosion = Instantiate(explosionprefeb, spawnPosition, Quaternion.identity);
            Destroy(explosion.gameObject, explosionDuration);



            // ��, �� , �� , �� ���� �����ϱ�
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

            //�������� ������ ��ŭ �����ִ� ���� ���� ���.
            position += direction;

            // �μ� �� ���� ������Ʈ �浹�� �ߴ�
            if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayer))
            {
                Debug.Log("�μ�������");
                return;
            }
            // ������ �ڽ��� ����ΰ�� �ش� ��ϻ��� ���� �����۵�.
            if (resut[0] = Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayer2))
            {
                Destroy(resut[0].gameObject);
                Debug.Log("block����");
                return;
            }


            //���ٱ� �߰�����
            Explosion explosion = Instantiate(explosionprefeb, position, transform.rotation);
            explosion.SetDirection(direction);
            Destroy(explosion.gameObject, explosionDuration);

            // �ִ� ���̿��� ��������� �ִ���̸� �ٿ������°�.
            Explode(position, direction, length - 1);
        }

        void Explode2(Vector2 position, Vector2 direction, int length)
        {
            while (length > 0)
            {
                //�������� ������ ��ŭ �����ִ� ���� ���� ���.
                position += direction;

                // �μ� �� ���� ������Ʈ �浹�� �ߴ�
                if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayer))
                {
                    return;
                }
                // ������ �ڽ��� ����ΰ�� �ش� ��ϻ��� ���� �����۵�.
                if (resut[0] = Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayer2))
                {
                    Destroy(resut[0].gameObject);
                    return;
                }

                //���ٱ� �߰�����
                Explosion explosion = Instantiate(explosionprefeb, position, transform.rotation);
                explosion.SetDirection(direction);
                Destroy(explosion.gameObject, explosionDuration);

                // �ִ� ���̿��� ��������� �ִ���̸� �ٿ������°�.
                --length;
            }
        }






    }
}
