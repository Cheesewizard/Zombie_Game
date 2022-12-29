using DUCK.Tween;
using DUCK.Tween.Easings;
using Quack.ReferenceMagic.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Guns
{
	public class BulletVisualiser : MonoBehaviour
	{
		[SerializeField, Required, Find(Destination.Self)]
		private LineRenderer lineRenderer;

		[SerializeField]
		private float missFadeDuration = 0.2f;

		[SerializeField]
		private float hitFadeDuration = 0.05f;

		private CustomAnimation lineFadeOutAnimation;

		[SerializeField]
		private Renderer bulletModel;

		private bool hasBulletModel;

		private void Awake()
		{
			lineFadeOutAnimation =
				new CustomAnimation(UpdateLineAlphaAndWidth, 1f, 0f, missFadeDuration, Ease.Circ.Out);
			lineRenderer.enabled = false;

			hasBulletModel = bulletModel != null;
		}

		public void InitVisual()
		{
			if (hasBulletModel) bulletModel.enabled = true;
		}

		private void UpdateLineAlphaAndWidth(float delta)
		{
			lineRenderer.widthMultiplier = delta;
			var currentColour = lineRenderer.startColor;
			lineRenderer.startColor = new Color(currentColour.r, currentColour.g, currentColour.b, delta);
		}

		public void StartDrawLine(Vector3 position)
		{
			lineRenderer.enabled = true;
			lineRenderer.SetPosition(0, position);
			lineRenderer.SetPosition(1, position);
			UpdateLineAlphaAndWidth(1f);
		}

		public void StopDrawLine(bool hit)
		{
			lineFadeOutAnimation.Duration = hit ? hitFadeDuration : missFadeDuration;
			lineFadeOutAnimation.Play(DisableLineRenderer);
		}

		public void UpdateLinePosition(Vector3 position)
		{
			lineRenderer.SetPosition(0, position);
		}

		private void DisableLineRenderer()
		{
			lineRenderer.enabled = false;
		}

		public void HideBullet()
		{
			if (hasBulletModel) bulletModel.enabled = false;
		}

		private void OnDestroy()
		{
			lineFadeOutAnimation.Abort();
		}
	}
}