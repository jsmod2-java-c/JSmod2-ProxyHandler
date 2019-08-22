using System;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;

namespace jsmod2
{
    public class NewEventHandlers : IEventHandlerGeneratorFinish,IEventHandlerLCZDecontaminate,IEventHandlerSCP914Activate,
                                    IEventHandlerScpDeathAnnouncement,IEventHandlerSummonVehicle,IEventHandlerWarheadChangeLever,
                                    IEventHandlerWarheadDetonate,IEventHandlerWarheadKeycardAccess,IEventHandlerWarheadStartCountdown,
                                    IEventHandler079AddExp,IEventHandler079CameraTeleport,IEventHandler079Door,IEventHandler079ElevatorTeleport,
                                    IEventHandler079LevelUp,IEventHandler079Lockdown,IEventHandler079Lock,IEventHandler079StartSpeaker,IEventHandler079StopSpeaker, 
                                    IEventHandler079TeslaGate,IEventHandler079UnlockDoors,IEventHandler106CreatePortal,IEventHandler106Teleport,IEventHandlerCallCommand,
                                    IEventHandlerCheckEscape,IEventHandlerContain106,IEventHandlerPlayerDie,IEventHandlerPlayerDropItem,IEventHandlerElevatorUse,IEventHandlerGeneratorAccess,
                                    IEventHandlerGeneratorEjectTablet,IEventHandlerGeneratorInsertTablet,IEventHandlerGeneratorUnlock,IEventHandlerGrenadeExplosion,
                                    IEventHandlerGrenadeHitPlayer,IEventHandlerHandcuffed,IEventHandlerPlayerHurt,IEventHandlerInitialAssignTeam,IEventHandlerIntercomCooldownCheck,
                                    IEventHandlerIntercom,IEventHandlerLure,IEventHandlerMakeNoise,IEventHandlerMedkitUse,IEventHandlerPocketDimensionEnter,IEventHandlerPocketDimensionExit,
                                    IEventHandlerRadioSwitch,IEventHandlerRecallZombie,IEventHandlerReload,IEventHandlerSCP914ChangeKnob,IEventHandlerSetRole,IEventHandlerShoot,
                                    IEventHandlerSpawn,IEventHandlerSpawnRagdoll,IEventHandlerThrowGrenade,IEventHandlerPlayerTriggerTesla,IEventHandlerScp096CooldownEnd,IEventHandlerScp096CooldownStart,
                                    IEventHandlerScp096Enrage,IEventHandlerScp096Panic,IEventHandlerConnect,IEventHandlerDisconnect,IEventHandlerFixedUpdate,IEventHandlerLateDisconnect,IEventHandlerLateUpdate,
                                    IEventHandlerRoundEnd,IEventHandlerRoundRestart,IEventHandlerSceneChanged,IEventHandlerSetServerName,IEventHandlerUpdate,IEventHandlerWaitingForPlayers,IEventHandlerDecideTeamRespawnQueue,
                                    IEventHandlerSetNTFUnitName,IEventHandlerSetSCPConfig,IEventHandlerTeamRespawn,IEventHandlerAdminQuery,IEventHandlerAuthCheck,IEventHandlerBan,IEventHandlerPlayerPickupItem,
                                    IEventHandlerPlayerPickupItemLate,IEventHandlerPlayerJoin,IEventHandlerSetConfig,IEventHandlerCheckRoundEnd,IEventHandlerInfected
                                    

