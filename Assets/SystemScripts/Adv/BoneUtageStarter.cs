using System.Collections;
using System.Collections.Generic;
using BoneGame.System;
using UnityEngine;
using Utage;

public class BoneUtageStarter : MonoBehaviour,ISceneInitializer
{
    [SerializeField] private AdvEngineStarter AdvEngineStarter;
    public void Initialization(SceneStartEventBase eventBase)
    {
        AdvEngineStarter.StartEngine();
    }
}
