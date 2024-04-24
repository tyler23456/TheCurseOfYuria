using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

namespace TCOY.Actors
{
    public class Actor : MonoBehaviour, IActor
    {
        protected IGlobal global;
        protected IFactory factory;
        protected IBattleManager battleManager;

        [SerializeField] new Collider2D collider2D;
        [SerializeField] Position position;
        [SerializeField] Climber climber;
        [SerializeField] Rotation rotation;
        [SerializeField] Stats stats;
        [SerializeField] ATBGuage aTBGuage;
        [SerializeField] GroundChecker groundChecker;
        [SerializeField] JumpEvent jumpEvent;
        [SerializeField] Character character;    

        Equipment equipment;
        Inventory magic;
        Inventory techniques;

        public Collider2D getCollider2D => collider2D;
        public GameObject getGameObject => gameObject;
        public IPosition getPosition => position;
        public IClimber getClimber => climber;
        public IRotation getRotation => rotation;
        public IStats getStats => stats;
        public Character getCharacter => character;
        public IEquipment getEquipment => equipment;
        public IInventory getMagic => magic;
        public IInventory getTechniques => techniques;

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