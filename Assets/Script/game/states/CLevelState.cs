﻿using UnityEngine;
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

    //List<List<int>> mVisibleTiles;

    //private int mTileCameraStartX;
    //private int mTileCameraEndX;
    //private int mTileCameraStartY;
    //private int mTileCameraEndY;

    //private int mOldTileCameraStartX;
    //private int mOldTileCameraStartY;

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
        mButtonHome.setVisible(false);

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
