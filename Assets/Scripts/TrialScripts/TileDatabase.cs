using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Tile Database" , menuName = " Farming/TileDatabase")]
public class TileDatabase : ScriptableObject
{
    [System.Serializable]
    public class TileTransition
    {
        public TileBase dryTile;
        public TileBase wetTile;
    }

    public List<TileTransition> tileTransitions;

    public TileBase GetWetTile(TileBase currentDryTile)
    {
        foreach (var transition in tileTransitions)
        {
            if (transition.dryTile == currentDryTile)
            {
                return transition.wetTile;
            }
        }

        return null;
    }
}
