using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class DORotate : DOBase
{
    [ContextMenuItem("Record", "RecordStart")]
    [ContextMenuItem("Apply", "ApplyStartValue")]
    public Vector3 startValue;

    [ContextMenuItem("Apply", "ApplyEndValue")]
    [ContextMenuItem("Record", "RecordEnd")]
    public Vector3 endValue = Vector3.one;
    public RotateMode rotateMode;

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
            if (!local)
                tween = c_Transform.DORotate(endValue, duration, rotateMode).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
            else
                tween = c_Transform.DOLocalRotate(endValue, duration, rotateMode).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());

            base.DO();
        }
        else
        {
            if (!local)
                transform.rotation = Quaternion.Euler(endValue);
            else
                transform.localRotation = Quaternion.Euler(endValue);
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
            if (!local)
                tween = c_Transform.DORotate(startValue, duration, rotateMode).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
            else
                tween = c_Transform.DOLocalRotate(startValue, duration, rotateMode).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());

            base.DORevert();
        }
        else
        {
            if (!local)
                transform.rotation = Quaternion.Euler(startValue);
            else
                transform.localRotation = Quaternion.Euler(startValue);
        }
    }
    public override void ResetDO()
    {
        transform.DOKill(true);
        if (!local)
            transform.rotation = Quaternion.Euler(startValue);
        else
            transform.localRotation = Quaternion.Euler(startValue);
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
            if (!local)
                tween = c_Transform.DORotate(endValue, duration, rotateMode).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
            else
                tween = c_Transform.DOLocalRotate(endValue, duration, rotateMode).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
        }
        else
        {
            if (!local)
                transform.rotation = Quaternion.Euler(endValue);
            else
                transform.localRotation = Quaternion.Euler(endValue);
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
        if (local)
        {
            startValue = transform.localEulerAngles;
        }
        else
        {
            startValue = transform.eulerAngles;
        }
    }
    void ApplyStartValue()
    {
#if UNITY_EDITOR
        Undo.RecordObject(transform, "ApplyedStartValue");
        EditorUtility.SetDirty(this);
#endif
        if (local)
        {
            transform.localRotation = Quaternion.Euler(startValue);
        }
        else
        {
            transform.rotation = Quaternion.Euler(startValue);
        }
    }
    void RecordEnd()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "ChangedEndValue");
        EditorUtility.SetDirty(this);
#endif
        if (local)
        {
            endValue = transform.localEulerAngles;
        }
        else
        {
            endValue = transform.eulerAngles;
        }
    }
    void ApplyEndValue()
    {
#if UNITY_EDITOR
        Undo.RecordObject(transform, "ApplyedEndValue");
        EditorUtility.SetDirty(this);
#endif
        if (local)
        {
            transform.localRotation = Quaternion.Euler(endValue);
        }
        else
        {
            transform.rotation = Quaternion.Euler(endValue);
        }
    }
}
