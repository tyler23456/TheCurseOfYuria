using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using FirstGearGames.SmoothCameraShaker;

namespace TCOY.UserActors
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class Actor : MonoBehaviour, IActor
    {
        protected IGlobal global;
        protected IFactory factory;

        [SerializeField] TargeterBase.Party party;
        [SerializeField] List<ItemBase> defaultItems;
        [SerializeField] protected Stats stats;

        protected Character character;
        protected new Camera camera;
        protected new Collider2D collider2D;
        protected SpriteRenderer[] spriteRenderers;
        protected Position position;
        protected Rotation rotation;
        protected ATBGuage aTBGuage;
        protected GroundChecker groundChecker;
        protected Inventory equipment;
        protected Inventory skills;
        protected UserAnimator animator;
        protected StatusEffects statusEffects;

        public TargeterBase.Party getParty => party;
        public Collider2D getCollider2D => collider2D;
        public GameObject getGameObject => gameObject;
        public IPosition getPosition => position;
        public IRotation getRotation => rotation;
        public IStats getStats => stats;
        public IATBGuage getATBGuage => aTBGuage;
        public Character getCharacter => character;
        public IInventory getEquipment => equipment;
        public IInventory getSkills => skills;
        public IAnimator getAnimator => animator;
        public IStatusEffects getStatusEffects => statusEffects;

        public List<Reactor> counters { get; private set; } = new List<Reactor>();
        public List<Reactor> interrupts { get; private set; } = new List<Reactor>();

        bool hasInitialized = false;

        protected void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (hasInitialized)
                return;

            hasInitialized = true;

            GameObject obj = GameObject.Find("/DontDestroyOnLoad");
            global = obj.GetComponent<IGlobal>();
            factory = obj.GetComponent<IFactory>();

            character = GetComponent<Character>();
            camera = GameObject.Find("/DontDestroyOnLoad/Main Camera").GetComponent<Camera>();
            collider2D = GetComponent<Collider2D>();
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            position = new Position(gameObject);
            rotation = new Rotation(gameObject);
            aTBGuage = new ATBGuage();
            groundChecker = new GroundChecker(gameObject);
            equipment = new Inventory();
            skills = new Inventory();
            animator = new UserAnimator(gameObject);

            statusEffects = new StatusEffects(factory);
            statusEffects.onAdd = (name) => factory.GetStatusEffect(name).OnAdd(this);
            statusEffects.onUpdate = (name) => { };
            statusEffects.onRemove = (name) => factory.GetStatusEffect(name).OnRemove(this);

            stats.Initialize();
            stats.onApplyDamage = (damage) => animator.Hit();
            stats.onApplyDamage += (damage) => { }; //play a hit soundFX
            stats.onApplyDamage += (damage) => global.AddDamagePopup(damage.ToString(), collider2D.bounds.center);
            stats.onApplyDamage += (damage) => CameraShakerHandler.Shake(global.getShakeData);
            stats.onApplyDamage += (damage) => StartCoroutine(HitAnimation());

            stats.onZeroHealth = () => factory.GetStatusEffect("KnockOut").Activate(this);

            stats.onApplyRecovery = (recovery) => global.AddRecoveryPopup(recovery.ToString(), collider2D.bounds.center);

            foreach (ItemBase item in defaultItems)
                factory.GetItem(item.name).Equip(this);
        }

        protected void Update()
        {
            rotation.Update();
            aTBGuage.Update();
            groundChecker.Update();
            statusEffects.Update();
        }

        protected void LateUpdate()
        {
            if (camera != null)
                camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position + new Vector3(0f, 0f, -1f), 0.3f);
        }

        protected IEnumerator HitAnimation()
        {
            float accumulator = Time.unscaledTime;
            while(Time.unscaledTime < accumulator + 0.5f)
            {
                foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                    spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return new WaitForSecondsRealtime(0.05f);
            }
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                spriteRenderer.enabled = true;
        }
    }
}