namespace Assets.Scripts.Game.Models
{
    public class GameModel
    {
        private PlayerModel[] players;
        private int currentPlayerIndex;

        public PlayerModel GetCurrentPlayerModel => players[currentPlayerIndex];

        public GameModel(PlayerModel[] players)
        {
            this.players = players;
            currentPlayerIndex = 0;
        }

        public void NextPlayerTurn()
        {
            if (currentPlayerIndex < players.Length - 1)
            {
                currentPlayerIndex++;
            }
            else
            {
                currentPlayerIndex = 0;
            }
            players[currentPlayerIndex].MakeTurn();
        }
    }
}