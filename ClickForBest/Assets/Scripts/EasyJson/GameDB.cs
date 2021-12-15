using System.Collections.Generic;

[System.Serializable]
public class GameDB
{
    public Score score;
    public int selected_store_item;
    public float last_session_time;
    public int last_session_click_ad_count;
    public int last_session_click_count;
    public int token;
}
[System.Serializable]
public class ScoreBoardPlayer
{
    public string name;
    public int score;
    public int order;
    public bool isMine;
}
[System.Serializable]
public class Score
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
