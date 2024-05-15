using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Actors
{
    public class Reactors
    {
        public List<Reactor> counters { get; private set; } = new List<Reactor>();
        public List<Reactor> interrupts { get; private set; } = new List<Reactor>();
    }
}