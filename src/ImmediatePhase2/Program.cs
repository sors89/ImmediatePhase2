using Terraria;
using Terraria.ID;
using TerrariaApi.Server;
using TShockAPI;

namespace ImmediatePhase2
{
    [ApiVersion(2, 1)]
    public class ImmediatePhase2 : TerrariaPlugin
    {
        public override string Author => "Sors";

        public override string Description => "Some bosses will enter its phase 2 after it is summoned";

        public override string Name => "ImmediatePhase2";

        public override Version Version => new(1, 0, 0);

        public ImmediatePhase2(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            ServerApi.Hooks.GameUpdate.Register(this, OnGameUpdate);
        }

        private static void OnNPCSpawn(object? sender, OTAPI.Hooks.NPC.SpawnEventArgs args)
        {
            NPC npc = Main.npc[args.Index];
            if (npc.boss && npc.active)
            {
                switch (npc.type)
                {
                    case NPCID.EyeofCthulhu:
                        npc.ai[0] = 3f;
                        NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, npc.whoAmI);
                        break;
                    case NPCID.Retinazer:
                        npc.ai[0] = 3f;
                        npc.ai[1] = npc.ai[2] = npc.ai[3] = 0.0f;
                        NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, npc.whoAmI);
                        break;
                    case NPCID.Spazmatism:
                        npc.ai[0] = 3f;
                        npc.ai[1] = npc.ai[2] = npc.ai[3] = 0.0f;
                        NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, npc.whoAmI);
                        break;
                    case NPCID.HallowBoss:
                        npc.ai[0] = 10f;
                        NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, npc.whoAmI);
                        break;
                }
            }
        }

        private void OnGameUpdate(EventArgs args)
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.boss && npc.active)
                {
                    if (npc.type == NPCID.DukeFishron) //had to put Duke here because of his long intro that i could not modify his ai immediately
                    {
                        if (npc.ai[0] == 0f)
                        {
                            npc.ai[0] = 4f;
                            npc.ai[1] = npc.ai[2] = npc.ai[3] = 0.0f;
                            NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, npc.whoAmI);
                        }
                        if (npc.ai[0] < 9f && npc.life <= (npc.lifeMax * 0.3)) //bonus since he has phase 3
                        {
                            npc.ai[0] = 9f;
                            npc.ai[1] = npc.ai[2] = npc.ai[3] = 0.0f;
                            NetMessage.SendData((int)PacketTypes.NpcUpdate, -1, -1, null, npc.whoAmI);
                        }
                    }
                }
            }
        }
    }
}
