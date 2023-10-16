using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using ParticleEffects;

namespace Events {
    /*
        NonMonoSingleton EventService Class. Handles and invokes all the Events in the project.
        Uses Actions & Events (Observer Pattern).
    */
    public class EventService : GenericNonMonoSingleton<EventService>
    {
        public event Action onPlayerFiredBullet;
        public event Action onEnemyDeath;
        public event Action onPlayerDeath;
        public event Action<ParticleEffectType, Vector3> onGameObjectDestroyed;
        public event Action<string> onAchievementUnlocked;

        /*
            Invokes the onPlayerFiredBullet event. This is then used to trigger achievements & UI.
        */
        public void InvokePlayerFiredEvent() {
            onPlayerFiredBullet?.Invoke();
        }

        /*
            Invokes the onEnemyDeath event. This is then used to trigger achievements & UI.
         */
        public void InvokeEnemyDeathEvent() {
            onEnemyDeath?.Invoke();
        }

        /*
            Invokes the onGameObjectDestroyed event. This is then used to trigger particle Effects.
        */
        public void InvokeParticleSystemEvent(ParticleEffectType particleEffectType, Vector3 position) {
            onGameObjectDestroyed?.Invoke(particleEffectType, position);
        }
        

        /*
            Invokes the onAchievementUnlocked event. This is then used to trigger achievements & UI.
        */
        public void InvokeAchievementUnlockedEvent(string achievementText) {
            onAchievementUnlocked?.Invoke(achievementText);
        }

        /*
            Invokes the onPlayerDeath event. This is then used to trigger Game Over UI.
         */
        public void InvokePlayerDeathEvent() {
            onPlayerDeath?.Invoke();
        }
    }
    
}
