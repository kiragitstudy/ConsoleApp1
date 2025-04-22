using System;
using System.Collections.Generic;

namespace UnifiedProject
{
    // Классы для автомобилей и фотоаппаратов
    public class Avto
    {
        public string Marka { get; set; }
        public double Moshnost { get; set; }
        public int Mesta { get; set; }

        public Avto(string marka, double moshnost, int mesta)
        {
            Marka = marka;
            Moshnost = moshnost;
            Mesta = mesta;
        }

        public virtual double Q() => 0.1 * Moshnost - Mesta;
    }

    public class AvtoProizvodstvo : Avto
    {
        public int GodProizvodstva { get; set; }

        public AvtoProizvodstvo(string marka, double moshnost, int mesta, int godProizvodstva)
            : base(marka, moshnost, mesta) => GodProizvodstva = godProizvodstva;

        public double Qp()
        {
            int currentYear = DateTime.Now.Year;
            return base.Q() - 1.5 * (currentYear - GodProizvodstva);
        }
    }

    public class Camera
    {
        public string Model { get; set; }
        public double Zoom { get; private set; }
        public string BodyMaterial { get; set; }

        public Camera(string model, double zoom, string bodyMaterial)
        {
            Model = model;
            if (zoom < 1 || zoom > 35) throw new ArgumentException("Zoom должен быть от 1 до 35");
            Zoom = zoom;
            BodyMaterial = bodyMaterial;
        }

        public virtual double CalculatePrice() => BodyMaterial == "plastic" ? (Zoom + 2) * 10 : (Zoom + 2) * 15;
        public string GetInfo() => $"Модель: {Model}, Zoom: {Zoom}x, Стоимость: ${CalculatePrice():F2}";
        public bool IsExpensive() => CalculatePrice() > 200;
    }

    public class DigitalCamera : Camera
    {
        public int Megapixels { get; private set; }

        public DigitalCamera(string model, double zoom, string bodyMaterial, int megapixels)
            : base(model, zoom, bodyMaterial) => Megapixels = megapixels;

        public override double CalculatePrice() => base.CalculatePrice() * Megapixels;
        public void UpgradeModel() => Megapixels += 2;
    }

    // Классы для чисел, сотрудников и автоматов
    public class BaseClass
    {
        public double X { get; set; }
        public double Y { get; set; }

        public BaseClass(double x, double y)
        {
            X = x;
            Y = y;
        }

        public virtual double Calculate() => X + Y;
    }

    public class DerivedClass : BaseClass
    {
        public double Z { get; set; }

        public DerivedClass(double x, double y, double z) : base(x, y) => Z = z;
        public override double Calculate() => Z == 0 ? throw new DivideByZeroException("Z не может быть нулём") : X / Z;
    }

    public class Employee
    {
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public int JoinYear { get; set; }

        public Employee(string lastName, decimal salary, int joinYear)
        {
            LastName = lastName;
            Salary = salary;
            JoinYear = joinYear;
        }

        public virtual string GetInfo() => $"Фамилия: {LastName}, Оклад: {Salary:C}$, Год приёма: {JoinYear}";
    }

    public class EnterpriseEmployee : Employee
    {
        public int BirthYear { get; set; }
        private int CurrentYear => DateTime.Now.Year;

        public EnterpriseEmployee(string lastName, decimal salary, int joinYear, int birthYear)
            : base(lastName, salary, joinYear) => BirthYear = birthYear;

        public (int Years, string Description) CalculateWorkYears()
        {
            int age = CurrentYear - BirthYear;
            int yearsTo60 = 60 - age;
            return yearsTo60 > 0 
                ? (yearsTo60, $"До пенсии: {yearsTo60} лет") 
                : (CurrentYear - JoinYear, $"Стаж после 60: {CurrentYear - JoinYear} лет");
        }

        public override string GetInfo() => base.GetInfo() + $", Год рождения: {BirthYear}";
    }

    public class StateMachine
    {
        protected int[,] transitionTable;
        protected int currentState;

        public StateMachine(int[,] transitions, int initialState)
        {
            transitionTable = transitions;
            currentState = initialState;
        }

