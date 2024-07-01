using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopData : IObtainedItemsData
{
    static float buyersRating { get; set; } = 1f;
    static float sellersRating { get; set; } = 1f;
}
