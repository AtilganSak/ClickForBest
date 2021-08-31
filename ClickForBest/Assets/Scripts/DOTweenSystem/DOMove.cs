using DG.Tweening;
using UnityEngine;
using UnityEditor;

public class DOMove : DOBase
{
    public bool useStartTarget;
    [ConditionalField("useStartTarget", true)]
    public Transform startTarget;

    public bool useEndTarget;
    [ConditionalField("useEndTarget", true)]
    public Transform endTarget;

    [ContextMenuItem("Record", "RecordStart")]
    [ContextMenuItem("Apply", "ApplyStartValue")]
    [ConditionalField("useStartTarget", false)]
    public Vector3 startValue;

    [ContextMenuItem("Apply", "ApplyEndValue")]
    [ContextMenuItem("Record", "RecordEnd")]
    [ConditionalField("useEndTarget", false)]
    public Vector3 endValue = Vector3.one;

    public bool local;

    public override void DO()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(c_Transform))
                    c_Transform.DOKill(true);
            }
            if (useEndTarget)
            {
                if (!local)
                    tween = c_Transform.DOMove(endTarget.position, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
                else
                    tween = c_Transform.DOLocalMove(endTarget.localPosition, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
            }
            else
            {
                if (!local)
                    tween = c_Transform.DOMove(endValue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
                else
                    tween = c_Transform.DOLocalMove(endValue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
            }
            base.DO();
        }
        else
        {
#if UNITY_EDITOR
            Undo.RegisterCompleteObjectUndo(transform, "Changed Transform Poisition");
#endif
            if (useEndTarget)
            {
                if (!local)
                    transform.position = endTarget.position;
                else
                    transform.localPosition = endTarget.localPosition;
            }
            else
            {
                if (!local)
                    transform.position = endValue;
                else
                    transform.localPosition = endValue;
            }

#if UNITY_EDITOR
            EditorUtility.SetDirty(transform);
#endif
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
            if (useStartTarget)
            {
                if (!local)
                    tween = c_Transform.DOMove(startTarget.position, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
                else
                    tween = c_Transform.DOLocalMove(startTarget.localPosition, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
            }
            else
            {
                if (!local)
                    tween = c_Transform.DOMove(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
                else
                    tween = c_Transform.DOLocalMove(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
            }
            base.DORevert();
        }
        else
        {
#if UNITY_EDITOR
            Undo.RegisterCompleteObjectUndo(transform, "Changed Transform Poisition");
#endif
            if (useStartTarget)
            {
                if (!local)
                    transform.position = startTarget.position;
                else
                    transform.localPosition = startTarget.localPosition;
            }
            else
            {
                if (!local)
                    transform.position = startValue;
                else
                    transform.localPosition = startValue;
            }
#if UNITY_EDITOR
            EditorUtility.SetDirty(transform);
#endif
        }
    }
    public override void ResetDO()
    {
#if UNITY_EDITOR
        Undo.RegisterCompleteObjectUndo(transform, "Changed Transform Poisition");
#endif
        transform.DOKill(true);
        if (useStartTarget)
        {
            if (!local)
                transform.position = startTarget.position;
            else
                transform.localPosition = startTarget.localPosition;
        }
        else
        {
            if (!local)
                transform.position = startValue;
            else
                transform.localPosition = startValue;
        }
#if UNITY_EDITOR
        Undo.RegisterCompleteObjectUndo(transform, "Changed Transform Poisition");
        EditorUtility.SetDirty(transform);
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
            if (useEndTarget)
            {
                if (!local)
                    tween = c_Transform.DOMove(endTarget.position, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
                else
                    tween = c_Transform.DOLocalMove(endTarget.localPosition, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
            }
            else
            {
                if (!local)
                    tween = c_Transform.DOMove(endValue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
                else
                    tween = c_Transform.DOLocalMove(endValue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
            }
        }
        else
        {
            if (useEndTarget)
            {
                if (!local)
                    transform.position = endTarget.position;
                else
                    transform.localPosition = endTarget.localPosition;
            }
            else
            {
                if (!local)
                    transform.position = endValue;
                else
                    transform.localPosition = endValue;
            }
        }
    }
    public override void Kill()
    {
        if (DOTween.IsTweening(transform))
            transform.DOKill();
    }

    void RecordStart()
    {
        if (!useStartTarget)
        {
#if UNITY_EDITOR
                Undo.RecordObject(this, "ChangedStartValue");
                EditorUtility.SetDirty(this);
#endif
            if (local)
            {
                startValue = transform.localPosition;
            }
            else
            {
                startValue = transform.position;
            }
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
            if (local)
            {
                transform.localPosition = startValue;
            }
            else
            {
                transform.position = startValue;
            }
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
            if (local)
            {
                endValue = transform.localPosition;
            }
            else
            {
                endValue = transform.position;
            }
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
            if (local)
            {
                transform.localPosition = endValue;
            }
            else
            {
                transform.position = endValue;
            }
        }
    }
}
