using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllColorTile : Tile
{
    public override void OnChangeTile()
    {
        //Do Nothing
    }

    public override void OnChoose()
    {
        EventContainer.OnchooseTile?.Invoke();
        EventContainer.OnSelectedTile?.Invoke(this);
        LineRenderer.enabled = true;
        for (int i = 0; i < LineRenderer.positionCount; i++)
        {
            var index = i;
            LineRenderer.SetPosition(index, transform.position);

        }
    }

    public override void OnCreateTile(int column, int row)
    {
        this.Column = column;
        this.Row = row;
        IsDestroyed = false;
        type = TileType.SpecialAllColor;
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
        transform.localScale = Vector3.one * 1.25f;
    }

    public override void OnRelease()
    {
        LineRenderer.enabled = false;
        //Debug.Log($"this tile releases");
        transform.localScale = Vector3.one;
        for (int i = 0; i < LineRenderer.positionCount; i++)
        {
            var index = i;
            LineRenderer.SetPosition(index, transform.position);
        }
    }

}
