using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class NormalTile : Tile, ITileLineRenderer
{
    string[] colorCollection = new string[5] { "#FF0000", "#FFFF00", "#00FF00", "#0000FF", "#FF00FF" };


    public override void OnChangeTile()
    {
        ChangeColor();
    }

    public override void OnCreateTile(int column, int row)
    {
        ChangeColor(true);
        this.Column = column;
        this.Row = row;
        IsDestroyed = false;
        type = TileType.Normal;
        LineRenderer.enabled = false;
    }

    public override void OnChoose()
    {
        //Debug.Log($"selected{Index} and color {Color.ToString()}");
        EventContainer.OnchooseTile?.Invoke();
        EventContainer.OnSelectedTile?.Invoke(this);
        LineRenderer.enabled = true;
        for (int i = 0; i < LineRenderer.positionCount; i++)
        {
            var index = i;
            LineRenderer.SetPosition(index, transform.position);

        }
    }

    void ChangeColor(bool firstTime = false )
    {
        if (firstTime)
        {
            Color = (TileColor)TakeColor(Enum.GetNames(typeof(TileColor)).Length - 1);
        }
        else
        {
            Color = (TileColor)TakeColor((int)Color);
        }
        ColorUtility.TryParseHtmlString(colorCollection[(int)Color], out Color colorTemp);
        sprite.color = colorTemp;
    }

    int TakeColor(int number = 0)
    {
        int max = Enum.GetNames(typeof(TileColor)).Length - 1;
        int random = Utility.random(0, max);
        int choosenColor = random == number ? TakeColor(random) : random ; 

            return choosenColor;
    }

    public override void OnDestroyTile()
    {
        LineRenderer.enabled = false;
        IsDestroyed = true;
        DestroyTile();
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

    public override void OnMatch()
    {
        transform.localScale = Vector3.one * 1.25f;
    }

}
