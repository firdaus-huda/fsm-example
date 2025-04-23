using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace NinjaFSM.UI.Input
{
	[RequireComponent(typeof(ButtonController))]
	[RequireComponent(typeof(Image))]
	public class ButtonView : MonoBehaviour
	{
		private const float AnimationDuration = 0.1f;
		
		private Tweener _scaleTween;

		public void OnPointerDown()
		{
			_scaleTween?.Kill();
			_scaleTween = transform.DOScale(0.9f, AnimationDuration);
		}

		public void OnPointerUp()
		{
			_scaleTween?.Kill();
			_scaleTween = transform.DOScale(1f, AnimationDuration);
		}
	}
}