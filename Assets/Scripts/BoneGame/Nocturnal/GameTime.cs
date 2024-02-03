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
    
        public IReadOnlyReactiveProperty<float> TimeElapsed => _timeElapsed;
        public IObservable<Unit> TimerIsEnd => _timerElapsedSubject;

        public GameTime()
        {
            _timeElapsed = new ReactiveProperty<float>(0.0f);
            _currentTime = new DateTime(2024, 10, 1, 20, 0, 0);
        }

        public void StartTimer()
        {        
            Observable.Interval(TimeSpan.FromMilliseconds(10))
                .TakeWhile(_ => _timeElapsed.Value < 18000)
                .Do(_ => AdvanceTime())
                .Do(_ => CheckIfTimerElapsed())
                .Subscribe()
                .AddTo(_disposable);
        }

        private void AdvanceTime()
        {
            _timeElapsed.Value++;
            // 3分で9時間 なので10ミリ秒で1.8秒
            _currentTime = _currentTime.AddSeconds(1.8d);
        }

        private void CheckIfTimerElapsed()
        {
            if (_timeElapsed.Value >= 18000)
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