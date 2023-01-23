using System.Collections.Generic;
using System.Xml;
using com.rainssong.io;
using UnityEngine;
using XLua;

namespace com.rainssong.RainAvgEngine
{
    /// <summary>
    /// 管理ScriptRunner
    /// </summary>
    public class ScriptManager
    {
        private List<ScriptRunner> scriptRunnerList = new List<ScriptRunner>();

        //current ScriptRunner
        private ScriptRunner currentScriptRunner => scriptRunnerList[scriptRunnerList.Count - 1];
        public LuaEnv luaEnv;

        public Dictionary<string, XmlDocument> xmlDic = new Dictionary<string, XmlDocument>();
        public ScriptManager(LuaEnv luaEnv)
        {
            this.luaEnv = luaEnv;
            scriptRunnerList.Add(new ScriptRunner());
            scriptRunnerList[scriptRunnerList.Count - 1].luaEnv = luaEnv;
        }

        /// <summary>
        /// 跳转脚本
        /// </summary>
        /// <param name="xmlName"></param>
        /// <param name="line"></param>
        public void RunScript(string xmlName, object line)
        {
            xmlName.Replace(".xml", "");
            var xmlUrl = AssetLoadManager.Instance.requestManager.GetRequest(xmlName).uri.AbsolutePath;

            currentScriptRunner.xml.Load(xmlUrl);
            currentScriptRunner.Goto(line);
        }
        public void Goto(object line = null)
        {
            currentScriptRunner.Goto(line);
        }

        public void Reset()
        {
            currentScriptRunner.Reset();
        }

        /// <summary>
        /// 运行子脚本（分支剧情）
        /// </summary>
        /// <param name="xmlName"></param>
        /// <param name="line"></param>
        public void RunChildScript(string xmlName, object line = null)
        {
            //var xml=xmlDic[xmlName];
            //RunChildScript(xml, line);

            var scriptRunner = new ScriptRunner();
            scriptRunner.luaEnv = luaEnv;

            scriptRunnerList.Add(scriptRunner);

            var xmlUrl = AssetLoadManager.Instance.requestManager.GetRequest(xmlName).uri.AbsolutePath;

            currentScriptRunner.xml.Load(xmlUrl);
            currentScriptRunner.Goto(line);
        }
        /// <summary>
        /// 运行子脚本（分支剧情）
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="line"></param>
        public void RunChildScript(XmlDocument xml, object line = null)
        {
            var scriptRunner = new ScriptRunner();
            scriptRunner.luaEnv = luaEnv;

            scriptRunnerList.Add(scriptRunner);

            currentScriptRunner.xml = xml;
            currentScriptRunner.Goto(line);
        }

        /// <summary>
        /// 回到父线剧情
        /// </summary>
        public void RunParentScript()
        {
            scriptRunnerList.RemoveAt(scriptRunnerList.Count - 1);
            Next();
        }

        public void Next()
        {
            currentScriptRunner.Next();
        }
    }

}