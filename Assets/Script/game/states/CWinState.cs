using UnityEngine;
using System.Collections;

public class CWinState : CGameState
{
    private CText mText;

    public CWinState()
    {
        mText = new CText("YOU WIN!", CText.alignment.TOP_CENTER, 60);
        mText.setXY(400, 400);
        mText.setColor(Color.white);
    }

    public override void init()
    {
        base.init();
    }

    public override void update()
    {
        base.update();
        mText.update();
    }

    public override void render()
    {
        base.render();
        mText.render();
    }

    public override void destroy()
    {
        base.destroy();
        mText.destroy();
        mText = null;
    }
}
