namespace Level
{
    public class LevelSettings
    {
        public int SelectedLevel => _selectedLevel;

        private int _selectedLevel;

        public void SetNextLevel(int selectedLevel)
        {
            _selectedLevel = selectedLevel;
        }
    }
}