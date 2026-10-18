using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Jostick_Move : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _JostickBackground;
    [SerializeField] private Image _Jostick;
    [SerializeField] private Image _JostickArea;

    private Vector2 _joystickStartPosition;
    private Camera _canvasCamera; // Добавляем ссылку на камеру Canvas

    public Vector2 _InputVector;

    public Color _activ, _InActiv;
    public bool isDown;

    void Start()
    {
        _joystickStartPosition = _JostickBackground.rectTransform.anchoredPosition;

        // Находим камеру Canvas
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null && canvas.worldCamera != null)
        {
            _canvasCamera = canvas.worldCamera;
        }
        else
        {
            // Если камера не назначена, используем Camera.main
            _canvasCamera = Camera.main;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 JostikPosition;

        // Используем _canvasCamera вместо null или Camera.main
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _JostickBackground.rectTransform,
            eventData.position,
            _canvasCamera, // Передаем камеру Canvas
            out JostikPosition))
        {
            JostikPosition.x = (JostikPosition.x * 2 / _JostickBackground.rectTransform.sizeDelta.x);
            JostikPosition.y = (JostikPosition.y * 2 / _JostickBackground.rectTransform.sizeDelta.y);
            _InputVector = new Vector2(JostikPosition.x, JostikPosition.y);
            _InputVector = (_InputVector.magnitude > 1f) ? _InputVector.normalized : _InputVector;
            _Jostick.rectTransform.anchoredPosition = new Vector2(
                _InputVector.x * (_JostickBackground.rectTransform.sizeDelta.x / 2),
                _InputVector.y * (_JostickBackground.rectTransform.sizeDelta.y / 2)
            );
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 JostikPosition;
        isDown = true;

        // Для OnPointerDown тоже передаем камеру
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _JostickArea.rectTransform,
            eventData.position,
            _canvasCamera, // Передаем камеру Canvas
            out JostikPosition))
        {
            _JostickBackground.rectTransform.anchoredPosition = new Vector2(JostikPosition.x, JostikPosition.y);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
        _JostickBackground.rectTransform.anchoredPosition = _joystickStartPosition;
        _Jostick.rectTransform.anchoredPosition = Vector2.zero;
    }
}
