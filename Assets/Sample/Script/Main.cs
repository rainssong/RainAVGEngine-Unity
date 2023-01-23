
using System.Xml;
using System;
using com.rainssong.RainAvgEngine;
using Unity;
using UnityEngine;
using XLua;
using com.rainssong.io;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


namespace com.rainssong.RainAvgEngine.example
{
    public class Main : MonoBehaviour
    {
        public StoryMono storyMono;
        void Start()
        {
            //scriptManager.Next();
            AssetLoadManager.Instance.baseURL = Application.streamingAssetsPath + "/";
            AssetLoadManager.Instance.loadConfigFile(Application.streamingAssetsPath + "/resource.json");
            AssetLoadManager.Instance.onComplete = OnLoadComp;
        }

        private void OnLoadComp()
        {
            storyMono.scriptManager.RunScript("mainXml",0);
        }
    }
}