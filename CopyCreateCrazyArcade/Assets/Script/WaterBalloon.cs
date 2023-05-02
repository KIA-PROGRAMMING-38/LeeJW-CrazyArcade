using Assets.Script;
using System.Collections;
using UnityEngine;

namespace Assets.Script
{
    public class WaterBalloon : MonoBehaviour
    {
        public Collider2D _collider;
        public Rigidbody2D _rigidbody;

        private const float BALLOON_CLEAR_TIME = 3f;
        public int currentPower = 1;

        public Explosion explosionPrefeb;
        public LayerMask NonDestroyLayer;
        public LayerMask destoryLayer;
        public ObjectPool<WaterBalloon> Pool { private get; set; }
        private ObjectPool<Explosion> _explosionPool;

        Collider2D[] target = new Collider2D[2];


        private IEnumerator myBoomCoroutine;
        private WaitForSeconds boomWaitTime = new WaitForSeconds(3);

        private void Awake()
        {
            _explosionPool = new ObjectPool<Explosion>(CreatePoolExplosion, TakeExplosionFromPool, ReturnExplosionToPool,
               (explosion) => Destroy(explosion.gameObject), 150, 500);

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
            if (collision.gameObject.CompareTag(StringHelper.Explosion))
            {
                BoomBalloon();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(StringHelper.Player))
            {
                _collider.isTrigger = false;
            }
        }

        private IEnumerator BoomCoroutine()
        {
            yield return boomWaitTime;

            BoomBalloon();
        }

        public void BoomBalloon()
        {
            Explosion explod = GetExplosionFromPool();
            Animator anim = explod.GetComponent<Animator>();
            explod.ExplosionSound();

            anim.SetBool(StringHelper.CenterExplosion, true);
            Pool.Release(this);
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
                return;
            
            position += direction;

            if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, NonDestroyLayer))
            {
                return;
            }
            if (target[0] = Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, destoryLayer))
            {
                if (target[0].gameObject.layer == LayerMask.NameToLayer(StringHelper.Block))
                {
                    Animator _anim = target[0].GetComponent<Animator>();
                    _anim.SetBool(StringHelper.BlockDestroy, true);
                    return;
                }
                if (target[0].gameObject.layer == LayerMask.NameToLayer(StringHelper.Item))
                {
                    target[0].gameObject.SetActive(false);
                }
            }

            Explosion explosion = GetExplosionFromPool();
            explosion.transform.position = position;
            explosion.SetDirection(direction);

            if (length == 1)
            {
                Animator _anim = explosion.GetComponent<Animator>();
                _anim.SetBool(StringHelper.MiddleExplosion, true);

            }
            Explode(position, direction, length - 1);
        }

        private Explosion CreatePoolExplosion()
        {
            Explosion explosion = Instantiate(explosionPrefeb, transform.position, transform.rotation);
            explosion.Pool = _explosionPool;
            return explosion;
        }
        private Explosion GetExplosionFromPool()
        {
            Debug.Assert(_explosionPool != null);
            Explosion explosion = _explosionPool.Get();
            return explosion;
        }
        private void TakeExplosionFromPool(Explosion explosion) => explosion.gameObject.SetActive(true);
        private void ReturnExplosionToPool(Explosion explosion) => explosion.gameObject.SetActive(false);

    }
}
