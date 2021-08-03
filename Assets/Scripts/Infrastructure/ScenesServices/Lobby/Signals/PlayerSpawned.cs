﻿using HabObjects;

 namespace Infrastructure.ScenesServices.Lobby.Signals
{
    public class PlayerSpawned
    {
        public Actor Actor { get; }

        public PlayerSpawned(Actor actor) => Actor = actor;
    }
}