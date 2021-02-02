﻿using LostAndFound.FindLosty;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.Engine
{
    public class Inventory<TGame, TRoom, TPlayer, TThing> : IEnumerable<BaseThing<TGame, TRoom, TPlayer, TThing>>
        where TGame : BaseGame<TGame, TRoom, TPlayer, TThing>
        where TRoom : BaseRoom<TGame, TRoom, TPlayer, TThing>
        where TPlayer : BasePlayer<TGame, TRoom, TPlayer, TThing>
        where TThing : BaseThing<TGame, TRoom, TPlayer, TThing>
    {
        private readonly BaseContainer<TGame, TRoom, TPlayer, TThing> Owner;
        private bool canAcceptNonTransferables;
        private Dictionary<string, BaseThing<TGame, TRoom, TPlayer, TThing>> dict = new Dictionary<string, BaseThing<TGame, TRoom, TPlayer, TThing>>();

        #region IEnumerable
        public IEnumerator<BaseThing<TGame, TRoom, TPlayer, TThing>> GetEnumerator() => dict.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => dict.Values.GetEnumerator();
        #endregion

        #region Helpers
        private static string _BuildKey(string itemName) => itemName.ToLowerInvariant();
        private static string _BuildKey(BaseThing<TGame, TRoom, TPlayer, TThing> item) => _BuildKey(item.Name);
        #endregion

        public Inventory(BaseContainer<TGame, TRoom, TPlayer, TThing> owner, bool canAcceptNonTransferables = true)
        {
            this.Owner = owner;
            this.canAcceptNonTransferables = canAcceptNonTransferables;
        }

        public void InitialAdd(params BaseThing<TGame, TRoom, TPlayer, TThing>[] items)
        {
            foreach (var item in items)
            {
                if (canAcceptNonTransferables || item.CanBeTransfered)
                {
                    var key = _BuildKey(item);

                    if (!dict.ContainsKey(key))
                    {
                        dict.Add(key, item);
                    }
                } else
                {
                    Console.Error.WriteLine($"Item {item} cannot be put into Inventory {Owner}");
                }
            }
        }

        public bool Transfer(string name, Inventory<TGame, TRoom, TPlayer, TThing> target)
        {
            var key = _BuildKey(name);
            if (!dict.ContainsKey(key) || target == null) return false;

            BaseThing<TGame, TRoom, TPlayer, TThing> item;
            if (dict.TryGetValue(key, out item))
            {
                if (item.CanBeTransfered)
                {
                    dict.Remove(key);
                    target.dict.Add(key, item);
                    return true;
                }
            }
            return true;
        }

        internal bool TryFind(string token, out BaseThing<TGame, TRoom, TPlayer, TThing> item, bool recursive = false)
        {
            var key = _BuildKey(token);
            if (!dict.TryGetValue(key, out item) && recursive)
            {
                foreach(var container in this.OfType<BaseContainer<TGame, TRoom, TPlayer, TThing>>())
                {
                    if (container.Inventory.TryFind(token, out item))
                        return true;
                }
                return false;
            }
            return true;
        }
    }
}
