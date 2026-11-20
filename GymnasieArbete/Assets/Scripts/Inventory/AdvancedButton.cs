using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
public class AdvancedButton : UnityEngine.UI.Selectable, IPointerClickHandler
{
    [Header("Click Events")]
    public UnityEvent OnLeftClick;
    public UnityEvent OnRightClick;

    private Coroutine _resetRoutine;

    protected override void Reset()
    {
        base.Reset();

        var imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            imageComponent = gameObject.AddComponent<Image>();
        }

        targetGraphic = imageComponent;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DoStateTransition(SelectionState.Pressed, true);

        switch (eventData.button)
        {
            default:
            case PointerEventData.InputButton.Left:
                OnLeftClick?.Invoke();
                break;
            case PointerEventData.InputButton.Right:
                OnRightClick?.Invoke();
                break;
        }

        if (_resetRoutine != null)
        {
            StopCoroutine(_resetRoutine);
        }

        _resetRoutine = StartCoroutine(OnFinishSubmit());
    }

    private IEnumerator OnFinishSubmit()
    {
        var fadeTime = colors.fadeDuration;
        var elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        DoStateTransition(currentSelectionState, false);
    }
}
#endif