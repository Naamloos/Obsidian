﻿using Obsidian.API;
using Obsidian.ChunkData;
using Obsidian.Utilities.Registry;
using Obsidian.WorldData.Generators.Overworld.Terrain;
using System;

namespace Obsidian.WorldData.Generators.Overworld
{
    public static class ChunkBuilder
    {
        public static void FillChunk(Chunk chunk, double[,] terrainHeightmap, double[,] bedrockHeightmap)
        {
            var air = Registry.GetBlock(Material.Air);
            var bedrock = Registry.GetBlock(Material.Bedrock);
            var stone = Registry.GetBlock(Material.Stone);

            for (int bx = 0; bx < 16; bx++)
            {
                for (int bz = 0; bz < 16; bz++)
                {
                    double terrainY = terrainHeightmap[bx, bz];
                    for (int by = 0; by < 256; by++)
                    {
                        var b = stone;
                        if (by > terrainY) { b = air; }
                        else if (by < bedrockHeightmap[bx, bz]) { b = bedrock; }

                        chunk.SetBlock(bx, by, bz, b);
                    }
                }
            }
        }

        public static void CarveCaves(OverworldTerrain noiseGen, Chunk chunk, double[,] rhm, double[,] bhm, bool debug = false)
        {
            var b = Registry.GetBlock(Material.CaveAir);
            for (int bx = 0; bx < 16; bx++)
            {
                for (int bz = 0; bz < 16; bz++)
                {
                    int tY = Math.Min((int)rhm[bx, bz], 64);
                    int brY = (int)bhm[bx, bz];
                    for (int by = brY; by < tY; by++)
                    {
                        bool caveAir = noiseGen.IsCave(bx + (chunk.X * 16), by, bz + (chunk.Z * 16));
                        if (caveAir)
                        {
                            if (debug) { b = Registry.GetBlock(Material.LightGrayStainedGlass); }
                            chunk.SetBlock(bx, by, bz, b);
                        }
                    }
                }
            }
        }
    }
}
