using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;

namespace TCOY.Actors
{
    public class Actor : MonoBehaviour, IActor
    {
        [SerializeField] Position position;
        [SerializeField] Climber climber;
        [SerializeField] Rotation rotation;
        [SerializeField] Stats stats;
        [SerializeField] ATBGuage aTBGuage;
        [SerializeField] GroundChecker groundChecker;
        [SerializeField] JumpEvent jumpEvent;
        [SerializeField] Character character;

        public GameObject getGameObject => gameObject;
        public IPosition getPosition => position;
        public IClimber getClimber => climber;
        public IRotation getRotation => rotation;
        public IStats getStats => stats;
        public Character getCharacter => character;

        public List<IReactor> counters { get; private set; } = new List<IReactor>();
        public List<IReactor> interrupts { get; private set; } = new List<IReactor>();

        protected void Start()
        {
            
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

        public void Equip(HeroEditor.Common.Data.ItemSprite item, HeroEditor.Common.Enums.EquipmentPart part, Color? color)
        {
            character.Equip(item, part, color);
        }
    }
}