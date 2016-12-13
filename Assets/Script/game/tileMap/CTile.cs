﻿using UnityEngine;
using System.Collections;

public class CTile : CAnimatedSprite
{
	// Tile index. Starting from 0.
	private int mTileIndex;

	private bool mIsWalkable;
    private bool mIsSpike;
    private int mTriggerType;
    private bool mIsActive = true;

	// Parametros: coordenada del tile (x, y) y el indice del tile.
	public CTile(int aX, int aY, int aTileIndex, Sprite aSprite)
	{
		setXY (aX, aY);
        setFrames(new Sprite[] { aSprite });
        setTileIndex(aTileIndex);

		//setImage (aSprite);
		setSortingLayerName ("TileMap");
        setName("Tile " + aTileIndex.ToString());
        
	}

	public void setTileIndex(int aTileIndex)
	{
		mTileIndex = aTileIndex;

		if (aTileIndex == 0 || aTileIndex == 12 || aTileIndex == 9) 
		{
			mIsWalkable = true;
		} 
		else 
		{
			mIsWalkable = false;
		}
        if (aTileIndex == 10 || aTileIndex == 11)
        {
            mIsSpike = true;
           // mIsWalkable = true;
        }
        if (aTileIndex == 12)
        {
            mTriggerType = 1;
            CTriggerManager.inst().add(this);
        }
        else if (aTileIndex == 9)
        {
            mTriggerType = 2;
            CTriggerManager.inst().add(this);
        }
        else
        {
            mTriggerType = 0;
        }
        switch (aTileIndex)
        {
            case 14:
                setFrames(Resources.LoadAll<Sprite>("Sprites/tiles/AnimatedTile14"));
                initAnimation(1, 3, 10, true);
                break;
            case 15:
                setFrames(Resources.LoadAll<Sprite>("Sprites/tiles/AnimatedTile15"));
                initAnimation(1, 3, 10, true);
                break;
            case 3:
                setFrames(Resources.LoadAll<Sprite>("Sprites/tiles/AnimatedTile03"));
                initAnimation(1, 3, 10, true);
                break;
            case 5:
                setFrames(Resources.LoadAll<Sprite>("Sprites/tiles/AnimatedTile05"));
                initAnimation(1, 3, 10, true);
                break;
            case 4:
                setFrames(Resources.LoadAll<Sprite>("Sprites/tiles/AnimatedTile04"));
                initAnimation(1, 3, 10, true);
                break;
            case 1:
                setFrames(Resources.LoadAll<Sprite>("Sprites/tiles/AnimatedTile01"));
                initAnimation(1, 3, 10, true);
                break;
            case 2:
                setFrames(Resources.LoadAll<Sprite>("Sprites/tiles/AnimatedTile02"));
                initAnimation(1, 3, 10, true);
                break;
            case 16:
                setFrames(Resources.LoadAll<Sprite>("Sprites/tiles/AnimatedTile16"));
                initAnimation(1, 3, 10, true);
                break;
            default:
                break;
        }
        
       
	}

	public int getTileIndex()
	{
		return mTileIndex;
	}

	override public void render()
	{
        if (getTileIndex() != 0)
        {
            base.render();
        }
        if(getTileIndex() != 11 && getTileIndex() != 10 && getTileIndex() != 12 && getTileIndex() != 9) { 
        if (getColor() != CTileMap.inst().actualColor)
        {
            setColor(CTileMap.inst().actualColor);
            //Debug.Log(" Mapa " + CTileMap.inst().actualColor.ToString());
            //Debug.Log("TILE COLOR " + getColor());
            
        }
        }



    }

	override public void update()
	{
		base.update ();
	}

	override public void destroy()
	{
        base.destroy();        
	}

	public bool isWalkable()
	{
		return mIsWalkable;
	}

    public void setWalkable(bool aIsWalkable)
	{
		mIsWalkable = aIsWalkable;
	}
    
    public int getTriggerType()
    {
        return mTriggerType;
    }

    public bool isActive()
    {
        return mIsActive;
    }

    public void setActive(bool aActive)
    {
        mIsActive = aActive;

       // Debug.Log(mIsActive);
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
