using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CBackgroundcs:CGameObject
    {
    public CSprite mBackground;

    public CBackgroundcs()
    {
        mBackground = new CSprite();
        mBackground.setName("Bakcground");
        mBackground.setSortingLayerName("Background");
        mBackground.setImage(Resources.Load<Sprite>("Sprites/backgruond/background2"));
        setX(-270);
        setY(100);
        mBackground.setAlpha(0.50f);
        //setVelX(550);
        setVelX(-15);
        
        
    }

    public override void update()
    {
        base.update();
        mBackground.update();
        if (CGame.inst().getCamera().getOnborder())
        {
            setVelX(0);
        }
        //if (mBackground.getColor() != CTileMap.inst().actualColor)
        //{
        //    mBackground.setColor(CTileMap.inst().actualColor);
        //    //Debug.Log(" Mapa " + CTileMap.inst().actualColor.ToString());
        //    //Debug.Log("TILE COLOR " + getColor());

        //}

    }
    public override void render()
    {
        base.render();
        mBackground.render();
        //mBackground.setXY(getX(), getY());
        mBackground.setXY(getX() + CGame.inst().getCamera().getX(), getY() + CGame.inst().getCamera().getY());
    }

    public override void destroy()
    {
        base.destroy();
        mBackground.destroy();
        mBackground = null;
    }

    public void resetPos()
    {
        setX(-270);
    }

}

