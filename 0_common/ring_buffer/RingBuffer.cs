using Godot;
using Godot.Collections;
using System;

namespace Game.DepressionClimber
{
    public partial class RingBuffer
    {
        Array<string> _content;
        int _index = 0;
        int _size = 0;
        bool _filled = false;

        public RingBuffer(int size = 300)
        {
            _size = size;
            _content = new();
            _content.Resize(size);
        }

        public void Append(string value)
        {
            _content[_index] = value;
            if (_index + 1 < _size)
                _index += 1;
            else
            {
                _index = 0;
                _filled = true;
            }
        }

        public string Join()
        {
            string result = "";
            if (_filled)
            {
                for (int i = _index; i < _size; i++)
                {
                    result += _content[i];
                }
            }
            for (int i = 0; i < _index; i++)
            {
                result += _content[i];
            }
            return result;
        }

        public void Clear()
        {
            _index = 0;
            _filled = false;
        }
    }

}