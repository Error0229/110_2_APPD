using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsCalculator
{
    public class CalculatorModel
    {
        public CalculatorModel()
        {
            Clear();
        }
        // The action after user click any number button
        public void ProcessNumber(int number)
        {
            if (_numberBuffer == Constants.NONE || (_textBoxString == Constants.ZERO_STRING && !_haveDot))
            {
                _textBoxString = Constants.NONE;
            }
            if (_theOperator == Constants.WAITING)
            {
                Clear();
            }
            _textBoxString += number.ToString();
            _numberBuffer += number.ToString();
        }

        // reset everything
        public void Clear()
        {
            _numberBuffer = _textBoxString = Constants.ZERO_STRING;
            _haveFirstNumber = false;
            _haveSecondNumber = false;
            _haveDot = false;
            _theOperator = Constants.SPACE;
        }

        // clear error
        public void ClearError()
        {
            if (_haveFirstNumber && _theOperator != Constants.WAITING)
            {
                _textBoxString = Constants.NONE;
                ProcessNumber(Constants.ZERO);
            }
        }

        // The action after user click '.' button
        public void ClickDot()
        {
            if (_haveDot)
            {
                return;
            }
            _haveDot = true;
            if (_theOperator == Constants.WAITING)
            {
                Clear();
                _numberBuffer = _textBoxString = Constants.ZERO_STRING + Constants.DOT;
                return;
            }
            _numberBuffer += Constants.DOT;
            _textBoxString += Constants.DOT;
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
                case Constants.ADDITION:
                    return (_firstNumber + _secondNumber);
                case Constants.SUBSTRACT:
                    return (_firstNumber - _secondNumber);
                case Constants.MULTIPLY:
                    return (_firstNumber * _secondNumber);
                case Constants.DIVISION:
                    return (_firstNumber / _secondNumber);
                default:
                    return Constants.ZERO_DOUBLE;
            }
        }

        // The action after user click any arithmetic operator
        public void ClickOperatorButton(char arithmetic) 
        {
            if ((!_haveFirstNumber && _numberBuffer != Constants.NONE) || (!_haveSecondNumber && _numberBuffer == Constants.NONE))
            {
                _theOperator = arithmetic;
            }
            LoadNumber();
            _numberBuffer = Constants.NONE;
            if (_haveFirstNumber && _haveSecondNumber)
            {
                ProcessCalculation(_theOperator);
                _theOperator = arithmetic == Constants.EQUAL ? Constants.WAITING : arithmetic;
            }
        }

        // Load number into memory
        private void LoadNumber()
        {
            if (!_haveFirstNumber && _numberBuffer != Constants.NONE)
            {
                _firstNumber = Double.Parse(_numberBuffer);
                _haveFirstNumber = true;
                _haveDot = false;
            }
            else if (!_haveSecondNumber && _numberBuffer != Constants.NONE)
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
