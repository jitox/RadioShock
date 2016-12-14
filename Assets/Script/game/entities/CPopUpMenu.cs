using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public  class CPopUpMenu :CGameObject
    {

    private const int MAIN_MENU = 0;
    private const int NEXT_LVL = 1;
    private const int TRY_AGAIN = 2;
    
    private int currentLvl;
    private CButtonSprite tryAgain;
    private CButtonSprite mainMenu;
    private CButtonSprite nextLvl;

    private CButtonSprite mButtonPlay;
    private int nextState;
    private bool mTransition = false;
    private bool mIsTransitionDone = false;
    private CSprite mLoading;

    public CPopUpMenu(int aLvl)
    {
        currentLvl = aLvl;

        tryAgain = new CButtonSprite();
        //tryAgain.setFrames(Resources.LoadAll<Sprite>("Sprites/ui/button/popUpMenu/Again"));
        tryAgain.setFrames(Resources.LoadAll<Sprite>("Sprites/ui/button/popUpMenu/Again"));
        tryAgain.gotoAndStop(1);
        tryAgain.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2);
        tryAgain.setWidth(320);
        tryAgain.setHeight(96);
        tryAgain.setSortingLayerName("UI");
        tryAgain.setName("button Try Again");

        mainMenu = new CButtonSprite();
        mainMenu.setFrames(Resources.LoadAll<Sprite>("Sprites/ui/button/popUpMenu/mainMenu"));
        mainMenu.gotoAndStop(2);
        mainMenu.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2 + 200);
        mainMenu.setWidth(320);
        mainMenu.setHeight(96);
        mainMenu.setSortingLayerName("UI");
        mainMenu.setName("button Main menu");

        nextLvl = new CButtonSprite();
        nextLvl.setFrames(Resources.LoadAll<Sprite>("Sprites/ui/button/popUpMenu/nextlevl"));
        nextLvl.gotoAndStop(3);
        nextLvl.setXY(CGameConstants.SCREEN_WIDTH / 2, CGameConstants.SCREEN_HEIGHT / 2 + 400);
        nextLvl.setWidth(320);
        nextLvl.setHeight(96);
        nextLvl.setSortingLayerName("UI");
        nextLvl.setName("button Next level");

        mLoading = new CSprite();
        mLoading.setImage(Resources.Load<Sprite>("Sprites/menu/loading"));
        mLoading.setSortingLayerName("Loading");
        mLoading.setName("Loading");
        mLoading.setXY(0, -1080);
        mLoading.setVisible(false);

    }

    public override void update()
    {
        base.update();

        mainMenu.update();
        nextLvl.update();
        tryAgain.update();
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
                SoundList.instance.stopMusic();
                switch (nextState)
                {
                    case MAIN_MENU:
                        CGame.inst().setState(new CMainMenuState());
                        break;
                    case NEXT_LVL:
                        CGame.inst().setState(new CLevelState(currentLvl + 1));
                        break;
                    case TRY_AGAIN:
                        CGame.inst().setState(new CLevelState(currentLvl));
                        break;
                }
                return;
               
            }
        }
        else
        {
            if (mainMenu.clicked())
            {
                //CGame.inst().setState(new CMainMenuState());
                nextState = MAIN_MENU;
                mTransition = true;
                return;
            }
            if (nextLvl.clicked())
            {
                //CGame.inst().setState(new CLevelState(currentLvl + 1));
                nextState = NEXT_LVL;
                mTransition = true;
                return;
            }
            if (tryAgain.clicked())
            {
                //CGame.inst().setState(new CLevelState(currentLvl));
                nextState = TRY_AGAIN;
                mTransition = true;
                return;
            }
        }

       
    }

    public override void render()
    {
        base.render();
        
        
        mainMenu.render();
       
        nextLvl.render();
        
        tryAgain.render();
        mLoading.render();
       
    }

    public override void destroy()
    {
        base.destroy();


        mainMenu.destroy();
        mainMenu = null;
        tryAgain.destroy();
        tryAgain = null;
        nextLvl.destroy();
        nextLvl = null;
        mLoading.destroy();
        mLoading = null;
    }

}

