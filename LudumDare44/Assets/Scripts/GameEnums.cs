using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InvestmentType
{
    [Description("Individual Stocks")]
    IndividualStocks,
    [Description("Index Fund")]
    IndexFund,
    Bonds,
    [Description("Real Estate")]
    RealEstate,
    [Description("Savings Account")]
    SavingsAccount
}

public enum StockRiskyness
{
    [VolatilityDetail(.5f, 80f, 20, 50, 5, 10)]
    Safe,
    [VolatilityDetail(1f, 80, 20, 50, 5, 15)]
    Normal,
    [VolatilityDetail(3f, 80, 25, 60, 7, 20)]
    Growth,
    [VolatilityDetail(4f, 70f, 35, 75, 10, 24)]
    Volatile,
    [VolatilityDetail(6f, 70f, 40, 80, 20, 35)]
    Excessive,
    [VolatilityDetail(8f, 80f, 50, 90, 24, 40)]
    Crypto
}

public enum JobStage
{
    Closed,
    Open
}

public enum JobDifficulty
{
    Easy = 8,
    Normal = 14,
    Hard = 20,
    Insane = 30
}
