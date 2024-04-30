using System.Collections.Generic;
using System.Linq;
using BoneGame.Nocturnal.GameData;
using NotImplementedException = System.NotImplementedException;

namespace BoneGame.Event.Trigger
{
    public class FlagTrigger:EventTriggerBase
    {
        public List<EventFlag> Triggers = new List<EventFlag>();
        public override bool CheckTrigger()
        {
            var flags = GameData.Instance().GetFrag();
            var complete = Triggers.Select(_ => _.Id).All(_ => flags.Contains(_));
            return complete;
        }
    }
}