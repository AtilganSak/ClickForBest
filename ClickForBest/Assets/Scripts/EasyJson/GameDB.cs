using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDB
{
    public Score score;
    public List<int> purchaseStoreItems = new List<int>();
    public int selected_store_item;
}
[System.Serializable]
public struct Score
{
    public int underK;
    public int k;
    public int m;
    public int b;
    public int t;
    public int q;
    public int qt;
    public int s;
    public int sp;
    public int o;
    public int n;
    public int d;
}