        public virtual void ProcessInput(string input)
        {
            Console.WriteLine("Начальное состояние: " + currentState);
            foreach (char symbol in input)
            {
                int symbolIndex = symbol switch
                {
                    'a' => 0, 'b' => 1, 'c' => 2,
                    _ => throw new ArgumentException($"Неизвестный символ: {symbol}")
                };

                if (currentState >= transitionTable.GetLength(0))
                    throw new InvalidOperationException("Несуществующее состояние");

                int nextState = transitionTable[currentState, symbolIndex];
                Console.WriteLine($"Символ '{symbol}': {currentState} -> {nextState}");
                currentState = nextState;
            }
        }

        public int GetCurrentState() => currentState;
    }

    public class EnhancedStateMachine : StateMachine
    {
        private List<string> history = new List<string>();

        public EnhancedStateMachine(int[,] transitions, int initialState) : base(transitions, initialState) { }

        public override void ProcessInput(string input)
        {
            history.Clear();
            history.Add($"Начальное состояние: {currentState}");
            foreach (char symbol in input)
            {
                base.ProcessInput(symbol.ToString());
                history.Add($"Текущее состояние: {currentState}");
            }
        }

        public void PrintHistory()
        {
            Console.WriteLine("\nИстория переходов:");
            foreach (var entry in history) Console.WriteLine(entry);
        }
    }

    class tttttt
    {
        static void ttt()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ГЛАВНОЕ МЕНЮ ===");
                Console.WriteLine("1 - Полиморфизм: Автомобили и фотоаппараты");
                Console.WriteLine("2 - Наследование: Числа и сотрудники");
                Console.WriteLine("3 - Автомат: Синхронный автомат");
                Console.WriteLine("4 - Выход");
                Console.Write("Выберите: ");

