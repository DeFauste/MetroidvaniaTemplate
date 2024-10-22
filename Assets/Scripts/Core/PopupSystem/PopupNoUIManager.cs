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
        Stack<PopupNoUI> stack = new Stack<PopupNoUI>();
        public int Count { get; private set; } = 0;
        private PopupNoUI _pfPopup;
        public PopupNoUIManager(PopupNoUI pfPopup, int count = 0)
        {
            _pfPopup = pfPopup;
            if (count > 0)
            {
                for (int i = 0; i < count; i++) Create("");
            }
        }

        public PopupNoUI Get(string text = "")
        {
            PopupNoUI popup;
            if(!stack.TryPop(out popup))
            {
                popup = Create(text);
                Count--;
            }
            ObserveState(popup);
            popup.SetActive(true);
            return popup;
        }
        public PopupNoUI Create(string text)
        {
            var popup = GameObject.Instantiate(_pfPopup);
            popup.SetText(text);
            return popup;
        }
        private void ObserveState(PopupNoUI popum)
        {
            popum.OnDisableAsObservable().Subscribe(_ =>
            {
                Relese(popum);
            }).AddTo(popum);
        }
        
        public void Relese(PopupNoUI popum)
        {
            popum.SetActive(false);
            if (!stack.Contains(popum))
            {
                stack.Push(popum);
                Count++;
            }
        }
    }
}
