using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using FirstGearGames.SmoothCameraShaker;
using HeroEditor.Common.Enums;

namespace TCOY.UserActors
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class Actor : MonoBehaviour, IActor
    {
        
        [SerializeField] TargeterBase.Party party;
        [SerializeField] bool _useDefaultItems = true;
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
        public IInventory getScrolls => skills;
        public IAnimator getAnimator => animator;
        public IStatusEffects getStatusEffects => statusEffects;

        public List<Reactor> getCounters => counters;
        public List<Reactor> getInterrupts => interrupts;

        public bool useDefaultItems { get { return _useDefaultItems; } set { _useDefaultItems = value; } }

        protected void Awake()
        {
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
            statusEffects.onAdd = (name) => StatFXDatabase.Instance.Get(name).OnAdd(this);
            statusEffects.onUpdate = (name) => { };
            statusEffects.onRemove = (name) => StatFXDatabase.Instance.Get(name).OnRemove(this);

            stats.Initialize();
            stats.onHPDamage = (damage) => animator.Hit();
            stats.onHPDamage += (damage) => { }; //play a hit soundFX
            stats.onHPDamage += (damage) => PopupManager.Instance.AddHPDamagePopup(damage, collider2D.bounds.center);
            stats.onHPDamage += (damage) => CameraShakerHandler.Shake(ShakeDatabase.Instance.Get("Hit"));
            stats.onHPDamage += (damage) => StartCoroutine(HitAnimation());

            stats.onZeroHealth = () => StatFXDatabase.Instance.Get("KnockOut").Activate(this);

            stats.onHPRecovery = (recovery) => PopupManager.Instance.AddHPRecoveryPopup(recovery, collider2D.bounds.center);

            stats.onMPDamage = (damage) => PopupManager.Instance.AddMPDamagePopup(damage, collider2D.bounds.center);

            stats.onMPRecovery = (recovery) => PopupManager.Instance.AddMPRecoveryPopup(recovery, collider2D.bounds.center);
        }

        protected void OnValidate()
        {
            if (useDefaultItems)
            {
                character = GetComponent<Character>();
                character.UnEquip(EquipmentPart.Helmet);
                character.UnEquip(EquipmentPart.Earrings);
                character.UnEquip(EquipmentPart.Glasses);
                character.UnEquip(EquipmentPart.Mask);
                character.UnEquip(EquipmentPart.MeleeWeapon1H);
                character.UnEquip(EquipmentPart.MeleeWeapon2H);
                character.UnEquip(EquipmentPart.Cape);
                character.UnEquip(EquipmentPart.Armor);
                character.UnEquip(EquipmentPart.Shield);
                character.UnEquip(EquipmentPart.Bow);
                foreach (ItemBase item in defaultItems)
                    character.Equip(item.itemSprite, item.itemType.part);
            }
                
        }

        protected void Start()
        {
            if (useDefaultItems)
                foreach (ItemBase item in defaultItems)
                    item.Equip(this);
        }


        protected void Update()
        {
            if (!GameStateManager.Instance.isPlaying)
                return;

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