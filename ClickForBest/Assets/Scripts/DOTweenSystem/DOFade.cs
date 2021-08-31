using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DOFade : DOBase
{
    public enum FadeSource
    {
        Image,
        CanvasGroup,
        SpriteRenderer,
        Material
    }

    [ContextMenuItem("Record", "RecordStart")]
    [ContextMenuItem("Apply", "ApplyStartValue")]
    public float startValue;

    [ContextMenuItem("Apply", "ApplyEndValue")]
    [ContextMenuItem("Record", "RecordEnd")]
    public float endvalue = 1;

    public FadeSource fadeSource;

    Image sourceImage;
    SpriteRenderer sourceSprite;
    CanvasGroup sourceCanvasGroup;
    Material sourceMaterial;

    internal override void VirtualEnable()
    {
        switch (fadeSource)
        {
            case FadeSource.Image:
                sourceImage = GetComponent<Image>();
                break;
            case FadeSource.CanvasGroup:
                sourceCanvasGroup = GetComponent<CanvasGroup>();
                break;
            case FadeSource.SpriteRenderer:
                sourceSprite = GetComponent<SpriteRenderer>();
                break;
            case FadeSource.Material:
                sourceMaterial = GetComponent<Renderer>().material;
                break;
        }
    }
    public override void DO()
    {
        switch (fadeSource)
        {
            case FadeSource.Image:
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(sourceImage))
                        sourceImage.DOKill(true);
                }
                tween = sourceImage.DOFade(endvalue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
                base.DO();
                break;
            case FadeSource.CanvasGroup:
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(sourceCanvasGroup))
                        sourceCanvasGroup.DOKill(true);
                }
                tween = sourceCanvasGroup.DOFade(endvalue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
                base.DO();
                break;
            case FadeSource.SpriteRenderer:
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(sourceSprite))
                        sourceSprite.DOKill(true);
                }
                tween = sourceSprite.DOFade(endvalue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
                base.DO();
                break;
            case FadeSource.Material:
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(sourceMaterial))
                        sourceMaterial.DOKill(true);
                }
                tween = sourceMaterial.DOFade(endvalue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
                base.DO();
                break;
        }
    }
    public override void DORevert()
    {
        switch (fadeSource)
        {
            case FadeSource.Image:
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(sourceImage))
                        sourceImage.DOKill(true);
                }
                tween = sourceImage.DOFade(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
                base.DORevert();
                break;
            case FadeSource.CanvasGroup:
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(sourceCanvasGroup))
                        sourceCanvasGroup.DOKill(true);
                }
                tween = sourceCanvasGroup.DOFade(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
                base.DORevert();
                break;
            case FadeSource.SpriteRenderer:
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(sourceSprite))
                        sourceSprite.DOKill(true);
                }
                tween = sourceSprite.DOFade(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
                base.DORevert();
                break;
            case FadeSource.Material:
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(sourceMaterial))
                        sourceMaterial.DOKill(true);
                }
                tween = sourceMaterial.DOFade(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
                base.DORevert();
                break;
        }
    }
    public override void ResetDO()
    {
        switch (fadeSource)
        {
            case FadeSource.Image:
                sourceImage = GetComponent<Image>();
                sourceImage.DOKill(true);
                Color color = sourceImage.color;
                color.a = startValue;
                sourceImage.color = color;
                break;
            case FadeSource.CanvasGroup:
                sourceCanvasGroup = GetComponent<CanvasGroup>();
                sourceCanvasGroup.DOKill(true);
                sourceCanvasGroup.alpha = startValue;
                break;
            case FadeSource.SpriteRenderer:
                sourceSprite = GetComponent<SpriteRenderer>();
                sourceSprite.DOKill(true);
                Color color2 = sourceSprite.color;
                color2.a = startValue;
                sourceSprite.color = color2;
                break;
            case FadeSource.Material:
                sourceMaterial = GetComponent<Renderer>().material;
                sourceMaterial.DOKill(true);
                Color color3 = sourceMaterial.color;
                color3.a = startValue;
                sourceMaterial.color = color3;
                break;
        }
    }
    public override void DOLoop()
    {
        switch (fadeSource)
        {
            case FadeSource.Image:
                if (DOTween.IsTweening(sourceImage))
                    sourceImage.DOKill(true);
                tween = sourceImage.DOFade(endvalue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
                break;
            case FadeSource.CanvasGroup:
                if (DOTween.IsTweening(sourceCanvasGroup))
                    sourceCanvasGroup.DOKill(true);
                tween = sourceCanvasGroup.DOFade(endvalue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
                break;
            case FadeSource.SpriteRenderer:
                if (DOTween.IsTweening(sourceSprite))
                    sourceSprite.DOKill(true);
                tween = sourceSprite.DOFade(endvalue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
                break;
            case FadeSource.Material:
                if (DOTween.IsTweening(sourceMaterial))
                    sourceMaterial.DOKill(true);
                tween = sourceMaterial.DOFade(endvalue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
                break;
        }
    }
    public override void Kill()
    {
        switch (fadeSource)
        {
            case FadeSource.Image:
                if (DOTween.IsTweening(sourceImage))
                    sourceImage.DOKill(true);
                break;
            case FadeSource.CanvasGroup:
                if (DOTween.IsTweening(sourceCanvasGroup))
                    sourceCanvasGroup.DOKill(true);
                break;
            case FadeSource.SpriteRenderer:
                if (DOTween.IsTweening(sourceSprite))
                    sourceSprite.DOKill(true);
                break;
            case FadeSource.Material:
                if (DOTween.IsTweening(sourceMaterial))
                    sourceMaterial.DOKill(true);
                break;
        }
    }

    void RecordStart()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "ChangedStartColor");
        EditorUtility.SetDirty(this);
#endif
        switch (fadeSource)
        {
            case FadeSource.Image:
                startValue = GetComponent<Image>().color.a;
                break;
            case FadeSource.CanvasGroup:
                startValue = GetComponent<CanvasGroup>().alpha;
                break;
            case FadeSource.SpriteRenderer:
                startValue = GetComponent<SpriteRenderer>().color.a;
                break;
            case FadeSource.Material:
                startValue = GetComponent<Renderer>().material.color.a;
                break;
        }
    }
    void ApplyStartValue()
    {
#if UNITY_EDITOR
        Undo.RecordObject(transform, "ApplyedStartColor");
        EditorUtility.SetDirty(this);
#endif
        switch (fadeSource)
        {
            case FadeSource.Image:
                Color color = GetComponent<Image>().color;
                color.a = startValue;
                GetComponent<Image>().color = color;
                break;
            case FadeSource.CanvasGroup:
                GetComponent<CanvasGroup>().alpha = startValue;
                break;
            case FadeSource.SpriteRenderer:
                Color color2 = GetComponent<SpriteRenderer>().color;
                color2.a = startValue;
                GetComponent<SpriteRenderer>().color = color2;
                break;
            case FadeSource.Material:
                Color color3 = GetComponent<Renderer>().material.color;
                color3.a = startValue;
                GetComponent<Renderer>().material.color = color3;
                break;
        }
    }
    void RecordEnd()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "ChangedEndColor");
        EditorUtility.SetDirty(this);
#endif
        switch (fadeSource)
        {
            case FadeSource.Image:
                endvalue = GetComponent<Image>().color.a;
                break;
            case FadeSource.CanvasGroup:
                endvalue = GetComponent<CanvasGroup>().alpha;
                break;
            case FadeSource.SpriteRenderer:
                endvalue = GetComponent<SpriteRenderer>().color.a;
                break;
            case FadeSource.Material:
                endvalue = GetComponent<Renderer>().material.color.a;
                break;
        }
    }
    void ApplyEndValue()
    {
#if UNITY_EDITOR
        Undo.RecordObject(transform, "ApplyedEndColor");
        EditorUtility.SetDirty(this);
#endif
        switch (fadeSource)
        {
            case FadeSource.Image:
                Color color = GetComponent<Image>().color;
                color.a = endvalue;
                GetComponent<Image>().color = color;
                break;
            case FadeSource.CanvasGroup:
                GetComponent<CanvasGroup>().alpha = endvalue;
                break;
            case FadeSource.SpriteRenderer:
                Color color2 = GetComponent<SpriteRenderer>().color;
                color2.a = endvalue;
                GetComponent<SpriteRenderer>().color = color2;
                break;
            case FadeSource.Material:
                Color color3 = GetComponent<Renderer>().material.color;
                color3.a = endvalue;
                GetComponent<Renderer>().material.color = color3;
                break;
        }
    }
}
