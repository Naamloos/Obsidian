﻿using Microsoft.Extensions.Logging;
using Obsidian.Entities;
using Obsidian.Serialization.Attributes;
using System.Threading.Tasks;

namespace Obsidian.Net.Packets.Play.Serverbound
{
    [ServerOnly]
    public partial class ClientStatus : IServerboundPacket
    {
        [Field(0), ActualType(typeof(int)), VarLength]
        public ClientAction Action { get; set; }

        public int Id => 0x04;

        public async ValueTask HandleAsync(Server server, Player player)
        {
            if (this.Action == ClientAction.PerformRespawn)
            {
                await player.RespawnAsync();
            }
        }
    }

    public enum ClientAction
    {
        PerformRespawn,
        RequestStats
    }
}