                switch (Console.ReadLine())
                {
                    case "1": RunAvtoCameraDemo(); break;
                    case "2": RunNumberEmployeeDemo(); break;
                    case "3": RunStateMachineDemo(); break;
                    case "4": return;
                    default: ShowError("Неверный выбор!"); break;
                }
            }
        }

        static void RunAvtoCameraDemo()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== АВТОМОБИЛИ И ФОТОАППАРАТЫ ===");
                Console.WriteLine("1 - Создать автомобиль (Базовый уровень)");
                Console.WriteLine("2 - Создать фотоаппарат (Средний уровень)");
                Console.WriteLine("3 - Назад");
                Console.Write("Выберите: ");

                switch (Console.ReadLine())
                {
                    case "1": CreateAvto(); break;
                    case "2": CreateCamera(); break;
                    case "3": return;
                    default: ShowError("Неверный выбор!"); break;
                }
            }
        }

        static void CreateAvto()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== СОЗДАНИЕ АВТОМОБИЛЯ ===");
                string marka = ReadString("Марка: ");
                double moshnost = ReadDouble("Мощность (кВт): ");
                int mesta = ReadInt("Количество мест: ");
                int god = ReadInt("Год производства: ");

                var car = new AvtoProizvodstvo(marka, moshnost, mesta, god);
                Console.WriteLine($"\nКачество Q: {car.Q():F2} [Q = 0.1 * Мощность * Число мест]");
                Console.WriteLine($"Качество Qp: {car.Qp():F2} [Qp = Q - 1.5 * (Текущий год - Год изготовления)]");
            }
            catch (Exception ex) { ShowError(ex.Message); }
            Wait();
        }

        static void CreateCamera()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== СОЗДАНИЕ ФОТОАППАРАТА ===");
                string model = ReadString("Модель: ");
                double zoom = ReadDouble("Zoom (1-35): ", x => x is >= 1 and <= 35 ? x : throw new Exception("Недопустимый Zoom"));
                string material = ReadString("Материал (metal/plastic): ").ToLower();
        
                if(material != "metal" && material != "plastic")
                    throw new ArgumentException("Материал должен быть 'metal' или 'plastic'");

                int mp = ReadInt("Мегапиксели: ");

                var basicCamera = new Camera(model, zoom, material);
                var digitalCamera = new DigitalCamera(model, zoom, material, mp);

                // Добавлен вывод информации о стоимости и формулы расчета
                Console.WriteLine("\nОбычный фотоаппарат:");
                Console.WriteLine($"[Формула: ({zoom} + 2) * {(material == "plastic" ? 10 : 15)}]");
                Console.WriteLine(basicCamera.GetInfo());
                Console.WriteLine($"Дорогой: {(basicCamera.IsExpensive() ? "Да" : "Нет")}");

                // Добавлен вывод формулы для цифровой камеры
                Console.WriteLine("\nЦифровой фотоаппарат:");
                Console.WriteLine($"[Формула: ({basicCamera.CalculatePrice():F2} * {mp}]");
                Console.WriteLine(digitalCamera.GetInfo());
                Console.WriteLine($"Дорогой: {(digitalCamera.IsExpensive() ? "Да" : "Нет")}");

                digitalCamera.UpgradeModel();
                Console.WriteLine("\nПосле обновления:");
                Console.WriteLine($"[Формула: ({basicCamera.CalculatePrice():F2} * {mp + 2}]");
                Console.WriteLine(digitalCamera.GetInfo());
            }
            catch (Exception ex) { ShowError(ex.Message); }
            Wait();
        }
        
        static void RunNumberEmployeeDemo()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ЧИСЛА И СОТРУДНИКИ ===");
                Console.WriteLine("1 - Работа с числами (Базовый уровень)");
                Console.WriteLine("2 - Работа с сотрудниками (Средний уровень)");
                Console.WriteLine("3 - Назад");
                Console.Write("Выберите: ");

                switch (Console.ReadLine())
                {
                    case "1": RunNumbers(); break;
                    case "2": RunEmployees(); break;
                    case "3": return;
                    default: ShowError("Неверный выбор!"); break;
                }
            }
        }

        static void RunNumbers()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== РАБОТА С ЧИСЛАМИ ===");
                double x = ReadDouble("X: ");
                double y = ReadDouble("Y: ");
                var baseObj = new BaseClass(x, y);
                Console.WriteLine($"Сумма X + Y: {baseObj.Calculate():F2}");

                double z = ReadDouble("Z: ");
                var derivedObj = new DerivedClass(x, y, z);
                Console.WriteLine($"Деление X / Z: {derivedObj.Calculate():F2}");
            }
            catch (Exception ex) { ShowError(ex.Message); }
            Wait();
        }

        static void RunEmployees()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== РАБОТА С СОТРУДНИКАМИ ===");
                string name = ReadString("Фамилия: ");
                decimal salary = ReadDecimal("Оклад: ");
                int joinYear = ReadInt("Год приёма: ");
                int birthYear = ReadInt("Год рождения: ");

                var emp = new EnterpriseEmployee(name, salary, joinYear, birthYear);
                var (years, desc) = emp.CalculateWorkYears();
                Console.WriteLine($"\n{emp.GetInfo()}\n{desc}");
            }
            catch (Exception ex) { ShowError(ex.Message); }
            Wait();
        }

        static void RunStateMachineDemo()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== СИНХРОННЫЙ АВТОМАТ ===");
                int[,] transitions = { {0,1,1}, {1,1,0}, {1,1,1}, {0,1,0}, {1,1,0} };
                var machine = new EnhancedStateMachine(transitions, 0);

                string input = ReadString("Введите последовательность (a/b/c): ").ToLower();
                machine.ProcessInput(input);
                machine.PrintHistory();
                Console.WriteLine($"Финальное состояние: {machine.GetCurrentState()}");
            }
            catch (Exception ex) { ShowError(ex.Message); }
            Wait();
        }

        static T ReadValue<T>(string prompt, Func<string, T> parser)
        {
            Console.Write(prompt);
            return parser(Console.ReadLine());
        }

        static string ReadString(string prompt) => ReadValue(prompt, x => x);
        static double ReadDouble(string prompt) => ReadValue(prompt, double.Parse);
        static int ReadInt(string prompt) => ReadValue(prompt, int.Parse);
        static decimal ReadDecimal(string prompt) => ReadValue(prompt, decimal.Parse);
        static double ReadDouble(string prompt, Func<double, double> validator) => 
            ReadValue(prompt, s => validator(double.Parse(s)));

        static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nОшибка: {message}");
            Console.ResetColor();
        }

        static void Wait()
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}