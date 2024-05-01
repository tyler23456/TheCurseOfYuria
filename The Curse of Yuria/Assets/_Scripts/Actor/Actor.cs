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

        [SerializeField] protected Camera camera;
        [SerializeField] protected new Collider2D collider2D;
        [SerializeField] protected Position position;
        [SerializeField] protected Climber climber;
        [SerializeField] protected Rotation rotation;
        [SerializeField] protected Stats stats;
        [SerializeField] protected ATBGuage aTBGuage;
        [SerializeField] protected GroundChecker groundChecker;
        [SerializeField] protected Character character;
        [SerializeField] protected Equipment equipment;
        [SerializeField] protected string attack;
        [SerializeField] protected Inventory magic;
        [SerializeField] protected Inventory techniques;
        [SerializeField] protected UserAnimator animator;

        public Collider2D getCollider2D => collider2D;
        public GameObject getGameObject => gameObject;
        public IPosition getPosition => position;
        public IClimber getClimber => climber;
        public IRotation getRotation => rotation;
        public IStats getStats => stats;
        public Character getCharacter => character;
        public IEquipment getEquipment => equipment;
        public string getAttack => attack;
        public IInventory getMagic => magic;
        public IInventory getTechniques => techniques;
        public IAnimator getAnimator => animator;

        public List<IReactor> counters { get; private set; } = new List<IReactor>();
        public List<IReactor> interrupts { get; private set; } = new List<IReactor>();

        protected void Start()
        {
            GameObject obj = GameObject.Find("/DontDestroyOnLoad");
            global = obj.GetComponent<IGlobal>();
            factory = obj.GetComponent<IFactory>();

            equipment.Start(factory);
        }

        protected void FixedUpdate()
        {
        }

        protected void Update()
        {
            climber.Update();
            rotation.Update();
            aTBGuage.Update();
            groundChecker.Update();
        }

        protected void LateUpdate()
        {
            if (camera != null)
                camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position + new Vector3(0f, 0f, -1f), 0.3f);
        }
    }
}