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
    Safe,
    Normal,
    Growth,
    Volatile,
    Excessive,
    Crypto
}
