# CalculatorWithAvaloniaUI
Make by Daniel Lima

# Relatório de Aprendizado: Criando uma Calculadora em C# com Avalonia e MVVM

## Introdução
Este relatório descreve os principais conceitos, boas práticas e padrões de projeto que um programador iniciante pode aprender ao desenvolver uma calculadora simples usando C# com Avalonia UI e o padrão MVVM (Model-View-ViewModel).

Através desse projeto, foram abordados conceitos fundamentais de:
- Linguagem C#
- Programação orientada a objetos (POO)
- Binding de dados
- Comandos (ICommand e RelayCommand)
- Notificação de mudanças de propriedades (INotifyPropertyChanged)
- Tipos genéricos
- Convenções de nomeação e boas práticas
- Padrão MVVM

Este relatório visa consolidar esse aprendizado para que o desenvolvedor possa aplicar esses conceitos em outros projetos de maior complexidade.

---

## 1. Estrutura do Projeto
A calculadora foi desenvolvida seguindo uma estrutura básica de MVVM:

- **Model**: Representa os dados e a lógica de negócios (não necessário para uma calculadora simples).
- **View**: Interface do usuário (UI).
- **ViewModel**: Faz a ligação entre a View e o Model, manipulando os dados e expondo comandos.

### Arquivos Principais:
- **MainWindow.axaml**: Definição da interface da calculadora.
- **MainWindowViewModel.cs**: Contém a lógica da calculadora e implementa a interface INotifyPropertyChanged para notificar mudanças na UI.
- **RelayCommand.cs**: Implementa a interface ICommand, permitindo a vinculação de comandos a botões na View.

---

## 2. Principais Conceitos Aplicados

### 2.1. Programação Orientada a Objetos (POO)
- **Classes**: Definem os objetos usados na aplicação, como `MainWindowViewModel` e `RelayCommand`.
- **Encapsulamento**: Propriedades privadas (_display) são expostas por meio de propriedades públicas (`Display`).
- **Herança**: A ViewModel implementa `INotifyPropertyChanged` para herdar seu comportamento.
- **Polimorfismo**: A interface `ICommand` é implementada por `RelayCommand`, permitindo que o mesmo padrão de comando seja reutilizado com diferentes lógicas.

### 2.2. Binding de Dados (Data Binding)
O **Binding** conecta a ViewModel à View, permitindo que a interface do usuário seja automaticamente atualizada quando o valor de uma propriedade muda.

**Exemplo:**
```csharp
public string Display
{
    get => _display;
    set => SetProperty(ref _display, value);
}
```
Quando `Display` é alterado, a interface reflete automaticamente essa mudança.

### 2.3. INotifyPropertyChanged
A interface `INotifyPropertyChanged` permite que a View seja notificada quando uma propriedade do ViewModel é modificada.

**Exemplo:**
```csharp
public event PropertyChangedEventHandler PropertyChanged;
protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
{
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
```
Sempre que uma propriedade muda, `OnPropertyChanged` é chamado para atualizar a interface.

### 2.4. Comandos (ICommand e RelayCommand)
Os comandos permitem a vinculação de ações a botões da interface.

**Exemplo:**
```csharp
public ICommand NumberCommand => new RelayCommand(param => AddNumber(param.ToString()));
```
Isso permite que, ao clicar em um botão, o método `AddNumber` seja executado.

A classe `RelayCommand` simplifica a implementação de `ICommand`, evitando a criação repetitiva de classes de comando para cada botão.

### 2.5. Tipos Genéricos (`<T>`)
Os tipos genéricos permitem que um método opere em diferentes tipos de dados sem duplicar código.

**Exemplo:**
```csharp
protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
{
    field = value;
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
```
Isso permite usar `SetProperty` para propriedades `string`, `int`, `bool`, etc.

### 2.6. Convenções e Boas Práticas
- **Uso de _ (underscore)**: Indica que a variável é privada (`_display`).
- **Propriedades públicas**: Permitem acesso aos campos privados de forma controlada (`Display`).
- **Nomes descritivos**: `AddNumber`, `SetOperator` e `Calculate` facilitam a compreensão do código.
- **Separar View de ViewModel**: Mantém o código organizado e facilita a manutenção.

---

## 3. Boas Práticas de Desenvolvimento

1. **Responsabilidade Única**: Cada classe tem uma responsabilidade clara. A ViewModel gerencia os dados e lógica, enquanto a View apenas exibe a interface.
2. **Reutilização de Código**: O uso de comandos genéricos (RelayCommand) evita a duplicação.
3. **Facilidade de Teste**: Com o ViewModel separado da View, é possível testá-lo sem interface gráfica.
4. **Manutenção Simples**: Ao utilizar MVVM, alterar a lógica da aplicação não requer grandes mudanças na interface.

---

## 4. Conclusão
Construir uma calculadora em C# usando Avalonia e MVVM é uma excelente forma de aprender conceitos essenciais de desenvolvimento de software.

Este projeto aborda desde conceitos básicos de POO até padrões de projeto mais avançados como MVVM e binding de dados.

Ao dominar esses conceitos, o programador estará preparado para desenvolver aplicações mais complexas e escaláveis, utilizando boas práticas e padrões da indústria.

