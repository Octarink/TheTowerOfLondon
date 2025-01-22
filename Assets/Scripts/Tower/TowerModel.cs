using UniRx;

namespace Tower
{
    public class TowerModel
    {
        public int MaxSteps = new();
        public ReactiveProperty<int> CurrentSteps = new();
    }
}