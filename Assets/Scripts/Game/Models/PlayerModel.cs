namespace Assets.Scripts.Game.Models
{
    public class PlayerModel
    {
        public string Name { get; private set; }
        public int Turns { get; private set; }

        public PlayerModel(int turns, string name)
        {
            Turns = turns;
            Name = name;
        }

        public void MakeTurn() => Turns++;
    }
}