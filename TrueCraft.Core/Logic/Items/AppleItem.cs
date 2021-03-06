using System;
using TrueCraft.API.Logic;

namespace TrueCraft.Core.Logic.Items
{
    public class AppleItem : FoodItem
    {
        public static readonly short ItemID = 0x104;

        public override short ID { get { return 0x104; } }

        public override float Restores { get { return 2; } }

        public override string DisplayName { get { return "Apple"; } }
    }
}