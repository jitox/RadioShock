using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CTileMap
{
    private static CTileMap mInst = null;

    public int MAP_WIDTH;
    public int MAP_HEIGHT;

    //public const int TILE_WIDTH = 48;
    //public const int TILE_HEIGHT = 48;
    public const int TILE_WIDTH = 70;
    public const int TILE_HEIGHT = 70;

    public int WORLD_WIDTH;
    public int WORLD_HEIGHT;

    List<List<CTile>> mMap;
    public static int[][] LEVEL;

    // Cantidad de tiles que hay.
    private const int NUM_TILES = 16;

    public Color actualColor;

    // Array con los sprites de los tiles.
    private Sprite[] mTiles;


    public CTileMap(int aLevel)
	{
		registerSingleton ();
        actualColor = Color.red;
        LEVEL = CMapLevels.getMapLevel(aLevel);


        MAP_HEIGHT = LEVEL.Length;
        MAP_WIDTH = LEVEL[0].Length;
        WORLD_WIDTH = MAP_WIDTH * TILE_WIDTH;
        WORLD_HEIGHT = MAP_HEIGHT * TILE_HEIGHT;

    mTiles = new Sprite [NUM_TILES];
		mTiles [0] = Resources.Load<Sprite> ("Sprites/tiles/tile000");
		mTiles [1] = Resources.Load<Sprite> ("Sprites/tiles/tile001");
		mTiles [2] = Resources.Load<Sprite> ("Sprites/tiles/tile002");
		mTiles [3] = Resources.Load<Sprite> ("Sprites/tiles/tile003");
		mTiles [4] = Resources.Load<Sprite> ("Sprites/tiles/tile004");
		mTiles [5] = Resources.Load<Sprite> ("Sprites/tiles/tile005");
        mTiles [6] = Resources.Load<Sprite> ("Sprites/tiles/tile006");
        mTiles [7] = Resources.Load<Sprite> ("Sprites/tiles/tile007");
        mTiles [8] = Resources.Load<Sprite> ("Sprites/tiles/tile008");
        mTiles [9] = Resources.Load<Sprite> ("Sprites/tiles/tile009");
        mTiles [10] = Resources.Load<Sprite> ("Sprites/tiles/tile010");
        mTiles [11] = Resources.Load<Sprite> ("Sprites/tiles/tile011");
        mTiles [12] = Resources.Load<Sprite> ("Sprites/tiles/tile012");
        mTiles[13] = Resources.Load<Sprite>("Sprites/tiles/tile013");
        mTiles[14] = Resources.Load<Sprite>("Sprites/tiles/tile014");
        mTiles[15] = Resources.Load<Sprite>("Sprites/tiles/tile015");

        loadLevel();
        
    

    }

	public static CTileMap inst()
	{
		return mInst;
	}

	private void registerSingleton()
	{
		if (mInst == null) 
		{
			mInst = this;
		}
		else 
		{
			throw new UnityException( "ERROR: Cannot create another instance of singleton class CTileMap.");
		}
	}

	public void loadLevel()
	{
		mMap = new List<List<CTile>> ();

		for (int y = 0; y < MAP_HEIGHT; y++) 
		{
			mMap.Add (new List<CTile> ());			

			for (int x = 0; x < MAP_WIDTH; x++) 
			{

                int index = LEVEL[y][x];
                
				CTile tile = new CTile(x * TILE_WIDTH, y * TILE_HEIGHT, index, mTiles[index]);
				mMap [y].Add (tile);
			}
		}
	}

	public void update()
	{
	}

	public void render()
	{
		for (int y = 0; y < MAP_HEIGHT; y++) 
		{
			for (int x = 0; x < MAP_WIDTH; x++) 
			{
                CCamera cam = CGame.inst().getCamera();
                float camMin = cam.getX() - CCamera.WIDTH;
                float camMax = cam.getX() + CCamera.WIDTH *2;
                if (mMap[y][x].getX()>=camMin  && mMap[y][x].getX()<=camMax)
				mMap [y] [x].render ();
			}
		}
	}

	public void destroy()
	{
		for (int y = MAP_HEIGHT - 1; y >= 0; y--) 
		{
			for (int x = MAP_WIDTH - 1; x > 0; x--) 
			{
				mMap [y] [x].destroy ();
				mMap [y] [x] = null;
			}
			mMap.RemoveAt (y);
		}

		mMap = null;
	}

	public bool isWalkable(int aTileX, int aTileY)
	{
		if (aTileX < 0 || aTileX >= MAP_WIDTH || aTileY < 0 || aTileY >= MAP_HEIGHT)
			return true;
		
		return mMap [aTileY] [aTileX].isWalkable ();
	}

    public int getTriggerType(int aTileX, int aTileY)
    {
        if (aTileX < 0 || aTileX >= MAP_WIDTH || aTileY < 0 || aTileY >= MAP_HEIGHT)
            return 0;

        return mMap[aTileY][aTileX].getTriggerType();
    }

    public void setActive(int aTileX, int aTileY, bool aActive)
    {
        if (aTileX < 0 || aTileX >= MAP_WIDTH || aTileY < 0 || aTileY >= MAP_HEIGHT)
            return;

        mMap[aTileY][aTileX].setActive(aActive);
    }

    public bool isActive(int aTileX, int aTileY)
    {
        if (aTileX < 0 || aTileX >= MAP_WIDTH || aTileY < 0 || aTileY >= MAP_HEIGHT)
            return false;

        return mMap[aTileY][aTileX].isActive();
    }

    public bool isSpike(int aTileX, int aTileY)
    {
        if (aTileX < 0 || aTileX >= MAP_WIDTH || aTileY < 0 || aTileY >= MAP_HEIGHT)
            return false;

        return mMap[aTileY][aTileX].isSpike();
    }

    public int getTileIndex(int aTileX, int aTileY)
	{
		if (aTileX < 0 || aTileX >= MAP_WIDTH || aTileY < 0 || aTileY >= MAP_HEIGHT)
			return 0;
		
		return mMap [aTileY] [aTileX].getTileIndex();
	}

    public  void renderAllMap()
    {
        for (int y = 0; y < MAP_HEIGHT; y++)
        {
            for (int x = 0; x < MAP_WIDTH; x++)
            {
                mMap[y][x].update();
                mMap[y][x].render();
            }
        }

    }
    public void ChangeColor()
    {
        
        if (actualColor == Color.red)
        {
            actualColor = Color.magenta;
        }
        else if (actualColor == Color.magenta)
        {
            actualColor = Color.cyan;
        }
        else if (actualColor == Color.cyan)
        {
            actualColor = Color.red;
        }

        
    }
}
