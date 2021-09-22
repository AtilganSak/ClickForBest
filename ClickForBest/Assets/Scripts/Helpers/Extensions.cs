using System;
using System.Collections.Generic;
using System.Globalization;

public static class Extensions
{
    public static string ToKMB(this int num)
    {
        if (num > 999999999 || num < -999999999)
        {
            return num.ToString("0,,,.###B", CultureInfo.InvariantCulture);
        }
        else
        if (num > 999999 || num < -999999)
        {
            return num.ToString("0,,.###M", CultureInfo.InvariantCulture);
        }
        else
        if (num > 999 || num < -999)
        {
            return num.ToString("0,.##K", CultureInfo.InvariantCulture);
        }
        else
        {
            return num.ToString(CultureInfo.InvariantCulture);
        }
    }
    public static void Shuffle<T>(this IList<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    public static string Calculate(this Score _score, int _value)
    {
        string result = "";
        _score.underK += _value;
        result = _score.underK.ToString();
        if (_score.underK > 999)
        {
            _score.k += _score.underK / 999;
            _score.underK = 0;
        }
        if (_score.k >= 1)
            result = _score.k + "." + _score.underK + "K";
        if (_score.k > 999)
        {
            _score.m += _score.k / 999;
            _score.k = 0;
        }
        if (_score.m >= 1)
            result = _score.m + "." + _score.k + "M";
        if (_score.m > 999)
        {
            _score.b += _score.m / 999;
            _score.m = 0;
        }
        if (_score.b >= 1)
            result = _score.b + "." + _score.m + "B";
        if (_score.b > 999)
        {
            _score.t += _score.b / 999;
            _score.b = 0;
        }
        if (_score.t >= 1)
            result = _score.t + "." + _score.b + "T";
        if (_score.t > 999)
        {
            _score.q += _score.t / 999;
            _score.q = 0;
        }
        if (_score.q >= 1)
            result = _score.q + "." + _score.t + "Q";
        if (_score.q > 999)
        {
            _score.qt += _score.q / 999;
            _score.q = 0;
        }
        if (_score.qt >= 1)
            result = _score.qt + "." + _score.q + "QT";
        if (_score.qt > 999)
        {
            _score.s += _score.qt / 999;
            _score.qt = 0;
        }
        if (_score.s >= 1)
            result = _score.s + "." + _score.qt + "S";
        if (_score.s > 999)
        {
            _score.sp += _score.s / 999;
            _score.s = 0;
        }
        if (_score.sp >= 1)
            result = _score.sp + "." + _score.s + "SP";
        if (_score.sp > 999)
        {
            _score.o += _score.sp / 999;
            _score.sp = 0;
        }
        if (_score.o >= 1)
            result = _score.o + "." + _score.sp + "O";
        if (_score.o > 999)
        {
            _score.n += _score.o / 999;
            _score.o = 0;
        }
        if (_score.n >= 1)
            result = _score.n + "." + _score.o + "N";
        if (_score.n > 999)
        {
            _score.d += _score.n / 999;
            _score.n = 0;
        }
        if (_score.d >= 1)
            result = _score.d + "." + _score.n + "D";

        return result;
    }
}