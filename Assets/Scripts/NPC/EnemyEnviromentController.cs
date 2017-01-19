using Assets.Scripts.Interfaces;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.NPC {
	public class EnemyEnviromentController : MonoBehaviour, IDamagable {

		public float maxHealth = 10.0f;
		public float damage = 1.0f;
		public float pushPower = 2.0f;

		private float health;
		private Collider2D _collider;
		private EnemyController _controller;

		// Use this for initialization
		void Start () {
			health = maxHealth;
			_collider = GetComponent<Collider2D>();
			_controller = GetComponent<EnemyController>();
		}
	
		void OnCollisionEnter2D( Collision2D coll ) {
			if (coll.transform.CompareTag("Player") && !_controller.rb.isKinematic) {
				PlayerManager.life.ReceiveDamage(damage, _collider);
				PushPlayerBack();
			}
		}

		public void Die() {
			Destroy(this.gameObject);
		}

		public void ReceiveDamage(float damageDone, Collider2D fromWho ) {
			health -= damageDone;
		}

		private void PushPlayerBack() {
			PlayerManager.movement.PushedTo(_controller.rb.velocity.normalized * -pushPower);
			_controller.Rotate();
		}
	}
}
