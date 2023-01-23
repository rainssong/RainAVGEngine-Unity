
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
using UnityEngine.EventSystems;

namespace com.rainssong.RainAvgEngine
{
    public class StoryMono : MonoBehaviour, IPointerClickHandler
    {
        public ScriptManager scriptManager;
        public LuaEnv luaEnv;


        public TalkArea talkArea;
        // public SpriteRenderer char1;
        public Image char1;
        public Image pic;
        // public SpriteRenderer char2;
        public Image bg;

        public AudioSource audioSource;

        public Selections selections;

        public Image transations;

        virtual public void Awake()
        {
            luaEnv = new LuaEnv();
            scriptManager = new ScriptManager(luaEnv);
            

            luaEnv.Global.Set<string, Action<string, string>>("talk", Talk);
            luaEnv.Global.Set<string, Action<string>>("changeBg", ChangeBg);
            luaEnv.Global.Set<string, Action<string[]>>("showOptions", ShowOptions);
            luaEnv.Global.Set<string, Action<string>>("showChar", ShowChar);
            luaEnv.Global.Set<string, Action<string>>("playMusic", PlayMusic);
            luaEnv.Global.Set<string, Action<object>>("go", scriptManager.Goto);
            luaEnv.Global.Set<string, Action>("runParentScript", scriptManager.RunParentScript);
            luaEnv.Global.Set<string, Action<string, object>>("runChildScript", scriptManager.RunChildScript);
            luaEnv.Global.Set<string, Action<object>>("startShake", StartShake);
            luaEnv.Global.Set<string, Action>("hideChar", char1.Hide);
            luaEnv.Global.Set<string, Action>("hideTalk", talkArea.Hide);
            luaEnv.Global.Set<string, Action<float>>("fadeOut", FadeOut);
            luaEnv.Global.Set<string, Action<float>>("fadeIn", FadeIn);
            luaEnv.Global.Set<string, Func<string, object>>("getValue", GetValue);
            luaEnv.Global.Set<string, Action<string, object>>("setValue", SetValue);
            luaEnv.Global.Set<string, Action>("gameOver", GameOver);
            luaEnv.Global.Set<string, Action<string>>("showPic", ShowPic);
            luaEnv.Global.Set<string, Action>("hidePic", pic.Hide);
            luaEnv.Global.Set<string, Action<float>>("wait", Wait);
            luaEnv.Global.Set("talkArea", talkArea);
            luaEnv.Global.Set("nextIconVisible", nextIconVisible);


            talkArea.Hide();
            char1.Hide();
            selections.Hide();
            pic.Hide();
            selections.onSelect = OnSelect;

        }


        public void StartGame()
        {
            Next();
            
        }

        private void SetValue(string key, object obj)
        {

            var t = obj.GetType();
            if (obj is float)
                PlayerPrefs.SetFloat(key, Convert.ToSingle(obj));
            if (obj is Int64)
                PlayerPrefs.SetFloat(key, Convert.ToInt32(obj));
            else
                PlayerPrefs.SetString(key, obj as String);
        }
        /// <summary>
        /// TODO:不支持复杂对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private object GetValue(string key)
        {
            var obj = PlayerPrefs.GetString(key);
            if (String.IsNullOrEmpty(obj))
                return PlayerPrefs.GetFloat(key);
            return PlayerPrefs.GetString(key);
        }

        private void FadeIn(float obj = 0.5f)
        {
            transations.DOColor(Color.clear, obj).onComplete = Next;
        }

        private void FadeOut(float obj = 0.5f)
        {
            transations.DOColor(Color.black, obj);
        }

        private void StartShake(object obj)
        {

        }

        private void OnSelect(int obj)
        {
            luaEnv.Global.Set<string, int>("lastSelect", selections.lastSelectIndex);
            luaEnv.Global.Set<string, int>("lastSelectIndex", selections.lastSelectIndex);
            luaEnv.Global.Set<string, string>("lastSelectString", selections.lastSelectStr);
            scriptManager.Next();
        }

        public void PlayMusic(string obj)
        {
            audioSource.clip = AssetLoadManager.Instance.requestManager.GetAudioClip(obj);
            audioSource.Play();
        }

        //FIXME：if(wait) wait.cancel();
        public void Next()
        {
            scriptManager.Next();
        }


        private void Wait(float time)
        {
            Invoke("Next", time);
        }

        public void OnClickBg()
        {
            if (selections.gameObject.activeSelf)
            return;

            if(!talkArea.nextIcon.IsActive())
            return;
                
            Next();
        }

        virtual public void GameOver()
        {
            SceneManager.LoadScene(0);
        }

        public void ShowOptions(string[] arg1)
        {
            Debug.Log(arg1);
            selections.ShowOptions(arg1);
        }

        public void ChangeBg(string arg1)
        {
            var sp = AssetLoadManager.Instance.requestManager.GetSprite(arg1);
            bg.sprite = sp;
        }

        public void ShowChar(string charName)
        {
            var sp = AssetLoadManager.Instance.requestManager.GetSprite(charName);
            char1.sprite = sp;
            char1.Show();
            //char1.sprite.texture.LoadImage(t2d.GetRawTextureData());
            // GameManager.gameView.talk(faceName, content, hasNextBtn);
        }

        public void ShowPic(string picName)
        {
            var sp = AssetLoadManager.Instance.requestManager.GetSprite(picName);
            pic.sprite = sp;
            pic.Show();
            //char1.sprite.texture.LoadImage(t2d.GetRawTextureData());
            // GameManager.gameView.talk(faceName, content, hasNextBtn);
        }

        /// <summary>
        /// xlua默认值为false
        /// </summary>
        /// <param name="faceName"></param>
        /// <param name="content"></param>
        /// <param name="hasNextBtn"></param>
        public void Talk(string faceName, string content)
        {
            talkArea.Talk(faceName, content);
            talkArea.Show();
            //Debug.Log(faceName + content);
            // GameManager.gameView.talk(faceName, content, hasNextBtn);
        }

        public bool nextIconVisible
        {
            get=>talkArea.nextIconVisible;
            set=>talkArea.nextIconVisible = value;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickBg();
        }
    }
}