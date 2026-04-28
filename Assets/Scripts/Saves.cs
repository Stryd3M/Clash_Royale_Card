namespace YG
{
    public partial class SavesYG
    {
        public int wins = 0;

        public void AddWin()
        {
            wins++;
            YG2.SetLeaderboard("Wins", wins);
        }
    }
}