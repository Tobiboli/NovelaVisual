using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(filwName = "NewDataHolder", menuName = "Data/New Data Holder")]
[System.Serializable]

public class DataHolder : ScriptableObject
{
    public List<GameScene> scenes;

}
