using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BoneGame.Nocturnal.GameData;
using BoneGame.System;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;
using UniRx;

namespace BoneGame.Nocturnal.Planetarium.Book
{
    public class BookModel
    {
        private List<BookMaster> _masters;
        private int NowPage;
        public Subject<BookMaster> PageSubject = new Subject<BookMaster>();

        public BookModel(List<BookMaster> masters)
        {
            _masters = masters.OrderBy(_ => _.Name, StringComparer.Create(new CultureInfo("ja-JP"), false)).ToList();
            NowPage = 0;
            PageSubject.OnNext(GetNowPage());
        }

        public void PageTurnFromCharacter(string character)
        {
            int index = CharacterTurner.GetIndex(_masters.Select(_ => _.Name).ToList(), character);
            NowPage = index;
            PageSubject.OnNext(GetNowPage());
        }

        public void PageTurnFromType(string type)
        {
        }

        public void SendNextPage()
        {
            if (NowPage >= _masters.Count - 1)
            {
                NowPage = 0;
            }
            else
            {
                NowPage++;
            }

            PageSubject.OnNext(GetNowPage());
        }

        public void SendBeforePage()
        {
            if (NowPage == 0)
            {
                NowPage = _masters.Count - 1;
            }
            else
            {
                NowPage--;
            }
            
            PageSubject.OnNext(GetNowPage());
        }

        public BookMaster GetNowPage()
        {
            return _masters[NowPage];
        }
    }
}