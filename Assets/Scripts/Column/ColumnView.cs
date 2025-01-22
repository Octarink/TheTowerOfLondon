using Tower;
using UniRx;
using UnityEngine;

namespace Column
{
    public class ColumnView : MonoBehaviour
    {
        [SerializeField] private Transform _groundPoint;
        [SerializeField] private ColumnType _columnType;

        public Transform GroundPoint => _groundPoint;
        public ColumnType ColumnType => _columnType;

        private Subject<ColumnType> _columnSelected;

        public void Initialize(Subject<ColumnType> columnSelected )
        {
            _columnSelected = columnSelected;
        }

        public void Select()
        {
            _columnSelected?.OnNext(_columnType);
        }
        
    }
}