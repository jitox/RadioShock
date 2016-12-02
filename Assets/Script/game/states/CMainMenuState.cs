using UnityEngine;
using System.Collections;

public class CMainMenuState : CGameState
{
	private CSprite mBackground;
    private CSprite mLoading;
    private bool mTransition = false;
    private bool mIsTransitionDone = false;
    private int mLevel;

	private CButtonSprite mButtonPlay;
    private CButtonSprite mButtonLevel2;

	public CMainMenuState()
	{

	}
	
	override public void init()
	{
		base.init ();

		mBackground = new CSprite ();
		mBackground.setImage (Resources.Load<Sprite> ("Sprites/menu/menu"));
		mBackground.setXY (0, 0);
		mBackground.setSortingLayerName("Background");
		mBackground.setName ("background");

        mLoading = new CSprite();
        mLoading.setImage(Resources.Load<Sprite>("Sprites/menu/loading"));
        mLoading.setSortingLayerName("UI");
        mLoading.setName("Loading");
        mLoading.setXY(0, -1080);
        mLoading.setVisible(false);

        mButtonPlay = new CButtonSprite ();
		mButtonPlay.setFrames (Resources.LoadAll<Sprite> ("Sprites/ui/button/Level1"));
		mButtonPlay.gotoAndStop (1);
		mButtonPlay.setXY (740, 670);
		mButtonPlay.setWidth (320);
		mButtonPlay.setHeight (96);
		mButtonPlay.setSortingLayerName ("UI");
		mButtonPlay.setName ("button Level 1");

        mButtonLevel2 = new CButtonSprite();
        mButtonLevel2.setFrames(Resources.LoadAll<Sprite>("Sprites/ui/button/Level2"));
        mButtonLevel2.gotoAndStop(1);
        mButtonLevel2.setXY(740, 790);
        mButtonLevel2.setWidth(320);
        mButtonLevel2.setHeight(96);
        mButtonLevel2.setSortingLayerName("UI");
        mButtonLevel2.setName("button Level 1");
    }
	
	override public void update()
	{
		base.update ();

        mButtonPlay.update();
        mButtonLevel2.update();
        mLoading.update();

        if (mTransition)
        {
            if (!mLoading.isVisible())
            {
                mLoading.setVisible(true);
                mLoading.setVelY(3000);
            }
            if (mLoading.getY() >= 0)
            {
                mLoading.setY(0);
                mIsTransitionDone = true;
            }
            if (mIsTransitionDone)
            {
                if (mLevel == 1)
                {
                    CGame.inst().setState(new CLevelState(1));
                    return;
                }
                else if (mLevel == 2)
                {
                    CGame.inst().setState(new CLevelState(2));
                    return;
                }
            }
        }
        else
        {
            if (mButtonPlay.clicked())
            {
                mTransition = true;
                mLevel = 1;
                return;
            }
            if (mButtonLevel2.clicked())
            {
                mTransition = true;
                mLevel = 2;
                return;
            }
        }
    }
	
	override public void render()
	{
		base.render ();

		mButtonPlay.render ();
        mButtonLevel2.render();
        mLoading.render();
	}
	
	override public void destroy()
	{
		base.destroy ();
		
		mBackground.destroy ();
		mBackground = null;

		mButtonPlay.destroy ();
		mButtonPlay = null;

        mButtonLevel2.destroy();
        mButtonLevel2 = null;

        mLoading.destroy();
        mLoading = null;
    }
	
}


