using System;
using System.Collections.Generic;
using BoneGame.Message;
using BoneGame.Nocturnal.GameData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

using UniRx;
using UnityEngine.UI;

namespace BoneGame.Nocturnal.Planetarium.Book
{
    public class BookPresenter : MonoBehaviour
    {
        [SerializeField] private Button NextPage;
        [SerializeField] private Button BeforePage;
        [SerializeField] private PageView PageView;
        private BookModel _model;
        private bool Initialized;

        public void Initialization(List<BookMaster> masters)
        {
            if (Initialized)
            {
                
            }
            else
            {
                _model = new BookModel(masters);
                PageView.DrawPage(_model.GetNowPage());
            
                _model.PageSubject.Subscribe(PageView.DrawPage).AddTo(this);
            }
        }

        private void OnEnable()
        {
            PageView.DrawPage(_model.GetNowPage());
        }

        private void Start()
        {
            NextPage.OnClickAsObservable().Subscribe(_ =>
            {
                _model.SendNextPage();

            }).AddTo(this);

            BeforePage.OnClickAsObservable().Subscribe(_ =>
            {
                _model.SendBeforePage();

            }).AddTo(this);
            
            Messenger.Receive<TypeTabSelectMessage>().Subscribe(_ =>
            {

            }).AddTo(this);

            Messenger.Receive<CharacterTabSelectMessage>().Subscribe(_ =>
            {
                _model.PageTurnFromCharacter(_.Tab.ToString());

            }).AddTo(this);
        }
    }
}