using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NinjaFSM.UI.Input
{
	public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		private ButtonView View;
        
		public event Action ButtonUp;
		public event Action ButtonDown;

		private void Awake()
		{
			View = GetComponent<ButtonView>();
		}

		public virtual void OnPointerDown(PointerEventData eventData)
		{
			if (View != null) { View.OnPointerDown(); }

			ButtonDown?.Invoke();
		}

		public virtual void OnPointerUp(PointerEventData eventData)
		{
			if (View != null) { View.OnPointerUp(); }
			
			ButtonUp?.Invoke();
		}
		
		protected virtual void OnDestroy()
		{
			ButtonUp = null;
			ButtonDown = null;
		}
	}
}