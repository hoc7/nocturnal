using System;
using UniRx;

namespace BoneGame.Data
{
    public class GameTime:IDisposable
    {
        private CompositeDisposable _disposable = new CompositeDisposable();
        private ReactiveProperty<float> _timeElapsed;
        private Subject<Unit> _timerElapsedSubject = new Subject<Unit>();
        public DateTime _currentTime { get; private set; }
        
        
        /// <summary>
        /// ゲーム内の世界での観測時間
        /// </summary>
        public float ActualGameSecond;
    
        /// <summary>
        /// 0.01秒毎に呼ばれる
        /// </summary>
        public IReadOnlyReactiveProperty<float> TimeElapsed => _timeElapsed;
        public IObservable<Unit> TimerIsEnd => _timerElapsedSubject;

        /// <summary>
        /// プレイ時間
        /// </summary>
        private float PlayTime;

        /// <summary>
        /// 現実世界時間の1秒でゲーム時間の何秒かかるか
        /// </summary>
        /// <returns></returns>
        public float Mult()
        {
            return ActualGameSecond / PlayTime;
        }

        private int EndTime()
        {
            return (int)PlayTime * 10;
        }

        /// <summary>
        /// 10ミリ秒毎に経つ時間
        /// </summary>
        /// <returns></returns>
        private double AddSecond()
        {
            return Mult() / 10d;
        }

        public GameTime(int playTime,int actualGameSecond, int year, int month, int day, int hour, int minute)
        {
            PlayTime = playTime;
            ActualGameSecond = actualGameSecond;
            _timeElapsed = new ReactiveProperty<float>(0.0f);
            _currentTime = new DateTime(year, month, day, hour, minute, 0);
        }

        public void StartTimer()
        {        
            Observable.Interval(TimeSpan.FromMilliseconds(100))
                .TakeWhile(_ => _timeElapsed.Value < EndTime())
                .Do(_ => AdvanceTime())
                .Do(_ => CheckIfTimerElapsed())
                .Subscribe()
                .AddTo(_disposable);
        }

        private void AdvanceTime()
        {
            _timeElapsed.Value++;
            // 3分で9時間 なので10ミリ秒で1.8秒
            _currentTime = _currentTime.AddSeconds(AddSecond());
        }

        private void CheckIfTimerElapsed()
        {
            if (_timeElapsed.Value >= EndTime())
            {
                _timerElapsedSubject.OnNext(Unit.Default);
            }
        }

        public void StopTimer()
        {
            _disposable.Clear();
        }
        
        

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}