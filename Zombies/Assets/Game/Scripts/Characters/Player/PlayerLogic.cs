using System;
using Assets.Scripts.Interfaces;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityDependencyInjection;
using UnityEngine;
using Zombieland.Gameplay.Services;

namespace Game.Scripts.Characters.Player
{
	public class PlayerLogic : MonoBehaviour
	{
		[Inject]
		private PlayerInputConsumerAccessService playerInput;

		[SerializeField, Required, Find(Destination.Scene)]
		private Camera targetCamera;

		public float movementSpeed = 20;
		public float health = 150;
		public bool godMode = true;
		private BoxCollider2D meleeHitBox;

		public InventoryObject inventory;

		public Action<bool> OnMovement;
		public Action OnKilled;

		private void Start()
		{
			meleeHitBox = gameObject.GetComponentInParent<BoxCollider2D>();
			//Cursor.visible = false; // maybe remove. Could add laser site as an item further into the gam
		}

		private void Update()
		{
			LookAtMouse();
			Move();
		}

		private void CheckMeleeAttack()
		{
			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				meleeHitBox.isTrigger = !meleeHitBox.isTrigger;
			}
		}

		private void Move()
		{
			var input = playerInput.playerInput.Player.Move.ReadValue<Vector2>();

			var newPosition = new Vector2(input.x, input.y) * (movementSpeed * Time.deltaTime);

			OnMovement?.Invoke(newPosition != Vector2.zero);
			transform.root.Translate(newPosition.x, newPosition.y, 0f, Space.World);
		}

		private void LookAtMouse()
		{
			var input = playerInput.playerInput.Player.Rotate.ReadValue<Vector2>();

			var worldPoint = targetCamera.ScreenToWorldPoint(new Vector3(input.x, input.y, targetCamera.nearClipPlane));
			var difference = worldPoint - transform.position;

			var angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			transform.parent.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
		}

		public void KillSelf()
		{
			OnKilled.Invoke();
		}

		public void TakeDamage(IWeapon weapon)
		{
			if (!godMode)
			{
				this.health -= weapon.GetDamage();
				Debug.Log($"Player health = {health}");
				if (this.health <= 0)
				{
					KillSelf();
				}
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Zombie"))
			{
				var attack = collision.gameObject.GetComponent<IZombie>();
				//this.TakeDamage(attack.GetDamage());
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			var item = collision.GetComponent<Item>();
			if (item)
			{
				inventory.AddItem(item.item, 1);
				Destroy(collision.gameObject);
			}
		}

		private void OnApplicationQuit()
		{
			inventory.container.Clear();
		}


		public float GetHealth()
		{
			return this.health;
		}

		public void TakeKnockBack(GameObject other, float force)
		{
			throw new System.NotImplementedException();
		}
	}
}

namespace Game.Scripts.Gameplay
{
}