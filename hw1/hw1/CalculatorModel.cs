using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsCalculator
{
    public class CalculatorModel
    {
        public const char SPACE = ' ';
        public const char WAITING = '?';
        public const char DOT = '.';
        public const char ADDITION = '+';
        public const char SUBSTRACT = '-';
        public const char MULTIPLY = '*';
        public const char DIVISION = '/';
        public const char EQUAL = '=';
        public const string NONE = "";
        public const string ZERO_STRING = "0";
        public const double ZERO = 0;
        public CalculatorModel()
        {
            Clear();
        }
        // The action after user click any number button
        public void ClickNumberButton(int number)
        {
            if (_numberBuffer == NONE || (_textBoxString == ZERO_STRING && !_haveDot))
            {
                _textBoxString = NONE;
            }
            if (_theOperator == WAITING)
            {
                Clear();
            }
            _textBoxString += number.ToString();
            _numberBuffer += number.ToString();
        }

        // reset everything
        public void Clear()
        {
            _textBoxString = NONE;
            _haveFirstNumber = false;
            _haveSecondNumber = false;
            _haveDot = false;
            _theOperator = SPACE;
        }

        // clear error
        public void ClearError()
        {
            if (_haveFirstNumber && _theOperator != WAITING)
            {
                _textBoxString = NONE;
                ClickNumberButton((int)ZERO);
            }
        }

        // The action after user click '.' button
        public void ClickDot()
        {
            if (_haveDot)
            {
                return;
            }
            if (_theOperator == WAITING)
            {
                Clear();
                _numberBuffer = _textBoxString = ZERO_STRING + DOT;
                return;
            }
            _haveDot = true;
            _numberBuffer += DOT;
            _textBoxString += DOT;
        }

        // process calculation
        public void ProcessCalculation(char arithmeticOperator)
        {
            var result = Calculate(arithmeticOperator);
            _textBoxString = result.ToString();
            _firstNumber = result;
            _haveSecondNumber = false;
        }

        // calculate the result
        public double Calculate(char arithmeticOperator)
        {
            switch (arithmeticOperator)
            {
                case ADDITION:
                    return (_firstNumber + _secondNumber);
                case SUBSTRACT:
                    return (_firstNumber - _secondNumber);
                case MULTIPLY:
                    return (_firstNumber * _secondNumber);
                case DIVISION:
                    return (_firstNumber / _secondNumber);
                default:
                    return ZERO;
            }
        }

        // The action after user click any arithmetic operator
        public void ClickOperatorButton(char arithmetic) 
        {
            if ((!_haveFirstNumber && _numberBuffer != NONE) || (!_haveSecondNumber && _numberBuffer == NONE))
            {
                _theOperator = arithmetic;
            }
            LoadNumber();
            _numberBuffer = NONE;
            if (_haveFirstNumber && _haveSecondNumber)
            {
                ProcessCalculation(_theOperator);
                _theOperator = arithmetic == EQUAL ? WAITING : arithmetic;
            }
        }

        // Load number into memory
        private void LoadNumber()
        {
            if (!_haveFirstNumber && _numberBuffer != NONE)
            {
                _firstNumber = Double.Parse(_numberBuffer);
                _haveFirstNumber = true;
                _haveDot = false;
            }
            else if (!_haveSecondNumber && _numberBuffer != NONE)
            {
                _secondNumber = Double.Parse(_numberBuffer);
                _haveSecondNumber = true;
                _haveDot = false;
            }
        }

        private string _textBoxString;
        private string _numberBuffer;
        private char _theOperator;
        private bool _haveFirstNumber;
        private bool _haveSecondNumber;
        private bool _haveDot;
        private double _firstNumber;
        private double _secondNumber;

        public string Text
        { 
            get 
            { 
                return _textBoxString; 
            }
        }

    }
}
