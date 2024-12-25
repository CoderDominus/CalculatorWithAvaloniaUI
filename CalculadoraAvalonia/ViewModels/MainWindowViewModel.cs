using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity; // Necessário para interação com botões e comandos.

namespace CalculadoraAvalonia.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Inicialização de variáveis privadas usadas para controle interno.
        private string _display = "0";  // Inicializa o display com "0" para evitar valores nulos.
        private string? _currentOperator = string.Empty;  // Define como anulável (?) e inicializa com vazio.
        private double _firstOperand = 0;  // Garante que o primeiro operando tenha um valor padrão de 0.
        private bool _isOperatorPressed = false;  // Flag para identificar se um operador foi pressionado.

        // Propriedade Display que reflete o valor mostrado na interface.
        public string Display
        {
            get => _display;  // Retorna o valor atual de _display.
            set => SetProperty(ref _display, value);  // Atualiza o valor usando o método SetProperty.
        }

        // Cria comandos que são vinculados aos botões da interface.
        public ICommand NumberCommand => new RelayCommand(param => AddNumber(param?.ToString() ?? "0"));
        // Se param for null, usa "0" como valor padrão.
        
        public ICommand OperatorCommand => new RelayCommand(param => SetOperator(param?.ToString() ?? "+"));
        // Se param for null, usa "+" como operador padrão.
        
        public ICommand CalculateCommand => new RelayCommand(_ => Calculate());
        // Comando para realizar o cálculo.

        public ICommand ClearCommand => new RelayCommand(_ => Clear());
        // Comando para limpar o display.

        // Adiciona um número ao display ou substitui o valor caso um operador tenha sido pressionado.
        private void AddNumber(string number)
        {
            if (_isOperatorPressed || Display == "0")
                Display = number;  // Substitui o valor no display.
            else
                Display += number;  // Adiciona o número ao final do display.

            _isOperatorPressed = false;  // Reseta o flag de operador pressionado.
        }

        // Define o operador e armazena o primeiro operando.
        private void SetOperator(string op)
        {
            _firstOperand = double.Parse(Display);  // Converte o valor do display para double.
            _currentOperator = op;  // Define o operador atual.
            _isOperatorPressed = true;  // Sinaliza que um operador foi pressionado.
        }

        // Realiza o cálculo com base nos operandos e no operador.
        private void Calculate()
        {
            double secondOperand = double.Parse(Display);  // Obtém o segundo operando.
            switch (_currentOperator)
            {
                case "+": 
                    Display = (_firstOperand + secondOperand).ToString();
                    break;
                case "-": 
                    Display = (_firstOperand - secondOperand).ToString();
                    break;
                case "*": 
                    Display = (_firstOperand * secondOperand).ToString();
                    break;
                case "/": 
                    Display = secondOperand != 0 ? (_firstOperand / secondOperand).ToString() : "Assim não poha!";
                    break;  // Evita divisão por zero, exibindo "Erro".
            }
        }

        // Limpa o display e reseta operandos e operador.
        private void Clear()
        {
            Display = "0";  // Reseta o display para 0.
            _firstOperand = 0;  // Zera o primeiro operando.
            _currentOperator = string.Empty;  // Reseta o operador.
        }

        // Evento para notificar mudanças na interface.
        public event PropertyChangedEventHandler? PropertyChanged;

        // Método que notifica quando uma propriedade é alterada.
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            field = value;  // Atualiza o valor do campo.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  // Notifica a interface.
        }
    }

    // Classe responsável por implementar comandos reutilizáveis.
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;  // Define a ação a ser executada.
        private readonly Func<object?, bool>? _canExecute;  // Define se o comando pode ser executado.

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;  // Inicializa a ação.
            _canExecute = canExecute;  // Define a função que verifica se o comando pode ser executado.
        }

        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute(parameter);
        // Se _canExecute for null, o comando sempre pode ser executado.

        public void Execute(object? parameter) => _execute(parameter);
        // Executa a ação definida.

        public event EventHandler? CanExecuteChanged;
        // Evento disparado quando a habilidade de executar um comando muda.
    }
}
