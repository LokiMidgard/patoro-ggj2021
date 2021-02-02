﻿using LostAndFound.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.FindLosty._01_EntryHall
{
    public class RightDoor : Thing
    {
        public override string Emoji => Emojis.Door;

        public RightDoor(FindLostyGame game) : base(game)
        {
        }
    }
}
