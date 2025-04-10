using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITile
{
    public void OnChoose();
    public void OnChangeTile();
    public void OnRelease();
    public void OnMatch();
}
