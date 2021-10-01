using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text score_text;
    [SerializeField] TMP_Text full_score_text;
    public bool save = true;
    public GameDB gameDB { get; private set; }

    private int boost = 1;
    private int total_coin_count;
    private int earning_current_score;

    private DOScale handle_doscale;


    int underK;
    int K;
    int M;
    int B;
    int T;
    int Q;
    int QT;
    int S;
    int SP;
    int O;
    int N;
    int D;

    private void OnApplicationQuit()
    {
        ReportScore();
        SaveGame();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            ReportScore();
            SaveGame();
        }
    }
    private void OnEnable()
    {
        LoadGame();
    }
    private void Start()
    {
        handle_doscale = ReferenceKeeper.Instance.Handle.GetComponent<DOScale>();

        Application.targetFrameRate = 90;

        boost = 1;

        WriteFullScore();

        ReferenceKeeper.Instance.Handle.onActive.AddListener(HandleActivated);
        ReferenceKeeper.Instance.Handle.onDeactive.AddListener(HandleDeactivated);

        PlayerPrefs.SetInt(PlayerKeys.SCREEN_SLEEP_TIME, Screen.sleepTimeout);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void ReportScore()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (earning_current_score > 0)
        {
            ReferenceKeeper.Instance.GooglePlayServices.EarnScore(earning_current_score);
            earning_current_score = 0;
        }
#endif
    }
    public void AddCoin()
    {
        total_coin_count++;

        if (total_coin_count >= 230)
        {
            if (!PlayerPrefs.HasKey(PlayerKeys.FIRST_HANDLE_SHOW))
            {
                PlayerPrefs.SetInt(PlayerKeys.FIRST_HANDLE_SHOW, 1);
                handle_doscale.DO();
            }
        }
    }
    public void RemoveCoin()
    {
        if (total_coin_count - 1 >= 0)
        {
            total_coin_count--;
        }
        if (total_coin_count <= 0)
        {
            ReferenceKeeper.Instance.ClickButton.raycastTarget = true;
            Color c = ReferenceKeeper.Instance.ClickButton.color;
            c.a = 1.0F;
            ReferenceKeeper.Instance.ClickButton.color = c;
            ReferenceKeeper.Instance.BottomCollider.SetActive(true);
            ReferenceKeeper.Instance.Handle.ResetUp();

            GC.Collect();
        }
    }
    public void AddScore()
    {
        if (boost > 1)
            ReferenceKeeper.Instance.BoostTextControl.Show(boost + "x");

        AddScore(boost);
    }
    [EasyButtons.Button]
    public void AddScore(int _value)
    {
        earning_current_score += _value;
        score_text.text = ScoreCalculate(_value);
        WriteFullScore();
        ReferenceKeeper.Instance.RosetteController.CheckOut(K, M, B);
    }
    public void SetBoost(int _value)
    {
        boost = _value;
    }
    //public void RemoveScore(int _underK, int _k)
    //{
    //    spending_score = _k * 1000 + underK;

    //    underK -= _underK;
    //    if (underK < 0)
    //    {
    //        underK = 1000 + underK;
    //    }
    //    K -= _k;
    //    if (K < 0)
    //    {
    //        K = 0;
    //        if (M > 0 || B > 0 || T > 0 || Q > 0 || QT > 0 || S > 0 || SP > 0 || O > 0 || N > 0 || D > 0)
    //        {
    //            K = 1000 - _k;
    //            M--;
    //            if (M < 0)
    //            {
    //                M = 0;
    //                if (B > 0 || T > 0 || Q > 0 || QT > 0 || S > 0 || SP > 0 || O > 0 || N > 0 || D > 0)
    //                {
    //                    M = 999;
    //                    B--;
    //                    if (B < 0)
    //                    {
    //                        B = 0;
    //                        if (T > 0 || Q > 0 || QT > 0 || S > 0 || SP > 0 || O > 0 || N > 0 || D > 0)
    //                        {
    //                            B = 999;
    //                            T--;
    //                            if (T < 0)
    //                            {
    //                                T = 0;
    //                                if (Q > 0 || QT > 0 || S > 0 || SP > 0 || O > 0 || N > 0 || D > 0)
    //                                {
    //                                    T = 999;
    //                                    Q--;
    //                                    if (Q < 0)
    //                                    {
    //                                        Q = 0;
    //                                        if (QT > 0 || S > 0 || SP > 0 || O > 0 || N > 0 || D > 0)
    //                                        {
    //                                            Q = 999;
    //                                            QT--;
    //                                            if (QT < 0)
    //                                            {
    //                                                QT = 0;
    //                                                if (S > 0 || SP > 0 || O > 0 || N > 0 || D > 0)
    //                                                {
    //                                                    QT = 999;
    //                                                    S--;
    //                                                    if (S < 0)
    //                                                    {
    //                                                        S = 0;
    //                                                        if (SP > 0 || O > 0 || N > 0 || D > 0)
    //                                                        {
    //                                                            S = 999;
    //                                                            SP--;
    //                                                            if (SP < 0)
    //                                                            {
    //                                                                SP = 0;
    //                                                                if (O > 0 || N > 0 || D > 0)
    //                                                                {
    //                                                                    SP = 999;
    //                                                                    O--;
    //                                                                    if (O < 0)
    //                                                                    {
    //                                                                        O = 0;
    //                                                                        if (N > 0 || D > 0)
    //                                                                        {
    //                                                                            O = 999;
    //                                                                            N--;
    //                                                                            if (N < 0)
    //                                                                            {
    //                                                                                N = 0;
    //                                                                                if (D > 0)
    //                                                                                {
    //                                                                                    N = 999;
    //                                                                                    D--;
    //                                                                                }
    //                                                                            }
    //                                                                        }
    //                                                                    }
    //                                                                }
    //                                                            }
    //                                                        }
    //                                                    }
    //                                                }
    //                                            }
    //                                        }
    //                                    }
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    score_text.text = ScoreCalculate(0);
    //}
    public bool HaveScore(int _underK, int _k, int _m)
    {
        if (B > 0 || T > 0 || Q > 0 || QT > 0 || S > 0 || SP > 0 || O > 0 || N > 0 || D > 0)
        {
            return true;
        }
        else if(M > _m)
        {
            return true;
        }
        else if(M == _m)
        {
            if (K > _k)
            {
                return true;
            }
            else if (K == _k)
            {
                if (underK >= _underK)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private string ScoreCalculate(int _value)
    {
        string result = "";
        underK += _value;
        result = underK.ToString();
        if (underK >= 1000)
        {
            K += underK / 1000;
            underK = underK - 1000 * (underK / 1000);
        }
        if (K >= 1)
        {
            if (underK < 10)
                result = K + ".0" + underK + "K";
            else
                result = K + "." + underK + "K";
        }
        if (K >= 1000)
        {
            M += K / 1000;
            K = K - 1000 * (K / 1000);
        }
        if (M >= 1)
        {
            if (K < 10)
                result = M + ".0" + K + "M";
            else
                result = M + "." + K + "M";
        }
        if (M >= 1000)
        {
            B += M / 1000;
            M = M - 1000 * (M / 1000);
        }
        if (B >= 1)
        {
            if (M < 10)
                result = B + ".0" + M + "B";
            else
                result = B + "." + M + "B";
        }
        if (B >= 1000)
        {
            T += B / 1000;
            B = B - 1000 * (B / 1000);
        }
        if (T >= 1)
        {
            if (B < 10)
                result = T + ".0" + B + "T";
            else
                result = T + "." + B + "T";
        }
        if (T >= 1000)
        {
            Q += T / 1000;
            T = T - 1000 * (T / 1000);
        }
        if (Q >= 1)
        {
            if (T < 10)
                result = Q + ".0" + T + "Q";
            else
                result = Q + "." + T + "Q";
        }
        if (Q >= 1000)
        {
            QT += Q / 1000;
            Q = Q - 1000 * (Q / 1000);
        }
        if (QT >= 1)
        {
            if (Q < 10)
                result = QT + ".0" + Q + "QT";
            else
                result = QT + "." + Q + "QT";
        }
        if (QT >= 1000)
        {
            S += QT / 1000;
            QT = QT - 1000 * (QT / 1000);
        }
        if (S >= 1)
        {
            if (QT < 10)
                result = S + ".0" + QT + "S";
            else
                result = S + "." + QT + "S";
        }
        if (S >= 1000)
        {
            SP += S / 1000;
            S = S - 1000 * (S / 1000);
        }
        if (SP >= 1)
        {
            if (S < 10)
                result = SP + ".0" + S + "SP";
            else
                result = SP + "." + S + "SP";
        }
        if (SP >= 1000)
        {
            O += SP / 1000;
            SP = SP - (SP / 1000);
        }
        if (O >= 1)
        {
            if (SP < 10)
                result = O + ".0" + SP + "O";
            else
                result = O + "." + SP + "O";
        }
        if (O >= 1000)
        {
            N += O / 1000;
            O = O - 1000 * (O / 1000);
        }
        if (N >= 1)
        {
            if (O < 10)
                result = N + ".0" + O + "N";
            else
                result = N + "." + O + "N";
        }
        if (N >= 1000)
        {
            D += N / 1000;
            N = N - 1000 * (N / 1000);
        }
        if (D >= 1)
        {
            if (N < 10)
                result = D + ".0" + N + "D";
            else
                result = D + "." + N + "D";
        }

        return result;
    }
    private void WriteFullScore()
    {
        string result = "0";
        if (underK > 0)
            result = underK.ToString();
        if (K > 0 || M > 0 || B > 0 || T > 0 || Q > 0 || QT > 0 || S > 0 || SP > 0 || O > 0 || N > 0 || D > 0)
        {
            if (underK < 10)
                result = K + "00" + underK;
            else if(underK < 100)
                result = K + "0" + underK;
            else
                result = K + "" + underK;
        }
        if (M > 0 || B > 0 || T > 0 || Q > 0 || QT > 0 || S > 0 || SP > 0 || O > 0 || N > 0 || D > 0)
        {
            if (K < 10)
                result = M + "00" + result;
            else if (K < 100)
                result = M + "0" + result;
            else
                result = M + "" + result;
        }
        if (B > 0 || T > 0 || Q > 0 || QT > 0 || S > 0 || SP > 0 || O > 0 || N > 0 || D > 0)
        {
            if (M < 10)
                result = B + "00" + result;
            else if (M < 100)
                result = B + "0" + result;
            else
                result = B + "" + result;
        }
        if (T > 0 || Q > 0 || QT > 0 || S > 0 || SP > 0 || O > 0 || N > 0 || D > 0)
        {
            if (B < 10)
                result = T + "00" + result;
            else if (B < 100)
                result = T + "0" + result;
            else
                result = T + "" + result;
        }
        if (Q > 0 || QT > 0 || S > 0 || SP > 0 || O > 0 || N > 0 || D > 0)
        {
            if (T < 10)
                result = Q + "00" + result;
            else if (T < 100)
                result = Q + "0" + result;
            else
                result = Q + "" + result;
        }
        if (QT > 0 || S > 0 || SP > 0 || O > 0 || N > 0 || D > 0)
        {
            if (Q < 10)
                result = QT + "00" + result;
            else if (Q < 100)
                result = QT + "0" + result;
            else
                result = QT + "" + result;
        }
        if (S > 0 || SP > 0 || O > 0 || N > 0 || D > 0)
        {
            if (QT < 10)
                result = S + "00" + result;
            else if (QT < 100)
                result = S + "0" + result;
            else
                result = S + "" + result;
        }
        if (SP > 0 || O > 0 || N > 0 || D > 0)
        {
            if (S < 10)
                result = SP + "00" + result;
            else if (S < 100)
                result = SP + "0" + result;
            else
                result = SP + "" + result;
        }
        if (O > 0 || N > 0 || D > 0)
        {
            if (SP < 10)
                result = O + "00" + result;
            else if (SP < 100)
                result = O + "0" + result;
            else
                result = O + "" + result;
        }
        if (N > 0 || D > 0)
        {
            if (O < 10)
                result = N + "00" + result;
            else if (O < 100)
                result = N + "0" + result;
            else
                result = N + "" + result;
        }
        if (D > 0)
        {
            if (N < 10)
                result = D + "00" + result;
            else if (N < 100)
                result = D + "0" + result;
            else
                result = D + "" + result;
        }
        full_score_text.text = result;
    }
    private void SaveGame()
    {
        if (!save) return;

        if (gameDB == null)
            gameDB = new GameDB();
        gameDB.score = new Score
        {
            underK = underK,
            k = K,
            m = M,
            b = B,
            t = T,
            q = Q,
            qt = QT,
            s = S,
            sp = SP,
            o = O,
            n = N,
            d = D
        };

        EasyJson.SaveJsonToFile(gameDB);

        SaveToFirebase();
    }
    private void SaveToFirebase()
    {
        if (ReferenceKeeper.Instance.FirebaseService)
            ReferenceKeeper.Instance.FirebaseService.SetGameDBAsync(gameDB);
    }
    private void LoadGame()
    {
        gameDB = EasyJson.GetJsonToFile<GameDB>();
        if (gameDB != null)
        {
            Score score = gameDB.score;
            underK = score.underK;
            K = score.k;
            M = score.m;
            B = score.b;
            T = score.t;
            Q = score.q;
            QT = score.qt;
            S = score.s;
            SP = score.sp;
            O = score.o;
            N = score.n;
            D = score.d;

            score_text.text = ScoreCalculate(0);
            ReferenceKeeper.Instance.RosetteController.LoadRosettes(K, M, B);
        }
    }
    private void HandleActivated()
    {
        ReferenceKeeper.Instance.ClickButton.raycastTarget = false;
        Color c = ReferenceKeeper.Instance.ClickButton.color;
        c.a = 0.5F;
        ReferenceKeeper.Instance.ClickButton.color = c;
        ReferenceKeeper.Instance.BottomCollider.SetActive(false);
    }
    private void HandleDeactivated()
    {
        ReferenceKeeper.Instance.ClickButton.raycastTarget = true;
        Color c = ReferenceKeeper.Instance.ClickButton.color;
        c.a = 1.0F;
        ReferenceKeeper.Instance.ClickButton.color = c;
        ReferenceKeeper.Instance.BottomCollider.SetActive(true);
    }

    /*Ekstra score basamagi eklenmek istenirse degisecek olan yerler:
     * ScoreCalculate()
     * RemoveScore()
     * HaveScore()
     * WriteFullScore()
     * SaveGame()
     * LoadGame()
    */
}
