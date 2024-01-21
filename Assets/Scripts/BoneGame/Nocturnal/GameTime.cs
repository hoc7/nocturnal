using System;
using UniRx;

namespace BoneGame.Data
{
    public class GameTime:IDisposable
    {
        private CompositeDisposable _disposable = new CompositeDisposable();
        private ReactiveProperty<float> _timeElapsed;
        private Subject<Unit> _timerElapsedSubject = new Subject<Unit>();
    
        public IReadOnlyReactiveProperty<float> TimeElapsed => _timeElapsed;
        public IObservable<Unit> TimerIsEnd => _timerElapsedSubject;

        public GameTime()
        {
            _timeElapsed = new ReactiveProperty<float>(0.0f);
        }

        public void StartTimer()
        {        
            Observable.Interval(TimeSpan.FromMilliseconds(10))
                .TakeWhile(_ => _timeElapsed.Value < 18000)
                .Do(_ => _timeElapsed.Value++)
                .Do(_ => CheckIfTimerElapsed())
                .Subscribe()
                .AddTo(_disposable);
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