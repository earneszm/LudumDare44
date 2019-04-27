using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class GameUtils
{
    public static string GetDescription(Enum en)
    {

        Type type = en.GetType();

        MemberInfo[] memInfo = type.GetMember(en.ToString());

        if (memInfo != null && memInfo.Length > 0)
        {
            object[] attrs = memInfo[0].GetCustomAttributes(typeof(Description), false);

            if (attrs != null && attrs.Length > 0)
                return ((Description)attrs[0]).Text;
        }

        return en.ToString();
    }

    public static VolatilityDetail GetVolatility(Enum en)
    {
        Type type = en.GetType();

        MemberInfo[] memInfo = type.GetMember(en.ToString());

        if (memInfo != null && memInfo.Length > 0)
        {
            object[] attrs = memInfo[0].GetCustomAttributes(typeof(VolatilityDetail), false);

            if (attrs != null && attrs.Length > 0)
                return ((VolatilityDetail)attrs[0]);
        }

        return null;
    }

    public static string ToDescription(this Enum en)
    {
        return GetDescription(en);
    }

    public static string GetMonthFromInteger(int num)
    {
        switch (num)
        {
            case 1: return "January";
            case 2: return "February";
            case 3: return "March";
            case 4: return "April";
            case 5: return "May";
            case 6: return "June";
            case 7: return "July";
            case 8: return "August";
            case 9: return "September";
            case 10: return "October";
            case 11: return "November";
            case 12: return "December";
        }

        return "";
    }

    public static Tuple<bool, float> GetStockChangePercent(VolatilityDetail detail)
    {
        bool isCrashing = UnityEngine.Random.Range(1f, 100f) < detail.chanceOfCrash;

        if(isCrashing)
            return Tuple.Create(true,  -1 * UnityEngine.Random.Range(detail.minCrashPercent, detail.maxCrashPercent));

        float directionModifier = UnityEngine.Random.Range(1f, 100f) < detail.chanceOfIncrease ? 1 : -1;

        return Tuple.Create(false, directionModifier * UnityEngine.Random.Range(detail.minIncrease, detail.maxIncrease));
    }
}

public class Description : Attribute
{
    public string Text;

    public Description(string text)
    {
        Text = text;
    }
}

public class VolatilityDetail : Attribute
{
    public float chanceOfCrash;
    public float minCrashPercent;
    public float maxCrashPercent;

    public float chanceOfIncrease;
    public float minIncrease;
    public float maxIncrease;

    public VolatilityDetail(float chanceOfCrash, float chanceOfIncrease, float minCrashPercent = 20f, float maxCrashPercent = 50f, float minIncrease = 1f, float maxIncrease = 35f)
    {
        this.chanceOfCrash = chanceOfCrash;
        this.chanceOfIncrease = chanceOfIncrease;

        this.minCrashPercent = minCrashPercent;
        this.maxCrashPercent = maxCrashPercent;

        this.minIncrease = minIncrease;
        this.maxIncrease = maxIncrease;
    }
}
