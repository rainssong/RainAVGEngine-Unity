using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace com.rainssong.RainAvgEngine
{
	
	/// <summary>
	/// 负责解释lua脚本
	/// </summary>
	public class ScriptRunner
	{
		public string ASSET_FOLDER = "assets/";
		//public Dictionary<string,int> CHAR_DICTIONARY = new Dictionary<string, int>();

		public int count = -1;

        public XmlDocument xml=new XmlDocument();

        public LuaEnv luaEnv;

		
		public void Reset()
		{
			count = -1;
		}
		
		/// <summary>
		/// 跳转到行
		/// </summary>
		/// <param name="line"></param>
		public void Goto(object line=null)
		{
			int length = xml["data"].ChildNodes.Count;
			// if (!string.IsNullOrEmpty(xmlName)) 
			// {	
			// 	count = 0;
			// } 
			int linen= 0;

			if(line is string)
			{
				var key=line as string;
				
				for (int i = 0; i < length; i++ )
				{
					var keyValue=xml.FirstChild.ChildNodes[i].Attributes["key"]?.Value;

					if(keyValue==key)
					{
						linen = i;
						break;
					}
				}
			}

			if(line is int)
			{
				linen= (int)line;
			}
			
			if (linen>=0)
			{
				count = linen;
			}
			if (count >= length)
				return;

			//object b=xml.ChildNodes;
			//object a=xml.ChildNodes;
			//object d=xml["data"]["motion"];
			//object e=xml.FirstChild.ChildNodes[1];

			string motion = xml.FirstChild.ChildNodes[count].InnerText;
			
			Debug.Log(motion);
			luaEnv.DoString(motion);
			
			//count++;
		}

		/// <summary>
		/// 跳转到标签行
		/// </summary>
		/// <param name="id"></param>
		public void Goto(string id)
		{
			int length = xml["data"].ChildNodes.Count;
			for (int i = 0; i < length; i++ )
			{
				Debug.Log(xml.FirstChild.ChildNodes[i].Attributes);
				if (xml.FirstChild.ChildNodes[i].Attributes["key"].ToString()==id)
				{
					count = i;
					break;
				}
			}

			//XmlNode xa = currentXml.FirstChild.SelectSingleNode("@" + id);
			//int i=currentXml.FirstChild.ChildNodes.ItemOf()

			Goto(count);
			//count = temp.childIndex();
		}
		
		public void Next()
		{
			Goto(count+1);
		}

		
	}

}