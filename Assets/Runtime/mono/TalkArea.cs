using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.rainssong.RainAvgEngine
{
    public class TalkArea : MonoBehaviour
    {
        public Text nameLabel;
        public Text contentLabel;

        public Image nextIcon;


        // Start is called before the first frame update
        void Start()
        {

        }

        //get set
        public bool nextIconVisible
        {
            get=>nextIcon.gameObject.activeSelf;
            set=>nextIcon.gameObject.SetActive(value);
        }


        public void Talk(string faceName, string content, bool hasNextBtn = false)
        {
            nameLabel.text = faceName;
            contentLabel.text = content;
            // nextIcon.gameObject.SetActive(hasNextBtn);
            // GameManager.gameView.talk(faceName, content, hasNextBtn);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}