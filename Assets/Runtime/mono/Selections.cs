using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace com.rainssong.RainAvgEngine
{
    public class Selections : MonoBehaviour
    {
        public Button[] buttons;
        public string lastSelectStr;

        /// <summary>
        /// 从1开始计数
        /// </summary>
        public int lastSelectIndex;

        public Action<int> onSelect;

        void Awake()
        {
            if(buttons.Length == 0)
            {
                buttons=GetComponentsInChildren<Button>();
            }
            for (int i = 0; i < buttons.Length; i++)
            {
                var index=i;
                var button=buttons[i];
                // button.onClick.AddListener(()=>OnButtonClick(index+1));//为了和lua数组一致，从1开始计数
                UnityEventTools.AddIntPersistentListener(button.onClick, OnButtonClick, index+1);
            }
        }


        public void ShowOptions(string[] options)
        {
            this.Show();
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < options.Length)
                {
                    buttons[i].GetComponentInChildren<Text>().text = options[i];
                    buttons[i].Show();
                }
                else
                    buttons[i].Hide();
            }
        }


        public void OnButtonClick(int index)
        {
            lastSelectIndex = index;
            lastSelectStr = buttons[index - 1].GetComponentInChildren<Text>().text;
            onSelect?.Invoke(index);
            this.Hide();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}