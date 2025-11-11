using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragSystem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Image _image;

    private CanvasGroup cg;
    private RectTransform rectTransform;
    private Vector2 startPosition;
    private Transform originalParent;
    private Transform topLevelParent;
    private int originalSiblingIndex;

    private void OnEnable()
    {
        rectTransform = _image.GetComponent<RectTransform>();
        startPosition = _image.rectTransform.position;
        cg = _image.GetComponent<CanvasGroup>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log("Click");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = _image.transform.parent;
        originalSiblingIndex = _image.transform.GetSiblingIndex();

        topLevelParent = transform.root;

        _image.transform.SetParent(topLevelParent);
        _image.transform.SetAsLastSibling();
        cg.blocksRaycasts = false;

        SetImageTransparency(0.8f);
        Debug.Log("IBeginDrag");
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;

        Debug.Log("Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.transform.SetParent(originalParent);
        _image.transform.SetSiblingIndex(originalSiblingIndex);
        rectTransform.anchoredPosition = startPosition;

        SetImageTransparency(1);
        cg.blocksRaycasts = true;
        Debug.Log("IEndDrag");
    }
    private void SetImageTransparency(float alpha)
    {
        if (alpha >= 0f && alpha <= 1f)
        {
            cg.alpha = alpha;
        }

    }
}
