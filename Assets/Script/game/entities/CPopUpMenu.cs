﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public  class CPopUpMenu :CGameObject
    {
    private int currentLvl;
    private CButtonSprite tryAgain;
    private CButtonSprite mainMenu;
    private CButtonSprite nextLvl;

    private CButtonSprite mButtonPlay;

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

        
    }

    public override void update()
    {
        base.update();

        mainMenu.update();
        nextLvl.update();
        tryAgain.update();

        if (mainMenu.clicked())
        {
            CGame.inst().setState(new CMainMenuState());
            return;
        }
        if (nextLvl.clicked())
        {
            CGame.inst().setState(new CLevelState(currentLvl + 1));
            return;
        }
        if (tryAgain.clicked())
        {
            CGame.inst().setState(new CLevelState(currentLvl));

            return;
        }
    }

    public override void render()
    {
        base.render();
        
        
        mainMenu.render();
       
        nextLvl.render();
        
        tryAgain.render();
       
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
    }

}
