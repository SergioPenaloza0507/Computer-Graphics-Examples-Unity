using System.Collections.Generic;
using UnityEngine;

namespace MOBAGame.Player
{
    [CreateAssetMenu(fileName = "Point and click Inputs", menuName = "Input Binders")]
    public class MOBAPlayerInputBinder : InputBinder
    {
        private readonly List<ButtonBinding> buttonBindings = new List<ButtonBinding>()
        {
            new ButtonBinding("Click", "Fire1"),
            new ButtonBinding("Attack_Q", "Q"),
            new ButtonBinding("Attack_W", "W"),
            new ButtonBinding("Attack_E", "E"),
            new ButtonBinding("Attack_R", "R")
        };

        public override void EvaluateBindings(InputHandler handler)
        {
            foreach (var binding in buttonBindings)
            {
                handler.QueryButton(binding.buttonId, binding.name);
            }
        }
    }
}
