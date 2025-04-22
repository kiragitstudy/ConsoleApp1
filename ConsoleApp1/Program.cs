using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ProgrammingTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            while (true)
            {
                Console.WriteLine("\n=== МЕНЮ ПРОГРАММЫ ===");
                Console.WriteLine("1. Синхронный автомат (Задание 10.1)");
                Console.WriteLine("2. Наследование классов - Работники (Задание 11.2)");
                Console.WriteLine("3. Полиморфизм методов - Полководец (Задание 11.3)");
                Console.WriteLine("4. Полиморфизм - Самолеты (Задание 10)");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите задание: ");
                
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        RunAutomatonTask();
                        break;
                    case "2":
                        RunInheritanceTask();
                        break;
                    case "3":
                        RunPolymorphismCommanderTask();
                        break;
                    case "4":
                        RunAircraftTask();
                        break;
                    case "0":
                        Console.WriteLine("Программа завершена.");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
        
        #region Задание 10.1: Синхронный автомат
        
        // Перечисление для состояний автомата
        enum State { A, B, C }
        
        static void RunAutomatonTask()
        {
            Console.Clear();
            Console.WriteLine("=== Задание 10.1: Синхронный автомат ===\n");
            
            // Создание и запуск автомата
            SynchronousAutomaton automaton = new SynchronousAutomaton();
            
            // Меню для тестирования автомата
            while (true)
            {
                Console.WriteLine("\nТекущее состояние: " + automaton.CurrentState);
                Console.WriteLine("1. Ввести входной сигнал (3 бита, например: 010)");
                Console.WriteLine("2. Сбросить автомат в начальное состояние");
                Console.WriteLine("3. Запустить демонстрационную последовательность");
                Console.WriteLine("0. Вернуться в главное меню");
                
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        Console.Write("Введите входной сигнал (3 бита): ");
                        string input = Console.ReadLine();
                        if (input.Length == 3 && input.All(c => c == '0' || c == '1'))
                        {
                            string output = automaton.ProcessInput(input);
                            Console.WriteLine($"Выходной сигнал: {output}");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: Входной сигнал должен состоять ровно из 3 бит (0 и 1)");
                        }
                        break;
                    case "2":
                        automaton.Reset();
                        Console.WriteLine("Автомат сброшен в начальное состояние A");
                        break;
                    case "3":
                        RunAutomatonDemo(automaton);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
        
        // Демонстрационная последовательность для автомата
        static void RunAutomatonDemo(SynchronousAutomaton automaton)
        {
            Console.WriteLine("\n=== Демонстрация работы автомата ===");
            automaton.Reset();
            
            // Демонстрация всех возможных переходов из диаграммы
            string[] testInputs = { "110", "010", "011", "011", "111", "010", "110" };
            
            Console.WriteLine("Начальное состояние: " + automaton.CurrentState);
            
            foreach (string input in testInputs)
            {
                string output = automaton.ProcessInput(input);
                Console.WriteLine($"Вход: {input}, Состояние: {automaton.CurrentState}, Выход: {output}");
            }
            
            Console.WriteLine("=== Демонстрация завершена ===");
        }
        
        // Класс, реализующий синхронный автомат по диаграмме из задания
        class SynchronousAutomaton
        {
            public State CurrentState { get; private set; }
            
            // Конструктор - устанавливает начальное состояние A
            public SynchronousAutomaton()
            {
                CurrentState = State.A;
            }
            
            // Метод для сброса автомата в начальное состояние
            public void Reset()
            {
                CurrentState = State.A;
            }
            
            // Обработка входного сигнала и возврат выходного
            public string ProcessInput(string input)
            {
                // Проверка корректности входного сигнала
                if (input.Length != 3 || !input.All(c => c == '0' || c == '1'))
                {
                    throw new ArgumentException("Входной сигнал должен состоять из 3 бит (0 и 1)");
                }
                
                // Определение следующего состояния и выходного сигнала по таблице переходов
                switch (CurrentState)
                {
                    case State.A:
                        if (input == "110")
                        {
                            // Остаемся в состоянии A (петля)
                            return "110";
                        }
                        else if (input == "010")
                        {
                            // Переход в состояние B
                            CurrentState = State.B;
                            return "110";
                        }
                        else if (input == "011")
                        {
                            // Переход в состояние C
                            CurrentState = State.C;
                            return "011";
                        }
                        break;
                        
                    case State.B:
                        if (input == "011" || input == "111")
                        {
                            // Переход в состояние C
                            CurrentState = State.C;
                            return input;
                        }
                        else if (input == "010")
                        {
                            // Остаемся в состоянии B
                            return "010";
                        }
                        break;
                        
                    case State.C:
                        if (input == "010" || input == "110")
                        {
                            // Переход в состояние A
                            CurrentState = State.A;
                            return input;
                        }
                        else if (input == "011" || input == "111")
                        {
                            // Остаемся в состоянии C
                            return input;
                        }
                        break;
                }
                
                // В случае неопределенного перехода возвращаем входной сигнал
                return input;
            }
        }
        
        #endregion
        
        #region Задание 11.2: Наследование классов - Работники
        
        static void RunInheritanceTask()
        {
            Console.Clear();
            Console.WriteLine("=== Задание 11.2: Наследование классов - Работники ===\n");
            
            while (true)
            {
                Console.WriteLine("\nМеню для работы с классами:");
                Console.WriteLine("1. Демонстрация работы с сотрудниками");
                Console.WriteLine("2. Создать новых сотрудников");
                Console.WriteLine("3. Применить повышение окладов");
                Console.WriteLine("4. Рассчитать потенциальную энергию");

                Console.WriteLine("0. Вернуться в главное меню");
                
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        DemonstrateEmployeeClasses();
                        break;
                    case "2":
                        CreateEmployees();
                        break;
                    case "3":
                        ApplySalaryIncrease();
                        break;
                    case "4":
                        CalculatePotentialEnergy();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
        
        static List<Employee> employees = new List<Employee>
        {
            new Employee("Иванов", "Менеджер", 50000),
            new Employee("Петров", "Дизайнер", 45000),
            new Employee("Сидоров", "Разработчик", 70000),
            new MovingEmployee("Иванченко", "Инженер", 65000, 10.5),
            new MovingEmployee("Петренко", "Техник", 48000, 15.2),
            new EnterpriseEmployee("Смирнова", "Руководитель", 80000, 95),
            new EnterpriseEmployee("Козлов", "Бухгалтер", 60000, 73),
            new EnterpriseEmployee("Васильев", "Аналитик", 65000, 85)
        };
        
        static void DemonstrateEmployeeClasses()
        {
            Console.WriteLine("\n=== Демонстрация работы с сотрудниками ===");
            
            Console.WriteLine("\nСписок всех сотрудников:");
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.GetInfo());
            }
            
            Console.WriteLine("\nДемонстрация родительского класса Employee:");
            Employee emp = new Employee("Тестов", "Тестировщик", 55000);
            Console.WriteLine("Создан новый сотрудник: " + emp.GetInfo());
            emp.Salary = 60000;
            Console.WriteLine("Изменен оклад: " + emp.GetInfo());
            
            Console.WriteLine("\nДемонстрация класса-потомка EnterpriseEmployee:");
            EnterpriseEmployee entEmp = new EnterpriseEmployee("Новиков", "Программист", 75000, 88);
            Console.WriteLine("Создан новый сотрудник предприятия: " + entEmp.GetInfo());
            
            Console.WriteLine("\nДемонстрация класса-потомка EnterpriseEmployee:");
            MovingEmployee movEmp = new MovingEmployee("Высокий", "Дизайнер", 99999, 120);
            Console.WriteLine("Создан новый сотрудник с высотой расположения: " + movEmp.GetInfo());
            
            Console.WriteLine("\nДемонстрация увеличения оклада:");
            double oldSalary = entEmp.Salary;
            entEmp.IncreaseSalary();
            Console.WriteLine($"Оклад до изменения: {oldSalary}, после увеличения: {entEmp.Salary}");
            
            Console.WriteLine("\n=== Демонстрация завершена ===");
        }
        
        static void CreateEmployees()
        {
            Console.WriteLine("\n=== Создание новых сотрудников ===");
            
            Console.WriteLine("Выберите тип сотрудника:");
            Console.WriteLine("1. Обычный сотрудник");
            Console.WriteLine("2. Сотрудник предприятия");
            Console.WriteLine("3. Сотрудник с высотой расположения");
            
            string type = Console.ReadLine();
            
            Console.Write("Введите фамилию: ");
            string name = Console.ReadLine();
            
            Console.Write("Введите должность: ");
            string position = Console.ReadLine();
            
            Console.Write("Введите оклад: ");
            if (!double.TryParse(Console.ReadLine(), out double salary))
            {
                Console.WriteLine("Ошибка: Неверный формат оклада.");
                return;
            }
            
            if (type == "1")
            {
                Employee newEmployee = new Employee(name, position, salary);
                employees.Add(newEmployee);
                Console.WriteLine("Создан новый сотрудник: " + newEmployee.GetInfo());
            }
            else if (type == "2")
            {
                Console.Write("Введите рейтинг (от 0 до 100): ");
                if (!int.TryParse(Console.ReadLine(), out int rating) || rating < 0 || rating > 100)
                {
                    Console.WriteLine("Ошибка: Неверный формат рейтинга.");
                    return;
                }
                
                EnterpriseEmployee newEmployee = new EnterpriseEmployee(name, position, salary, rating);
                employees.Add(newEmployee);
                Console.WriteLine("Создан новый сотрудник предприятия: " + newEmployee.GetInfo());
            }
            else if (type == "3")
            {
                Console.Write("Введите высоту расположения (метры): ");
                if (!double.TryParse(Console.ReadLine(), out double height) || height < 0)
                {
                    Console.WriteLine("Ошибка: Неверное значение высоты.");
                    return;
                }
            
                MovingEmployee newEmployee = new MovingEmployee(name, position, salary, height);
                employees.Add(newEmployee);
                Console.WriteLine("Создан новый сотрудник с высотой расположения: " + newEmployee.GetInfo());
            }
            else
            {
                Console.WriteLine("Неверный выбор типа сотрудника.");
            }
        }
        
        static void ApplySalaryIncrease()
        {
            Console.WriteLine("\n=== Применение повышения окладов ===");
            
            Console.WriteLine("Оклады до повышения:");
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.GetInfo());
            }
            
            // Применяем повышение окладов для сотрудников с рейтингом
            foreach (var employee in employees)
            {
                if (employee is EnterpriseEmployee enterpriseEmployee)
                {
                    enterpriseEmployee.IncreaseSalary();
                }
            }
            
            Console.WriteLine("\nОклады после повышения:");
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.GetInfo());
            }
        }
        
        class Employee
        {
            public string Name { get; set; }
            public string Position { get; set; }
            public double Salary { get; set; }
            
            public Employee(string name, string position, double salary)
            {
                Name = name;
                Position = position;
                Salary = salary;
            }
            
            public virtual string GetInfo()
            {
                return $"{Name}, {Position}, оклад: {Salary:C}";
            }
        }
        
        // Класс-потомок для расчета потенциальной энергии
        class MovingEmployee : Employee
        {
            public double Height { get; set; }
    
            public MovingEmployee(string name, string position, double salary, double height)
                : base(name, position, salary)
            {
                Height = height;
            }
    
            // Метод для определения потенциальной энергии
            // E = m * g * h
            public double CalculatePotentialEnergy(double mass = 0)
            {
                if (mass <= 0) mass = Salary / 1000;
                const double g = 9.8;
                return mass * g * Height;
            }
            public override string GetInfo()
            {
                return $"{base.GetInfo()}, высота: {Height} м";
            }
        }
        
        static void CalculatePotentialEnergy()
        {
            Console.WriteLine("\n=== Расчет потенциальной энергии ===");
            
            Console.WriteLine("\nСотрудники с высотой расположения:");
            int index = 1;
            List<MovingEmployee> movingEmployees = new List<MovingEmployee>();
            
            foreach (var employee in employees)
            {
                if (employee is MovingEmployee movingEmployee)
                {
                    Console.WriteLine($"{index}. {movingEmployee.GetInfo()}");
                    movingEmployees.Add(movingEmployee);
                    index++;
                }
            }
            
            if (movingEmployees.Count == 0)
            {
                Console.WriteLine("Нет сотрудников с высотой расположения.");
                return;
            }
            
            Console.Write("\nВыберите номер сотрудника для расчета: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > movingEmployees.Count)
            {
                Console.WriteLine("Ошибка: Неверный выбор.");
                return;
            }
            
            MovingEmployee selected = movingEmployees[choice - 1];
            
            Console.Write("Введите массу (кг) или оставьте пустым для использования значения по умолчанию: ");
            string massInput = Console.ReadLine();
            double mass = 0;
            
            if (!string.IsNullOrWhiteSpace(massInput))
            {
                if (!double.TryParse(massInput, out mass) || mass <= 0)
                {
                    Console.WriteLine("Ошибка: Неверное значение массы.");
                    return;
                }
            }
            
            double energy = selected.CalculatePotentialEnergy(mass);
            Console.WriteLine($"Потенциальная энергия: {energy:F2} Дж");
        }
        
        class EnterpriseEmployee : Employee
        {
            public int Rating { get; set; }
            
            public EnterpriseEmployee(string name, string position, double salary, int rating)
                : base(name, position, salary)
            {
                Rating = rating;
            }
            
            public override string GetInfo()
            {
                return $"{base.GetInfo()}, рейтинг: {Rating}/100";
            }
            
            public void IncreaseSalary()
            {
                if (Rating >= 90 && Rating <= 100)
                {
                    Salary *= 1.6;
                }
                else if (Rating >= 75 && Rating < 90)
                {
                    Salary *= 1.4;
                }
                else if (Rating >= 60 && Rating < 75)
                {
                    Salary *= 1.2;
                }
            }
        }
        
        #endregion
        
        #region Задание 11.3: Полиморфизм методов - Полководец
        
        static void RunPolymorphismCommanderTask()
        {
            Console.Clear();
            Console.WriteLine("=== Задание 11.3: Полиморфизм методов - Полководец ===\n");
            
            while (true)
            {
                Console.WriteLine("\nМеню для работы с классами полководцев:");
                Console.WriteLine("1. Демонстрация работы с классами полководцев");
                Console.WriteLine("2. Создать нового полководца");
                Console.WriteLine("3. Создать нового полководца с дополнительным полем");
                Console.WriteLine("0. Вернуться в главное меню");
                
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        DemonstrateCommanderClasses();
                        break;
                    case "2":
                        CreateCommander();
                        break;
                    case "3":
                        CreateAdvancedCommander();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
        
        static List<Commander> commanders = new List<Commander>
        {
            new Commander("Суворов", 60, 40),
            new Commander("Кутузов", 45, 30),
            new Commander("Жуков", 120, 70),
            new AdvancedCommander("Наполеон", 80, 55, 25),
            new AdvancedCommander("Александр Македонский", 35, 30, 5),
            new AdvancedCommander("Цезарь", 50, 40, 10)
        };
        
        static void DemonstrateCommanderClasses()
        {
            Console.WriteLine("\n=== Демонстрация работы с полководцами ===");
            
            Console.WriteLine("\nСписок всех полководцев:");
            foreach (var commander in commanders)
            {
                Console.WriteLine(commander.GetInfo());
                Console.WriteLine($"Качество: {((Commander)commander).GetQuality():F3}");
                
                if (commander is AdvancedCommander advancedCommander)
                {
                    Console.WriteLine($"Улучшенное качество: {advancedCommander.GetQuality():F3}");
                }
                Console.WriteLine();
            }
            
            Console.WriteLine("\nСоздание нового полководца базового класса:");
            Commander newCommander = new Commander("Тестовый", 30, 20);
            Console.WriteLine(newCommander.GetInfo());
            Console.WriteLine($"Качество: {newCommander.GetQuality():F3}");
            
            Console.WriteLine("\nСоздание нового полководца с дополнительным полем:");
            AdvancedCommander advCommander = new AdvancedCommander("Тестовый-2", 40, 35, 15);
            Console.WriteLine(advCommander.GetInfo());
            Console.WriteLine($"Базовое качество (формула Q): {((Commander)advCommander).GetQuality():F3}");
            Console.WriteLine($"Улучшенное качество (формула Qp): {advCommander.GetQuality():F3}");
            
            Console.WriteLine("\n=== Демонстрация завершена ===");
        }
        
        static void CreateCommander()
        {
            Console.WriteLine("\n=== Создание нового полководца ===");
            
            Console.Write("Введите фамилию: ");
            string name = Console.ReadLine();
            
            Console.Write("Введите количество битв: ");
            if (!int.TryParse(Console.ReadLine(), out int battles) || battles < 0)
            {
                Console.WriteLine("Ошибка: Неверный формат числа битв.");
                return;
            }
            
            Console.Write("Введите количество побед: ");
            if (!int.TryParse(Console.ReadLine(), out int wins) || wins < 0 || wins > battles)
            {
                Console.WriteLine("Ошибка: Неверное количество побед (должно быть не больше количества битв).");
                return;
            }
            
            Commander newCommander = new Commander(name, battles, wins);
            commanders.Add(newCommander);
            
            Console.WriteLine("\nСоздан новый полководец:");
            Console.WriteLine(newCommander.GetInfo());
            Console.WriteLine($"Качество: {newCommander.GetQuality():F3}");
        }
        
        static void CreateAdvancedCommander()
        {
            Console.WriteLine("\n=== Создание нового полководца с дополнительным полем ===");
            
            Console.Write("Введите фамилию: ");
            string name = Console.ReadLine();
            
            Console.Write("Введите количество битв: ");
            if (!int.TryParse(Console.ReadLine(), out int battles) || battles < 0)
            {
                Console.WriteLine("Ошибка: Неверный формат числа битв.");
                return;
            }
            
            Console.Write("Введите количество побед: ");
            if (!int.TryParse(Console.ReadLine(), out int wins) || wins < 0 || wins > battles)
            {
                Console.WriteLine("Ошибка: Неверное количество побед (должно быть не больше количества битв).");
                return;
            }
            
            Console.Write("Введите количество побед с меньшими силами: ");
            if (!int.TryParse(Console.ReadLine(), out int minorityWins) || minorityWins < 0 || minorityWins > wins)
            {
                Console.WriteLine("Ошибка: Неверное количество побед с меньшими силами (должно быть не больше общего количества побед).");
                return;
            }
            
            AdvancedCommander newCommander = new AdvancedCommander(name, battles, wins, minorityWins);
            commanders.Add(newCommander);
            
            Console.WriteLine("\nСоздан новый полководец с дополнительным полем:");
            Console.WriteLine(newCommander.GetInfo());
            Console.WriteLine($"Базовое качество (формула Q): {((Commander)newCommander).GetQuality():F3}");
            Console.WriteLine($"Улучшенное качество (формула Qp): {newCommander.GetQuality():F3}");
        }
        
        class Commander
        {
            public string Name { get; set; }
            public int Battles { get; set; }
            public int Wins { get; set; }
            
            public Commander(string name, int battles, int wins)
            {
                Name = name;
                Battles = battles;
                Wins = wins;
            }
            
            public virtual double GetQuality()
            {
                if (Battles == 0) return 0;
                return Math.Pow(Wins, 2) / Battles;
            }
            
            public virtual string GetInfo()
            {
                return $"Полководец: {Name}, Битв: {Battles}, Побед: {Wins}";
            }
        }
        
        class AdvancedCommander : Commander
        {
            public int MinorityWins { get; set; }
            
            public AdvancedCommander(string name, int battles, int wins, int minorityWins)
                : base(name, battles, wins)
            {
                MinorityWins = minorityWins;
            }
            
            public override double GetQuality()
            {
                if (Battles == 0) return 0;
                return Math.Pow(MinorityWins, 2) / Battles + base.GetQuality();
            }
            
            public override string GetInfo()
            {
                return $"{base.GetInfo()}, Побед с меньшими силами: {MinorityWins}";
            }
        }
        
        #endregion
        
        #region Задание 10: Полиморфизм - Самолеты
        
        static void RunAircraftTask()
        {
            Console.Clear();
            Console.WriteLine("=== Задание 10: Полиморфизм - Самолеты ===\n");
            
            // Создаем самолеты разных типов для демонстрации
            Aircraft plane = new Aircraft("Boeing", "747", 980, 13100);
            Bomber bomber = new Bomber("Ту", "160", 2200, 19700);
            Fighter fighter = new Fighter("МиГ", "29", 2450, 18000);
            
            while (true)
            {
                Console.WriteLine("\nМеню для работы с самолетами:");
                Console.WriteLine("1. Показать информацию о всех самолетах");
                Console.WriteLine("2. Создать новый самолет базового класса");
                Console.WriteLine("3. Создать новый бомбардировщик");
                Console.WriteLine("4. Создать новый истребитель");
                Console.WriteLine("5. Демонстрация полиморфизма (стоимость самолетов)");
                Console.WriteLine("0. Вернуться в главное меню");
                
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        ShowAircraftInfo(plane, bomber, fighter);
                        break;
                    case "2":
                        plane = CreateAircraft();
                        break;
                    case "3":
                        bomber = CreateBomber();
                        break;
                    case "4":
                        fighter = CreateFighter();
                        break;
                    case "5":
                        DemonstrateAircraftPolymorphism(plane, bomber, fighter);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
        
        static void ShowAircraftInfo(Aircraft plane, Bomber bomber, Fighter fighter)
        {
            Console.WriteLine("\n=== Информация о самолетах ===");
            
            Console.WriteLine("\nБазовый самолет:");
            Console.WriteLine(plane.GetInfo());
            Console.WriteLine($"Стоимость: {plane.GetCost():C}");
            
            Console.WriteLine("\nБомбардировщик:");
            Console.WriteLine(bomber.GetInfo());
            Console.WriteLine($"Стоимость: {bomber.GetCost():C}");
            
            Console.WriteLine("\nИстребитель:");
            Console.WriteLine(fighter.GetInfo());
            Console.WriteLine($"Стоимость: {fighter.GetCost():C}");
        }
        
        static Aircraft CreateAircraft()
        {
            Console.WriteLine("\n=== Создание нового самолета ===");
            
            Console.Write("Введите марку: ");
            string brand = Console.ReadLine();
            
            Console.Write("Введите модель: ");
            string model = Console.ReadLine();
            
            Console.Write("Введите максимальную скорость (км/ч): ");
            if (!double.TryParse(Console.ReadLine(), out double maxSpeed) || maxSpeed <= 0)
            {
                Console.WriteLine("Ошибка: Неверное значение скорости.");
                return new Aircraft("Неизвестно", "Неизвестно", 800, 10000);
            }
            
            Console.Write("Введите максимальную высоту (метров): ");
            if (!double.TryParse(Console.ReadLine(), out double maxAltitude) || maxAltitude <= 0)
            {
                Console.WriteLine("Ошибка: Неверное значение высоты.");
                return new Aircraft("Неизвестно", "Неизвестно", 800, 10000);
            }
            
            Aircraft newAircraft = new Aircraft(brand, model, maxSpeed, maxAltitude);
            Console.WriteLine("\nСоздан новый самолет:");
            Console.WriteLine(newAircraft.GetInfo());
            
            return newAircraft;
        }
        
        static Bomber CreateBomber()
        {
            Console.WriteLine("\n=== Создание нового бомбардировщика ===");
            
            Console.Write("Введите марку: ");
            string brand = Console.ReadLine();
            
            Console.Write("Введите модель: ");
            string model = Console.ReadLine();
            
            Console.Write("Введите максимальную скорость (км/ч): ");
            if (!double.TryParse(Console.ReadLine(), out double maxSpeed) || maxSpeed <= 0)
            {
                Console.WriteLine("Ошибка: Неверное значение скорости.");
                return new Bomber("Неизвестно", "Неизвестно", 1200, 12000);
            }
            
            Console.Write("Введите максимальную высоту (метров): ");
            if (!double.TryParse(Console.ReadLine(), out double maxAltitude) || maxAltitude <= 0)
            {
                Console.WriteLine("Ошибка: Неверное значение высоты.");
                return new Bomber("Неизвестно", "Неизвестно", 1200, 12000);
            }
            
            Bomber newBomber = new Bomber(brand, model, maxSpeed, maxAltitude);
            Console.WriteLine("\nСоздан новый бомбардировщик:");
            Console.WriteLine(newBomber.GetInfo());
            
            return newBomber;
        }
        
        static Fighter CreateFighter()
        {
            Console.WriteLine("\n=== Создание нового истребителя ===");
            
            Console.Write("Введите марку: ");
            string brand = Console.ReadLine();
            
            Console.Write("Введите модель: ");
            string model = Console.ReadLine();
            
            Console.Write("Введите максимальную скорость (км/ч): ");
            if (!double.TryParse(Console.ReadLine(), out double maxSpeed) || maxSpeed <= 0)
            {
                Console.WriteLine("Ошибка: Неверное значение скорости.");
                return new Fighter("Неизвестно", "Неизвестно", 2000, 15000);
            }
            
            Console.Write("Введите максимальную высоту (метров): ");
            if (!double.TryParse(Console.ReadLine(), out double maxAltitude) || maxAltitude <= 0)
            {
                Console.WriteLine("Ошибка: Неверное значение высоты.");
                return new Fighter("Неизвестно", "Неизвестно", 2000, 15000);
            }
            
            Fighter newFighter = new Fighter(brand, model, maxSpeed, maxAltitude);
            Console.WriteLine("\nСоздан новый истребитель:");
            Console.WriteLine(newFighter.GetInfo());
            
            return newFighter;
        }
        
        static void DemonstrateAircraftPolymorphism(Aircraft plane, Bomber bomber, Fighter fighter)
        {
            Console.WriteLine("\n=== Демонстрация полиморфизма ===");
            
            Aircraft[] planes = { plane, bomber, fighter };
            
            Console.WriteLine("\nРасчет стоимости для разных типов самолетов:");
            foreach (var aircraft in planes)
            {
                Console.WriteLine($"{aircraft.GetType().Name}: {aircraft.GetInfo()}");
                Console.WriteLine($"Стоимость: {aircraft.GetCost():C}");
                Console.WriteLine();
            }
            
            Console.WriteLine("=== Демонстрация завершена ===");
        }
        
        class Aircraft
        {
            public string Brand { get; set; }
            public string Model { get; set; }
            public double MaxSpeed { get; set; }
            public double MaxAltitude { get; set; }
            
            public Aircraft(string brand, string model, double maxSpeed, double maxAltitude)
            {
                Brand = brand;
                Model = model;
                MaxSpeed = maxSpeed;
                MaxAltitude = maxAltitude;
            }
            
            public virtual double GetCost()
            {
                return MaxSpeed * 1000 + MaxAltitude * 100;
            }
            
            public virtual string GetInfo()
            {
                return $"Марка: {Brand}, Модель: {Model}, " +
                       $"Макс. скорость: {MaxSpeed} км/ч, Макс. высота: {MaxAltitude} м";
            }
        }
        
        class Bomber : Aircraft
        {
            public Bomber(string brand, string model, double maxSpeed, double maxAltitude)
                : base(brand, model, maxSpeed, maxAltitude)
            {
            }
            
            public override double GetCost()
            {
                return base.GetCost() * 2;
            }
            
            public override string GetInfo()
            {
                return $"Бомбардировщик: {base.GetInfo()}";
            }
        }
        
        class Fighter : Aircraft
        {
            public Fighter(string brand, string model, double maxSpeed, double maxAltitude)
                : base(brand, model, maxSpeed, maxAltitude)
            {
            }
            
            public override double GetCost()
            {
                return base.GetCost() * 3;
            }
            
            public override string GetInfo()
            {
                return $"Истребитель: {base.GetInfo()}";
            }
        }
        
        #endregion
    }
}