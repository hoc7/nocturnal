using System;
using System.Collections.Generic;
using System.Linq;
using BoneGame.Data;
using BoneGame.Event;
using BoneGame.Message;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;

namespace BoneGame.Nocturnal.Planetarium.Purpose
{
    public class PurposePresenter : MonoBehaviour
    {
        [SerializeField] private PurposeView PurposeView;
        private PurposeModel _model;
        
        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="purposeMasterBases"></param>
        public void Initialization(List<PurposeMasterBase> purposeMasterBases)
        {
            _model = new PurposeModel(purposeMasterBases);
            _model.Subject.Subscribe(_ =>
            {
                PurposeView.DrawPurpose(_);
            }).AddTo(this);
            PurposeView.DrawPurpose(_model.NowPurposeMaster);
        }

        private void Start()
        {
            Messenger.Receive<PurposeCheckMessage>().Subscribe(_ =>
            {
                Check(_.stars,_.signs);
            }).AddTo(this);
        }

        public void Check(List<int> starIds, List<int> signIds)
        {
            bool isClear = starIds.Any(_ => _model.Check(_)) || signIds.Any(_ => _model.Check(_));

            if (isClear)
            {
                Debug.LogFormat("{0}をクリア",_model.NowPurposeMaster.GetText());
                _model.CreateNextPurpose();
            }
            else
            {
                Debug.Log("Not Clear");
            }
        }
    }
}