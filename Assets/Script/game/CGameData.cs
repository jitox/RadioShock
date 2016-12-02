using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CGameData 
{

    private static CGameData mInst = null;

    //El maximo lvl que podes jugar
    public int maxLevel = 1;

   

    public CGameData()
    {
        
        registerSingleton();
    }

    public static CGameData inst()
    {
        return mInst;
    }

   
    private void registerSingleton()
    {
        if (mInst == null)
        {
            mInst = this;
        }
        else
        {
            throw new UnityException("ERROR: Cannot create another instance of singleton class CParticleManager.");
        }
    }




  
}

