using UnityEngine;
using System.Collections;

public class CWinState : CGameState
{
    private CText mText;
    
    private int currentLvl;
    private CPopUpMenu aPopup;



    public CWinState(int aLvl)
    {
        mText = new CText("YOU WIN!", CText.alignment.TOP_CENTER, 60);
        mText.setXY(400, 400);
        mText.setColor(Color.white);
        currentLvl = aLvl;
        aPopup = new CPopUpMenu(aLvl);
        CGame.inst().getCamera().setX(0);

        
    }

    public override void init()
    {
        base.init();  
           

    }

    public override void update()
    {
        base.update();
        mText.update();
        aPopup.update();
       



    }

    public override void render()
    {
        base.render();
        mText.render();
        aPopup.render();
       
    }

    public override void destroy()
    {
        base.destroy();
        mText.destroy();
        mText = null;
        aPopup.destroy();
        aPopup = null;
       
       

    }
}
