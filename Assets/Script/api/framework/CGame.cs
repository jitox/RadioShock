using UnityEngine;
using System.Collections;

public class CGame : MonoBehaviour 
{
	static private CGame mInstance;
	private CGameState mState;

	private CCamera mCamera;
	private CPlayerNew mPlayer;
	private CShip mShip;
    private CTriggerManager mTriggerManager;
    private CParticleManager mParticleManager;
    //Test commit
	void Awake()
	{
		if (mInstance != null) 
		{
			throw new UnityException ("Error in CGame(). You are not allowed to instantiate it more than once.");
		}

		mInstance = this;

		CMouse.init();
		CKeyboard.init ();
        mTriggerManager = new CTriggerManager();
        mParticleManager = new CParticleManager();
		//setState(new CLevelState ());
		setState(new CMainMenuState ());
	}

	static public CGame inst()
	{
		return mInstance;
	}

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		update ();
	}

	void LateUpdate()
	{
		render ();
	}

	private void update()
	{
		CMouse.update ();
		CKeyboard.update ();
		mState.update ();
	}

	private void render()
	{
		mState.render ();
	}

	public void destroy()
	{
		CMouse.destroy ();
		CKeyboard.destroy ();
		if (mState != null) 
		{
			mState.destroy ();
			mState = null;
		}
		mInstance = null;
	}

	public void setState(CGameState aState)
	{
		if (mState != null) 
		{
			mState.destroy();
			mState = null;
		}

		mState = aState;
		mState.init ();
	}

	public CGameState getState()
	{
		return mState;
	}

	public void setCamera(CCamera aCamera)
	{
		mCamera = aCamera;
	}

	public CCamera getCamera()
	{
		return mCamera;
	}

	public void setPlayer(CPlayerNew aPlayer)
	{
		mPlayer = aPlayer;
	}

	public CPlayerNew getPlayer()
	{
		return mPlayer;
	}

	public void setShip(CShip aShip)
	{
		mShip = aShip;
	}

	public CShip getShip()
	{
		return mShip;
	}
}
