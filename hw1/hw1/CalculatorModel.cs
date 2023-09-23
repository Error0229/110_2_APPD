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
        public const char DOT = '.';
        public const char ADDITION = '+';
        public const char SUBSTRACT = '-';
        public const char MULTIPLY = '*';
        public const char DIVISION = '/';
        public const char EQUAL = '=';
        
        public CalculatorModel() 
        {
            _haveNumber = false;
            _haveDot = false;
        }
        // The action after user click any number button
        public void ClickNumberButton(int number)
        {
            _textBoxString += number.ToString() + SPACE;
            _numberBuffer += number.ToString();
        }
        // The action after user click '.' button
        public void ClickDot()
        {
            if (_haveDot)
            {
                return;
            }
            _numberBuffer += DOT;
        }
        public void Calculate(char arithmeticOperator)
        {

        }
        // The action after user click any arithmetic operator
        public void ClickOperatorButton(char arithmetic) 
        {
            if (_haveNumber)
            {
                _secondNumber = Double.Parse(_numberBuffer);
                Calculate(arithmetic);
                return; 
            }
            else
            {
                _firstNumber = Double.Parse(_numberBuffer);
                _haveNumber = true;
            }
            
        }
        private string _textBoxString;
        private string _numberBuffer;
        private char _theOperator;
        private bool _haveNumber;
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
