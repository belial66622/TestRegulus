using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour, ITile, ITileLineRenderer
{
    protected SpriteRenderer sprite;
    public TileColor Color;
    public int Index = 0;
    [SerializeField]
    protected float duration;

    [SerializeField]
    protected int Column, Row;

    protected bool IsDestroyed;

    protected bool IsMoving = false;

    protected bool IsLastTile = false;

    public TileType type;
    public LineRenderer LineRenderer { get; protected set; }

    protected void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        LineRenderer = GetComponent<LineRenderer>();
    }


    public abstract void OnChoose();
    public abstract void OnCreateTile(int column, int row);

    public abstract void OnChangeTile();

    public abstract void OnDestroyTile();

    public abstract void OnRelease();

    public abstract void OnMatch();


    /// <summary>
    /// Return row and column
    /// </summary>
    /// <returns>Row x and column y</returns>
    public Vector2 GetRowAndColumn()
    {
        return new Vector2(Row, Column);
    }

    /// <summary>
    /// Set row and Column. Row first then column.
    /// </summary>
    public void SetRowAndColumn(int row, int column)
    {
        this.Row = row;
        this.Column = column;
    }

    public void MoveTile(Vector3 from, Vector3 to)
    {
        StartCoroutine(MoveTo(from, to));
    }

    IEnumerator MoveTo(Vector3 from, Vector3 to)
    {
        float t = 0;
        while (t < 1)
        {
            yield return null;
            t += Time.deltaTime/duration;
            transform.position = Vector3.Lerp(from, to, t);
        }
        transform.position = Vector3.Lerp(from, to, 1);
    }
    public void SetLastItem(bool condition)
    {
        IsLastTile = condition;
    }

    protected void DestroyTile()
    {
        StartCoroutine(DestroyCour());
    }

    IEnumerator DestroyCour()
    {
        float t = 0;
        while (t < 1)
        {
            yield return null;
            t += Time.deltaTime / duration;
            transform.localScale = Vector3.one * (1 - t);
        }
        if (IsLastTile)
        {
            EventContainer.OnLastTileDestroyed?.Invoke();
        }
        SetLastItem(false);
        Destroy(this.gameObject, 0.1f);
    }

    public void ActivateHighlight(bool condition)
    {
        transform.GetChild(0).gameObject.SetActive(condition);
    }
}
