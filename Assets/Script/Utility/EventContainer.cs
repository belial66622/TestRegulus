using System;

public static class EventContainer
{
    public static Action OnchooseTile;
    public static Action OnReleaseTile;
    public static Action<Tile> OnSelectedTile;
    public static Action OnLastTileDestroyed;
    public static Action<int,int> OnBombClicked;
    public static Action OnHighlightClicked;
}
