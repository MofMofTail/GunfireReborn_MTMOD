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
        public const string Version = "1.0.1"; // Version of the Mod. (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod. (Set as null if none)
    }

    public class MTMOD : MelonMod
    {
        public static bool InfBullets;
        public static bool Menu = true;


        public override void OnApplicationStart() // Runs after Game Initialization.
        {
            MelonLogger.Msg("GunfireReborn_MTMOD Loder");
        }

        public override void OnUpdate() // Runs once per frame.
        {
            try
            {
                if (InfBullets || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
                    Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    if (HeroCameraManager.HeroObj != null && HeroCameraManager.HeroObj.BulletPreFormCom != null &&
                        HeroCameraManager.HeroObj.BulletPreFormCom.weapondict != null)
                        foreach (var weapon in HeroCameraManager.HeroObj.BulletPreFormCom.weapondict)
                        {
                            if (InfBullets)
                            {
                                weapon.value.ModifyBulletInMagzine(200, 200);
                                weapon.value.ReloadBulletImmediately();
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
                    InfBullets = !InfBullets;
                }

                if (Input.GetKeyUp(KeyCode.Home))
                {
                    Menu = !Menu;
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (HeroCameraManager.HeroObj != null) HeroCameraManager.HeroObj.playerProp.Speed += 100;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (HeroCameraManager.HeroObj != null) HeroCameraManager.HeroObj.playerProp.Speed -= 100;
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
                        Transform monsterTransform;
                        float neardis = 500f;
                        foreach (var monster in monsters)
                        {
                            if (monster.BloodBarCom.BloodBar.hp >= 0.05)
                            {
                                monsterTransform = monster.BodyPartCom.GetWeakTrans();
                                if (monsterTransform == null) continue;
                                Vector3 vector =
                                    CameraManager.MainCameraCom.WorldToViewportPoint(monsterTransform.position);
                                bool flag = false;
                                if (vector.x >= 0.45f && vector.x <= 0.55f && vector.y >= 0.45f && vector.y <= 0.55f &&
                                    vector.z > 0f)
                                {
                                    vector.y = 0f;
                                    vector.x = 0.5f - vector.x;
                                    vector.x = Screen.width * vector.x;
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
                                        if (raycastHit.collider.gameObject.layer == 0 ||
                                            raycastHit.collider.gameObject.layer == 30 ||
                                            raycastHit.collider.gameObject.layer == 31)
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
                            var heroPosition = HeroCameraManager.HeroObj.gameTrans.position;
                            var nearmonsPosition = nearmons.position;
                            objpos.x = heroPosition.x;
                            objpos.y = nearmonsPosition.y + 0.2f;
                            objpos.z = heroPosition.z;
                            Vector3 forward = nearmonsPosition - objpos;
                            forward.y += 0.12f;
                            Quaternion rotation = Quaternion.LookRotation(forward);
                            HeroCameraManager.HeroObj.gameTrans.rotation = rotation;
                            forward = nearmonsPosition - campos;
                            forward.y += 0.12f;
                            Quaternion rotation2 = Quaternion.LookRotation(forward);
                            CameraManager.MainCamera.rotation = rotation2;
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public override void OnGUI() // Can run multiple times per frame. Mostly used for Unity's IMGUI.
        {
            try
            {
                if (Menu)
                {
                    int GUIY = 400;
                    GUI.Label(new Rect(30f, (float) GUIY, 150f, 20f), MTMOD.Menu ? "Home 菜单: 开启" : "Home 菜单: 关闭");
                    GUIY += 20;
                    GUI.Label(new Rect(30f, (float) GUIY, 300, 20f), "←→ 加减移动速度,↑↓加减爆炸范围");
                    GUIY += 20;
                    GUI.Label(new Rect(30f, (float) GUIY, 150f, 20f), MTMOD.InfBullets ? "F1 无限子弹: 开启" : "F1 无限子弹: 关闭");
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}