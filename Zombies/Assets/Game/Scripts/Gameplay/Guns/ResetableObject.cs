using UnityEngine;

namespace Game.Scripts.Gameplay.Guns
{
	public class ResetableObject : MonoBehaviour
	{
		protected new Transform transform;
		private Vector3 initPosition;
		private Quaternion initRotation;

		public virtual void Init()
		{
			transform = base.transform;
			initPosition = transform.localPosition;
			initRotation = transform.localRotation;
		}

		public virtual void ResetTransform(Transform parent)
		{
			transform.SetParent(parent);
			transform.localPosition = initPosition;
			transform.localRotation = initRotation;
		}
	}
}