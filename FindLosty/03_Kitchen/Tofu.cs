﻿using LostAndFound.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.FindLosty.Things
{
    public class Tofu : Item
    {
        public override string Emoji => Emojis.Tofu;

        public Tofu(FindLostyGame game) : base(game) { WasMentioned = true; }

        public bool Frozen = true;
        public int UseCount = 0;

        public override string LookText {
            get {
                if (Frozen)
                    return "A box of frozen tofu.";
                else
                    return "A box of warm, smelly tofu.";
            }
        }

        public override bool Use(Player sender, BaseThing<FindLostyGame, Room, Player, Thing> other, bool isFlippedCall = false)
        {
            if (other is null) {
                if (Frozen)
                {
                    sender.Reply("You lick the frozen tofu. Besides a strange taste in your mouth nothing happens.");
                }
                else
                {
                    if (UseCount < 3)
                    {
                        sender.Reply("You take a bite of tofu. It tastes good.");
                        UseCount++;
                    }
                    else
                    {
                        sender.Reply("There is not much tofu left and you feel like you might need some tofu later.");
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
