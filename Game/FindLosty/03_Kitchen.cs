﻿using DSharpPlus.Entities;
using LostAndFound.Engine;
using LostAndFound.Engine.Attributes;
using LostAndFound.Engine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.Game.FindLosty
{
    public class Kitchen : CommonRoom
    {
        public override string Name => "Kitchen";
        #region LocalState
        bool FirePitOn = false;
        bool FridgeDoorOpen = false;
        bool ShelvesDoorOpen = false;
        string ThingInMicroWave = null;
        string ThingInMicroWaveIcon = null;

        #endregion

        #region Inventory
        protected override IEnumerable<(string, string, string)> InitialInventory =>
            new List<(string, string, string)> {
            // ("keys", Emojis.Keys, "Some Keys"),
        };
        #endregion

        #region HELP
        protected override bool IsCommandVisible(string cmd)
        {
            return base.IsCommandVisible(cmd);
        }
        #endregion

        #region LOOK
        protected override bool IsItemVisible(string itemKey)
        {
            return base.IsItemVisible(itemKey);
        }

        protected override string DescribeRoom(GameCommand cmd)
        {
            var pit_string = DescribeThing("pit", cmd);
            var roast_string = DescribeThing("roast", cmd);
            var microwave_string = DescribeThing("microwave", cmd);
            return $@"
                There are [shelves] at one wall and a large [refrigerator] on the other.
                In the middle of the room is a large fire [pit]. {pit_string}
                {roast_string}
                {microwave_string}
                There is one [door] leading to the dining room.
        ".FormatMultiline();
        }

        protected override string DescribeThing(string thing, GameCommand cmd)
        {
            switch (thing.ToLowerInvariant())
            {
                case "shelf":
                case "shelves":
                    {
                        return "Some shelves.";
                    }
                case "roast":
                    {
                        return "On the fire pit there is a tasty pork [roast]. It's well done.";
                    } break;
                case "fire_pit":
                case "pit":
                    {
                        if (FirePitOn)
                            return $"The fire is roaring.";
                        else
                            return $"There is no fire. Some logs are still smoldering.";
                    } break;
                case "refrigerator":
                    {
                        string message = "A large fridge.";
                        if (FridgeDoorOpen)
                            message += " The door is open.";
                        if (Inventory.ContainsKey("tofu"))
                            message += " There is a box of [tofu] inside.";
                        return message;
                    } break;
                case "microwave":
                    {
                        string message = "A [microwave].";
                        if (ThingInMicroWave == null)
                            message += " There is nothing inside.";
                        else
                            message += $" There is {ThingInMicroWave} inside.";
                        return message;
                    } break;
                default:
                    return base.DescribeThing(thing, cmd);
            }
        }
        #endregion

        #region LISTEN
        protected override string MakeSounds(GameCommand cmd)
        {
            return "The refrigerator hums and the fire is soaring.";
        }

        protected override string ListenAtThing(string thing, GameCommand cmd)
        {
            switch (thing)
            {
                case "refrigerator": return $"It hums.";
                case "shelf":
                case "shelves": return $"You can faintly hear 'The sound of silence' by Simon and Garfunkel.";
                default: return base.ListenAtThing(thing, cmd);
            }
        }

        #endregion

        #region TAKE
        protected override string WhyIsItemNotTakeable(string itemKey)
        {
            return base.WhyIsItemNotTakeable(itemKey);
        }
        #endregion

        #region KICK
        protected override string KickThing(string thing, GameCommand cmd)
        {
            return base.KickThing(thing, cmd);
        }
        #endregion


        #region OPEN
        protected override (bool succes, string msg) OpenThing(string thing, GameCommand cmd)
        {
            return base.OpenThing(thing, cmd);
                /*
            switch(thing)
            {
                case "shelves":
                case "shelf":
                    {
                        if (!ShelvesDoorOpen)
                        {
                            Inventory.Create("matches", Emojis.Heart, "Some matches.");
                            ShelvesDoorOpen = true;
                            return (true, "The shelf door opens.");
                        }
                        else
                            return (false, "It's already open.");

                    } break;
                case "fridge":
                case "refrigerator":
                    {
                        if (!FridgeDoorOpen)
                        {
                            Inventory.Create("tofu", Emojis.Heart, "Some delicios tofu. Consumable by humans and animals.");
                            FridgeDoorOpen = true;
                            return (true, "The refigerator door opens.");
                        }
                        else
                            return (false, "It's already open.");
                    } break;
                default:
                    return base.OpenThing(thing, cmd);
            }
                */
        }
        #endregion

        [Command("PUT", "put [something] in [something], eg put door in microwave")]
        public void PutCommand(PlayerCommand cmd)
        {
            /*
            if (cmd.Player is not Player player) return;

            if (cmd.Args.Count == 3 && cmd.Args[1] == "in")
            {
                if (player.Inventory.ContainsKey(cmd.Args[0]))
                {
                    if (cmd.Args[2] == "microwave")
                    {
                        if (ThingInMicroWave != null)
                        {
                            player.SendGameEvent($"There is already {ThingInMicroWave} inside");
                        }
                        else
                        {
                            player.SendGameEvent($"You put the {cmd.Args[0]} in the micowave");
                            ThingInMicroWave = cmd.Args[0];
                            ThingInMicroWaveIcon = player.Inventory[cmd.Args[0]].Emoji;
                            player.Inventory.Remove(cmd.Args[0]);
                        }
                    }
                    else
                    {
                        player.SendGameEvent($"Put it in the what?!!??");
                    }
                }
                else
                {
                    player.SendGameEvent($"You don't have a {cmd.Args[0]}");
                }

            }
            else
            {
                player.SendGameEvent($"You need to 'put <something> in <something>'");
            }
            */
        }

        [Command("GET", "get [something] from [something], eg get door from microwave")]
        public void GetCommand(PlayerCommand cmd)
        {
            /*
            if (cmd.Player is not Player player) return;

            if (cmd.Args.Count == 3 && cmd.Args[1] == "from")
            {
                    if (cmd.Args[2] == "microwave")
                    {
                        if (ThingInMicroWave == cmd.Args[0])
                        {
                            ThingInMicroWave = null;
                            player.Inventory.Create(cmd.Args[0], ThingInMicroWaveIcon, $"It's a {cmd.Args[0]}");
                            player.SendGameEvent($"You took {cmd.Args[0]}.");
                        }
                        else
                        {
                            player.SendGameEvent($"There is no {cmd.Args[0]} in the microwave.");
                        }
                    }
                    else
                    {
                        player.SendGameEvent($"I don't see a {cmd.Args[2]}");
                    }

            }
            else
            {
                player.SendGameEvent($"You need to 'get <something> from <something>'");
            }
            */
        }
    }
}

