using RogueSharp.DiceNotation;
using RogueSharpTutorial.Controller;

namespace RogueSharpTutorial.Model
{
    public static class CommonActions
    {
        public static void UpdateAlertStatus(Monster actor, bool isTargetInView)
        {
            if (isTargetInView && actor.IsAggressive && !actor.TurnsAlerted.HasValue)
            {
                actor.Game.MessageLog.Add(actor.Name + " is eager to fight " + actor.Game.Player.Name + ".");
                actor.TurnsAlerted = 0;
            }
            else if (!isTargetInView && actor.TurnsAlerted.HasValue)
            {
                actor.TurnsAlerted++;
            }
            else if (isTargetInView && actor.TurnsAlerted.HasValue)
            {
                actor.TurnsAlerted = 0;
            }

            if (actor.TurnsAlerted > 15)
            {
                actor.TurnsAlerted = null;
            }
        }
    }
}