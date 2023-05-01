using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace Assets.Script
{

    public class Explosion : MonoBehaviour
    {
        private WaitForSeconds destroytime = new WaitForSeconds(0.36f);
        private AudioSource _audio;
        public AudioClip _clip;
        public ObjectPool<Explosion> Pool;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
            StartCoroutine(ClearTime());
        }

        private IEnumerator ClearTime()
        {
            yield return destroytime;
            Pool.Release(this);   
           
        }

        public void ExplosionSound()
        {
            _audio.clip = _clip;
            _audio.Play();  
        }
        public void SetDirection(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x);
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        }

    }

}
