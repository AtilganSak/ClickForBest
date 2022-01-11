using System;
using UnityEngine;
using UnityEngine.UI;

public class PageBar : MonoBehaviour
{
    [SerializeField] int default_page;
    [SerializeField] BarContent[] contents;

    [SerializeField] Transform highlight;

    public float highlight_speed;
    private CanvasGroup active_page;
    private Vector3 highlight_target;

    private void Start()
    {
        if (contents != null)
        {
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i].onClick += OpenPage;
                contents[i].Init(i);
                contents[i].page.alpha = 0;
                contents[i].page.blocksRaycasts = false;
            }
            OpenPage(default_page);
        }
    }
    private void Update()
    {
        if ((highlight.position - highlight_target).magnitude > 0.1F)
        {
            highlight.position = Vector3.MoveTowards(highlight.position, highlight_target, Time.deltaTime * highlight_speed);
        }
        else
        {
            highlight.position = highlight_target;
        }
    }
    private void OpenPage(int _index)
    {
        ClosePage(_index);

        MoveHighlight(contents[_index].button.transform.position);
    }
    private void ClosePage(int _index)
    {
        if (active_page != null)
        {
            active_page.alpha = 0;
            active_page.blocksRaycasts = false;
        }
        active_page = contents[_index].page;
        active_page.alpha = 1;
        active_page.blocksRaycasts = true;
    }
    private void MoveHighlight(Vector3 _pos)
    {
        highlight_target = _pos;
    }

    [Serializable]
    public class BarContent
    {
        public CanvasGroup page;
        public Button button;

        public Action<int> onClick;

        private int page_index;

        public void Init(int _index)
        {
            if (button != null)
            {
                button.onClick.AddListener(FireEvent);
            }
            page_index = _index;
        }
        private void FireEvent()
        {
            if (onClick != null)
            {
                onClick.Invoke(page_index);
            }
        }
    }
}
