using System;
using Column;
using UnityEngine;

namespace Tower
{
    public class TowerInputController : MonoBehaviour
    {
        private RaycastHit _hit;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out _hit, 100))
                {
                    if (_hit.collider.TryGetComponent(out ColumnView columnView))
                    {
                        columnView.Select();
                    }
                }
            }
        }
    }
}