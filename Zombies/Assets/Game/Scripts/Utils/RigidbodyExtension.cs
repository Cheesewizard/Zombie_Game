using UnityEngine;

namespace Game.Scripts.Utils
{
	public static class RigidbodyExtension
	{
		public static void Attach(this Rigidbody2D rigidbody, Transform parent)
		{
			// Reset the velocity if the rigid body isn't kinematic
			if (!rigidbody.isKinematic)
			{
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = 0f;
			}
			rigidbody.isKinematic = true;
			rigidbody.transform.SetParent(parent);
			rigidbody.simulated = false;
		}

		public static void Detach(this Rigidbody2D rigidbody)
		{
			rigidbody.transform.DetatchFromParent();
			rigidbody.isKinematic = true;
			rigidbody.simulated = true;
		}
	}
}