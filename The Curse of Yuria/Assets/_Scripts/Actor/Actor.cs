using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;

namespace TCOY.Actors
{
    public class Actor : MonoBehaviour, IActor
    {
        IGlobal global;
        IFactory factory;
        IBattleManager battleManager;

        [SerializeField] Position position;
        [SerializeField] Climber climber;
        [SerializeField] Rotation rotation;
        [SerializeField] Stats stats;
        [SerializeField] ATBGuage aTBGuage;
        [SerializeField] GroundChecker groundChecker;
        [SerializeField] JumpEvent jumpEvent;
        [SerializeField] Character character;

        Equipment equipment;

        public GameObject getGameObject => gameObject;
        public IPosition getPosition => position;
        public IClimber getClimber => climber;
        public IRotation getRotation => rotation;
        public IStats getStats => stats;
        public Character getCharacter => character;
        public IEquipment getEquipment => equipment;

        public List<IReactor> counters { get; private set; } = new List<IReactor>();
        public List<IReactor> interrupts { get; private set; } = new List<IReactor>();

        protected void Start()
        {
            GameObject obj = GameObject.Find("/DontDestroyOnLoad");
            global = obj.GetComponent<IGlobal>();
            factory = obj.GetComponent<IFactory>();
            battleManager = obj.GetComponent<IBattleManager>();
        }

        protected void Update()
        {
            position.Update();
            climber.Update();
            rotation.Update();
            aTBGuage.Update();
            groundChecker.Update();
            jumpEvent.Update();
        }
    }
}