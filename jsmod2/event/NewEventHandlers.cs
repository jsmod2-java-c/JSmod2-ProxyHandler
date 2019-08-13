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
                                    IEventHandlerSetNTFUnitName,IEventHandlerSetSCPConfig,IEventHandlerTeamRespawn
                                    

    {
        public void OnGeneratorFinish(GeneratorFinishEvent ev)
        {
            ProxyHandler.handler.sendEventObject(ev,0x06,new IdMapping()
                .appendId(Lib.ID,ev)
                .appendId(Lib.EVENT_GENERATOR_ID,ev.Generator)
                .appendId(Lib.EVENT_GENERATOR_ROOM_ID,ev.Generator.Room)
            );
        }

        public void OnDecontaminate()
        {
            ProxyHandler.handler.sendEventObject(null,0x07,new IdMapping().appendId(Lib.ID,""));
        }

        public void OnSCP914Activate(SCP914ActivateEvent ev)
        {
            ProxyHandler.handler.sendEventObject(ev,0x08,new IdMapping()
                .appendId(Lib.ID,ev)
                .appendId(Lib.EVENT_USER_ID,ev.User)
                .appendId(Lib.EVENT_USER_SCPDATA_ID,ev.User.Scp079Data)
                .appendId(Lib.EVENT_USER_TEAMROLE_ID,ev.User.TeamRole)
            );
        }

        public void OnScpDeathAnnouncement(ScpDeathAnnouncementEvent ev)
        {
            ProxyHandler.handler.sendEventObject(ev,0x09,new IdMapping()
                .appendId(Lib.ID,ev)
                .appendId(Lib.EVENT_DEAD_ID,ev.DeadPlayer)
                .appendId(Lib.EVENT_DEAD_SCPDATA_ID,ev.DeadPlayer.Scp079Data)
                .appendId(Lib.EVENT_DEAD_TEAMROLE_ID,ev.DeadPlayer.TeamRole)
            );
        }

        public void OnSummonVehicle(SummonVehicleEvent ev)
        {
            ProxyHandler.handler.sendEventObject(ev,0x0a,new IdMapping()
                .appendId(Lib.ID,ev)
            );
        }

        public void OnChangeLever(WarheadChangeLeverEvent ev)
        {
            ProxyHandler.handler.sendEventObject(ev,0x0b,new IdMapping()
                .appendId(Lib.PLAYER_ID,ev.Player)
                .appendId(Lib.PLAYER_EVENT_SCPDATA_ID,ev.Player.Scp079Data)
                .appendId(Lib.PLAYER_EVENT_TEAM_ROLE_ID,ev.Player.TeamRole)
            );
        }

        void IEventHandlerGeneratorFinish.OnGeneratorFinish(GeneratorFinishEvent ev)
        {
            OnGeneratorFinish(ev);
        }

        void IEventHandlerLCZDecontaminate.OnDecontaminate()
        {
            OnDecontaminate();
        }

        void IEventHandlerSCP914Activate.OnSCP914Activate(SCP914ActivateEvent ev)
        {
            OnSCP914Activate(ev);
        }

        void IEventHandlerScpDeathAnnouncement.OnScpDeathAnnouncement(ScpDeathAnnouncementEvent ev)
        {
            OnScpDeathAnnouncement(ev);
        }

        void IEventHandlerSummonVehicle.OnSummonVehicle(SummonVehicleEvent ev)
        {
            OnSummonVehicle(ev);
        }

        void IEventHandlerWarheadChangeLever.OnChangeLever(WarheadChangeLeverEvent ev)
        {
            OnChangeLever(ev);
        }

        public void OnDetonate()
        {
            throw new System.NotImplementedException();
        }

        public void OnWarheadKeycardAccess(WarheadKeycardAccessEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnStartCountdown(WarheadStartEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void On079AddExp(Player079AddExpEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void On079CameraTeleport(Player079CameraTeleportEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void On079Door(Player079DoorEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void On079ElevatorTeleport(Player079ElevatorTeleportEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void On079LevelUp(Player079LevelUpEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void On079Lockdown(Player079LockdownEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void On079Lock(Player079LockEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void On079StartSpeaker(Player079StartSpeakerEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void On079StopSpeaker(Player079StopSpeakerEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void On079TeslaGate(Player079TeslaGateEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void On079UnlockDoors(Player079UnlockDoorsEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void On106CreatePortal(Player106CreatePortalEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void On106Teleport(Player106TeleportEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnCallCommand(PlayerCallCommandEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnCheckEscape(PlayerCheckEscapeEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnContain106(PlayerContain106Event ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnPlayerDie(PlayerDeathEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnPlayerDropItem(PlayerDropItemEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnElevatorUse(PlayerElevatorUseEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnGeneratorAccess(PlayerGeneratorAccessEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnGeneratorEjectTablet(PlayerGeneratorEjectTabletEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnGeneratorUnlock(PlayerGeneratorUnlockEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnGrenadeExplosion(PlayerGrenadeExplosion ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnGrenadeHitPlayer(PlayerGrenadeHitPlayer ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnHandcuffed(PlayerHandcuffedEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnPlayerHurt(PlayerHurtEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnAssignTeam(PlayerInitialAssignTeamEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnIntercomCooldownCheck(PlayerIntercomCooldownCheckEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnIntercom(PlayerIntercomEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnLure(PlayerLureEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnMakeNoise(PlayerMakeNoiseEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnMedkitUse(PlayerMedkitUseEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnPocketDimensionEnter(PlayerPocketDimensionEnterEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnPocketDimensionExit(PlayerPocketDimensionExitEvent ev)
        {
            throw new System.NotImplementedException();
        }

        void IEventHandlerWarheadDetonate.OnDetonate()
        {
            OnDetonate();
        }

        void IEventHandlerWarheadKeycardAccess.OnWarheadKeycardAccess(WarheadKeycardAccessEvent ev)
        {
            OnWarheadKeycardAccess(ev);
        }

        void IEventHandlerWarheadStartCountdown.OnStartCountdown(WarheadStartEvent ev)
        {
            OnStartCountdown(ev);
        }

        void IEventHandler079AddExp.On079AddExp(Player079AddExpEvent ev)
        {
            On079AddExp(ev);
        }

        void IEventHandler079CameraTeleport.On079CameraTeleport(Player079CameraTeleportEvent ev)
        {
            On079CameraTeleport(ev);
        }

        void IEventHandler079Door.On079Door(Player079DoorEvent ev)
        {
            On079Door(ev);
        }

        void IEventHandler079ElevatorTeleport.On079ElevatorTeleport(Player079ElevatorTeleportEvent ev)
        {
            On079ElevatorTeleport(ev);
        }

        void IEventHandler079LevelUp.On079LevelUp(Player079LevelUpEvent ev)
        {
            On079LevelUp(ev);
        }

        void IEventHandler079Lockdown.On079Lockdown(Player079LockdownEvent ev)
        {
            On079Lockdown(ev);
        }

        void IEventHandler079Lock.On079Lock(Player079LockEvent ev)
        {
            On079Lock(ev);
        }

        void IEventHandler079StartSpeaker.On079StartSpeaker(Player079StartSpeakerEvent ev)
        {
            On079StartSpeaker(ev);
        }

        void IEventHandler079StopSpeaker.On079StopSpeaker(Player079StopSpeakerEvent ev)
        {
            On079StopSpeaker(ev);
        }

        void IEventHandler079TeslaGate.On079TeslaGate(Player079TeslaGateEvent ev)
        {
            On079TeslaGate(ev);
        }

        void IEventHandler079UnlockDoors.On079UnlockDoors(Player079UnlockDoorsEvent ev)
        {
            On079UnlockDoors(ev);
        }

        void IEventHandler106CreatePortal.On106CreatePortal(Player106CreatePortalEvent ev)
        {
            On106CreatePortal(ev);
        }

        void IEventHandler106Teleport.On106Teleport(Player106TeleportEvent ev)
        {
            On106Teleport(ev);
        }

        void IEventHandlerCallCommand.OnCallCommand(PlayerCallCommandEvent ev)
        {
            OnCallCommand(ev);
        }

        void IEventHandlerCheckEscape.OnCheckEscape(PlayerCheckEscapeEvent ev)
        {
            OnCheckEscape(ev);
        }

        void IEventHandlerContain106.OnContain106(PlayerContain106Event ev)
        {
            OnContain106(ev);
        }

        void IEventHandlerPlayerDie.OnPlayerDie(PlayerDeathEvent ev)
        {
            OnPlayerDie(ev);
        }

        void IEventHandlerPlayerDropItem.OnPlayerDropItem(PlayerDropItemEvent ev)
        {
            OnPlayerDropItem(ev);
        }

        void IEventHandlerElevatorUse.OnElevatorUse(PlayerElevatorUseEvent ev)
        {
            OnElevatorUse(ev);
        }

        void IEventHandlerGeneratorAccess.OnGeneratorAccess(PlayerGeneratorAccessEvent ev)
        {
            OnGeneratorAccess(ev);
        }

        void IEventHandlerGeneratorEjectTablet.OnGeneratorEjectTablet(PlayerGeneratorEjectTabletEvent ev)
        {
            OnGeneratorEjectTablet(ev);
        }

        void IEventHandlerGeneratorInsertTablet.OnGeneratorInsertTablet(PlayerGeneratorInsertTabletEvent ev)
        {
            OnGeneratorInsertTablet(ev);
        }

        void IEventHandlerGeneratorUnlock.OnGeneratorUnlock(PlayerGeneratorUnlockEvent ev)
        {
            OnGeneratorUnlock(ev);
        }

        void IEventHandlerGrenadeExplosion.OnGrenadeExplosion(PlayerGrenadeExplosion ev)
        {
            OnGrenadeExplosion(ev);
        }

        void IEventHandlerGrenadeHitPlayer.OnGrenadeHitPlayer(PlayerGrenadeHitPlayer ev)
        {
            OnGrenadeHitPlayer(ev);
        }

        void IEventHandlerHandcuffed.OnHandcuffed(PlayerHandcuffedEvent ev)
        {
            OnHandcuffed(ev);
        }

        void IEventHandlerPlayerHurt.OnPlayerHurt(PlayerHurtEvent ev)
        {
            OnPlayerHurt(ev);
        }

        void IEventHandlerInitialAssignTeam.OnAssignTeam(PlayerInitialAssignTeamEvent ev)
        {
            OnAssignTeam(ev);
        }

        void IEventHandlerIntercomCooldownCheck.OnIntercomCooldownCheck(PlayerIntercomCooldownCheckEvent ev)
        {
            OnIntercomCooldownCheck(ev);
        }

        void IEventHandlerIntercom.OnIntercom(PlayerIntercomEvent ev)
        {
            OnIntercom(ev);
        }

        void IEventHandlerLure.OnLure(PlayerLureEvent ev)
        {
            OnLure(ev);
        }

        void IEventHandlerMakeNoise.OnMakeNoise(PlayerMakeNoiseEvent ev)
        {
            OnMakeNoise(ev);
        }

        void IEventHandlerMedkitUse.OnMedkitUse(PlayerMedkitUseEvent ev)
        {
            OnMedkitUse(ev);
        }

        void IEventHandlerPocketDimensionEnter.OnPocketDimensionEnter(PlayerPocketDimensionEnterEvent ev)
        {
            OnPocketDimensionEnter(ev);
        }

        void IEventHandlerPocketDimensionExit.OnPocketDimensionExit(PlayerPocketDimensionExitEvent ev)
        {
            OnPocketDimensionExit(ev);
        }

        public void OnPlayerRadioSwitch(PlayerRadioSwitchEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnRecallZombie(PlayerRecallZombieEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnReload(PlayerReloadEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnSCP914ChangeKnob(PlayerSCP914ChangeKnobEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnSetRole(PlayerSetRoleEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnShoot(PlayerShootEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnSpawn(PlayerSpawnEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnSpawnRagdoll(PlayerSpawnRagdollEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnThrowGrenade(PlayerThrowGrenadeEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnPlayerTriggerTesla(PlayerTriggerTeslaEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnScp096CooldownEnd(Scp096CooldownEndEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnScp096CooldownStart(Scp096CooldownStartEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnScp096Enrage(Scp096EnrageEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnScp096Panic(Scp096PanicEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnConnect(ConnectEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnDisconnect(DisconnectEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnFixedUpdate(FixedUpdateEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnLateDisconnect(LateDisconnectEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnLateUpdate(LateUpdateEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnRoundEnd(RoundEndEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnRoundRestart(RoundRestartEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnSceneChanged(SceneChangedEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnSetServerName(SetServerNameEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnUpdate(UpdateEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnDecideTeamRespawnQueue(DecideRespawnQueueEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnSetNTFUnitName(SetNTFUnitNameEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnSetSCPConfig(SetSCPConfigEvent ev)
        {
            throw new System.NotImplementedException();
        }

        public void OnTeamRespawn(TeamRespawnEvent ev)
        {
            throw new System.NotImplementedException();
        }
    }
}