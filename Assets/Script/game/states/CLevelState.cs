using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CLevelState : CGameState
{
	//private CPlayer mPlayer;
	private CPlayerNew mPlayer;

	private CBulletManager mBulletManager;
	private CEnemyManager mEnemyManager;
    public CBackgroundcs mBackground;

	private CTileMap mMap;

	private CCamera mCamera;
    private  int currentLvl;

    private CButtonSprite mButtonHome;

    List<List<int>> mVisibleTiles;

    private int mTileCameraStartX;
    private int mTileCameraEndX;
    private int mTileCameraStartY;
    private int mTileCameraEndY;

    private int mOldTileCameraStartX;
    private int mOldTileCameraStartY;

    public CLevelState(int aLevel)
	{
		mPlayer = new CPlayerNew ();
	    CGame.inst ().setPlayer (mPlayer);
        currentLvl = aLevel;
		//mPlayer = new CShip ();
		//CGame.inst ().setShip (mPlayer);

		mBulletManager = new CBulletManager ();
		mEnemyManager = new CEnemyManager ();
      
       

        mMap = new CTileMap(currentLvl);

		mCamera = new CCamera ();
		mCamera.setXY (0, 0);
		mCamera.setVelX (30);
		CGame.inst ().setCamera (mCamera);
		mCamera.setGameObjectToFollow (mPlayer);

        mBackground = new CBackgroundcs();

        mVisibleTiles = new List<List<int>>();
        mVisibleTiles.Add(new List<int>());

        mOldTileCameraStartY = -1;
        mOldTileCameraStartX = -1;

    }

	override public void init()
	{
		base.init ();

        //mBackground = new CSprite ();
        //mBackground.setImage (Resources.Load<Sprite> ("Sprites/game_background"));
        //mBackground.setXY (0, 0);
        //mBackground.setSortingLayerName ("Background");
        //mBackground.setName ("background");

        mButtonHome = new CButtonSprite();
        mButtonHome.setFrames(Resources.LoadAll<Sprite>("Sprites/ui/button/InGame"));
        mButtonHome.gotoAndStop(1);
        mButtonHome.setXY(CGameConstants.SCREEN_WIDTH, 20);
        mButtonHome.setWidth(100);
        mButtonHome.setHeight(100);
        mButtonHome.setSortingLayerName("UI");
        mButtonHome.setName("button Home");
        mButtonHome.setVisible(true);

        //eateCannons ();
    }

	override public void update()
	{
		base.update ();

		if (CKeyboard.firstPress (CKeyboard.ESCAPE)) 
		{
			CGame.inst().setState(new CMainMenuState());
			return;
		}

        mButtonHome.update();

        mButtonHome.setXY(CGame.inst().getCamera().getX() + CGameConstants.SCREEN_WIDTH, 20);

        mBackground.update ();
		mPlayer.update ();
		mBulletManager.update ();
		mEnemyManager.update ();
		mMap.update ();
        CParticleManager.inst().update();
        
		mCamera.update ();


        if (mButtonHome.clicked())
        {
            CGame.inst().setState(new CMainMenuState());
            return;
        }

        if (mPlayer.getWin())
        {
            CGame.inst().setState(new CWinState(currentLvl));            
            return;
        }

        //Know if player is in state DIE.
        if (mPlayer.getState() == 4)
        {
            mOldTileCameraStartX = -1;
        }

        mTileCameraStartX = (int)mCamera.getX() / CTileMap.TILE_WIDTH;
        mTileCameraEndX = ((int)mCamera.getX() + CCamera.WIDTH) / CTileMap.TILE_WIDTH;

        mTileCameraStartY = (int)mCamera.getY() / CTileMap.TILE_HEIGHT;
        mTileCameraEndY = ((int)mCamera.getY() + CCamera.HEIGHT) / CTileMap.TILE_HEIGHT;


        if (mTileCameraStartX <= 0)
        {
            mTileCameraStartX = 0;
        }
        if (mTileCameraEndX >= CTileMap.inst().MAP_WIDTH)
        {
            mTileCameraEndX = CTileMap.inst().MAP_WIDTH;
        }

        if (mTileCameraStartY <= 0)
        {
            mTileCameraStartY = 0;
        }
        if (mTileCameraEndY >= CTileMap.inst().MAP_HEIGHT)
        {
            mTileCameraEndY = CTileMap.inst().MAP_HEIGHT;
        }


        if (mTileCameraStartX != mOldTileCameraStartX || mTileCameraStartY != mOldTileCameraStartY)
        {

            for (int i = 0; i < mVisibleTiles.Count; i++)
            {
                //mVisibleTiles[i][1] --> x
                //mVisibleTiles[i][0] --> y


                if (mVisibleTiles[i].Count != 0)
                {

                    if ((mVisibleTiles[i][1] < mTileCameraStartX && !(mVisibleTiles[i][1] > mTileCameraEndX + 1))|| (mVisibleTiles[i][1] > mTileCameraEndX + 1 && !(mVisibleTiles[i][1] < mTileCameraStartX)))
                    {
                        mMap.getMap()[mVisibleTiles[i][0]][mVisibleTiles[i][1]].setVisible(false);
                        mVisibleTiles.Remove(new List<int>() { mVisibleTiles[i][0], mVisibleTiles[i][1] });
                    }

                }

            }


            for (int x = mTileCameraStartX; x <= mTileCameraEndX + 1; x++)
            {
                for (int y = mTileCameraStartY; y <= mTileCameraEndY + 1; y++)
                {
                    if (y < mMap.getMap().Count)
                    {
                        if (x < mMap.getMap()[y].Count)
                        {
                            if (!mMap.getMap()[y][x].isVisible())
                            {

                                mMap.getMap()[y][x].setVisible(true);
                                mVisibleTiles.Add(new List<int>() { y, x });


                            }
                        }

                    }

                }
            }
        }





        mOldTileCameraStartX = mTileCameraStartX;
        mOldTileCameraStartY = mTileCameraStartY;
    }

    override public void render()
	{
		base.render ();

		mBackground.render ();
		mPlayer.render ();
		mBulletManager.render ();
		mEnemyManager.render ();
        
        
        CParticleManager.inst().render();
        mMap.render ();

        mButtonHome.render();

		mCamera.render ();
	}

	// TODO: Al apretar Escape da error.
	override public void destroy()
	{
		base.destroy ();

		mBackground.destroy ();
		mBackground = null;
		mPlayer.destroy ();
		mPlayer = null;
		mBulletManager.destroy ();
		mBulletManager = null;
		mEnemyManager.destroy ();
		mEnemyManager = null;
        CTriggerManager.inst().mArray.Clear();
        CParticleManager.inst().mArray.Clear();
        mVisibleTiles = null;

        mButtonHome.destroy();
        mButtonHome = null;

		mMap.destroy ();
		mMap = null;
		//mCamera.destroy ();
		//mCamera = null;
	}



	

    override public void passAllMap()
    {
        mMap.renderAllMap();
    }
}
