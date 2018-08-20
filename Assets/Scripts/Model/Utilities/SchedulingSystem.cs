using System.Linq;
using System.Collections.Generic;
using RogueSharpTutorial.Model.Interfaces;

namespace RogueSharpTutorial.Model
{
    public class SchedulingSystem
    {
        private int time;
        private readonly SortedDictionary<int, List<IScheduleable>> scheduleables;

        public SchedulingSystem()
        {
            time = 0;
            scheduleables = new SortedDictionary<int, List<IScheduleable>>();
        }

        /// <summary>
        /// Add a new object to the schedule. Place it at the current time plus the object's Time property.
        /// </summary>
        /// <param name="scheduleable"></param>
        public void Add(IScheduleable scheduleable)
        {
            int key = time + scheduleable.Time;
            if (!scheduleables.ContainsKey(key))
            {
                scheduleables.Add(key, new List<IScheduleable>());
            }
            scheduleables[key].Add(scheduleable);
        }

        /// <summary>
        /// Remove a specific object from the schedule. Useful for when an monster is killed to remove it before it's action comes up again.
        /// </summary>
        /// <param name="scheduleable"></param>
        public void Remove(IScheduleable scheduleable)
        {
            KeyValuePair<int, List<IScheduleable>> scheduleableListFound = new KeyValuePair<int, List<IScheduleable>>(-1, null);

            foreach (var scheduleablesList in scheduleables)
            {
                if (scheduleablesList.Value.Contains(scheduleable))
                {
                    scheduleableListFound = scheduleablesList;
                    break;
                }
            }
            if (scheduleableListFound.Value != null)
            {
                scheduleableListFound.Value.Remove(scheduleable);
                if (scheduleableListFound.Value.Count <= 0)
                {
                    scheduleables.Remove(scheduleableListFound.Key);
                }
            }
        }

        /// <summary>
        /// Get the next object whose turn it is from the schedule. Advance time if necessary.
        /// </summary>
        /// <returns></returns>
        public IScheduleable Get()
        {
            var firstScheduleableGroup = scheduleables.First();
            var firstScheduleable = firstScheduleableGroup.Value.First();
            Remove(firstScheduleable);
            time = firstScheduleableGroup.Key;
            return firstScheduleable;
        }

        /// <summary>
        /// Get the current time (turn) for the schedule.
        /// </summary>
        /// <returns></returns>
        public int GetTime()
        {
            return time;
        }

        /// <summary>
        /// Reset the time and clear out the schedule.
        /// </summary>
        public void Clear()
        {
            time = 0;
            scheduleables.Clear();
        }
    }
}