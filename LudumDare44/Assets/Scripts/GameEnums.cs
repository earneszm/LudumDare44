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
    [VolatilityDetail(.5f, 80f, 20, 50, 1, 4)]
    Safe,
    [VolatilityDetail(1f, 75f, 20, 50, 2, 6)]
    Normal,
    [VolatilityDetail(3f, 65f, 20, 55, 3, 8)]
    Growth,
    [VolatilityDetail(4f, 65f, 25, 65, 5, 12)]
    Volatile,
    [VolatilityDetail(6f, 70f, 30, 75, 15, 24)]
    Excessive,
    [VolatilityDetail(8f, 80f, 35, 90, 18, 30)]
    Crypto
}

public enum JobStage
{
    Closed,
    Open
}
