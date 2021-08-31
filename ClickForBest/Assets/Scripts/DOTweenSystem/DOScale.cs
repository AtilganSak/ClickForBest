using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class DOScale : DOBase
{
    [ContextMenuItem("Record", "RecordStart")]
    [ContextMenuItem("Apply", "ApplyStartValue")]
    public Vector3 startValue = Vector3.one;

    [ContextMenuItem("Apply", "ApplyEndValue")]
    [ContextMenuItem("Record", "RecordEnd")]
    public Vector3 endValue = Vector3.one;

    public override void DO()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(c_Transform))
                    c_Transform.DOKill(true);
            }
            tween = c_Transform.DOScale(endValue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());

            base.DO();
        }
        else
        {
            transform.localScale = endValue;
        }
    }
    public override void DORevert()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(c_Transform))
                    c_Transform.DOKill(true);
            }
            tween = c_Transform.DOScale(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());

            base.DORevert();
        }
        else
        {
            transform.localScale = startValue;
        }
    }
    public override void ResetDO()
    {
#if UNITY_EDITOR
        Undo.RecordObject(gameObject, name + "Changed transform");
#endif
        transform.DOKill(true);
        transform.localScale = startValue;

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
                if (DOTween.IsTweening(c_Transform))
                    c_Transform.DOKill(true);
            }
            tween = c_Transform.DOScale(endValue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
        }
        else
        {
            transform.localScale = endValue;
        }
    }
    public override void Kill()
    {
        if (DOTween.IsTweening(transform))
            transform.DOKill();
    }

    void RecordStart()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "ChangedStartValue");
        EditorUtility.SetDirty(this);
#endif
        startValue = transform.localScale;
    }
    void ApplyStartValue()
    {
#if UNITY_EDITOR
        Undo.RecordObject(transform, "ApplyedStartValue");
        EditorUtility.SetDirty(this);
#endif
        transform.localScale = startValue;
    }
    void RecordEnd()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "ChangedEndValue");
        EditorUtility.SetDirty(this);
#endif
        endValue = transform.localScale;
    }
    void ApplyEndValue()
    {
#if UNITY_EDITOR
        Undo.RecordObject(transform, "ApplyedEndValue");
        EditorUtility.SetDirty(this);
#endif
        transform.localScale = endValue;
    }
}
