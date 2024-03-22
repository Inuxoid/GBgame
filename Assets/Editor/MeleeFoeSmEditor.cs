using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using StateMachines.FoeSM;
using StateMachines.PlayerSM;
using System.Reflection;
using Presenters;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using Dto;
using Let.Foes;
using TMPro;

[CustomEditor(typeof(FoeSM))]
public class MeleeFoeSmEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FoeSM foeSM = (FoeSM)target;

        if (GUILayout.Button("Fill Fields"))
        {
            FillFields(foeSM);
        }
    }

    private void FillFields(FoeSM foeSM)
    {
        // Находим и устанавливаем простые поля
        foeSM.foeEyes = foeSM.gameObject.transform.Find("EnemyEyes").gameObject;
        foeSM.playerSm = FindObjectOfType<PlayerSM>();
        foeSM.patrolPath = foeSM.GetComponent<PatrolPath>();
        foeSM.rigidbody = foeSM.GetComponent <Rigidbody>();
        foeSM.player = foeSM.playerSm.transform.Find("HeadColl").Find("PlayerBodyCenter").gameObject;
        foeSM.animator = foeSM.GetComponent<Animator>();
        foeSM.scoreCounter = foeSM.GetComponent<ScoreCounter>();
        foeSM.canvas = foeSM.transform.Find("HpCanvas").gameObject;
        foeSM.vision = foeSM.transform.Find("Vision").gameObject;
        foeSM.excPoint = foeSM.transform.Find("Attention").gameObject;
        // TODO unify borders foeSM.leftBorder = GameObject.Find("");
        // foeSM.rightBorder
        foeSM.stateTmpro = foeSM.transform.Find("HpCanvas (1)").Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

        // SETTINGS
        foeSM.currentHealth = 150;
        foeSM.maxHealth = 150;
        foeSM.flip = 1;
        foeSM.enemySpeed = 2;
        foeSM.enemyFightSpeed = 2.5f;
        foeSM.enemyDamage = 20;
        foeSM.enemyRangeDamage = 0;
        foeSM.detectDistance = 5;
        foeSM.hHearDistance = 1.6f;
        foeSM.vHearDistance = 2;
        foeSM.verticalDetectDistance = 1.6f;
        foeSM.timer = 3;
        foeSM.isPlayerInFrontOf = false;
        //foeSM.visionColorInHide = new Color(49, 42, 89, 96);
        foeSM.maxShieldStrength = 0;
        foeSM.rangeAttackDistance = 0;
        foeSM.meleeAttackDistance = 1;
        EditorUtility.SetDirty(foeSM);
    }
}
