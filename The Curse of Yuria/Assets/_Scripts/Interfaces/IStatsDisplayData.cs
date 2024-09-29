using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatsDisplayData
{
    static bool NeedsToBeRefreshed { get; set; } = false;
}
