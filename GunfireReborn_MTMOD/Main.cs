using HeroCameraName;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using UnityEngine;
namespace GunfireReborn_MTMOD
{
    public static class BuildInfo
    {
        public const string Name = "GunfireReborn_MTMOD"; // Name of the Mod. (MUST BE SET)
        public const string Description = "GunfireReborn Mod "; // Description for the Mod. (Set as null if none)
        public const string Author = "MT"; // Author of the Mod. (MUST BE SET)
        public const string Company = null; // Company that made the Mod. (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod. (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod. (Set as null if none)
    }
    public class MTMOD : MelonMod
    {
        public static bool Infbullets = false;
        public static bool weaponmod = false;
        public static bool caidan = true;


        public override void OnApplicationStart() // Runs after Game Initialization.
        {
            MelonLogger.Msg("GunfireReborn_MTMOD Loder");
        }
        public override void OnUpdate() // Runs once per frame.
        {
            try
            {
                if ((MTMOD.Infbullets || MTMOD.weaponmod || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)))
                {
                    if (HeroCameraManager.HeroObj != null && HeroCameraManager.HeroObj.BulletPreFormCom != null && HeroCameraManager.HeroObj.BulletPreFormCom.weapondict != null)
                        foreach (var weapon in HeroCameraManager.HeroObj.BulletPreFormCom.weapondict)
                        {
                            if (MTMOD.Infbullets)
                            {
                                weapon.value.ModifyBulletInMagzine(200, 200);
                                weapon.value.ReloadBulletImmediately();
                            }
                            if (MTMOD.weaponmod)
                            {
                                List<int> list = weapon.value.WeaponAttr.Accuracy;
                                list[0] = 100000;
                                weapon.value.WeaponAttr.Accuracy = list;//武器精确度
                                weapon.value.WeaponAttr.Stability = list;//武器稳定性
                                // weapon.value.WeaponAttr.MaxBullet = 200;//弹夹最大子弹数目
                                weapon.value.WeaponAttr.AttDis = 9999f;//射击距离
                                weapon.value.WeaponAttr.FillTime = 5;//换弹时间

                                List<int> attSpeed = weapon.value.WeaponAttr.AttSpeed;
                                attSpeed[0] = weapon.value.WeaponAttr.AttSpeed[0] + 1000;//部分武器只会很鬼畜但攻速没加多少
                                weapon.value.WeaponAttr.AttSpeed = attSpeed;

                                if (weapon.value.WeaponAttr.BulletSpeed >= 50f && weapon.value.WeaponAttr.BulletSpeed != 55f || weapon.value.WeaponAttr.BulletSpeed == 30f)
                                {
                                    weapon.value.WeaponAttr.BulletSpeed = 500;//弹道速度
                                }

                            }
                            if (Input.GetKeyUp(KeyCode.UpArrow))
                            {
                                weapon.value.WeaponAttr.Radius += 5f;
                            }
                            if (Input.GetKeyUp(KeyCode.DownArrow))
                            {
                                weapon.value.WeaponAttr.Radius -= 5f;
                            }
                        }
                }
                if (Input.GetKeyUp(KeyCode.F1))
                {
                    MTMOD.Infbullets = !MTMOD.Infbullets;
                }
                if (Input.GetKeyUp(KeyCode.F2))
                {
                    MTMOD.weaponmod = !MTMOD.weaponmod;
                }
                if (Input.GetKeyUp(KeyCode.Home))
                {
                    MTMOD.caidan = !MTMOD.caidan;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    HeroCameraManager.HeroObj.playerProp.Speed += 100;
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    HeroCameraManager.HeroObj.playerProp.Speed -= 100;
                }
                if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    HeroMoveManager.HMMJS.jumping.baseHeight += 0.2f;
                }
                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    HeroMoveManager.HMMJS.jumping.baseHeight -= 0.2f;
                }
               


                if (Input.GetKey(KeyCode.F))
                {
                    List<NewPlayerObject> monsters = NewPlayerManager.GetMonsters();
                    if (monsters != null)
                    {
                        Vector3 campos = CameraManager.MainCamera.position;
                        Transform nearmons = null;
                        Transform monsterTransform = null;
                        float neardis = 500f;
                        foreach (var monster in monsters)
                        {
                            if ((double)monster.BloodBarCom.BloodBar.hp >= 0.05)
                            {
                                monsterTransform = monster.BodyPartCom.GetWeakTrans(false);
                                if (monsterTransform == null) continue;
                                Vector3 vector = CameraManager.MainCameraCom.WorldToViewportPoint(monsterTransform.position);
                                bool flag = false;
                                if (vector.x >= 0.45f && vector.x <= 0.55f && vector.y >= 0.45f && vector.y <= 0.55f && vector.z > 0f)
                                {
                                    vector.y = 0f;
                                    vector.x = 0.5f - vector.x;
                                    vector.x = (float)Screen.width * vector.x;
                                    vector.z = 0f;
                                    flag = true;
                                }
                                if (flag)
                                {
                                    vector = monsterTransform.position - campos;
                                    vector.y += 1.2f;
                                    float curdis = vector.magnitude;
                                    var hits = Physics.RaycastAll(new Ray(campos, vector), curdis);
                                    bool visible = true;
                                    foreach (RaycastHit raycastHit in hits)
                                    {
                                        if (raycastHit.collider.gameObject.layer == 0 || raycastHit.collider.gameObject.layer == 30 || raycastHit.collider.gameObject.layer == 31)
                                        {
                                            visible = false;
                                            break;
                                        }
                                    }
                                    if (visible && curdis < neardis)
                                    {
                                        neardis = curdis;
                                        nearmons = monsterTransform;
                                    }
                                }
                            }
                        }
                        if (nearmons != null)
                        {
                            Vector3 objpos = new Vector3();
                            objpos.x = HeroCameraManager.HeroObj.gameTrans.position.x;
                            objpos.y = nearmons.position.y + 0.2f;
                            objpos.z = HeroCameraManager.HeroObj.gameTrans.position.z;
                            Vector3 forward = nearmons.position - objpos;
                            forward.y += 0.12f;
                            Quaternion rotation = Quaternion.LookRotation(forward);
                            HeroCameraManager.HeroObj.gameTrans.rotation = rotation;
                            forward = nearmons.position - campos;
                            forward.y += 0.12f;
                            Quaternion rotation2 = Quaternion.LookRotation(forward);
                            CameraManager.MainCamera.rotation = rotation2;
                        }
                    }
                }
            }
            catch
            {
            }
        }
        
        public override void OnGUI() // Can run multiple times per frame. Mostly used for Unity's IMGUI.
        {

            try
            {
                if (MTMOD.caidan)
                {
                    int GUIY = 400;
                    GUI.Label(new Rect(30f, (float)GUIY, 150f, 20f), MTMOD.caidan ? "Home 菜单: 开启" : "Home 菜单: 关闭");
                    GUIY += 20;
                    GUI.Label(new Rect(30f, (float)GUIY, 300, 20f), "←→ 加减移动速度,↑↓加减爆炸范围");
                    GUIY += 20;
                    GUI.Label(new Rect(30f, (float)GUIY, 150f, 20f), MTMOD.Infbullets ? "F1 无限子弹: 开启" : "F1 无限子弹: 关闭");
                    GUIY += 20;
                    GUI.Label(new Rect(30f, (float)GUIY, 150f, 20f), MTMOD.weaponmod ? "F2 武器改装: 开启" : "F2 武器改装: 关闭");
                }
            }
            catch { }
        }

    }
}
