using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StockType")]
public class StockType : ScriptableObject
{
    public float MinStartingValue;
    public float MaxStartingValue;
    public StockRiskyness Riskyness;

}
