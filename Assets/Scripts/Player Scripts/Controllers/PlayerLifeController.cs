using System.Collections;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Managers;
using Assets.Scripts.ProgressBars.Scripts;
using Assets.Scripts.Scene_Managament;
using UnityEngine;

namespace Assets.Scripts.Player_Scripts.Controllers {
    public class PlayerLifeController : MonoBehaviour, IDamagable {
        public float maxHealth;
        public ProgressBar healthMeter;
        public float invulnarabilityTime = 2.0f;

        private bool isInvulnerable;
        private float health;

        // Use this for initialization
        void Start () {
            healthMeter.SetPercentage(1.0f); //Begin the game with full health
            health = maxHealth;
        }

        //From IDamagable
        public void ReceiveDamage(float damageDone, Collider2D fromWho) {
            if (isInvulnerable) return;
            StartCoroutine("ReceivingDamage", damageDone);
            healthMeter.SetPercentage(Mathf.InverseLerp(0.0f, maxHealth, health - damageDone), invulnarabilityTime * 0.5f);
            if(health <= 0.0f) {
                Die();
            }
            else {
                StartCoroutine("BecomeInvulnerable", fromWho);
            }
        }

        //From IDamagable
        public void Die() {
            LoadScene.ReloadScene();
        }

        IEnumerator BecomeInvulnerable(Collider2D fromWho) {
            isInvulnerable = true;
            Physics2D.IgnoreCollision(fromWho, PlayerManager.coll);
            yield return new WaitForSeconds(invulnarabilityTime);
            Physics2D.IgnoreCollision(fromWho, PlayerManager.coll, false);
            isInvulnerable = false;
        }

        IEnumerator ReceivingDamage(float damageDone) {
            float targetHealth = health - damageDone;
            float initTime = Time.time;
            while(health > targetHealth) {
                health = Mathf.Lerp(health, targetHealth, (Time.time - initTime) / invulnarabilityTime * 0.5f);
                yield return new WaitForEndOfFrame();
            }
        
        }
    }
}
