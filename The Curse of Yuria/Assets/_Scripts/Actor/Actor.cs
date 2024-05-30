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
        [SerializeField] TargeterBase.Party party;
        [SerializeField] List<ItemBase> defaultItems;
        [SerializeField] protected Stats stats;
        [SerializeField] protected ATBGuage aTBGuage;
        [SerializeField] List<Reactor> counters;
        [SerializeField] List<Reactor> interrupts;

        protected Character character;
        protected new Camera camera;
        protected new Collider2D collider2D;
        protected SpriteRenderer[] spriteRenderers;
        protected Position position;
        protected Rotation rotation;
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

        public List<Reactor> getCounters => counters;
        public List<Reactor> getInterrupts => interrupts;

        bool hasInitialized = false;

        protected void Start()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            if (hasInitialized)
                return;

            hasInitialized = true;

            character = GetComponent<Character>();
            collider2D = GetComponent<Collider2D>();
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            position = new Position(gameObject);
            rotation = new Rotation(gameObject);
            aTBGuage = new ATBGuage();
            groundChecker = new GroundChecker(gameObject);
            equipment = new Inventory();
            skills = new Inventory();
            animator = new UserAnimator(gameObject);

            statusEffects = new StatusEffects();
            statusEffects.onAdd = (name) => Factory.instance.GetStatusEffect(name).OnAdd(this);
            statusEffects.onUpdate = (name) => { };
            statusEffects.onRemove = (name) => Factory.instance.GetStatusEffect(name).OnRemove(this);

            stats.Initialize();
            stats.onApplyDamage = (damage) => animator.Hit();
            stats.onApplyDamage += (damage) => { }; //play a hit soundFX
            stats.onApplyDamage += (damage) => PopupManager.Instance.AddDamagePopup(damage, collider2D.bounds.center);
            stats.onApplyDamage += (damage) => CameraShakerHandler.Shake(Global.Instance.getShakeData);
            stats.onApplyDamage += (damage) => StartCoroutine(HitAnimation());

            stats.onZeroHealth = () => Factory.instance.GetStatusEffect("KnockOut").Activate(this);

            stats.onApplyRecovery = (recovery) => PopupManager.Instance.AddRecoveryPopup(recovery, collider2D.bounds.center);

            foreach (ItemBase item in defaultItems)
            {
                Debug.Log(item.itemType.part.ToString());
                item.Equip(this);
            }
                
        }

        protected void Update()
        {
            rotation.Update();
            aTBGuage.Update();
            groundChecker.Update();
            statusEffects.Update();
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