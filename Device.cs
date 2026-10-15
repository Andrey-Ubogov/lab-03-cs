using System;

namespace SmartHome
{
    /// <summary>
    /// Устройство умного дома: часть комнаты (композиция).
    /// Не может существовать без комнаты.
    /// </summary>
    public class Device
    {
        private string _name;
        private bool _isOn;
        private bool _isBroken;
        private int _powerConsumption;

        // Свойства
        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public bool IsOn
        {
            get => _isOn;
            private set => _isOn = value;
        }

        public bool IsBroken
        {
            get => _isBroken;
            private set => _isBroken = value;
        }

        public int PowerConsumption
        {
            get => _powerConsumption;
            private set => _powerConsumption = value;
        }

        // Конструктор с параметрами
        public Device(string name, int power)
        {
            _name = name;
            _isOn = false;
            _isBroken = false;
            _powerConsumption = power;
            Console.WriteLine($"[Device] Создано устройство: {_name}");
        }

        // Конструктор по умолчанию
        public Device() : this("Безымянное", 0)
        {
            
        }

        // Финализатор для демонстрации.
        ~Device()
        {
            Console.WriteLine($"[Device] Финализатор: устройство {_name} уничтожено сборщиком мусора.");
        }

        // Содержательные методы (бизнес-логика)

        public void TurnOn()
        {
            // ПРОВЕРКА ПРАВИЛА: нельзя включить сломанное устройство
            if (_isBroken)
            {
                Console.WriteLine($"  Ошибка: Устройство '{_name}' сломано! Нельзя включить.");
                return;
            }
            _isOn = true;
            Console.WriteLine($"  Устройство '{_name}' включено.");
        }

        public void TurnOff()
        {
            _isOn = false;
            Console.WriteLine($"  Устройство '{_name}' выключено.");
        }

        public void Break()
        {
            _isBroken = true;
            _isOn = false;
            Console.WriteLine($"  Устройство '{_name}' сломалось!");
        }

        public void Repair()
        {
            _isBroken = false;
            Console.WriteLine($"  Устройство '{_name}' отремонтировано.");
        }

        // Переопределяем ToString() — метод, который вызывается при печати объекта..
        public override string ToString()
        {
            string state = _isBroken ? "сломано" : (_isOn ? "включено" : "выключено");
            return $"Устройство '{_name}' ({_powerConsumption} Вт) — {state}";
        }
    }
}