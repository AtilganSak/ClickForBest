using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosetteController : MonoBehaviour
{
    [System.Serializable]
    public struct Item
    {
        public Sprite icon;
        public int score_k;
        public int score_m;
        public int score_b;
        public bool earned;

        public bool IsTrue(int _k, int _m, int _b)
        {
            if (_b > 0 && _b >= score_b)
                earned = true;
            else if (_m > 0 && _m >= score_m && score_b == 0)
                earned = true;
            else if (_k > 0 && _k >= score_k && score_m == 0 && score_b == 0)
                earned = true;
            else
                earned = false;
            return earned;
        }
    }

    [SerializeField] Item[] items;

    private RosetteSpawner spawner;

    private void OnEnable()
    {
        spawner = FindObjectOfType<RosetteSpawner>();
    }
    public void CheckOut(int _k, int _m, int _b)
    {
        if (items != null)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (!items[i].earned)
                {
                    if (items[i].IsTrue(_k,_m,_b))
                    {
                        spawner.GenerateRossette(items[i]);
                    }
                }
            }
        }
    }
    public void LoadRosettes(int _k, int _m, int _b)
    {
        if (items != null)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (!items[i].earned)
                {
                    if (items[i].IsTrue(_k, _m, _b))
                    {
                        spawner.LoadRosette(items[i]);
                    }
                }
            }
        }
    }
}
