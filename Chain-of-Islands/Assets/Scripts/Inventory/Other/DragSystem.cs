using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragSystem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Image _image;

    private GraphicRaycaster raycaster;
    private CanvasGroup cg;
    private RectTransform rectTransform;
    private Vector2 startPosition;
    private Transform originalParent;
    private Transform topLevelParent;
    private int originalSiblingIndex;
    private GameObject clickedObject;

    public event Action<UIInventorySlot> OnClickedOnSlot;

    private void Start()
    {
        raycaster = GetComponentInParent<GraphicRaycaster>();
        rectTransform = _image?.GetComponent<RectTransform>();

        startPosition = _image.rectTransform.position;

        cg = _image?.GetComponent<CanvasGroup>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        clickedObject = eventData?.pointerCurrentRaycast.gameObject;
        OnClickedOnSlot?.Invoke(clickedObject?.GetComponent<UIInventorySlot>());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        clickedObject = eventData.pointerDrag.gameObject;
        OnClickedOnSlot?.Invoke(clickedObject?.GetComponent<UIInventorySlot>());

        originalParent = _image.transform.parent;
        originalSiblingIndex = _image.transform.GetSiblingIndex();

        topLevelParent = transform.root;
        _image.transform.SetParent(topLevelParent);
        _image.transform.SetAsLastSibling();

        cg.blocksRaycasts = false;
        SetImageTransparency(0.8f);
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.transform.SetParent(originalParent);
        _image.transform.SetSiblingIndex(originalSiblingIndex);
        rectTransform.anchoredPosition = startPosition;

        cg.blocksRaycasts = true;
        SetImageTransparency(1);
    }

    private void SetImageTransparency(float alpha)
    {
        if (alpha >= 0f && alpha <= 1f)
        {
            cg.alpha = alpha;
        }
    }
}