    {

        public void send(Event e, IdMapping mapping)
        {
            ProxyHandler.handler.sendEventObject(e,mapping.appendId(Lib.ID,e));
        }

        public void send(Type t, IdMapping mapping)
        {
            ProxyHandler.handler.sendEventObject(t,mapping);
        }

        public void send(Type t)
        {
            send(t,new IdMapping().appendId(Lib.ID,""));
        }
        
        void IEventHandlerSetConfig.OnSetConfig(SetConfigEvent ev)
        {
            send(ev,new IdMapping());
        }

        void IEventHandlerCheckRoundEnd.OnCheckRoundEnd(CheckRoundEndEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.ROUND_ID,ev.Round)
                .appendId(Lib.ROUND_STATS_ID,ev.Round.Stats)
            );
        }

        void IEventHandlerInfected.OnPlayerInfected(PlayerInfectedEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.ATTACKER_ID,ev.Attacker)
                .appendId(Lib.EVENT_ATTACKE_SCPDATA_ID,ev.Attacker.Scp079Data)
                .appendId(Lib.EVENT_ATTACKER_TEAMROLE_ID,ev.Attacker.TeamRole)
            );
        }
        void IEventHandlerPlayerPickupItemLate.OnPlayerPickupItemLate(PlayerPickupItemLateEvent ev)
        {
            send(ev,
                new IdMapping()
                    .appendId(Lib.ITEM_EVENT_ID,ev.Item)
                    .appendId(Lib.PLAYER_ID, ev.Player)
                    .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                    .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                
            );
        }
        
        void IEventHandlerPlayerJoin.OnPlayerJoin(PlayerJoinEvent ev)
        {
            send(ev, 
                new IdMapping()
                    .appendId(Lib.PLAYER_ID, ev.Player)
                    .appendId(Lib.PLAYER_EVENT_SCPDATA_ID, ev.Player.Scp079Data)
                    .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID, ev.Player.TeamRole));
        }
        void IEventHandlerPlayerPickupItem.OnPlayerPickupItem(PlayerPickupItemEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.ITEM_EVENT_ID,ev.Item)
            );
        }
        void IEventHandlerBan.OnBan(BanEvent ev)
        {
            send(ev, 
                new IdMapping()
                    .appendId(Lib.ADMIN_ID,  ev.Admin)
                    .appendId(Lib.ADMIN_EVENT_SCPDATA_ID,  ev.Admin.Scp079Data)
                    .appendId(Lib.ADMIN_EVENT_TEAM_ROLE_ID,ev.Admin.TeamRole)
                    .appendId(Lib.PLAYER_ID,  ev.Player)
                    .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                    .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                        
            );
                
        }
        void IEventHandlerAuthCheck.OnAuthCheck(AuthCheckEvent ev)
        {
            send(ev, 
                new IdMapping()
                    .appendId("requester-" + Lib.ID, ev.Requester)
                    .appendId(Lib.AUTH_CHECK_EVENT_REQUESTER_SCPDATA_ID, 
                        ev.Requester.Scp079Data).appendId("requester-"+Lib.PLAYER_TEAM_ROLE_ID,ev.Requester.TeamRole));

        }
        void IEventHandlerAdminQuery.OnAdminQuery(AdminQueryEvent ev)
        {
            send(ev, 
                new IdMapping()
                    .appendId(Lib.ADMIN_ID, ev.Admin)
                    .appendId(Lib.ADMIN_EVENT_SCPDATA_ID,  ev.Admin.Scp079Data)
                    .appendId(Lib.ADMIN_EVENT_TEAM_ROLE_ID, ev.Admin.TeamRole)
            );
        }



        void IEventHandlerGeneratorFinish.OnGeneratorFinish(GeneratorFinishEvent ev)
        {
            
            send(ev,new IdMapping()
                .appendId(Lib.EVENT_GENERATOR_ID,ev.Generator)
                .appendId(Lib.EVENT_GENERATOR_ROOM_ID,ev.Generator.Room)
            );
        }

        void IEventHandlerLCZDecontaminate.OnDecontaminate()
        {
            send(typeof(LCZDecontaminateEvent));
        }

        void IEventHandlerSCP914Activate.OnSCP914Activate(SCP914ActivateEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.EVENT_USER_ID,ev.User)
                .appendId(Lib.EVENT_USER_SCPDATA_ID,ev.User.Scp079Data)
                .appendId(Lib.EVENT_USER_TEAMROLE_ID,ev.User.TeamRole)
            );
        }

        void IEventHandlerScpDeathAnnouncement.OnScpDeathAnnouncement(ScpDeathAnnouncementEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.EVENT_DEAD_ID,ev.DeadPlayer)
                .appendId(Lib.EVENT_DEAD_SCPDATA_ID,ev.DeadPlayer.Scp079Data)
                .appendId(Lib.EVENT_DEAD_TEAMROLE_ID,ev.DeadPlayer.TeamRole)
            );
        }

        void IEventHandlerSummonVehicle.OnSummonVehicle(SummonVehicleEvent ev)
        {
            send(ev,new IdMapping());
        }

        void IEventHandlerWarheadChangeLever.OnChangeLever(WarheadChangeLeverEvent ev)
        {
            
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerWarheadDetonate.OnDetonate()
        {
            send(typeof(WarheadDetonateEvent));
        }

        void IEventHandlerWarheadKeycardAccess.OnWarheadKeycardAccess(WarheadKeycardAccessEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerWarheadStartCountdown.OnStartCountdown(WarheadStartEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.EVENT_ACTIVATOR_ID,ev.Activator)
                .appendId(Lib.EVENT_ACTIVATOR_SCPDATA_ID,ev.Activator.Scp079Data)
                .appendId(Lib.EVENT_ACTIVATOR_TEAMROLE_ID,ev.Activator.TeamRole)
            );
        }

        void IEventHandler079AddExp.On079AddExp(Player079AddExpEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );  
        }

        void IEventHandler079CameraTeleport.On079CameraTeleport(Player079CameraTeleportEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandler079Door.On079Door(Player079DoorEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.EVENT_DOOR_ID,ev.Door)
            );
            //Door
        }

        void IEventHandler079ElevatorTeleport.On079ElevatorTeleport(Player079ElevatorTeleportEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.EVENT_ELEVATOR_ID,ev.Elevator)
            );
        }

        void IEventHandler079LevelUp.On079LevelUp(Player079LevelUpEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandler079Lockdown.On079Lockdown(Player079LockdownEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.ROOM_ID,ev.Room)
            );
        }

        void IEventHandler079Lock.On079Lock(Player079LockEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.EVENT_DOOR_ID,ev.Door)
            );
            
        }

        void IEventHandler079StartSpeaker.On079StartSpeaker(Player079StartSpeakerEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.ROOM_ID,ev.Room)
            );
        }

        void IEventHandler079StopSpeaker.On079StopSpeaker(Player079StopSpeakerEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.ROOM_ID,ev.Room)
            );
        }

        void IEventHandler079TeslaGate.On079TeslaGate(Player079TeslaGateEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.TESLAGATE_ID,ev.TeslaGate)
            );
        }

        void IEventHandler079UnlockDoors.On079UnlockDoors(Player079UnlockDoorsEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandler106CreatePortal.On106CreatePortal(Player106CreatePortalEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandler106Teleport.On106Teleport(Player106TeleportEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerCallCommand.OnCallCommand(PlayerCallCommandEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerCheckEscape.OnCheckEscape(PlayerCheckEscapeEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerContain106.OnContain106(PlayerContain106Event ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerPlayerDie.OnPlayerDie(PlayerDeathEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.EVENT_KILLER_ID,ev.Killer)
                .appendId(Lib.EVENT_KILLER_SCPDATA_ID,ev.Killer.Scp079Data)
                .appendId(Lib.EVENT_KILLER_TEAMROLE_ID,ev.Killer.TeamRole)
            
            );
        }

        void IEventHandlerPlayerDropItem.OnPlayerDropItem(PlayerDropItemEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.ITEM_EVENT_ID,ev.Item)
            );
        }

        void IEventHandlerElevatorUse.OnElevatorUse(PlayerElevatorUseEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.EVENT_ELEVATOR_ID,ev.Elevator)
            );
        }

        void IEventHandlerGeneratorAccess.OnGeneratorAccess(PlayerGeneratorAccessEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.EVENT_GENERATOR_ID,ev.Generator)
                .appendId(Lib.EVENT_GENERATOR_ROOM_ID,ev.Generator.Room)
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerGeneratorEjectTablet.OnGeneratorEjectTablet(PlayerGeneratorEjectTabletEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.EVENT_GENERATOR_ID,ev.Generator)
                .appendId(Lib.EVENT_GENERATOR_ROOM_ID,ev.Generator.Room)
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerGeneratorInsertTablet.OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.EVENT_GENERATOR_ID,ev.Generator)
                .appendId(Lib.EVENT_GENERATOR_ROOM_ID,ev.Generator.Room)
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerGeneratorUnlock.OnGeneratorUnlock(PlayerGeneratorUnlockEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.EVENT_GENERATOR_ID,ev.Generator)
                .appendId(Lib.EVENT_GENERATOR_ROOM_ID,ev.Generator.Room)
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerGrenadeExplosion.OnGrenadeExplosion(PlayerGrenadeExplosion ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerGrenadeHitPlayer.OnGrenadeHitPlayer(PlayerGrenadeHitPlayer ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.VICTIM_ID,ev.Victim)
                .appendId(Lib.EVENT_VICTIM_SCPDATA_ID,ev.Victim.Scp079Data)
                .appendId(Lib.EVENT_VICTIM_TEAMROLE_ID,ev.Victim.TeamRole)
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );  
        }

        void IEventHandlerHandcuffed.OnHandcuffed(PlayerHandcuffedEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.OWNER_ID,ev.Owner)
                .appendId(Lib.EVENT_OWNER_SCPDATA_ID,ev.Owner.Scp079Data)
                .appendId(Lib.EVENT_OWNER_TEAMROLE_ID,ev.Owner.TeamRole)
            );
        }

        void IEventHandlerPlayerHurt.OnPlayerHurt(PlayerHurtEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.ATTACKER_ID,ev.Attacker)
                .appendId(Lib.EVENT_ATTACKE_SCPDATA_ID,ev.Attacker.Scp079Data)
                .appendId(Lib.EVENT_ATTACKER_TEAMROLE_ID,ev.Attacker.TeamRole)
            );
        }

        void IEventHandlerInitialAssignTeam.OnAssignTeam(PlayerInitialAssignTeamEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerIntercomCooldownCheck.OnIntercomCooldownCheck(PlayerIntercomCooldownCheckEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerIntercom.OnIntercom(PlayerIntercomEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerLure.OnLure(PlayerLureEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerMakeNoise.OnMakeNoise(PlayerMakeNoiseEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerMedkitUse.OnMedkitUse(PlayerMedkitUseEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerPocketDimensionEnter.OnPocketDimensionEnter(PlayerPocketDimensionEnterEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.ATTACKER_ID,ev.Attacker)
                .appendId(Lib.EVENT_ATTACKE_SCPDATA_ID,ev.Attacker.Scp079Data)
                .appendId(Lib.EVENT_ATTACKER_TEAMROLE_ID,ev.Attacker.TeamRole)
            );
        }

        void IEventHandlerPocketDimensionExit.OnPocketDimensionExit(PlayerPocketDimensionExitEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        public void OnPlayerRadioSwitch(PlayerRadioSwitchEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        public void OnRecallZombie(PlayerRecallZombieEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.TARGET_ID,ev.Target)
                .appendId(Lib.EVENT_TARGET_SCPDATA_ID,ev.Target.Scp079Data)
                .appendId(Lib.EVENT_TARGET_TEAMROLE_ID,ev.Target.TeamRole)
            );
        }

        public void OnReload(PlayerReloadEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        public void OnSCP914ChangeKnob(PlayerSCP914ChangeKnobEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        public void OnSetRole(PlayerSetRoleEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.PLAYER_TEAM_ROLE_ID,ev.TeamRole)
            );
        }

        public void OnShoot(PlayerShootEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.TARGET_ID,ev.Target)
                .appendId(Lib.EVENT_TARGET_SCPDATA_ID,ev.Target.Scp079Data)
                .appendId(Lib.EVENT_TARGET_TEAMROLE_ID,ev.Target.TeamRole)
            );
            
        }

        public void OnSpawn(PlayerSpawnEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        public void OnSpawnRagdoll(PlayerSpawnRagdollEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.ATTACKER_ID,ev.Attacker)
                .appendId(Lib.EVENT_ATTACKE_SCPDATA_ID,ev.Attacker.Scp079Data)
                .appendId(Lib.EVENT_ATTACKER_TEAMROLE_ID,ev.Attacker.TeamRole)
            );
        }

        public void OnThrowGrenade(PlayerThrowGrenadeEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
                .appendId(Lib.TESLAGATE_ID,ev.TeslaGate)
            );
        }

        public void OnScp096CooldownEnd(Scp096CooldownEndEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        public void OnScp096CooldownStart(Scp096CooldownStartEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        public void OnScp096Enrage(Scp096EnrageEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        public void OnScp096Panic(Scp096PanicEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.PLAYER_ID,  ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,  ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        public void OnConnect(ConnectEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.CONNECTION_ID,ev.Connection)
            );
        }

        public void OnDisconnect(DisconnectEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.CONNECTION_ID,ev.Connection)
            );
        }

        public void OnFixedUpdate(FixedUpdateEvent ev)
        {
            send(ev,new IdMapping());
        }

        public void OnLateDisconnect(LateDisconnectEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.CONNECTION_ID,ev.Connection)
            );
        }

        public void OnLateUpdate(LateUpdateEvent ev)
        {
            send(ev,new IdMapping());
        }

        public void OnRoundEnd(RoundEndEvent ev)
        {
            send(ev,new IdMapping()
                .appendId(Lib.ROUND_ID,ev.Round)
                .appendId(Lib.ROUND_STATS_ID,ev.Round.Stats)
            );
        }

        public void OnRoundRestart(RoundRestartEvent ev)
        {
            send(ev,new IdMapping());
        }

        public void OnSceneChanged(SceneChangedEvent ev)
        {
            send(ev,new IdMapping());
        }

        public void OnSetServerName(SetServerNameEvent ev)
        {
            send(ev,new IdMapping());
        }

        public void OnUpdate(UpdateEvent ev)
        {
            send(ev,new IdMapping());
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            send(ev,new IdMapping());
        }

        public void OnDecideTeamRespawnQueue(DecideRespawnQueueEvent ev)
        {
            send(ev,new IdMapping());
        }

        public void OnSetNTFUnitName(SetNTFUnitNameEvent ev)
        {
            send(ev,new IdMapping());
        }

        public void OnSetSCPConfig(SetSCPConfigEvent ev)
        {
            send(ev,new IdMapping());
        }

        public void OnTeamRespawn(TeamRespawnEvent ev)
        {
            send(ev,new IdMapping());
        }
    }
}