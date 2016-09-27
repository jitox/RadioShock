using UnityEngine;
using System.Collections;

public class CLevel2State : CGameState
{
	//private CPlayer mPlayer;
	private CPlayerNew mPlayer;

	private CBulletManager mBulletManager;
	private CEnemyManager mEnemyManager;
    private CTriggerManager mTriggerManager;
    private CParticleManager mParticleManager;

	private CSprite mBackground;

	private CTileMap mMap;

	private CCamera mCamera;

	public CLevel2State()
	{
		mPlayer = new CPlayerNew ();
	    CGame.inst ().setPlayer (mPlayer);

		//mPlayer = new CShip ();
		//CGame.inst ().setShip (mPlayer);

		mBulletManager = new CBulletManager ();
		mEnemyManager = new CEnemyManager ();
        mTriggerManager = new CTriggerManager();
        mParticleManager = new CParticleManager();

        mMap = new CTileMap(1);

		mCamera = new CCamera ();
		mCamera.setXY (0, 0);
		mCamera.setVelX (30);
		CGame.inst ().setCamera (mCamera);
		mCamera.setGameObjectToFollow (mPlayer);
	}

	override public void init()
	{
        Debug.Log("Level2");
		base.init ();

		//mBackground = new CSprite ();
		//mBackground.setImage (Resources.Load<Sprite> ("Sprites/game_background"));
		//mBackground.setXY (0, 0);
		//mBackground.setSortingLayerName ("Background");
		//mBackground.setName ("background");

		createAsteroids ();
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

		//mBackground.update ();
		mPlayer.update ();
		mBulletManager.update ();
		mEnemyManager.update ();
		mMap.update ();
        mParticleManager.update();
		mCamera.update ();

        if (mPlayer.getWin())
        {
            CGame.inst().setState(new CWinState());
            return;
        }
	}

	override public void render()
	{
		base.render ();

		//mBackground.render ();
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

		//mBackground.destroy ();
		//mBackground = null;
		mPlayer.destroy ();
		mPlayer = null;
		mBulletManager.destroy ();
		mBulletManager = null;
		mEnemyManager.destroy ();
		mEnemyManager = null;
        mTriggerManager.destroy();
        mTriggerManager = null;
        mParticleManager.destroy();
        mParticleManager = null;

		mMap.destroy ();
		mMap = null;
		mCamera.destroy ();
		mCamera = null;
	}

	private void createAsteroids()
	{
		/*CAsteroid a;

		for (int i = 0; i < 3; i++) 
		{
			a = new CAsteroid(CAsteroid.ASTEROID_BIG);
			a.setX(CMath.randomIntBetween(a.getRadius(), CTileMap.WORLD_WIDTH - a.getRadius()));
			a.setY (CMath.randomIntBetween(a.getRadius(), CTileMap.WORLD_HEIGHT - a.getRadius()));
			a.setVelX(CMath.randomIntBetween(-300, 300));
			a.setVelY(0);
			a.setAccelX(0);
			a.setAccelY(200);
			CEnemyManager.inst().add(a);
		}*/
	}

	private void createCannons()
	{
		CCannon c;

		c = new CCannon (3, 3);
		CEnemyManager.inst().add(c);

		c = new CCannon (11, 3);
		CEnemyManager.inst().add(c);

		c = new CCannon (16, 6);
		CEnemyManager.inst().add(c);
	}

    public void passAllMap()
    {
        mMap.renderAllMap();
    }
}
