using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DOMaterialColor : DOBase
{
    [ContextMenuItem("Record", "RecordStart")]
    [ContextMenuItem("Apply", "ApplyStartValue")]
    public Color startColor = Color.white;

    [ContextMenuItem("Apply", "ApplyEndValue")]
    [ContextMenuItem("Record", "RecordEnd")]
    public Color endColor = Color.white;

    Material sourceMaterial;

    internal override void VirtualEnable()
    {
        sourceMaterial = GetComponent<Renderer>().material;
    }
    public override void DO()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(c_Transform))
                    sourceMaterial.DOKill(true);
            }
            tween = sourceMaterial.DOColor(endColor, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
            base.DO();
        }
        else
        {
            GetComponent<Renderer>().material.color = endColor;
        }
    }
    public override void DORevert()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(c_Transform))
                    sourceMaterial.DOKill(true);
            }
            tween = sourceMaterial.DOColor(startColor, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
            base.DORevert();
        }
        else
        {
            GetComponent<Renderer>().material.color = startColor;
        }
    }
    public override void ResetDO()
    {
        sourceMaterial.DOKill(true);
        sourceMaterial.color = startColor;
    }
    public override void DOLoop()
    {
        if (Application.isPlaying)
        {
            if (!allowTwin)
            {
                if (DOTween.IsTweening(c_Transform))
                    sourceMaterial.DOKill(true);
            }
            tween = sourceMaterial.DOColor(startColor, duration).SetDelay(revertDelay).SetEase(ease).SetLoops(-1,loopType);
        }
        else
        {
            GetComponent<Renderer>().material.color = startColor;
        }
    }
    public override void Kill()
    {
        if (DOTween.IsTweening(sourceMaterial))
            sourceMaterial.DOKill();
    }

    void RecordStart()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "ChangedStartColor");
        EditorUtility.SetDirty(this);
#endif
        startColor = GetComponent<Renderer>().material.color;
    }
    void ApplyStartValue()
    {
#if UNITY_EDITOR
        Undo.RecordObject(transform, "ApplyedStartColor");
        EditorUtility.SetDirty(this);
#endif
        GetComponent<Renderer>().material.color = startColor;
    }
    void RecordEnd()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "ChangedEndColor");
        EditorUtility.SetDirty(this);
#endif
        endColor = GetComponent<Renderer>().material.color;
    }
    void ApplyEndValue()
    {
#if UNITY_EDITOR
        Undo.RecordObject(transform, "ApplyedEndColor");
        EditorUtility.SetDirty(this);
#endif
        GetComponent<Renderer>().material.color = endColor;
    }
}
