using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostTextControl : MonoBehaviour
{
    public RectTransform txt_prefab;

    private Canvas canvas;

    private Vector2 start_position;
    private Vector2 origin_position;

    private void OnEnable()
    {
        canvas = GetComponentInParent<Canvas>();

        start_position = GetComponent<RectTransform>().anchoredPosition;
        origin_position = ReferenceKeeper.Instance.ClickButton.GetComponent<RectTransform>().anchoredPosition;
    }
    [EasyButtons.Button]
    public void Show(string _text)
    {
        RectTransform tr = Instantiate(txt_prefab);
        tr.anchoredPosition = origin_position;
        tr.gameObject.SetActive(true);
        tr.GetComponent<TMPro.TMP_Text>().text = _text;
        tr.transform.SetParent(transform);
        tr.transform.localScale = Vector3.one;
        Vector3 newPos = start_position;
        newPos.x += Random.Range(-200.0F, 200.0F);
        DOAnchorPos dOAnchorPos = tr.GetComponent<DOAnchorPos>();
        dOAnchorPos.endValue = newPos;
        dOAnchorPos.DO();
        tr.GetComponent<DOFade>().DO();
    }
}
