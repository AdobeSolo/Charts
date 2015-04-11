using System.Collections.Generic;
using System.Windows.Media;

namespace Cvte.Windows.Controls.Charts
{
    internal class ItemColorSet
    {
        private IList<string> _colorSet = new List<string>();
        private int _currentColorIndex;

        public int CurrrentColorIndex
        {
            get { return _currentColorIndex; }
            set { _currentColorIndex = value; }
        }

        public void SetColorSet(IList<string> colorSet)
        {
            _currentColorIndex = 0;
            if (colorSet == null)
            {
                _colorSet = new List<string>();
                return;
            }
            _colorSet = colorSet;
        }

        public Color GetCurrentColor()
        {
            if (_colorSet.Count > 0 && _currentColorIndex < _colorSet.Count)
            {
                // ReSharper disable once PossibleNullReferenceException
                var color = (Color)ColorConverter.ConvertFromString(_colorSet[_currentColorIndex]);
                _currentColorIndex = _currentColorIndex == _colorSet.Count - 1 ? 0 : _currentColorIndex + 1;
                return color;
            }
            // ReSharper disable once PossibleNullReferenceException
            return (Color)ColorConverter.ConvertFromString("#88b220");
        }
    }
}
