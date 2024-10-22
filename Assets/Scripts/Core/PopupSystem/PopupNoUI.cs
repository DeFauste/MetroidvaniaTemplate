using System;
using R3;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Core.PopupSystem
{
    /// <summary>
    /// PopupNoUI позваляет создать простой текст на экране без использования Canvas. Без указания времени через метод SetTimeLife()
    /// необходимо вручную контролировать время жизни.
    /// </summary>
    public class PopupNoUI: MonoBehaviour
    {
        [SerializeField] private TextMeshPro _tmp;
        private void Awake()
        {
            if(_tmp == null) _tmp = GetComponent<TextMeshPro>();
        }

        public void Construct(string text, Color  colorText, int fontSize = -1, TMPro.FontStyles fontStyle = TMPro.FontStyles.Normal)
        {
            _tmp.text = text;
            _tmp.color = colorText;
            _tmp.fontSize = fontSize;
            _tmp.fontStyle = fontStyle;
        }

        public PopupNoUI SetText(string text)
        {
            _tmp.text = text;
            return this;
        }
        public PopupNoUI SetColor(Color color)
        {
            _tmp.color = color;
            return this;
        }
        public void SetActive(bool active)  => gameObject.SetActive(active);
        public void SetParent(Transform parent)
        {
            gameObject.transform.SetParent(parent, false);
        }
        public void SetTimeLife(double second)
        {
            if (second > 0)
            {
                Observable.Timer(TimeSpan.FromSeconds(second))
                 .Subscribe(_ => SetActive(false))
                 .AddTo(this);
            }
        }
        public void SetPosition(Vector3 position) => gameObject.transform.position = position;
        
    }
}
