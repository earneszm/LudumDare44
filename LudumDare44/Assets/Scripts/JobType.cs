using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JobType")]
public class JobType : ScriptableObject
{    
    public JobDifficulty difficulty;
    public Sprite sprite;
}
