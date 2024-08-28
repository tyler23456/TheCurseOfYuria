using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using FirstGearGames.SmoothCameraShaker;
using HeroEditor.Common.Enums;
using System;

namespace TCOY.UserActors
{
    [RequireComponent(typeof(Collider2D))]
    
    public class Actor : MonoBehaviour, IActor
    {
        [SerializeField] protected bool _useDefaultItems = true;
        [SerializeField] protected Stats stats;
        [SerializeField] protected ATBGuage aTBGuage;
        [SerializeField] protected List<Reactor> counters;
        [SerializeField] protected List<Reactor> interrupts;
        [SerializeField] protected Color _trajectoryPathColor;

        protected DefaultItems editorEquipper;
        protected new Collider2D collider2D;
        protected AudioSource audioSource;
        protected SpriteRenderer[] spriteRenderers;
        protected Inventory equipment;
        protected Inventory skills;
        protected StatusEffects statusEffects;
        protected HitAnimator hitAnimator;
        protected FadeAnimator fadeAnimator;
        protected SpriteFlipper spriteFlipper;
        protected Detection detection;

        public SpriteRenderer[] getSpriteRenderers => spriteRenderers;
        public Collider2D getCollider2D => collider2D;
        public AudioSource getAudioSource => audioSource;
        public GameObject obj => gameObject;
        public IStats getStats => stats;
        public IATBGuage getATBGuage => aTBGuage;
        public IInventory getEquipment => equipment;
        public IInventory getScrolls => skills;
        public IStatusEffects getStatusEffects => statusEffects;
        public HitAnimator getHitAnimator => hitAnimator;
        public IFadeAnimator getFadeAnimator => fadeAnimator;
        public ISpriteFlipper getSpriteFlipper => spriteFlipper;
        public IDetection getDetection => detection;
        public List<Reactor> getCounters => counters;
        public List<Reactor> getInterrupts => interrupts;

        public bool useDefaultItems { get { return _useDefaultItems; } set { _useDefaultItems = value; } }
        public Color trajectoryPathColor { get { return _trajectoryPathColor; } set { _trajectoryPathColor = value; } }


        public void Reset()
        {
            counters = new List<Reactor>();
            interrupts = new List<Reactor>();
        }

        protected void Awake()
        {
            editorEquipper = GetComponent<DefaultItems>();
            collider2D = GetComponent<Collider2D>();
            audioSource = GetComponent<AudioSource>();
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            aTBGuage = new ATBGuage();
            equipment = new Inventory();
            skills = new Inventory();
            detection = new Detection();
            statusEffects = new StatusEffects();

            hitAnimator = new HitAnimator(this, spriteRenderers);
            fadeAnimator = new FadeAnimator(this, spriteRenderers);
            spriteFlipper = new SpriteFlipper(spriteRenderers);

            stats.Initialize();
            stats.onHPDamage += (damage) => PopupFactory.Instance.AddHPDamagePopup(damage, collider2D.bounds.center);
            stats.onHPDamage += (damage) => CameraShakerHandler.Shake(ShakeManager.Instance.Get("Hit"));
            stats.onHPDamage += (damage) => hitAnimator.Start();

            stats.onZeroHealthEnter = () => StatFXDatabase.Instance.getKnockOut.Activate(this);
            stats.onZeroHealthExit = () => statusEffects.Remove("KnockOut");

            stats.onHPRecovery = (recovery) => PopupFactory.Instance.AddHPRecoveryPopup(recovery, collider2D.bounds.center);

            stats.onMPDamage = (damage) => PopupFactory.Instance.AddMPDamagePopup(damage, collider2D.bounds.center);

            stats.onMPRecovery = (recovery) => PopupFactory.Instance.AddMPRecoveryPopup(recovery, collider2D.bounds.center);
        }

        protected void Start()
        {
            if (editorEquipper == null || _useDefaultItems == false)
                return;

            Equipable[] items = editorEquipper.GetDefaultItems();

            foreach (Equipable item in items)
                item.Equip(this);
        }


        protected void Update()
        {
            if (!GameStateManager.Instance.isPlaying)
                return;

            aTBGuage.Update();
        }

        public virtual void RotateToward(Vector3 point)
        {
            Vector3 direction = (point - transform.position).normalized;

            if (direction.x > 0f && transform.eulerAngles.y >= 90f)
                transform.eulerAngles = new Vector3(0f, 0f, 0f);

            else if (direction.x < 0f && transform.eulerAngles.y < 90f)
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
}