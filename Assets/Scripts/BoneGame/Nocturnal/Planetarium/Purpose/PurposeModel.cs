using System.Collections.Generic;
using System.Linq;
using BoneGame.Event;
using UniRx;

namespace BoneGame.Nocturnal.Planetarium.Purpose
{
    /// <summary>
    /// 現在の目的を管理するクラス
    /// </summary>
    public class PurposeModel
    {
        /// <summary>
        /// Represents a collection of PurposeMasterBase instances in a queue
        /// </summary>
        public Queue<PurposeMasterBase> PurposeMasterBases = new Queue<PurposeMasterBase>();

        /// <summary>
        /// Gets the current purpose master.
        /// </summary>
        /// <remarks>
        /// The NowPurposeMaster property returns the PurposeMasterBase object at the top of the PurposeMasterBases stack,
        /// without removing it.
        /// </remarks>
        public PurposeMasterBase NowPurposeMaster => PurposeMasterBases.Peek();

        public Subject<PurposeMasterBase> Subject;
        
        /// <summary>
        /// Represents a PurposeModel object.
        /// </summary>
        /// <param name="masterBases">A list of PurposeMasterBase objects.</param>
        /// <remarks>
        /// This constructor initializes a PurposeModel object with a queue of PurposeMasterBase objects.
        /// </remarks>
        public PurposeModel(List<PurposeMasterBase> masterBases)
        {
            PurposeMasterBases = new Queue<PurposeMasterBase>(masterBases);
            Subject = new Subject<PurposeMasterBase>();
        }
        
        /// <summary>
        /// Adds a purpose to the queue of purpose masters.
        /// </summary>
        /// <param name="purpose">The purpose master to be added.</param>
        public void AddPurpose(PurposeMasterBase purpose)
        {
            PurposeMasterBases.Enqueue(purpose);
        }

        /// <summary>
        /// Removes the first purpose from the PurposeMasterBases queue.
        /// </summary>
        private void RemovePurpose()
        {
            PurposeMasterBases.Dequeue();
        }

        public void CreateNextPurpose()
        {
            RemovePurpose();
            if (PurposeMasterBases.Any())
            {
                Subject.OnNext(NowPurposeMaster);
            }
            else
            {
//                RecreatePurpose();
            }
        }

        /// <summary>
        /// 目的と合致しているものが撮影できているかチェック
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Check(int id)
        {
            return NowPurposeMaster.Check(id);
        }


    }
}