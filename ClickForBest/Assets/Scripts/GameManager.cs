using System;
using UnityEngine;
using TMPro;
using System.Globalization;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text score_text;
    [SerializeField] TMP_Text full_score_text;
    public GameDB gameDB { get; private set; }

    private int boost = 1;
    private int total_coin_count;

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
        SaveGame();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
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

        //Get score and level on the PlayerPrefs or Firebase
        //score_text.text = score.ToKMB();
        //full_score_text.text = score.ToString();

        ReferenceKeeper.Instance.Handle.onActive.AddListener(HandleActivated);
        ReferenceKeeper.Instance.Handle.onDeactive.AddListener(HandleDeactivated);
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

        score_text.text = ScoreCalculate(boost);
    }
    [EasyButtons.Button]
    public void AddScore(int _value)
    {
        score_text.text = ScoreCalculate(_value);
    }
    public void SetBoost(int _value)
    {
        boost = _value;
    }
    public void RemoveScore(int _underK, int _k)
    {
        underK -= _underK;
        if (underK < 0)
        {
            underK = 1000 - underK;
        }
        K -= _k;
        score_text.text = ScoreCalculate(0);
    }
    public bool HaveScore(int _underK, int _k)
    {
        if (K > _k)
        {
            return true;
        }
        else if(K == _k)
        {
            if (underK >= _underK)
            {
                return true;
            }
        }
        return false;
    }
    private string ScoreCalculate(int _value)
    {
        string result = "";
        underK += _value;
        result = underK.ToString();
        if (underK > 999)
        {
            K += underK / 999;
            underK = underK - 1000;
        }
        if (K >= 1)
            result = K + "." + underK + "K";
        if (K > 999)
        {
            M += K / 999;
            K = 0;
        }
        if (M >= 1)
            result = M + "." + K + "M";
        if (M > 999)
        {
            B += M / 999;
            M = 0;
        }
        if (B >= 1)
            result = B + "." + M + "B";
        if (B > 999)
        {
            T += B / 999;
            B = 0;
        }
        if (T >= 1)
            result = T + "." + B + "T";
        if (T > 999)
        {
            Q += T / 999;
            Q = 0;
        }
        if (Q >= 1)
            result = Q + "." + T + "Q";
        if (Q > 999)
        {
            QT += Q / 999;
            Q = 0;
        }
        if (QT >= 1)
            result = QT + "." + Q + "QT";
        if (QT > 999)
        {
            S += QT / 999;
            QT = 0;
        }
        if (S >= 1)
            result = S + "." + QT + "S";
        if (S > 999)
        {
            SP += S / 999;
            S = 0;
        }
        if (SP >= 1)
            result = SP + "." + S + "SP";
        if (SP > 999)
        {
            O += SP / 999;
            SP = 0;
        }
        if (O >= 1)
            result = O + "." + SP + "O";
        if (O > 999)
        {
            N += O / 999;
            O = 0;
        }
        if (N >= 1)
            result = N + "." + O + "N";
        if (N > 999)
        {
            D += N / 999;
            N = 0;
        }
        if (D >= 1)
            result = D + "." + N + "D";

        return result;
    }
    private void SaveGame()
    {
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
        if(ReferenceKeeper.Instance.FirebaseService)
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
}
