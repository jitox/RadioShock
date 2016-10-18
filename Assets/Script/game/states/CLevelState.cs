using UnityEngine;
using System.Collections;

public class CLevelState : CGameState
{
	//private CPlayer mPlayer;
	private CPlayerNew mPlayer;

	private CBulletManager mBulletManager;
	private CEnemyManager mEnemyManager;
    private CTriggerManager mTriggerManager;
    private CParticleManager mParticleManager;

	public CBackgroundcs mBackground;

	private CTileMap mMap;

	private CCamera mCamera;
    private  int currentLvl;

    

	public CLevelState(int aLevel)
	{
		mPlayer = new CPlayerNew ();
	    CGame.inst ().setPlayer (mPlayer);
        currentLvl = aLevel;
		//mPlayer = new CShip ();
		//CGame.inst ().setShip (mPlayer);

		mBulletManager = new CBulletManager ();
		mEnemyManager = new CEnemyManager ();
        mTriggerManager = new CTriggerManager();
        mParticleManager = new CParticleManager();

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

		mBackground.update ();
		mPlayer.update ();
		mBulletManager.update ();
		mEnemyManager.update ();
		mMap.update ();
        mParticleManager.update();
		mCamera.update ();

        if (!mPlayer.getWin())
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
        mTriggerManager.render();
        mParticleManager.render();

        mMap.render ();

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
        //mTriggerManager.destroy();
        //mTriggerManager = null;
        mParticleManager.destroy();
        mParticleManager = null;

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
