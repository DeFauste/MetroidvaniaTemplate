using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Core.PopupSystem
{
    /// <summary>
    /// PopupDialogUI позволяет создать диалог с двумя вариантами ответа. Центруется и масштабируется относительно родительского Canvas.
    /// </summary>
    public class PopupDialogUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tmpQuestion;
        [SerializeField] protected Button _btnPositive;
        [SerializeField] private TextMeshProUGUI _tmpBtnPositive;
        [SerializeField] protected Button _btnNegative;
        [SerializeField] private TextMeshProUGUI _tmpBtnNegative;
        public void Construct(string tmpQuestion, string tmpPositive, string tmpNegative, Action actionPositive, Action actionNegative)
        {
            _tmpQuestion.text = tmpQuestion;
            _tmpBtnPositive.text = tmpPositive;
            _tmpBtnNegative.text = tmpNegative;
            _btnPositive.onClick.AddListener(() =>
            {
                actionPositive();
                SetActive(false);
            });
            _btnNegative.onClick.AddListener(() =>
            {
                actionNegative();
                SetActive(false);
            });
            SetActive(true);
        }
        public void SetPosition(RectTransform rectTransform) => gameObject.transform.position = rectTransform.position;
        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

    }
}
