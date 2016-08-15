using UnityEngine;
using System.Collections;

public class CMagnet : CAnimatedSprite
{
    private bool mIsActive = true;
    public static int MAGNET_HEIGHT = 48;
    public static int MAGNET_WIDTH = 48;

    public CMagnet(int aTileX, int aTileY)
    {
        setXY(aTileX * CTileMap.TILE_WIDTH, aTileY * CTileMap.TILE_HEIGHT);
    }

    override public void update()
    {

    }

    public void setActive(bool aActive)
    {
        mIsActive = aActive;
    }

    public bool isActive()
    {
        return mIsActive;
    }
}
