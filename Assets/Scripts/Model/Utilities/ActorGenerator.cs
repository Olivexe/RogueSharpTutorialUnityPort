using RogueSharp;
using RogueSharpTutorial.Controller;
using RogueSharpTutorial.Model;

namespace RogueSharpTutorial.Utilities
{
    public static class ActorGenerator
    {
        private static Player _player = null;

        public static Monster CreateMonster(Game game, int level, Point location)
        {
            GamePool<Monster> monsterPool = new GamePool<Monster>();
            monsterPool.Add(Kobold.Create(game, level), 25);
            monsterPool.Add(Ooze.Create(game, level), 25);
            monsterPool.Add(Goblin.Create(game, level), 50);
            if(level > 5) monsterPool.Add(FungalTower.Create(game, level), 5);

            Monster monster = monsterPool.Get();
            monster.X = location.X;
            monster.Y = location.Y;

            return monster;
        }

        public static Monster CreateMonster(MonsterList monsterName, Game game, int level, Point location)
        {
            Monster monster;

            switch(monsterName)
            {
                case MonsterList.goblin:
                    monster = Goblin.Create(game, level);
                    break;
                case MonsterList.kobold:
                    monster = Kobold.Create(game, level);
                    break;
                case MonsterList.ooze:
                    monster = Ooze.Create(game, level);
                    break;
                case MonsterList.fungalSpore:
                    monster = FungalSpore.Create(game, level);
                    break;
                case MonsterList.fungalTower:
                    monster = FungalTower.Create(game, level);
                    break;
                default:
                    monster = null;
                    break;
            }
            if(monster != null)
            {
                monster.X = location.X;
                monster.Y = location.Y;
            }

            return monster;
        }

        public static Monster CreateBarbarian(Game game, int level, Point location)
        {
            Monster barbarian = Barbarian.Create(game, level);
            barbarian.X = location.X;
            barbarian.Y = location.Y;

            return barbarian;
        }

        public static Player CreatePlayer(Game game)
        {
            _player = game.Player;

            if (_player == null)
            {
                _player = new Player(game)
                {
                    Attack = 2,
                    AttackChance = 50,
                    Awareness = 7,
                    Color = Colors.Player,
                    Defense = 2,
                    DefenseChance = 40,
                    Gold = 0,
                    Health = 50,
                    MaxHealth = 50,
                    Name = "Rogue",
                    Speed = 8,
                    Symbol = '@',

                    QAbility = new DoNothing(game, _player),
                    WAbility = new DoNothing(game, _player),
                    EAbility = new DoNothing(game, _player),
                    RAbility = new DoNothing(game, _player),

                    Item1 = new NoItem(game),
                    Item2 = new NoItem(game),
                    Item3 = new NoItem(game),
                    Item4 = new NoItem(game),

                    Head = HeadEquipment.None(game),
                    Body = BodyEquipment.None(game),
                    Hand = HandEquipment.None(game),
                    Feet = FeetEquipment.None(game)
                };
            }

            return _player;
        }
    }
}