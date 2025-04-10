using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTile : Tile
{
    public override void OnChangeTile()
    {
        //do Nothing
    }

    public override void OnChoose()
    {
        EventContainer.OnBombClicked?.Invoke(Column,Row);
    }

    public override void OnCreateTile(int column, int row)
    {
        this.Column = column;
        this.Row = row;
        IsDestroyed = false;
        type = TileType.SpecialBomb;
        LineRenderer.enabled = false;
        Color = TileColor.NoColor;
    }

    public override void OnDestroyTile()
    {
        LineRenderer.enabled = false;
        IsDestroyed = true;
        DestroyTile();
    }

    public override void OnMatch()
    {
       //do Nothing
    }

    public override void OnRelease()
    {
        //Do Nothing
    }
}
