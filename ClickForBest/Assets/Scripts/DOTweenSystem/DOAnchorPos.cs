using DG.Tweening;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class DOAnchorPos : DOBase
{
    public bool useStartTarget;
    [ConditionalField("useStartTarget", true)]
    public RectTransform startTarget;
    
    public bool useEndTarget;
    [ConditionalField("useEndTarget", true)]
    public RectTransform endTarget;

    [ContextMenuItem("Record", "RecordStart")]
    [ContextMenuItem("Apply", "ApplyStartValue")]
    [ConditionalField("useStartTarget", false)]
    public Vector2 startValue;

    [ContextMenuItem("Apply", "ApplyEndValue")]
    [ContextMenuItem("Record", "RecordEnd")]
    [ConditionalField("useEndTarget", false)]
    public Vector2 endValue = Vector2.one;

    RectTransform  c_TransformRect;

    internal override void VirtualEnable()
    {
        c_TransformRect = GetComponent<RectTransform>();
    }
    public override void DO()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(c_TransformRect))
                    c_TransformRect.DOKill();
            }
            if(useEndTarget)
                tween = c_TransformRect.DOAnchorPos(endTarget.anchoredPosition, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
            else
                tween = c_TransformRect.DOAnchorPos(endValue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());

            base.DO();
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = endValue;
        }
    }
    public override void DORevert()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(c_TransformRect))
                    c_TransformRect.DOKill();
            }
            if (useStartTarget)
                tween = c_TransformRect.DOAnchorPos(startTarget.anchoredPosition, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
            else
                tween = c_TransformRect.DOAnchorPos(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());

            base.DORevert();
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = startValue;
        }
    }
    public override void ResetDO()
    {
#if UNITY_EDITOR
        Undo.RecordObject(gameObject, name + "Changed transform");
#endif
        GetComponent<RectTransform>().DOKill();
        if (useStartTarget)
            GetComponent<RectTransform>().anchoredPosition = startTarget.anchoredPosition;
        else
            GetComponent<RectTransform>().anchoredPosition = startValue;
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
    public override void DOLoop()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(c_TransformRect))
                    c_TransformRect.DOKill();
            }
            if (useEndTarget)
                tween = c_TransformRect.DOAnchorPos(endTarget.anchoredPosition, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
            else
                tween = c_TransformRect.DOAnchorPos(endValue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
        }
        else
        {
            if (useEndTarget)
                GetComponent<RectTransform>().anchoredPosition = endTarget.anchoredPosition;
            else
                GetComponent<RectTransform>().anchoredPosition = endValue;
        }
    }
    public override void Kill()
    {
        if (DOTween.IsTweening(c_TransformRect))
            c_TransformRect.DOKill();
    }

    void RecordStart()
    {
        if (!useStartTarget)
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "ChangedStartValue");
            EditorUtility.SetDirty(this);
#endif
            startValue = GetComponent<RectTransform>().anchoredPosition;
        }
    }
    void ApplyStartValue()
    {
#if UNITY_EDITOR
        Undo.RecordObject(transform, "ApplyedStartValue");
        EditorUtility.SetDirty(this);
#endif
        if (!useStartTarget)
        {
            GetComponent<RectTransform>().anchoredPosition = startValue;
        }
    }
    void RecordEnd()
    {
        if (!useEndTarget)
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "ChangedEndValue");
            EditorUtility.SetDirty(this);
#endif
            endValue = GetComponent<RectTransform>().anchoredPosition;
        }
    }
    void ApplyEndValue()
    {
        if (!useEndTarget)
        {
#if UNITY_EDITOR
            Undo.RecordObject(transform, "ApplyedEndValue");
            EditorUtility.SetDirty(this);
#endif
            GetComponent<RectTransform>().anchoredPosition = endValue;
        }
    }
}
