using UnityEngine;
using System.Collections;

public class CTile : CSprite
{
	// Tile index. Starting from 0.
	private int mTileIndex;

	private bool mIsWalkable;
    private bool mIsSpike;

	// Parametros: coordenada del tile (x, y) y el indice del tile.
	public CTile(int aX, int aY, int aTileIndex, Sprite aSprite)
	{
		setXY (aX, aY);
		setTileIndex(aTileIndex);

		setImage (aSprite);
		setSortingLayerName ("TileMap");
	}

	public void setTileIndex(int aTileIndex)
	{
		mTileIndex = aTileIndex;

		if (aTileIndex == 0) 
		{
			mIsWalkable = true;
		} 
		else 
		{
			mIsWalkable = false;
		}
        if (aTileIndex == 6 || aTileIndex == 7)
        {
            mIsSpike = true;
        }
	}

	public int getTileIndex()
	{
		return mTileIndex;
	}

	override public void render()
	{
		base.render ();
	}

	override public void update()
	{
		base.update ();
	}

	override public void destroy()
	{
	}

	public bool isWalkable()
	{
		return mIsWalkable;
	}

	public void setWalkable(bool aIsWalkable)
	{
		mIsWalkable = aIsWalkable;
	}
    public bool isSpike()
    {
        return mIsSpike;
    }

    public void setSpike(bool aIsSpike)
    {
        mIsSpike = aIsSpike;
    }
}
