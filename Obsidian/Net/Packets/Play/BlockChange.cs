﻿using Obsidian.Util;

namespace Obsidian.Net.Packets
{
    public class BlockChange : Packet
    {
        [Variable(VariableType.Position)]
        public Position Location { get; private set; }

        [Variable(VariableType.VarInt)]
        public int BlockId { get; private set; }

        public BlockChange(Position loc, int block) : base(0x0B, new byte[0])
        {
            Location = loc;
            BlockId = block;
        }

        public BlockChange(byte[] data) : base(0x0B, data) { }
    }
}