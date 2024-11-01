using System;
using System.Collections.Generic;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Assets.Scripts.Core.PopupSystem
{
    /// <summary>
    /// PopupNoUIManager позволяет создавать PopupNoUI. При наличии свободного PopupNoUI возвращает уже созданный объект.
    /// Объект считается неиспользуемым и помещяется обратно в stack если он Disable.
    /// </summary>
    public class PopupNoUIManager
    {
        private Stack<PopupNoUI> _popupStack = new Stack<PopupNoUI>();
        public int Count => _popupStack.Count;
        private PopupNoUI _pfPopup;

        public PopupNoUIManager(PopupNoUI pfPopup, int count = 0)
        {
            _pfPopup = pfPopup;
            if (count > 0)
            {
                for (int i = 0; i < count; i++) CreatePopup();
            }
        }

        public PopupNoUI GetPopup(string text = "")
        {
            PopupNoUI popup = _popupStack.Count > 0 ? _popupStack.Pop() : CreatePopup();
            ObserveState(popup);
            popup.SetText(text);
            popup.SetActive(true);
            return popup;
        }

        private PopupNoUI CreatePopup()
        {
            return GameObject.Instantiate(_pfPopup);
        }

        private void ObserveState(PopupNoUI popup)
        {
            popup.OnDisableAsObservable()
                .Subscribe(_ => ReleasePopup(popup))
                .AddTo(popup);
        }

        public void ReleasePopup(PopupNoUI popup)
        {
            popup.SetActive(false);
            if (!_popupStack.Contains(popup))
            {
                _popupStack.Push(popup);
            }
        }
    }
}
