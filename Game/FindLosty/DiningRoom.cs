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
    public class DiningRoom : CommonRoom
    {
        public override string Name => "DiningRoom";

        protected override bool IsCommandVisible(string cmd)
        {
            switch (cmd)
            {
            }
            return base.IsCommandVisible(cmd);
        }
    }
}
