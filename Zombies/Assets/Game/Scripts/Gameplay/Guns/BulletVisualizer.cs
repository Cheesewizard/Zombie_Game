using DUCK.Tween;
using DUCK.Tween.Easings;
using UnityEngine;

namespace Game.Scripts.Gameplay.Guns
{
	public class BulletVisualizer : MonoBehaviour
	{
		[SerializeField]
		private LineRenderer lineRenderer;

		[SerializeField]
		private float missFadeDuration = 0.2f;

		[SerializeField]
		private float hitFadeDuration = 0.05f;

		private CustomAnimation lineFadeOutAnimation;
		private void Awake()
		{
			lineFadeOutAnimation =
				new CustomAnimation(UpdateLineAlphaAndWidth, 1f, 0f, missFadeDuration, Ease.Circ.Out);
			lineRenderer.enabled = false;

			//hasBulletModel = bulletModel != null;
		}

		private void UpdateLineAlphaAndWidth(float delta)
		{
			lineRenderer.widthMultiplier = delta;
			lineRenderer.startColor = new Color(1f, 1f, 1f, delta);
		}

		private void OnDestroy()
		{
			lineFadeOutAnimation.Abort();
		}
	}
}