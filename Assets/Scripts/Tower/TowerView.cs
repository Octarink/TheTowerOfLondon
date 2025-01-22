using System;
using System.Collections.Generic;
using Column;
using DG.Tweening;
using Level;
using Tor;
using UniRx;
using UnityEngine;
using Zenject;

namespace Tower
{
    public class TowerView : MonoBehaviour
    {
        [SerializeField] private ColumnView _leftColumnView;
        [SerializeField] private ColumnView _centerColumnView;
        [SerializeField] private ColumnView _rightColumnView;
        [SerializeField] private float offset = 0.2f;
        [SerializeField] private Transform _bufferPoint;

        public ColumnView LeftColumnView => _leftColumnView;
        public ColumnView CenterColumnView => _centerColumnView;
        public ColumnView RightColumnView => _rightColumnView;

        private SignalBus _signalBus;
        private TowerModel _towerModel;
        
        private Subject<ColumnType> _columnSelected = new();

        private Stack<TorView> _leftColumn = new();
        private Stack<TorView> _centerColumn = new();
        private Stack<TorView> _rightColumn = new();

        private TorView _buffer;

        private bool _isBuffered;
        private int _torCount;
        private int _currentSteps;
        private int _maxSteps;

        [Inject]
        private void Construct(SignalBus signalBus,TowerModel towerModel)
        {
            _signalBus = signalBus;
            _towerModel = towerModel;
        }
        
        private void Start()
        {
            _leftColumnView.Initialize(_columnSelected);
            _centerColumnView.Initialize(_columnSelected);
            _rightColumnView.Initialize(_columnSelected);
            
            _columnSelected.Subscribe(ColumnSelected);
        }

        public void PushLeft(TorView tor, bool animate = false)
        {
            var position = _leftColumnView.GroundPoint.position;
            position.y += (_leftColumn.Count * offset);
            _leftColumn.Push(tor);
            if (animate)
            {
                var sequence = DOTween.Sequence();
                sequence.Append(tor.transform.DOMoveX(position.x, 0.3f));
                sequence.Append(tor.transform.DOMoveY(position.y, 0.3f));
                sequence.Play();
            }
            else
            {
                tor.transform.position = position;
            }
        }

        public void PushCenter(TorView tor, bool animate = false)
        {
            var position = _centerColumnView.GroundPoint.position;
            position.y += (_centerColumn.Count * offset);
            _centerColumn.Push(tor);
            if (animate)
            {
                var sequence = DOTween.Sequence();
                sequence.Append(tor.transform.DOMoveX(position.x, 0.3f));
                sequence.Append(tor.transform.DOMoveY(position.y, 0.3f));
                sequence.Play();
            }
            else
            {
                tor.transform.position = position;
            }
        }

        public void PushRight(TorView tor, bool animate = false)
        {
            var position = _rightColumnView.GroundPoint.position;
            position.y += (_rightColumn.Count * offset);
            _rightColumn.Push(tor);
            if (animate)
            {
                var sequence = DOTween.Sequence();
                sequence.Append(tor.transform.DOMoveX(position.x, 0.3f));
                sequence.Append(tor.transform.DOMoveY(position.y, 0.3f));
                sequence.Play();
            }
            else
            {
                tor.transform.position = position;
            }
        }

        public void Initialize(int torCount, int maxSteps)
        {
            _torCount = torCount;
            _currentSteps = 0;
            _maxSteps = maxSteps;
            _towerModel.MaxSteps = _maxSteps;
        }
        
        private void ColumnSelected(ColumnType columnType)
        {
            if (!_isBuffered)
            {
                Pop(columnType,true);
            }
            else
            {
                _currentSteps++;
                _towerModel.CurrentSteps.Value = _currentSteps;
                Push(columnType);
                CheckWin();
            }
        }

        private void Pop(ColumnType columnType, bool animate = false)
        {
            if (columnType == ColumnType.Left)
            {
                if (_leftColumn.Count > 0)
                {
                    var tor = _leftColumn.Pop();
                    _buffer = tor;
                    _isBuffered = true;
                    if (animate)
                    {
                        var sequence = DOTween.Sequence();
                        sequence.Append(tor.transform.DOMoveY(_bufferPoint.position.y, 0.3f));
                        sequence.Append(tor.transform.DOMoveX(_bufferPoint.position.x, 0.3f));
                        sequence.Play();   
                    }
                    else
                    {
                        tor.transform.position = _bufferPoint.position;
                    }
                }
            }
            if (columnType == ColumnType.Center)
            {
                if (_centerColumn.Count > 0)
                {
                    var tor = _centerColumn.Pop();
                    _buffer = tor;
                    _isBuffered = true;
                    if (animate)
                    {
                        var sequence = DOTween.Sequence();
                        sequence.Append(tor.transform.DOMoveY(_bufferPoint.position.y, 0.3f));
                        sequence.Append(tor.transform.DOMoveX(_bufferPoint.position.x, 0.3f));
                        sequence.Play();   
                    }
                    else
                    {
                        tor.transform.position = _bufferPoint.position;
                    }                }
            }
            if (columnType == ColumnType.Right)
            {
                if (_rightColumn.Count > 0)
                {
                    var tor = _rightColumn.Pop();
                    _buffer = tor;
                    _isBuffered = true;
                    if (animate)
                    {
                        var sequence = DOTween.Sequence();
                        sequence.Append(tor.transform.DOMoveY(_bufferPoint.position.y, 0.3f));
                        sequence.Append(tor.transform.DOMoveX(_bufferPoint.position.x, 0.3f));
                        sequence.Play();   
                    }
                    else
                    {
                        tor.transform.position = _bufferPoint.position;
                    }                }
            }
        }

        private void Push(ColumnType columnType)
        {
            if (_buffer != null)
            {
                if (columnType == ColumnType.Left)
                {
                    PushLeft(_buffer,true);
                    _buffer = null;
                    _isBuffered = false;
                }
                if (columnType == ColumnType.Center)
                {
                    PushCenter(_buffer,true);
                    _buffer = null;
                    _isBuffered = false;
                }
                if (columnType == ColumnType.Right)
                {
                    PushRight(_buffer,true);
                    _buffer = null;
                    _isBuffered = false;
                }
            }
        }

        private void CheckWin()
        {
            if (_rightColumn.Count == _torCount && _currentSteps <= _maxSteps)
            {
                bool win = true;
                var bufferArray = _rightColumn.ToArray();
                for (int i = 0; i < bufferArray.Length-1; i++)
                {
                    if (bufferArray[i].Number < bufferArray[i + 1].Number)
                    {
                        win = false;
                        break;
                    }
                    
                }

                if (win)
                {
                    _signalBus.Fire<LevelSignals.Win>();
                }
            }

            if (_currentSteps > _maxSteps)
            {
                _signalBus.Fire<LevelSignals.Lose>();
            }
        }
    }
}