using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Actors
{
    public class Reactors
    {
        public List<IReactor> counters { get; private set; } = new List<IReactor>();
        public List<IReactor> interrupts { get; private set; } = new List<IReactor>();
    }
}