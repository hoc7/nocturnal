﻿using System;
using BoneGame.Message;
using TMPro;
using UniRx;
using UnityEngine;

namespace BoneGame.Dialogue
{
    public class DialogueTextBox : MonoBehaviour
    {
        [SerializeField] private GameObject TextBoxObject;
        [SerializeField] private TextMeshProUGUI Name;
        [SerializeField] private TextMeshProUGUI Text;

        private bool NowOpen;
        
        private void Awake()
        {
            Messenger.Receive<DialogueTextMessage>().Subscribe(_ =>
            {
                //Name.text = _.Entity.Actor.Name;
                Text.text = _.Entity.Text;
                TMP_TextInfo textInfo = new TMP_TextInfo(Text);
                
                int lineCount = textInfo.lineInfo.Length;
                int maxCharCount;
                for (int i = 0; i < lineCount; i++)
                {
                    TMP_LineInfo lineInfo = textInfo.lineInfo[i];
                    int charCount = lineInfo.characterCount;
                }
                
                
                if (!NowOpen)
                {
                    TextBoxObject.SetActive(true);
                }
            }).AddTo(this);

            Messenger.Receive<EndDialogueMessage>().Subscribe(_ =>
            {
                //Name.text = string.Empty;
                Text.text = string.Empty;
                TextBoxObject.SetActive(false);
                NowOpen = false;

            }).AddTo(this);
        }
    }
}