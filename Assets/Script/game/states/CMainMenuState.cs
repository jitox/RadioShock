using UnityEngine;
using System.Collections;

public class CMainMenuState : CGameState
{
	private CSprite mBackground;

	private CButtonSprite mButtonPlay;
    private CButtonSprite mButtonLevel2;

	public CMainMenuState()
	{

	}
	
	override public void init()
	{
		base.init ();

		mBackground = new CSprite ();
		mBackground.setImage (Resources.Load<Sprite> ("Sprites/menu/menu_background"));
		mBackground.setXY (0, 0);
		mBackground.setSortingLayerName("Background");
		mBackground.setName ("background");

		mButtonPlay = new CButtonSprite ();
		mButtonPlay.setFrames (Resources.LoadAll<Sprite> ("Sprites/ui/button/Level1"));
		mButtonPlay.gotoAndStop (1);
		mButtonPlay.setXY (CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2);
		mButtonPlay.setWidth (320);
		mButtonPlay.setHeight (96);
		mButtonPlay.setSortingLayerName ("UI");
		mButtonPlay.setName ("button Level 1");

        mButtonLevel2 = new CButtonSprite();
        mButtonLevel2.setFrames(Resources.LoadAll<Sprite>("Sprites/ui/button/Level2"));
        mButtonLevel2.gotoAndStop(1);
        mButtonLevel2.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2 + 200);
        mButtonLevel2.setWidth(320);
        mButtonLevel2.setHeight(96);
        mButtonLevel2.setSortingLayerName("UI");
        mButtonLevel2.setName("button Level 1");
    }
	
	override public void update()
	{
		base.update ();

		mButtonPlay.update ();
        mButtonLevel2.update();

		if (mButtonPlay.clicked ()) 
		{
			CGame.inst ().setState(new CLevelState (1));
			return;
		}
        if (mButtonLevel2.clicked())
        {
            CGame.inst().setState(new CLevelState(2));
            return;
        }
	}
	
	override public void render()
	{
		base.render ();

		mButtonPlay.render ();
        mButtonLevel2.render();
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
    }
	
}


